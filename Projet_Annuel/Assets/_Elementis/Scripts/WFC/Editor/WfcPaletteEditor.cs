using UnityEditor;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    [CustomEditor(typeof(WfcPalette))]
    public class WfcPaletteEditor : Editor
    {
        private const string tick = "\u2714";
        private const string cross = "X";

        public override void OnInspectorGUI()
        {

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("entries"), true);
            serializedObject.ApplyModifiedProperties();
            DrawMatching();
        }

        void DrawMatching()
        {
            var property = serializedObject.FindProperty("matchOverridesList");
            property.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(property.isExpanded, new GUIContent("Matches"));

            if (property.isExpanded)
            {

                EditorGUI.indentLevel += 1;

                var r = EditorGUILayout.BeginVertical();

                var boxStyle = GUI.skin.box;
                var tileSize = 20;
                var palette = (WfcPalette)target;
                var count = palette.entries.Count;


                var boxRect = boxStyle.margin.Remove(r);
                var innerRect = boxStyle.padding.Remove(boxRect);

                innerRect.height = (count + 1) * tileSize;
                boxRect = boxStyle.padding.Add(innerRect);

                GUI.Box(boxRect, "");
                GUILayout.Space(boxRect.height);

                // work out mouse position
                var mousePosition = Event.current.mousePosition;
                int? mouseI = null, mouseJ = null;
                if (innerRect.Contains(mousePosition))
                {
                    var i = (int)(mousePosition.x - innerRect.x) / tileSize - 1;
                    var j = (int)(mousePosition.y - innerRect.y) / tileSize - 1;
                    if (i >= 0 && i < count && j >= 0 && j < count)
                    {
                        mouseI = i;
                        mouseJ = j;
                    }
                }

                // Draw top row
                for (var i = 0; i < count; i++)
                {
                    var rect = new Rect(tileSize * (i + 1) + innerRect.x, innerRect.y, tileSize, tileSize);
                    TextureUtil.DrawBox(rect, false, palette, i);
                }
                // Draw left column
                for (var i = 0; i < count; i++)
                {
                    var rect = new Rect(innerRect.x, tileSize * (i + 1) + innerRect.y, tileSize, tileSize);
                    TextureUtil.DrawBox(rect, false, palette, i);
                }
                // Draw grid
                var centerStyle = new GUIStyle(GUIStyle.none);
                centerStyle.alignment = TextAnchor.MiddleCenter;
                var centerStyleBold = new GUIStyle(centerStyle);
                centerStyleBold.fontStyle = FontStyle.Bold;

                var t1 = TextureUtil.MakeTexture(1, 1, new Color(0, 0, 0, 0));
                var t2 = TextureUtil.MakeTexture(1, 1, new Color(0, 0, 0, 0.1f));
                var t3 = TextureUtil.MakeTexture(1, 1, new Color(0, 0, 0, 0.2f));

                for (var i = 0; i < count; i++)
                {
                    for (var j = 0; j < count; j++)
                    {
                        var darkness = (i % 2) + (j % 2);
                        centerStyleBold.normal.background = centerStyle.normal.background = darkness == 0 ? t1 : darkness == 1 ? t2 : t3;
                        var rect = new Rect(tileSize * (i + 1) + innerRect.x, tileSize * (j + 1) + innerRect.y, tileSize, tileSize);
                        var match = palette.Match(i, j);
                        var tooltip = palette.entries[i].name + " - " + palette.entries[j].name;
                        var hasOverride = palette.matchOverrides.ContainsKey((i, j));
                        var style = hasOverride ? centerStyleBold : centerStyle;
                        var mark = match ? tick : hasOverride ? cross : "";
                        GUI.Box(rect, new GUIContent(mark, tooltip), style);
                    }
                }

                if (Event.current.type == EventType.MouseDown)
                {
                    if (mouseI != null)
                    {
                        var i = mouseI.Value;
                        var j = mouseJ.Value;
                        var match = palette.Match(i, j);
                        var hasOverride = palette.matchOverrides.ContainsKey((i, j));
                        if (hasOverride)
                        {
                            // Sadly, calling seralizedObject.Update() doesn't seem to work.
                            //palette.matchOverrides.Remove((i, j));
                            //palette.matchOverrides.Remove((j, i));
                            var mol = serializedObject.FindProperty("matchOverridesList");
                            for (var u = 0; u < mol.arraySize; u++)
                            {
                                var v = mol.GetArrayElementAtIndex(u);
                                var v1 = v.FindPropertyRelative("color1").intValue;
                                var v2 = v.FindPropertyRelative("color2").intValue;
                                if (v1 == i && v2 == j || v1 == j && v2 == i)
                                {
                                    mol.DeleteArrayElementAtIndex(u);
                                    u--;
                                }
                            }
                        }
                        else
                        {
                            // Sadly, calling seralizedObject.Update() doesn't seem to work.
                            //palette.matchOverrides[(i, j)] = !match;
                            //palette.matchOverrides[(j, i)] = !match;
                            var mol = serializedObject.FindProperty("matchOverridesList");
                            var s = mol.arraySize;
                            mol.arraySize += i == j ? 1 : 2;
                            var i1 = mol.GetArrayElementAtIndex(s);
                            i1.FindPropertyRelative("color1").intValue = i;
                            i1.FindPropertyRelative("color2").intValue = j;
                            i1.FindPropertyRelative("isMatch").boolValue = !match;
                            if (i != j)
                            {
                                var i2 = mol.GetArrayElementAtIndex(s + 1);
                                i2.FindPropertyRelative("color1").intValue = j;
                                i2.FindPropertyRelative("color2").intValue = i;
                                i2.FindPropertyRelative("isMatch").boolValue = !match;
                            }
                        }
                        serializedObject.ApplyModifiedProperties();
                        palette.OnAfterDeserialize();
                        Repaint();
                    }
                }

                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel -= 1;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}