using UnityEditor;

namespace LeTai.TrueShadow.Editor
{
public static class Utility
{
    public static T FindAsset<T>(string assetName) where T : UnityEngine.Object
    {
        var guids = AssetDatabase.FindAssets("l:TrueShadowEditorResources " + assetName);
        var path  = AssetDatabase.GUIDToAssetPath(guids[0]);
        return AssetDatabase.LoadAssetAtPath<T>(path);
    }
}
}
