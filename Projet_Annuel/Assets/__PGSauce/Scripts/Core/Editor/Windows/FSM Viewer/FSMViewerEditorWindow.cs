using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.Globalization;
using System.Linq;
using PGSauce.Core.FSM.Base;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Strings;

namespace PGSauce.Core.PGEditor
{
    public class FSMViewerEditorWindow : EditorWindow
    {
        [MenuItem(MenuPaths.MenuBase + "View FSM")]
        private static void InitWindow()
        {
            _window = GetWindow<FSMViewerEditorWindow>();
            LoadResources();
            _window.RegenerateGraph();
            _window.minSize = new Vector2(600, 600);
            _window._weaveStartTime = Time.realtimeSinceStartup;
            _window.Show();
        }

        private void OnGUI()
        {
            try
            {
                if (_skin != null)
                {
                    GUI.skin = _skin;
                }
            
                _graph.MoveTo(_targetGraphOffset);

                if (_forceLayoutGraph is {WithinThreshold: true})
                {
                    Repaint();
                }

                DrawFsm();
            }
            catch (Exception)
            {
                InitWindow();
            }

            try
            {
                DrawUi();
            }
            catch (Exception e)
            {
                PGDebug.SetException(e).LogException();
                throw;
            }
        }

        private static GUISkin _skin;
        
        public const float Stiffness = 80f;
        private const int MaximumCycles = 1000;
        private const float TimeStep = 0.05f;
        public const float OverDrawSize = 5f;
        public const float LineOffset = 13f;
        public const string ArrowIconName = "ArrowIcon";
        private const string LogoName = "Logo";
        private const string RegenerateName = "Fsm Regenerate Button";
        private const string CenterName = "Fsm Center Button";
        private const string GuiSkinName = "FSM GUI SKIN";
        private const string StopName = "FSM Stop";
        public const float ForceLayoutThreshold = 1f;
        public const float ForceLayoutRepulsion = 80000.0f;
        public const float ForceLayoutDamping = 0.5f;

        private static FSMViewerEditorWindow _window;
        private float _weaveStartTime = -1f;
        private HashSet<AbstractState> _visited;
        private HashSet<AbstractState> _allStates;
        private AbstractState _initialState;
        private List<IAnyTransition> _any;
        private bool _saveState;
        private Graph _graph;
        private Background _background;
        private List<VisualNode> _visualNodes;
        private List<VisualEdge> _visualEdges;
        private Vector2 _targetGraphOffset;
        private ForceLayoutGraph _forceLayoutGraph;
        private static Texture _logo;
        private static Texture _centerButtonTexture;
        private static Texture _regenerateButtonTexture;
        private static Texture _stopIcon;
        private VisualNode _nodeOnPointer;
        private IStateMachine _stateMachine;

        private static void LoadResources()
        {
            _skin = Resources.Load(GuiSkinName) as GUISkin;
            _logo = Resources.Load(LogoName) as Texture;
            _centerButtonTexture = Resources.Load(CenterName) as Texture;
            _regenerateButtonTexture = Resources.Load(RegenerateName) as Texture;
            _stopIcon = Resources.Load(StopName) as Texture;
        }

        private void DrawFsm()
        {
            _background.Draw(_graph);
            _forceLayoutGraph.Update(TimeStep);
            foreach (var edge in _visualEdges)
            {
                edge.DrawEdge(_graph);
            }
            foreach (var edge in _visualEdges)
            {
                edge.DrawArrow(_graph);
            }

            foreach (var node in _visualNodes)
            {
                node.Draw(_graph);
            }
        }
        
        private void DrawUi()
        {
            var e = Event.current;
			var centerRect = new Rect (10f, position.size.y * (.15f + .2f), 45f, 45f);
			var generateRect = new Rect (10f, position.size.y * (.15f + .3f), 45f, 45f);
            var mousePosition = Event.current.mousePosition;

            GUI.DrawTexture (new Rect (10f, 10f, 100f, 100f), _logo);

            GenerateButton(centerRect, _centerButtonTexture, Recenter, mousePosition, "Re-Center");
            GenerateButton(generateRect, _regenerateButtonTexture, () =>
            {
                _weaveStartTime = Time.realtimeSinceStartup;
                _window.RegenerateGraph();
            }, mousePosition, "Regenerate");
            
            if (_forceLayoutGraph != null) {
                var color = GUI.color;
                var skin = GUI.skin;
                GUI.color = Color.black;
                GUI.skin = null;
                GUI.Box (new Rect (0f, position.size.y - 30f, position.size.x * .2f + 100f + (_saveState ? 40f : 0f), 30f), "");
                GUI.skin = skin;
                GUI.color = color;
                GUI.Box (new Rect (10f, position.size.y - 20f, position.size.x * .2f, 10f), "");
                GUI.color = Color.green;
                GUI.Box (new Rect (10f, position.size.y - 20f, position.size.x * .2f * (_forceLayoutGraph.WithinThreshold ? 1f : ((float)_forceLayoutGraph.Count / _forceLayoutGraph.MaxCycles)), 10f), "");
                GUI.color = Color.white;

                GUI.Label (new Rect (position.size.x * .2f + 30f, position.size.y - 25f, 100f, 50f), _forceLayoutGraph.WithinThreshold ? "Complete" : Mathf.RoundToInt (((float)_forceLayoutGraph.Count / _forceLayoutGraph.MaxCycles) * 100) + "%");
                GUI.Label (new Rect (position.size.x * .2f + 100f, position.size.y - 25f, 100f, 50f), _saveState ? "Saved" : "");
                if (!_forceLayoutGraph.WithinThreshold) {

                    var savedStyle = GUIStyle.none;

                    GUI.Box (new Rect (0f, 0f, position.size.x, position.size.y), "");
                    var weavingRect = new Rect (position.size / 2f + new Vector2 (-75f, -50f - (position.size.y / 4f - 45f) - 20f), new Vector2 (200f, 50f));
                    GUI.Box (weavingRect, "");
                    if (Mathf.RoundToInt (Time.realtimeSinceStartup - _weaveStartTime) % 2 == 0) {
                        savedStyle.alignment = TextAnchor.MiddleCenter;
                        savedStyle.fontSize = 20;
                        savedStyle.normal.textColor = Color.white;
                        GUI.Label (weavingRect, "Weaving...", savedStyle);
                    }

                    if (GUI.Button(new Rect(position.size.x * .2f + 60f, position.size.y - 25f, 20f, 20f), _stopIcon))
                    {
                        _forceLayoutGraph.Count = _forceLayoutGraph.MaxCycles;
                    }
                    Repaint ();

                    if (e.button == 0)
                    {
                        _targetGraphOffset += e.delta / 2f;
                    }
					
                    return;
                }
                GUI.color = color;
            }
            
            var posX = position.size.x;
            ZoomUI(posX);
            MouseEvent(e);
            DrawInitialState();
        }

        private void DrawInitialState()
        {
            if (_stateMachine == null)
            {
                return;
            }
            var state = _stateMachine.GetCurrentState();
            if (state == null)
            {
                state = _initialState;
            }
            if(state == null){return;}
            var defaultColor = GUI.color;
            var n = _graph.GetNode(state);
            if (n == null)
            {
                return;
            }
            var node = n.VisualNodeInstance;
            GUI.color = Color.yellow;
            GUI.skin.box.fontSize = VisualNode.FontSize;
            GUI.Box(node.DrawableRect, node.Title);
            GUI.color = defaultColor;
        }

        private void MouseEvent(Event e)
        {
            if (e.button != 0) {return;}
            switch (e.type)
            {
                case EventType.MouseDown:
                {
                    if (_visualNodes != null)
                    {
                        foreach (var n in _visualNodes.Where(n => n.DrawableRect.Contains(e.mousePosition)))
                        {
                            _nodeOnPointer = n;
                        }
                    }
                    break;
                }
                case EventType.MouseUp:
                    _nodeOnPointer = null;
                    break;
            }

            if (_nodeOnPointer == null)
            {
                _targetGraphOffset += e.delta / 2f;
            }
            else
            {
                _nodeOnPointer.NodeInstance.Position += e.delta / 2f / VisualNode.SizeMultiplier;
                _saveState = false;
                var defaultColor = GUI.color;
                GUI.color = Color.yellow;
                GUI.skin.box.fontSize = VisualNode.FontSize;
                GUI.Box(_nodeOnPointer.DrawableRect, _nodeOnPointer.Title);
                GUI.color = defaultColor;
            }

            Repaint();
        }

        private void ZoomUI(float posX)
        {
	        var sizeMultiplier = VisualNode.SizeMultiplier;
	        var zoomOutRect = new Rect(posX - 202f, 70f, 50f, 50f);
	        var zoomInRect = new Rect(posX - 62f, 70f, 50f, 50f);
	        if (GUI.Button(zoomInRect, "+"))
	        {
		        if (sizeMultiplier < 1.2f)
		        {
			        sizeMultiplier += .1f;

			        _targetGraphOffset += Vector2.one * (-60f);
			        _graph.GraphOffset = _targetGraphOffset;
		        }
		        else
		        {
			        sizeMultiplier = 1.2f;
		        }
	        }

	        if (GUI.Button(zoomOutRect, "-"))
	        {
		        if (sizeMultiplier > .5f)
		        {
			        sizeMultiplier -= .1f;
			        _targetGraphOffset += Vector2.one * (60f);
			        _graph.GraphOffset = _targetGraphOffset;
		        }
		        else
		        {
			        sizeMultiplier = .5f;
		        }
	        }

	        VisualNode.SizeMultiplier = sizeMultiplier;

	        var sizeMultGuiStyle = GUIStyle.none;
	        sizeMultGuiStyle.fontSize = 15;
	        sizeMultGuiStyle.fontStyle = FontStyle.Bold;
	        sizeMultGuiStyle.alignment = TextAnchor.MiddleCenter;
	        sizeMultGuiStyle.normal.textColor = Color.white;
	        GUI.Label(new Rect(zoomOutRect.xMax + (zoomInRect.xMin - zoomOutRect.xMax) / 4f, 70f, 50f, 50f),
		        ((Mathf.Round(sizeMultiplier * 100f * 2f - 100f)) > 0 ? (Mathf.Round(sizeMultiplier * 100f * 2f - 100f)) : 1)
		        .ToString(CultureInfo.InvariantCulture) + '%', sizeMultGuiStyle);
        }

        private static void GenerateButton(Rect rect, Texture texture, Action action, Vector2 mousePosition, string tooltip)
        {
            if (GUI.Button(rect, texture))
            {
                action();
            }

            if (rect.Contains(mousePosition))
            {
                GUI.Box(new Rect(new Vector2(rect.xMax, rect.yMin), new Vector2(70f, 25f)), tooltip);
            }
        }

        private void RegenerateGraph()
        {
            _saveState = false;
            Recenter();
            _graph = new Graph();
            _background = new Background(ref _window);
            
            _visited = new HashSet<AbstractState>();
            _allStates = new HashSet<AbstractState>();
            InitializeInitialState();
            if(_initialState == null) { return; }
            FindAllStates(_initialState);
            GenerateAny();
            GenerateNodesAndEdges();
            _forceLayoutGraph = new ForceLayoutGraph(_graph, MaximumCycles);
        }

        private void GenerateAny()
        {
            foreach (var state in _allStates)
            {
                state.CleanTemporaryTransitions();
            }
            var newStates = new HashSet<AbstractState>();
            foreach (var any in _any)
            {
                var excluded = any.ExcludedStates();
                foreach (var state in _allStates)
                {
                    if (!excluded.Contains(state))
                    {
                        state.AddTemporaryTransition(any);
                        newStates.Add(any.GetTargetState());
                    }
                }
            }

            foreach (var state in newStates)
            {
                _allStates.Add(state);
            }
        }

        private void Recenter()
        {
            var center =_window.position.size / 2f;
            _targetGraphOffset = center;
        }

        private void InitializeInitialState()
        {
            _initialState = null;
            foreach (var o in Selection.GetFiltered(typeof(IStateMachine), SelectionMode.Assets))
            {
                _stateMachine = (IStateMachine) o;
                
                if (_stateMachine != null)
                {
                    _any = _stateMachine.GetAny();
                    _initialState = _stateMachine.GetInitialState();
                    break;
                }
            }
        }

        private void GenerateNodesAndEdges()
        {
            GenerateNodes();
            GenerateEdges();
        }

        private void GenerateEdges()
        {
            foreach (var fromState in _allStates)
            {
                var transitions = fromState.GetTransitionsIncludingTemporary();
                foreach (var transition in transitions)
                {
                    var toState = transition.GetTargetState();

                    if (toState.IsNullState) {continue;}
                    var edge = new DependencyEdge($"{fromState.Name()} => {toState.Name()}",
                        _graph.GetNode(fromState), _graph.GetNode(toState), transition.GetDecision(), Stiffness);
                    _graph.AddEdge(edge);
                    _visualEdges.Add(new VisualEdge(edge));
                }
            }
        }

        private void GenerateNodes()
        {
            _visualNodes = new List<VisualNode>();
            _visualEdges = new List<VisualEdge>();

            foreach (var node in _allStates.Select(state => new Node(state)))
            {
                _graph.AddNode(node);
                _visualNodes.Add(new VisualNode(node));
            }
        }

        private void FindAllStates(AbstractState state)
        {
            if (state.IsNullState) { return; }
            if (_visited.Contains(state)) { return; }

            _visited.Add(state);
            _allStates.Add(state);

            foreach (var t in state.GetTransitions().Select(transition => transition.GetTargetState()))
            {
                if (!t.IsNullState)
                {
                    _allStates.Add(t);
                }

                FindAllStates(t);
            }
        }

        
    }
}
