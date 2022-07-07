using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditorInternal;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    [CustomEditor(typeof(WfcGenerator))]
    public class WfcGeneratorEditor : Editor
    {
        private const string ChangeBounds = "Change Bounds";

        private class CustomHandle : BoxBoundsHandle
        {
            public WfcGenerator generator;

            protected override Bounds OnHandleChanged(
                PrimitiveBoundsHandle.HandleDirection handle,
                Bounds boundsOnClick,
                Bounds newBounds)
            {
                // Enforce minimum size for bounds
                // And ensure it is property quantized
                switch (handle)
                {
                    case HandleDirection.NegativeX:
                    case HandleDirection.NegativeY:
                    case HandleDirection.NegativeZ:
                        newBounds.min = Vector3.Min(newBounds.min, newBounds.max - generator.tileSize);
                        newBounds.min = Round(newBounds.min - newBounds.max) + newBounds.max;
                        break;
                    case HandleDirection.PositiveX:
                    case HandleDirection.PositiveY:
                    case HandleDirection.PositiveZ:
                        newBounds.max = Vector3.Max(newBounds.max, newBounds.min + generator.tileSize);
                        newBounds.max = Round(newBounds.max - newBounds.min) + newBounds.min;
                        break;
                }

                Undo.RecordObject(generator, ChangeBounds);

                generator.Bounds = newBounds;

                return newBounds;
            }

            Vector3 Round(Vector3 m)
            {
                m.x = generator.tileSize.x * ((int) Math.Round(m.x / generator.tileSize.x));
                m.y = generator.tileSize.y * ((int) Math.Round(m.y / generator.tileSize.y));
                m.z = generator.tileSize.z * ((int) Math.Round(m.z / generator.tileSize.z));
                return m;
            }
        }

        private const string GenerateTiles = "Generate tiles";

        private bool gridShapeToggle = true;
        private bool generationOptionsToggle = true;

        private ReorderableList rl;

        SerializedProperty list;
        private GUIStyle headerBackground;
        int controlId;

        int selectorIndex = -1;

        const int k_fieldPadding = 2;
        const int k_elementPadding = 5;

        CustomHandle h = new CustomHandle();


        private void OnEnable()
        {
            list = serializedObject.FindProperty("tiles");

            rl = new ReorderableList(serializedObject, list, true, false, true, true);


            rl.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty targetElement = list.GetArrayElementAtIndex(index);
                if (targetElement.hasVisibleChildren)
                    rect.xMin += 10;
                var tileProperty = targetElement.FindPropertyRelative("tile");
                var weightProperty = targetElement.FindPropertyRelative("weight");
                var tile = (WfcTileBase) tileProperty.objectReferenceValue;
                var tileName = tile?.gameObject.name ?? "None";

                var tileRect = rect;
                tileRect.height = EditorGUI.GetPropertyHeight(tileProperty);
                var weightRect = rect;
                weightRect.yMin = tileRect.yMax + k_fieldPadding;
                weightRect.height = EditorGUI.GetPropertyHeight(weightProperty);
                EditorGUI.PropertyField(tileRect, tileProperty);
                EditorGUI.PropertyField(weightRect, weightProperty);
            };

            rl.elementHeightCallback = (int index) =>
            {
                SerializedProperty targetElement = list.GetArrayElementAtIndex(index);
                var tileProperty = targetElement.FindPropertyRelative("tile");
                var weightProperty = targetElement.FindPropertyRelative("weight");
                return EditorGUI.GetPropertyHeight(tileProperty) + k_fieldPadding +
                       EditorGUI.GetPropertyHeight(weightProperty) + k_elementPadding;
            };

            rl.drawElementBackgroundCallback = (rect, index, active, focused) =>
            {
                var styleHighlight = GUI.skin.FindStyle("MeTransitionSelectHead");
                if (focused == false)
                    return;
                rect.height = rl.elementHeightCallback(index);
                GUI.Box(rect, GUIContent.none, styleHighlight);
            };

            rl.onAddCallback = l =>
            {
                ++rl.serializedProperty.arraySize;
                rl.index = rl.serializedProperty.arraySize - 1;
                list.GetArrayElementAtIndex(rl.index).FindPropertyRelative("weight").floatValue = 1.0f;
                selectorIndex = rl.index;
                controlId = EditorGUIUtility.GetControlID(FocusType.Passive);
                EditorGUIUtility.ShowObjectPicker<WfcTileBase>(this, true, null, controlId);
            };

            var generator = target as WfcGenerator;

            h.center = generator.Center;
            h.size = Vector3.Scale(generator.tileSize, generator.Size);
            h.generator = generator;
        }

        public override void OnInspectorGUI()
        {
            var generator = target as WfcGenerator;

            this.headerBackground = this.headerBackground ?? (GUIStyle) "RL Header";
            serializedObject.Update();

            GridShapeGUI();
            GenerationOptionsGUI();
            TileListGUI();
            serializedObject.ApplyModifiedProperties();

            Validate();

            EditorUtility.ClearProgressBar();

            var clearable = !generator.GetTileOutput().IsEmpty;

            GUI.enabled = clearable;

            if (GUILayout.Button("Clear"))
            {
                Clear();
            }

            GUI.enabled = true;

            if (GUILayout.Button(clearable ? "Regenerate" : "Generate"))
            {
                // Undo the last generation
                Undo.SetCurrentGroupName(GenerateTiles);
                if (clearable)
                {
                    Clear();
                }

                Generate(generator);
            }
        }

        private void SetReadable(GameObject go, Mesh mesh)
        {
            if (mesh == null) return;
            if (mesh.isReadable) return;
            var path = AssetDatabase.GetAssetPath(mesh);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning($"Unable to find asset for a mesh on {go}");
                return;
            }

            var importer = (ModelImporter) AssetImporter.GetAtPath(path);
            if (importer == null)
            {
                Debug.LogWarning($"Unable to find model importer for asset {path}");
                return;
            }

            Debug.Log($"Updating import settings for asset {path}");
            importer.isReadable = true;
            importer.SaveAndReimport();
        }

        private void SetTangents(GameObject go, Mesh mesh)
        {
            if (mesh == null) return;
            var path = AssetDatabase.GetAssetPath(mesh);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning($"Unable to find asset for a mesh on {go}");
                return;
            }

            var importer = (ModelImporter) AssetImporter.GetAtPath(path);
            if (importer == null)
            {
                Debug.LogWarning($"Unable to find model importer for asset {path}");
                return;
            }

            Debug.Log($"Updating import settings for asset {path}");
            importer.importTangents = ModelImporterTangents.CalculateMikk;
            importer.SaveAndReimport();
        }

        private void Validate()
        {
            var generator = target as WfcGenerator;

            var allTiles = generator.tiles.Select(x => x.tile).Where(x => x != null);


            var cellTypes = generator.GetCellTypes();
            if (cellTypes.Count > 1)
            {
                EditorGUILayout.HelpBox(
                    $"You cannot mix tiles of multiple cell types, such as {string.Join(", ", cellTypes.Select(x => x.GetType().Name))}.\n",
                    MessageType.Warning);
            }

            var missizedTiles = generator.GetMisSizedTiles().ToList();
            if (missizedTiles.Count > 0)
            {
                if (HelpBoxWithButton(
                    $"Some tiles do not have the same tileSize as the generator, {generator.tileSize}, this can cause unexpected behaviour.\n" +
                    "NB: Big tiles should still share the same value of tileSize\n" +
                    "Affected tiles:\n" +
                    string.Join("\n", missizedTiles), "Resize tiles", MessageType.Warning))
                {
                    foreach (var tile in missizedTiles)
                    {
                        tile.tileSize = generator.tileSize;
                    }
                }
            }

            var palette = generator.tiles.Select(x => x.tile?.palette).FirstOrDefault();
            var wrongPaletteTiles = allTiles.Where(x => x.palette != palette).ToList();
            if (wrongPaletteTiles.Count > 0)
            {
                if (HelpBoxWithButton(
                    $"Some tiles do not all have the same palette. E.g. {wrongPaletteTiles.First().name}", "Fix it",
                    MessageType.Warning))
                {
                    foreach (var tile in wrongPaletteTiles)
                    {
                        tile.palette = palette;
                    }
                }
            }
        }

        private void Clear()
        {
            var generator = target as WfcGenerator;
            var tileOutput = generator.GetTileOutput();
            tileOutput.ClearTiles(new UnityEditorEngineInterface(ShouldRecordUndo(generator), GenerateTiles));
        }

        private bool ShouldRecordUndo(WfcGenerator generator)
        {
            return true;
        }

        // Wraps generation with a progress bar and cancellation button.
        private void Generate(WfcGenerator generator)
        {
            var cts = new CancellationTokenSource();
            string progressText = "";
            float progress = 0.0f;

            var tileOutput = generator.GetTileOutput();

            // Mirrors private method TesseraGenerator.HandleComplete
            void OnComplete(WfcCompletion completion)
            {
                completion.LogErrror();
                if (!completion.success && generator.failureMode == FailureMode.Cancel)
                {
                    return;
                }

                if (tileOutput != null)
                {
                    tileOutput.UpdateTiles(completion,
                        new UnityEditorEngineInterface(ShouldRecordUndo(generator), GenerateTiles));
                }
            }

            var enumerator = generator.StartGenerate(new WfcOptions()
            {
                onComplete = OnComplete,
                progress = (t, p) =>
                {
                    progressText = t;
                    progress = p;
                },
                cancellationToken = cts.Token
            });

            var last = DateTime.Now;
            // Update progress this frequently.
            // Too fast and it'll slow down generation.
            var freq = TimeSpan.FromSeconds(0.1);
            try
            {
                while (enumerator.MoveNext())
                {
                    var a = enumerator.Current;
                    if (last + freq < DateTime.Now)
                    {
                        last = DateTime.Now;
                        if (EditorUtility.DisplayCancelableProgressBar("Generating", progressText, progress))
                        {
                            cts.Cancel();
                            EditorUtility.ClearProgressBar();
                        }
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // Ignore
            }
            catch (OperationCanceledException)
            {
                // Ignore
            }

            EditorUtility.ClearProgressBar();
            GUIUtility.ExitGUI();
        }

        [DrawGizmo(GizmoType.Selected)]
        static void DrawGizmo(WfcGenerator generator, GizmoType gizmoType)
        {
        }

        protected virtual void OnSceneGUI()
        {
            var generator = target as WfcGenerator;

            if (Event.current.type == EventType.MouseDown)
            {
                mouseDown = true;
            }

            if (Event.current.type == EventType.MouseUp)
            {
                mouseDown = false;
            }

            EditorGUI.BeginChangeCheck();
            Handles.matrix = generator.gameObject.transform.localToWorldMatrix;
            h.DrawHandle();
            Handles.matrix = Matrix4x4.identity;
            if (EditorGUI.EndChangeCheck())
            {
            }

            if (!mouseDown)
            {
                h.center = generator.Center;
                h.size = Vector3.Scale(generator.tileSize, generator.Size);
            }
        }

        private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);
        private bool mouseDown;

        private void Vector2IntField(string label, SerializedProperty property)
        {
            var v3 = property.vector3IntValue;
            var v2 = new Vector2Int(v3.x, v3.y);
            v2 = EditorGUILayout.Vector2IntField(label, v2);
            property.vector3IntValue = new Vector3Int(v2.x, v2.y, 0);
        }

        private void Vector2Field(string label, SerializedProperty property)
        {
            var v3 = property.vector3Value;
            var v2 = new Vector2(v3.x, v3.y);
            v2 = EditorGUILayout.Vector2Field(label, v2);
            property.vector3Value = new Vector3(v2.x, v2.y, 0);
        }

        private void GridShapeGUI()
        {
            gridShapeToggle = EditorGUILayout.BeginFoldoutHeaderGroup(gridShapeToggle, "Grid Shape");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("center"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("size"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tileSize"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("skybox"));

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void GenerationOptionsGUI()
        {
            var generator = target as WfcGenerator;

            generationOptionsToggle = EditorGUILayout.BeginFoldoutHeaderGroup(generationOptionsToggle, "Generation");

            EditorGUILayout.PropertyField(serializedObject.FindProperty("retries"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("backtrack"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stepLimit"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("algorithm"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("failureMode"));
            if (generator.failureMode != FailureMode.Cancel)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("uncertaintyTile"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("contradictionTile"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("scaleUncertainyTile"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void TileListGUI()
        {
            if (Event.current.commandName == "ObjectSelectorUpdated" &&
                EditorGUIUtility.GetObjectPickerControlID() == controlId)
            {
                if (selectorIndex >= 0)
                {
                    var tileObject = (GameObject) EditorGUIUtility.GetObjectPickerObject();
                    var tile = tileObject.GetComponent<WfcTile>();
                    list.GetArrayElementAtIndex(selectorIndex).FindPropertyRelative("tile").objectReferenceValue = tile;
                }
            }

            if (Event.current.commandName == "ObjectSelectorClosed" &&
                EditorGUIUtility.GetObjectPickerControlID() == controlId)
            {
                selectorIndex = -1;
            }

            list.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(list.isExpanded, new GUIContent("Tiles"));

            if (list.isExpanded)
            {
                var r1 = GUILayoutUtility.GetLastRect();

                rl.DoLayoutList();

                var r2 = GUILayoutUtility.GetLastRect();

                var r = new Rect(r1.xMin, r1.yMax, r1.width, r2.yMax - r1.yMax);

                if (r.Contains(Event.current.mousePosition))
                {
                    if (Event.current.type == EventType.DragUpdated)
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                        Event.current.Use();
                    }
                    else if (Event.current.type == EventType.DragPerform)
                    {
                        for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
                        {
                            var t = (DragAndDrop.objectReferences[i] as WfcTileBase) ??
                                    (DragAndDrop.objectReferences[i] as GameObject)?.GetComponent<WfcTileBase>();
                            if (t != null)
                            {
                                ++rl.serializedProperty.arraySize;
                                rl.index = rl.serializedProperty.arraySize - 1;
                                list.GetArrayElementAtIndex(rl.index).FindPropertyRelative("weight").floatValue = 1.0f;
                                list.GetArrayElementAtIndex(rl.index).FindPropertyRelative("tile")
                                    .objectReferenceValue = t;
                            }
                        }

                        Event.current.Use();
                    }
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();


            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Delete && rl.index >= 0)
            {
                list.DeleteArrayElementAtIndex(rl.index);
                if (rl.index >= list.arraySize - 1)
                {
                    rl.index = list.arraySize - 1;
                }
            }
        }

        private static Texture2D s_WarningIcon;

        internal static bool HelpBoxWithButton(string message, string buttonMessage, MessageType type)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label(new GUIContent(message, warningIcon));
            var r = GUILayout.Button(buttonMessage);
            EditorGUILayout.EndVertical();
            return r;
        }

        internal static bool HelpBoxWithButton(string message, MessageType type)
        {
            return HelpBoxWithButton(message, "Fix it!", type);
        }

        private static Texture2D warningIcon => s_WarningIcon
            ? s_WarningIcon
            : (s_WarningIcon = (Texture2D) EditorGUIUtility.IconContent("console.warnicon").image);
    }
}