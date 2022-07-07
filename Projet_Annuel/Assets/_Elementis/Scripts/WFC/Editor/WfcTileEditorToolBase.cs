using System;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.PGDebugging;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    [CanEditMultipleObjects]
    public abstract class WfcTileEditorToolBase : EditorTool
    {
       private static string Paint = "Paint";
        private static string AddCubes = "Add Cubes";
        private static string RemoveCubes = "Remove Cubes";

        protected abstract PaintMode PaintMode { get; }

        private static EditorTool[] s_Tile3dEditorTools = null;
        private static float s_Tile3dEditorToolsToolbarSize = 0.0f;

        /// <summary>
        /// All currently active Editor Tools which work with the Tile Palette
        /// </summary>
        public static EditorTool[] tile3dEditorTools
        {
            get
            {
                if (IsCachedEditorToolsInvalid())
                    InstantiateEditorTools();
                return s_Tile3dEditorTools;
            }
        }

        /// <summary>
        /// The horizontal size of a Toolbar with all the TilemapEditorTools
        /// </summary>
        public static float tilemapEditorToolsToolbarSize
        {
            get
            {
                if (IsCachedEditorToolsInvalid())
                    InstantiateEditorTools();
                return s_Tile3dEditorToolsToolbarSize;
            }
        }

        public static EditorTool[] S_Tile3dEditorTools { get => s_Tile3dEditorTools; set => s_Tile3dEditorTools = value; }

        internal static bool IsActive(Type toolType)
        {
#if UNITY_2020_2_OR_NEWER
            return ToolManager.activeToolType != null && ToolManager.activeToolType == toolType;
#else
            return EditorTools.activeToolType != null && EditorTools.activeToolType == toolType;
#endif
        }

        private static bool IsCachedEditorToolsInvalid()
        {

            return s_Tile3dEditorTools == null || s_Tile3dEditorTools.Length == 0
                // Editor tools keep being set to null?
                || s_Tile3dEditorTools.Any(x => x == null);
        }

        private static void InstantiateEditorTools()
        {
            s_Tile3dEditorTools = new EditorTool[]
            {
                CreateInstance<PencilPaintTool>(),
                CreateInstance<EdgePaintTool>(),
                CreateInstance<FacePaintTool>(),
                CreateInstance<VertexPaintTool>(),
                CreateInstance<AddCubeTool>(),
                CreateInstance<RemoveCubeTool>(),
            };
            GUIStyle toolbarStyle = "Command";
            s_Tile3dEditorToolsToolbarSize = s_Tile3dEditorTools.Sum(x => toolbarStyle.CalcSize(x.toolbarIcon).x);
        }


        // https://forum.unity.com/threads/tools-api.587716/


        private static WfcTileEditorToolBase _instance;
        private static bool _isToolActivated;
        [InitializeOnLoadMethod]
        static void CheckForToolChange()
        {
            void OnActiveToolChanged()
            {
                var toolIsActive = ToolManager.activeToolType.IsSubclassOf(typeof(WfcTileEditorToolBase));

                if (toolIsActive && _isToolActivated == false)
                {
                    _isToolActivated = true;
                    _instance.Activate();
                }
                else if (toolIsActive == false && _isToolActivated)
                {
                    _isToolActivated = false;
                    _instance.Deactivate();
                }
            }
            ToolManager.activeToolChanged += OnActiveToolChanged;
        }

        void OnEnable()
        {
            _instance = this;
        }

        private void Activate()
        {
            WfcTilePaintEditorWindow.Init();
        }

        private void Deactivate()
        {

        }


        private IEnumerable<WfcTileBase> Tile3dTargets()
        {
            var duplicates = new HashSet<WfcTileBase>();
            var targetList = targets;
            if(WfcTilePaintingState.showAll)
            {
                targetList = FindObjectsOfType(typeof(WfcTileBase));
            }
            foreach (var t in targetList)
            {
                if (t is WfcTileBase tile3d)
                {
                    yield return tile3d;
                }
                else if (t is GameObject g)
                {
                    tile3d = g.GetComponentInParent<WfcTileBase>();
                    if (tile3d != null)
                    {
                        if (duplicates.Add(tile3d))
                        {
                            yield return tile3d;
                        }
                    }
                }
            }
        }

        private ICellDrawing GetCellDrawingType(WfcTileBase tile)
        {
            if (tile is WfcTile)
            {
                return CubeCellDrawing.Instance;
            }
            throw new Exception();
        }

        private class Hit
        {
            public WfcTileBase target;
            public Vector3Int offset;
            public CellFaceDirection dir;
            public Vector3 point;
            public SubFace p;
        }

        // Ray casts to all targets and finds what the cursor is pointing at, if anything
        private Hit FindHit(PaintMode? forcePaintMode = null)
        {
            Hit best = null;
            float? bestDistance = null;
            foreach (var t in Tile3dTargets())
            {
                var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                var localRay = new Ray(t.transform.InverseTransformPoint(ray.origin), t.transform.InverseTransformVector(ray.direction));

                var showBackface = WfcTilePaintingState.showBackface;
                if (showBackface)
                {
                    localRay = new Ray(localRay.origin + 1000 * localRay.direction, -localRay.direction);
                }

                var cellDrawingType = GetCellDrawingType(t);

                foreach (var offset in t.offsets)
                {
                    var raycastCellHit = cellDrawingType.Raycast(localRay, offset, t.center, t.tileSize, showBackface ?  bestDistance : null, showBackface ? null : bestDistance);

                    if (raycastCellHit == null)
                        continue;

                    // Skip interior walls
                    if (showBackface)
                    {
                        var o2 = cellDrawingType.Move(offset, raycastCellHit.dir);
                        if (t.offsets.Contains(o2))
                        {
                            continue;
                        }
                    }

                    best = new Hit
                    {
                        target = t,
                        offset = offset,
                        dir = raycastCellHit.dir,
                        point = raycastCellHit.point,
                    };
                    bestDistance = (best.point - localRay.origin).magnitude;

                    var p2 = raycastCellHit.subface;
                    best.p = cellDrawingType.RoundSubFace(offset, raycastCellHit.dir, p2, forcePaintMode ?? PaintMode);
                }
            }
            return best;
        }

        // Returns if the hit would affect the selected location with the given paiting mode
        bool IsHighlight(Hit hit, WfcTileBase t, Vector3Int offset, CellFaceDirection dir, SubFace p)
        {
            if (hit?.target != t)
                return false;

            return GetCellDrawingType(t).IsAffected(
                hit.offset,
                hit.dir,
                hit.p,
                offset,
                dir,
                p,
                PaintMode
                );
        }

        // Draws all the targets, plus highlighting
        private void Repaint(Hit hit, int controlId)
        {
            var facesToDraw = new List<(Hit, WfcFaceDetails, int)>();

            foreach (var t in Tile3dTargets())
            {
                var cellDrawingType = GetCellDrawingType(t);

                foreach (var (offset, dir, faceDetails) in t.faceDetails)
                {

                    GetCellDrawingType(t).GetFaceCenterAndNormal(offset, dir, t.center, t.tileSize, out var faceCenter, out var faceNormal);
                    var currentCamera = Camera.current;
                    var point = currentCamera.transform.position - t.transform.TransformPoint(faceCenter);
                    
                    if (!cellDrawingType.Is2D)
                    {
                        bool backface;
                        if (currentCamera.orthographic)
                        {
                            backface = Vector3.Dot(t.transform.TransformVector(faceNormal), currentCamera.transform.forward) > 0;
                        }
                        else
                        {
                            backface = Vector3.Dot(t.transform.TransformVector(faceNormal), point) < 0;
                        }

                        //Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

                        if (backface ^ (WfcTilePaintingState.showBackface))
                            continue;
                    }

                    foreach (var p in cellDrawingType.GetSubFaces(offset, dir))
                    {
                        var index = cellDrawingType.GetSubFaceValue(faceDetails, p);
                        var h = new Hit
                        {
                            target = t,
                            offset = offset,
                            dir = dir,
                            p = p,
                            point = point,
                        };
                        facesToDraw.Add((h, faceDetails, index));
                    }
                }
            }

            // Painters algorithm for drawing
            facesToDraw = facesToDraw.OrderBy(x => -x.Item1.point.sqrMagnitude).ToList();

            // Work out when to draw wireframes
            var doWireframes = new bool[facesToDraw.Count];
            var drawnWireframes = new HashSet<(WfcTileBase, Vector3Int, CellFaceDirection)>();
            for (var i = facesToDraw.Count - 1; i >= 0; i--)
            {
                var h = facesToDraw[i].Item1;
                doWireframes[i] = drawnWireframes.Add((h.target, h.offset, h.dir));
            }

            var selectedPaintIndex = Event.current.modifiers == EventModifiers.Shift ? 0 : WfcTilePaintingState.paintIndex;

            foreach (var ((h, faceDetails, index), doWireframe) in facesToDraw.Zip(doWireframes, (a, b) => Tuple.Create(a, b)))
            {
                var t = h.target;
                var offset = h.offset;
                var dir = h.dir;
                var p = h.p;

                var cellDrawing = GetCellDrawingType(t);

                var isHighlight = HandleUtility.nearestControl == controlId && IsHighlight(hit, t, offset, dir, p);
                var paintIndex = isHighlight ? selectedPaintIndex : index;
                var palette = t.palette;
                if (t.palette == null)
                {
                    PGDebug.Message($"NO PALETTE").LogError();
                }
                var color = palette.GetColor(paintIndex);
                var opacity = WfcTilePaintingState.hideAll ? 0 : WfcTilePaintingState.opacity;
                if (opacity > 0)
                {
                    if (paintIndex == 0)
                    {
                        color.a = opacity * 0.5f;
                    }
                    else if (isHighlight)
                    {
                        color.a = opacity * 1.2f;
                    }
                    else
                    {
                        color.a = opacity;
                    }
                    Handles.color = color;

                    var vertices = cellDrawing.GetSubFaceVertices(offset, dir, p, t.center, t.tileSize)
                        .Select(t.transform.TransformPoint)
                        .ToArray();

                    Handles.DrawAAConvexPolygon(vertices);
                }
                
                // Draws a whie border for faces. Looks good, but a bit slower
                //Handles.color = Color.white;
                //Handles.DrawAAPolyLine(vertices);
                //Handles.DrawAAPolyLine(vertices[vertices.Length - 1], vertices[0]);


                if (doWireframe)
                {
                    var vertices = cellDrawing.GetFaceVertices(offset, dir, t.center, t.tileSize)
                    .Select(t.transform.TransformPoint)
                    .ToArray();
                    Handles.color = Color.black;
                    if (vertices.Length > 0)
                    {
                        Handles.DrawAAPolyLine(vertices);
                        Handles.DrawAAPolyLine(vertices[vertices.Length - 1], vertices[0]);
                    }
                }
            }
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if (Tile3dTargets().Count() == 0)
            {
                return;
            }

            int controlId = EditorGUIUtility.GetControlID(FocusType.Passive);

            var hit = FindHit();

            if (hit != null)
            {
                EditorGUIUtility.AddCursorRect(new Rect(-5000, -5000, 10000, 10000), MouseCursor.Arrow);
            }

            var pencilHit = FindHit(PaintMode.Pencil);

            // Show tooltip
            if (pencilHit != null)
            {
                if (pencilHit.target.palette == null)
                {
                    PGDebug.Message($"NO PALETTE").LogError();
                }

                var palette = pencilHit.target.palette;
                if (pencilHit.target.TryGet(pencilHit.offset, pencilHit.dir, out var faceDetails))
                {
                    var index = GetCellDrawingType(pencilHit.target).GetSubFaceValue(faceDetails, pencilHit.p);
                    if (0 <= index && index < palette.entryCount)
                    {
                        var entry = palette.entries[index];
                        var content = new GUIContent("", entry.name);
                        var style = GUI.skin.label;
                        var position = pencilHit.target.transform.TransformPoint(pencilHit.point);
                        Handles.BeginGUI();
                        var rect = HandleUtility.WorldPointToSizedRect(position, content, style);
                        GUI.Label(rect, content, style);
                        Handles.EndGUI();
                        //Handles.Label(position, content, style);
                    }
                }
            }

            void DoPaint()
            {
                var t = hit.target;
                if (PaintMode == PaintMode.Add)
                {
                    Undo.RecordObject(t, AddCubes);

                    var cellDrawingType = GetCellDrawingType(t);
                    var o2 = cellDrawingType.Move(hit.offset, hit.dir);
                    t.AddOffset(o2);
                }
                else if (PaintMode == PaintMode.Remove)
                {
                    Undo.RecordObject(t, RemoveCubes);

                    if (t.offsets.Count > 1)
                    {
                        t.RemoveOffset(hit.offset);
                    }
                }
                else
                {
                    Undo.RecordObject(t, Paint);
                    var cellDrawingType = GetCellDrawingType(t);
                    var paintIndex = Event.current.modifiers == EventModifiers.Shift ? 0 : WfcTilePaintingState.paintIndex;
                    foreach (var (offset, dir, faceDetails) in t.faceDetails)
                    {
                        foreach (var p in cellDrawingType.GetSubFaces(offset, dir))
                        {
                            if (HandleUtility.nearestControl == controlId && IsHighlight(hit, t, offset, dir, p))
                            {
                                cellDrawingType.SetSubFaceValue(faceDetails, p, paintIndex);
                            }
                        }
                    }
                }
            }

            if (Event.current.type == EventType.Repaint)
            {
                Repaint(hit, controlId);
            }
            else if (Event.current.type == EventType.Layout)
            {
                if (hit != null)
                {
                    HandleUtility.AddControl(controlId, 3.0f);
                }
            }
            else if (Event.current.type == EventType.MouseDown)
            {
                if (hit != null && HandleUtility.nearestControl == controlId && Event.current.button == 0)
                {
                    DoPaint();
                    GUIUtility.hotControl = controlId;
                    Event.current.Use();
                }
            }
            else if (Event.current.type == EventType.MouseMove)
            {
                // TODO: Only if necessary?
                window.Repaint();
            }
            else if (Event.current.type == EventType.MouseDrag)
            {
                if (HandleUtility.nearestControl == controlId && GUIUtility.hotControl == controlId)
                {
                    if (hit != null && PaintMode != PaintMode.Add && PaintMode != PaintMode.Remove)
                    {
                        DoPaint();
                    }
                    Event.current.Use();
                }
            }
            else if (Event.current.type == EventType.MouseUp)
            {
                if (HandleUtility.nearestControl == controlId && GUIUtility.hotControl == controlId)
                {
                    GUIUtility.hotControl = 0;
                    Event.current.Use();
                }
            }
            else if (Event.current.type == EventType.KeyDown)
            {
                CheckKeyPresses();
            }
        }

        internal static void CheckKeyPresses()
        {
            if (Event.current.type == EventType.KeyDown)
            {
                // Toggle show backfaces
                if (Event.current.keyCode == KeyCode.Z && Event.current.modifiers == EventModifiers.None)
                {
                    WfcTilePaintingState.showBackface = !WfcTilePaintingState.showBackface;
                    Event.current.Use();
                    EditorWindow.GetWindow(typeof(WfcTilePaintEditorWindow))?.Repaint();
                }

                // Toggle show all
                if (Event.current.keyCode == KeyCode.A && Event.current.modifiers == EventModifiers.None)
                {
                    WfcTilePaintingState.showAll = !WfcTilePaintingState.showAll;
                    Event.current.Use();
                    EditorWindow.GetWindow(typeof(WfcTilePaintEditorWindow))?.Repaint();
                }

                // Toggle hide all
                if (Event.current.keyCode == KeyCode.A && Event.current.modifiers == EventModifiers.Shift)
                {
                    WfcTilePaintingState.hideAll = !WfcTilePaintingState.hideAll;
                    Event.current.Use();
                    EditorWindow.GetWindow(typeof(WfcTilePaintEditorWindow))?.Repaint();
                }

                // Switch last 2 paint colors
                if (Event.current.keyCode == KeyCode.X && Event.current.modifiers == EventModifiers.None)
                {
                    (WfcTilePaintingState.paintIndex, WfcTilePaintingState.lastPaintIndex) =
                        (WfcTilePaintingState.lastPaintIndex, WfcTilePaintingState.paintIndex);
                    Event.current.Use();
                    EditorWindow.GetWindow(typeof(WfcTilePaintEditorWindow))?.Repaint();
                }
            }
        }
    }
    
    [EditorTool("Tile Vertex Paint")]
    public class PencilPaintTool : WfcTileEditorToolBase
    {
        private static class Styles
        {
            public static GUIContent toolbarIcon = new GUIContent(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Tessera/Editor/Resources/paint_pencil.png"), "Tile Pencil Paint");
        }

        protected override PaintMode PaintMode => PaintMode.Pencil;
        public override GUIContent toolbarIcon => Styles.toolbarIcon;
    }

    [EditorTool("Tile Edge Paint")]
    public class EdgePaintTool : WfcTileEditorToolBase
    {
        private static class Styles
        {
            public static GUIContent toolbarIcon = new GUIContent(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Tessera/Editor/Resources/paint_edge.png"), "Tile Edge Paint");
        }

        protected override PaintMode PaintMode => PaintMode.Edge;
        public override GUIContent toolbarIcon => Styles.toolbarIcon;
    }

    [EditorTool("Tile Face Paint")]
    public class FacePaintTool : WfcTileEditorToolBase
    {
        private static class Styles
        {
            public static GUIContent toolbarIcon = new GUIContent(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Tessera/Editor/Resources/paint_face.png"), "Tile Face Paint");
        }

        protected override PaintMode PaintMode => PaintMode.Face;
        public override GUIContent toolbarIcon => Styles.toolbarIcon;
    }

    [EditorTool("Tile Vertex Paint")]
    public class VertexPaintTool : WfcTileEditorToolBase
    {
        private static class Styles
        {
            public static GUIContent toolbarIcon = new GUIContent(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Tessera/Editor/Resources/paint_vertex.png"), "Tile Vertex Paint");
        }

        protected override PaintMode PaintMode => PaintMode.Vertex;
        public override GUIContent toolbarIcon => Styles.toolbarIcon;
    }

    [EditorTool("Tile Add Cube")]
    public class AddCubeTool : WfcTileEditorToolBase
    {
        private static class Styles
        {
            public static GUIContent toolbarIcon = new GUIContent(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Tessera/Editor/Resources/cube_add.png"), "Tile Add Cube");
        }

        protected override PaintMode PaintMode => PaintMode.Add;
        public override GUIContent toolbarIcon => Styles.toolbarIcon;
    }

    [EditorTool("Tile Remove Cube")]
    public class RemoveCubeTool : WfcTileEditorToolBase
    {
        private static class Styles
        {
            public static GUIContent toolbarIcon = new GUIContent(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Tessera/Editor/Resources/cube_remove.png"), "Tile Remove Cube");
        }

        protected override PaintMode PaintMode => PaintMode.Remove;
        public override GUIContent toolbarIcon => Styles.toolbarIcon;
    }
}