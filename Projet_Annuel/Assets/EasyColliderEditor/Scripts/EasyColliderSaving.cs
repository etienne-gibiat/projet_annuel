#if(UNITY_EDITOR)
using UnityEngine;
using UnityEditor;
using System.IO;
namespace ECE
{
  public static class EasyColliderSaving
  {
    /// <summary>
    /// Static preferences asset that is currently loaded.
    /// </summary>
    /// <value></value>
    static EasyColliderPreferences ECEPreferences { get { return EasyColliderPreferences.Preferences; } }

    /// <summary>
    /// Gets a valid path to save a convex hull at to feed into save convex hull meshes function.
    /// </summary>
    /// <param name="go">selected gameobject</param>
    /// <param name="ECEPreferences">preferences object</param>
    /// <returns>full save path (ie C:/UnityProjects/ProjectName/Assets/Folder/baseObject)</returns>
    public static string GetValidConvexHullPath(GameObject go)
    {
      // use default specified path
      string path = ECEPreferences.SaveConvexHullPath + go.name;
      // get path to go
      if (ECEPreferences.SaveConvexHullMeshAtSelected)
      {
        // bandaid for scaled temporary skinned mesh:
        // as the scaled mesh filter is added during setup with the name Scaled Mesh Filter (Temporary)
        if (go.name.Contains("Scaled Mesh Filter"))
        {
          go = go.transform.parent.gameObject; // set the gameobject to the temp's parent (as that will be a part of the prefab if it is one and thus should work.)
        }

        UnityEngine.Object foundObject = null;
#if UNITY_2018_2_OR_NEWER
        foundObject = PrefabUtility.GetCorrespondingObjectFromSource(go);
#else
        foundObject = PrefabUtility.GetPrefabParent(go);
        if (foundObject == null)
        {
          foundObject = PrefabUtility.FindPrefabRoot(go);
        }
#endif
        string altPath = AssetDatabase.GetAssetPath(foundObject);
        // but only use the path it if it exists.
        if (altPath != null && altPath != "" && foundObject != null)
        {
          int index = altPath.LastIndexOf("/");
          if (index >= 0)
          {
            path = altPath.Remove(index + 1) + foundObject.name;
          }
        }
      }

      bool badPath = false;
      // paths should be in the form of Assets/subfolders/gameobjectname
      string assetDatabaseFolder = Application.dataPath;
      int adfIndex = path.LastIndexOf("/");
      if (adfIndex >= 0)
      {
        assetDatabaseFolder = path.Remove(adfIndex);
      }
      else { badPath = true; }

      // C:/pathtoprojectfiles/Assets/subfolders/gameobjectname
      // datapath has the /Assets on it, so we removed the "Assets" part as that's already part of the path.
      string fullPath = Application.dataPath.Remove(Application.dataPath.Length - 6) + path;

      // the directory of the full path:" C:/projectfiles/subfolders
      string directory = Application.dataPath;
      int directoryIndex = fullPath.LastIndexOf("/");
      if (directoryIndex >= 0)
      {
        directory = fullPath.Remove(directoryIndex);
      }
      else { badPath = true; }

      //for debugging malformed paths, somehow bad paths can sometimes be created.
      // bool forceBadPath = true;
      // if the directory specified or found does not exist, fall back to using the location of this script.
      if (badPath || !Directory.Exists(directory) || !AssetDatabase.IsValidFolder(assetDatabaseFolder))
      {
        fullPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(ECEPreferences));
        int fullPathIndex = fullPath.LastIndexOf("/");
        // fullPathIndex = forceBadPath ? -1 : 0;
        if (fullPathIndex >= 0)
        {
          var previousDefaultPath = ECEPreferences.SaveConvexHullPath;
          // trying to save in default path, but it's possible the user moved the folder, but didn't change the default save path.
          // so we want to check if it's the default, and update it if needed.
          // instead of checking the default, we check if it contains EasyColliderEditor in the path name, as then we can handle if the folder moves multiple times.
          // if the user changes the default path, they likely want something specific,
          // so we want to conitnue logging the warning instead of reseting and displaying once and never again.
          if (ECEPreferences.SaveConvexHullPath.Contains("EasyColliderEditor"))
          {
            // reset the default path and see if it's changed, if it has, re-call this same method, if it hasn't we just conitnue on as before.
            ECEPreferences.ResetDefaultSavePath();
            if (ECEPreferences.SaveConvexHullPath != previousDefaultPath)
            {
              return GetValidConvexHullPath(go);
            }
          }
          // this would get caleld if the user changed the default path, but then moved the folders around.
          fullPath = fullPath.Remove(fullPathIndex);
          // add an additional "Is it possible this folder has been moved" mention into the warning.
          Debug.LogWarning("Easy Collider Editor: Convex Hull save path specified in Easy Collider Editor does not exist. Saving in: " + fullPath + " as a fallback. If the folder has been moved or deleted, update to a different folder in the edit preferences foldout." + "\n\nDebug information: fp:" + fullPath + "\ndir:" + directory + "\nadf:" + assetDatabaseFolder + "\np:" + path);
          fullPath = fullPath + "/" + go.name;
        }
        else
        {
          // in the event that we still somehow have a malformed path, we'll save at the default asset root and let the user know.
          Debug.LogWarning("Easy Collider Editor: Unable to save at the normal fallback path. Saving at the root Assets/ project path. Try updating the folder in the edit preferences foldout.\n\nDebug information: fp:" + fullPath + "\ndir:" + directory + "\nadf:" + assetDatabaseFolder + "\np:" + path);
          fullPath = Application.dataPath + "/" + go.name;
        }
      }
      return fullPath;
    }

    /// <summary>
    /// goes thorugh the path and finds the first non-existing path that can be used to save.
    /// </summary>
    /// <param name="path">Full path up to save location: ie C:/UnityProjects/ProjectName/Assets/Folder/baseObject</param>
    /// <param name="suffix">Suffix to add to save files ie _Suffix_</param>
    /// <returns>first valid path for AssetDatabase.CreateAsset ie baseObject_Suffix_0</returns>
    private static string GetFirstValidAssetPath(string path, string suffix)
    {

      string validPath = path;
      if (File.Exists(validPath + suffix + "0.asset"))
      {
        int i = 1;
        while (File.Exists(validPath + suffix + i + ".asset"))
        {
          i += 1;
        }
        validPath += suffix + i + ".asset";
      }
      else
      {
        validPath += suffix + "0.asset";
      }
      // have "/Assets/" in the directory earlier than the unity project could still cause issues.
      // Debug.Log(path);
      int index = path.LastIndexOf("/Assets/");
      if (index >= 0)
      {
        validPath = validPath.Remove(0, index);
        validPath = validPath.Remove(0, 1);
      }
      // Debug.Log(validPath);
      return validPath;
    }

    /// <summary>
    /// Creates and saves a mesh asset in the asset database with appropriate path and suffix.
    /// </summary>
    /// <param name="mesh">mesh</param>
    /// <param name="attachTo">gameobject the mesh will be attached to, used to find asset path.</param>
    public static void CreateAndSaveMeshAsset(Mesh mesh, GameObject attachTo)
    {
      if (mesh != null && !DoesMeshAssetExists(mesh))
      {
        string savePath = GetValidConvexHullPath(attachTo);
        if (savePath != "")
        {
          string assetPath = GetFirstValidAssetPath(savePath, ECEPreferences.SaveConvexHullSuffix);
          AssetDatabase.CreateAsset(mesh, assetPath);
          AssetDatabase.SaveAssets();
        }
      }
    }

    /// <summary>
    /// Checks if the asset already exists (needed for rotate and duplicate, as the mesh is the same mesh repeated.)
    /// </summary>
    /// <param name="mesh"></param>
    /// <returns></returns>
    public static bool DoesMeshAssetExists(Mesh mesh)
    {
      string p = AssetDatabase.GetAssetPath(mesh);
      if (p == null || p.Length == 0)
      {
        return false;
      }
      return true;
    }

    /// <summary>
    /// Creates and saves an array of mesh assets in the assetdatabase at the path with the the format "savePath"+"suffix"+[0-n].asset
    /// </summary>
    /// <param name="savePath">Full path up to save location: ie C:/UnityProjects/ProjectName/Assets/Folder/baseObject</param>
    /// <param name="suffix">Suffix to add to save files ie _Suffix_</param>
    public static void CreateAndSaveMeshAssets(Mesh[] meshes, string savePath, string suffix)
    {
      for (int i = 0; i < meshes.Length; i++)
      {
        // get a new valid path for each mesh to save.
        string path = GetFirstValidAssetPath(savePath, suffix);
        try
        {
          AssetDatabase.CreateAsset(meshes[i], path);
        }
        catch (System.Exception error)
        {
          Debug.LogError("Error saving at path:" + path + ". Try changing the save CH path in Easy Collider Editor's preferences to a different folder.\n" + error.ToString());
        }
      }
      AssetDatabase.SaveAssets();
    }

  }
}
#endif
