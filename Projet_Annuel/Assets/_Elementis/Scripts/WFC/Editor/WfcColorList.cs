using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    internal static class TextureUtil
    {
        private static Texture eraser;
 
        public static Texture Eraser => eraser = (eraser ?? EditorGUIUtility.IconContent("Grid.EraserTool").image);
 
        public static Color GetContrastColor(Color c)
        {
            Color.RGBToHSV(c, out var h, out var s, out var v);
            return v > 0.5 ? Color.black : Color.white;
        }
 
        public static void DrawBox(Rect tileRect, bool selected, WfcPalette palette, int index)
        {
            var entry = palette.GetEntry(index);
            var c = palette.GetColor(index);
            var name = entry?.name ?? "";
            var texture = index == 0 ? Eraser : MakeTexture((int)tileRect.width, (int)tileRect.height, c);
            if (selected)
            {
                var contrast = GetContrastColor(c);
                var contrastTexture = MakeTexture((int)tileRect.width, (int)tileRect.height, contrast);
                GUI.Box(tileRect, new GUIContent(contrastTexture, name), GUIStyle.none);
                GUI.DrawTexture(new RectOffset(2, 2, 2, 2).Remove(tileRect), texture);
            }
            else
            {
                //GUI.DrawTexture(tileRect, texture);
                GUI.Box(tileRect, new GUIContent(texture, name), GUIStyle.none);
            }
        }
 
        public static Texture2D MakeTexture(int width, int height, Color color)
        {
            var texture = new Texture2D(width, height);
            Color[] pixels = Enumerable.Repeat(color, width * height).ToArray();
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
    }
    
    public class WfcColorList
    {
        private readonly SerializedProperty list;
        private readonly WfcPalette palette;

        public WfcColorList(SerializedProperty list, WfcPalette palette)
        {
            this.list = list;
            this.palette = palette;
        }


        public void Draw()
        {
            var set = new HashSet<int>();
            for (int i = 0; i < list.arraySize; i++)
            {
                set.Add((int)list.GetArrayElementAtIndex(i).intValue);
            }

            list.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(list.isExpanded, list.displayName);

            if (list.isExpanded)
            {
                int oldIndentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel++;

                for (var index = 0; index < palette.entryCount; index++)
                {
                    var entry = palette.entries[index];
                    var current = set.Contains(index);
                    var texture = TextureUtil.MakeTexture(1, 1, entry.color);

                    GUILayout.BeginHorizontal();
                    var labelStyle = new GUIStyle(EditorStyles.label);
                    labelStyle.normal.background = texture;
                    labelStyle.normal.textColor = TextureUtil.GetContrastColor(entry.color);
                    labelStyle.hover.background = texture;
                    labelStyle.hover.textColor = TextureUtil.GetContrastColor(entry.color);
                    labelStyle.focused.background = texture;
                    labelStyle.focused.textColor = TextureUtil.GetContrastColor(entry.color);

                    EditorGUILayout.PrefixLabel(new GUIContent(entry.name, entry.name), labelStyle, labelStyle);

                    var newValue = EditorGUILayout.Toggle(current);

                    GUILayout.EndHorizontal();
                    if (newValue && !current)
                    {
                        list.arraySize += 1;
                        list.GetArrayElementAtIndex(list.arraySize - 1).intValue = index;
                    }
                    if (!newValue && current)
                    {
                        for (int i = 0; i < list.arraySize; i++)
                        {
                            if (list.GetArrayElementAtIndex(i).intValue == index)
                            {
                                list.DeleteArrayElementAtIndex(i);
                                break;
                            }
                        }
                    }
                }

                EditorGUI.indentLevel = oldIndentLevel;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}