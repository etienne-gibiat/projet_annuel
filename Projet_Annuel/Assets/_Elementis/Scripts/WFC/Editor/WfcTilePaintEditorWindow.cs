using PGSauce.Core.PGDebugging;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    
    public class WfcTilePaintEditorWindow : EditorWindow
     {
         private Texture2D paintPencil;
         private Texture2D paintEdge;
         private Texture2D paintFace;
         private Texture2D paintVertex;
         private Texture2D cubeAdd;
         private Texture2D cubeRemove;
 
         [MenuItem("Elementis/WFC Tile Painting")]
         public static void Init()
         {
             // Get existing open window or if none, make a new one:
             var window = (WfcTilePaintEditorWindow)GetWindow(typeof(WfcTilePaintEditorWindow));
             window.Show();
         }
 
         private void OnEnable()
         {
             var path = "Assets/_Elementis/Textures/Resources/";
             paintPencil = AssetDatabase.LoadAssetAtPath<Texture2D>($"{path}paint_pencil.png");
             paintEdge = AssetDatabase.LoadAssetAtPath<Texture2D>($"{path}paint_edge.png");
             paintFace = AssetDatabase.LoadAssetAtPath<Texture2D>($"{path}paint_face.png");
             paintVertex = AssetDatabase.LoadAssetAtPath<Texture2D>($"{path}paint_vertex.png");
             cubeAdd = AssetDatabase.LoadAssetAtPath<Texture2D>($"{path}cube_add.png");
             cubeRemove = AssetDatabase.LoadAssetAtPath<Texture2D>($"{path}cube_remove.png");
 
             titleContent = new GUIContent("Tile Painting");
             ToolManager.activeToolChanged += Repaint;
         }
 
         void OnGUI()
         {
             GUILayout.Label("View", EditorStyles.boldLabel);
 
             WfcTilePaintingState.showBackface = EditorGUILayout.Toggle(new GUIContent("Show Backfaces", "Z to toggle"), WfcTilePaintingState.showBackface);
 
             var opacity = WfcTilePaintingState.hideAll ? 0 : WfcTilePaintingState.opacity;
             opacity = EditorGUILayout.Slider(new GUIContent("Opacity", "Shift-A to Hide All"), opacity, 0, 1);
             if (opacity != 0)
             {
                 WfcTilePaintingState.hideAll = false;
             }
             if(!WfcTilePaintingState.hideAll)
             {
                 WfcTilePaintingState.opacity = opacity;
             }
 
 
             WfcTilePaintingState.showAll = EditorGUILayout.Toggle(new GUIContent("Show All", "A to toggle"), WfcTilePaintingState.showAll);
 
             GUILayout.Label("Painting", EditorStyles.boldLabel);
 
             // TODO: See C:\Program Files\Unity\Hub\Editor\2019.2.3f1\Editor\Data\Resources\PackageManager\BuiltInPackages\com.unity.2d.tilemap\Editor\EditorTools
             // For some further stuff to do here
 
             var paintIndex = Event.current.modifiers == EventModifiers.Shift ? 0 : WfcTilePaintingState.paintIndex;
 
             GUILayout.Label("Paint Mode");
 
             GUILayout.BeginHorizontal(new GUILayoutOption[0]);
             GUILayout.FlexibleSpace();
             EditorGUILayout.EditorToolbar(WfcTileEditorToolBase.tile3dEditorTools);
 
             GUILayout.FlexibleSpace();
             GUILayout.EndHorizontal();
 
             GUILayout.Label("Paint Color");
 
             var r = EditorGUILayout.BeginVertical();
 
             r.width = position.width;
 
             var boxStyle = GUI.skin.box;
             var tileSize = 20;
 
             var boxRect = boxStyle.margin.Remove(r);
             var innerRect = boxStyle.padding.Remove(boxRect);
 
             // Get palette 
             WfcPalette palette = null;
             if(Selection.activeGameObject != null)
             {
                 if(Selection.activeGameObject.GetComponentInParent<WfcTileBase>() is WfcTileBase t)
                 {
                     palette = t.palette;
                 }
             }
             if(palette == null)
             {
                 PGDebug.Message("PALETTE IS NULL").LogError();
             }
 
             var tilesPerRow = (int)(innerRect.width / tileSize);
             if (tilesPerRow > 0)
             {
                 
                 innerRect.height = (palette.entryCount + tilesPerRow - 1) / tilesPerRow * tileSize;
                 boxRect = boxStyle.padding.Add(innerRect);
 
                 GUI.Box(boxRect, "");
 
                 for (var i = 0; i < palette.entryCount; i++)
                 {
                     var x = innerRect.x + (i % tilesPerRow) * tileSize;
                     var y = innerRect.y + (i / tilesPerRow) * tileSize;
                     var tileRect = new Rect(x, y, tileSize, tileSize);
 
                     var selected = paintIndex == i;
                     if (Event.current.type == EventType.Repaint)
                     {
                         TextureUtil.DrawBox(tileRect, selected, palette, i);
                     }
                 }
 
                 if (Event.current.type == EventType.MouseDown)
                 {
                     var mousePosition = Event.current.mousePosition;
                     if (innerRect.Contains(mousePosition))
                     {
                         var x = (int)(mousePosition.x - innerRect.x) / tileSize;
                         var y = (int)(mousePosition.y - innerRect.y) / tileSize;
                         var i = x + y * tilesPerRow;
                         if (WfcTilePaintingState.paintIndex != i)
                         {
                             WfcTilePaintingState.lastPaintIndex = WfcTilePaintingState.paintIndex;
                             WfcTilePaintingState.paintIndex = i;
                             Repaint();
                         }
                     }
                 }
 
 
             }
             EditorGUILayout.EndVertical();
 
             GUILayout.Space(boxStyle.margin.Add(boxRect).height);
 
             WfcTileEditorToolBase.CheckKeyPresses();
         }
     }
}