using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using MonKey.Extensions;
using PGSauce.Core.Fsm.WithSo;
using PGSauce.Core.Strings;
using PGSauce.Core.FSM.WithSo;
using PGSauce.Core.PGDebugging;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;

namespace PGSauce.Core.PGEditor
{
    public class FSMCodeGeneratorOdinWindow : CodeGeneratingOdinWindowBase, IActionTemplate, IDecisionTemplate, IStateTemplate, IStateControllerTemplate, IAnyTemplate
    {
        [MenuItem(MenuPaths.MenuBase + "Code Generation/FSM, Action, Decision")]
        public static void OpenWindow()
        {
            ShowWindow<FSMCodeGeneratorOdinWindow>("FSM, Action, Decision");
            Reload();
        }

        [Button]
        private static void Reload()
        {
            GetStateMachines();
        }

        public enum ActionType
        {
            Enter = 0,
            Exit = 1,
            Update = 2,
            FixedUpdate = 3
        }
        
        private FSMGenerationAction _fsmGenerationAction;
        private FSMActionGenerationAction _fsmActionGenerationAction;
        private FSMDecisionGenerationAction _fsmDecisionGenerationAction;
        
        private string _baseScriptsPath = "";
        private string _defaultValue = "";
        private static List<string> _statesTypes;
        private List<string> _currentStates;
        private string _currentDecisionName;
        
        private static List<string> _stateMachines;
        private static Dictionary<string, Type> _loadedFsmTypes;
        private static Dictionary<string, Type> _loadStateTypes;
        
        protected override string InsidePgSauceSubNameSpace => "FSM.WithSo";
        public string StateControllerName => _stateControllerName;

        private string[] StateMachines
        {
            get
            {
                if (_stateMachines == null)
                {
                    GetStateMachines();
                }
                // ReSharper disable once PossibleNullReferenceException
                return _stateMachines.ToArray();
            }
        }
        
        [ValueDropdown("StateMachines"), TabGroup("New Action"), TabGroup("New Decision"), OnValueChanged("LoadStates"), ShowInInspector] private string _stateControllerName = "";
        
        private string[] CurrentStates => _currentStates == null ? Array.Empty<string>() : _currentStates.ToArray();
        [ValueDropdown("CurrentStates"), TabGroup("New Action"), TabGroup("New Decision"), ShowInInspector] private string _selectedStateType = "";
        public string SelectedState => FormatState(_selectedStateType);
        

        [ShowInInspector, TabGroup("New Action"), LabelText("Name of The Action")]
        public string ActionName => $"{ActionTypeName}{SelectedState}Action{NAME()}";
        [ShowInInspector, TabGroup("New Action")]
        private ActionType _actionType;
        public string ActionTypeName => _actionType.ToString();
        [Button("Generate Action", ButtonSizes.Large), TabGroup("New Action"), GUIColor(0,1,0)]
        private void GenerateActionButton()
        {
            _fsmActionGenerationAction.Generate();
        }
        

        [ShowInInspector, TabGroup("New Decision"), LabelText("Name of The Decision")]
        private string DecisionPart => $"{SelectedState}To{ToThisState}";
        [ValueDropdown("CurrentStates"), TabGroup("New Decision"), ShowInInspector] private string toThisState = "";
        public string ToThisState => FormatState(toThisState);
        [Button("Generate Decision", ButtonSizes.Large), TabGroup("New Decision"), GUIColor(0,1,0)]
        private void GenerateDecisionButton()
        {
            _fsmDecisionGenerationAction.Generate();
        }
        
        [ShowInInspector, LabelText("Name of The FSM"), TabGroup("New FSM"), PropertyOrder(-500)]
        [ValidateInput("IsNotNullOrEmpty", "Can't be empty!")]
        private string _name = "";

        private string _decisionPart;

        [Button("Generate New FSM", ButtonSizes.Large), TabGroup("New FSM"), GUIColor(0,1,0), PropertyOrder(-100)]
        private void GenerateFsmButton()
        {
            _fsmGenerationAction.Generate();
        }
        
        protected override void OnInspectorGUIBehaviour()
        {
            base.OnInspectorGUIBehaviour();
            _fsmGenerationAction = new FSMGenerationAction(this);
            _fsmActionGenerationAction = new FSMActionGenerationAction(this);
            _fsmDecisionGenerationAction = new FSMDecisionGenerationAction(this);
        }

        public bool IsNotNullOrEmpty(string val)
        {
            return !string.IsNullOrEmpty(val);
        }

        public void GenerateNewFsm()
        {
            GenerateFolders();
            
            GenerateStateController();
            GenerateState();
            GenerateAny();
            
            GenerateFsmDecision( "True", _baseScriptsPath, "true");
            GenerateFsmDecision("False", _baseScriptsPath, "false");
            
            /*
            EditorPrefs.SetBool("ShouldCreateAsset", true);
            EditorPrefs.SetString("FalseDecisionAssetName", string.Format("{0}Decision{1}", "False", objectName));
            EditorPrefs.SetString("TrueDecisionAssetName", string.Format("{0}Decision{1}", "True", objectName));
            EditorPrefs.SetString("StateAssetName", string.Format("State{0}", objectName));
            */
        }

        public void GenerateNewFsmAction()
        {
            var result = Templates.fsmAction.ReplaceTemplateWithData(new ActionTemplate(this));
            var intoPath = Path.Combine(AssetsDirPath, LocalSelectedPath);
            var filename = $"{ACTIONNAME()}.cs";

            AbstractGenerateOneFile(result, intoPath, filename);
        }
        
        public void GenerateNewFsmDecision()
        {
            GenerateFsmDecision(DecisionPart, Path.Combine(AssetsDirPath, LocalSelectedPath), "true || false");
        }
        
        public string SUBNAMESPACE()
        {
            return GetSubNamespace();
        }

        public string MENUANY()
        {
            return $"{CreateMenuPrefixPath}\"Finite State Machine/Any/{NAME()}\"";
        }

        public string ANYNAME()
        {
            return $"Any{_name}";
        }

        public string MENUSTATE()
        {
            return $"{CreateMenuPrefixPath}\"Finite State Machine/States/{NAME()}\"";
        }

        public string STATENAME()
        {
            return $"State{_name}";
        }

        public string MENUDECISION()
        {
            return $"{CreateMenuPrefixPath}\"Finite State Machine/Decisions/{NAME()}/{_decisionPart}\"";
        }

        public string MENUACTION()
        {
            return $"{CreateMenuPrefixPath}\"Finite State Machine/Actions/{NAME()}/{ACTIONNAME()}\"";
        }

        public string STATECONTROLLERNAME()
        {
            return _stateControllerName.IsNullOrEmpty() ? $"MonoStateMachine{_name}" : _stateControllerName;
        }

        public string NAME()
        {
            return _name.IsNullOrEmpty() ? _stateControllerName.Replace("MonoStateMachine", "") : _name;
        }

        public string DECISIONNAME()
        {
            return _currentDecisionName;
        }

        public string DEFAULTVALUE()
        {
            return _defaultValue;
        }
        
        public string ACTIONNAME()
        {
            return ActionName;
        }
        
        /// <summary>
        /// Called by Odin
        /// </summary>
        private void LoadStates()
        {
            _selectedStateType = "";
            if (string.IsNullOrEmpty(_stateControllerName))
            {
                return;
            }

            var machineType = Type.GetType(_loadedFsmTypes[_stateControllerName].AssemblyQualifiedName ?? string.Empty);
            
            if(machineType == null){return;}

            var method = typeof(FSMCodeGeneratorOdinWindow).GetMethod("GetLoadedStates", BindingFlags.Instance | BindingFlags.NonPublic);

            if (method == null)
            {
                PGDebug.Message($"Method GetLoadedStates is null, maybe it was changed from private to public ? or got a new argument ? NEW SIGNATURE ?").Log();
                return;
            }
            var generic = method.MakeGenericMethod(machineType);


            var sos = (List<SoStateBase>) generic.Invoke(this, null);

            if (sos == null)
            {
                PGDebug.Message($"no so found").Log();
                _currentStates = new List<string>();
            }
            else
            {
                PGDebug.Message($"Found {sos.Count} states").Log();
                _currentStates = sos.Where(so => !so.IsNullState).Select(so => so.StateName).ToList();
                PGDebug.Log(_currentStates);
            }
        }
        
        [UsedImplicitly]
        private List<SoStateBase> GetLoadedStates<T>() where T : MonoStateMachineWithSo<T>
        {
            var fsmName = typeof(T).Name;

            if (_statesTypes.Any(stateName => stateName.Contains(fsmName)))
            {
                var typeName = _statesTypes.First(stateName => stateName.Contains(fsmName));
                var type = _loadStateTypes[typeName];
                var states = PGAssets.GetAllInstances<SoState<T>>(type);
                return states.Cast<SoStateBase>().ToList();
            }

            return new List<SoStateBase>();
        }
        
        private void GenerateFolders()
        {
            var folderPath = Path.Combine(AssetsDirPath, LocalSelectedPath, $"State Machine {NAME()}");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            Directory.CreateDirectory(_baseScriptsPath = Path.Combine(folderPath, "__BASE SCRIPTS"));

            Directory.CreateDirectory(Path.Combine(folderPath, "User Scripts"));

            Directory.CreateDirectory(Path.Combine(folderPath, "User Scripts", "Actions"));
            Directory.CreateDirectory(Path.Combine(folderPath, "User Scripts", "Decisions"));

            Directory.CreateDirectory(Path.Combine(folderPath, "Scriptable Objects"));

            Directory.CreateDirectory(Path.Combine(folderPath, "Scriptable Objects", "Actions"));
            Directory.CreateDirectory(Path.Combine(folderPath, "Scriptable Objects", "Decisions"));
            Directory.CreateDirectory(Path.Combine(folderPath, "Scriptable Objects", "States"));
        }
        
        private void GenerateStateController()
        {
            var result = Templates.fsmStateController.ReplaceTemplateWithData( new StateControllerTemplate(this));
            var intoPath = Path.Combine(_baseScriptsPath);
            var filename = $"{STATECONTROLLERNAME()}.cs";
            
            AbstractGenerateOneFile(result, intoPath, filename);
        }
        
        private void GenerateAny()
        {
            var result = Templates.fsmAny.ReplaceTemplateWithData( new AnyTemplate(this));
            var intoPath = Path.Combine(_baseScriptsPath);
            var filename = $"{ANYNAME()}.cs";
            
            AbstractGenerateOneFile(result, intoPath, filename);
        }
        
        private void GenerateState()
        {
            var result = Templates.fsmState.ReplaceTemplateWithData(new StateTemplate(this));
            var intoPath = Path.Combine(_baseScriptsPath);
            var filename = $"{STATENAME()}.cs";
            
            AbstractGenerateOneFile(result, intoPath, filename);
        }
        
        
        private void GenerateFsmDecision(string decisionPart, string pathToScript, string defaultValue)
        {
            _decisionPart = decisionPart;
            _currentDecisionName = $"Decision{decisionPart}{NAME()}";
            _defaultValue = defaultValue;

            var result = Templates.fsmDecision.ReplaceTemplateWithData(new DecisionTemplate(this));
            var intoPath = Path.Combine(pathToScript);
            var filename = $"{_currentDecisionName}.cs";

            AbstractGenerateOneFile(result, intoPath, filename);
        }

        private static void GetStateMachines()
        {
            _loadedFsmTypes = new Dictionary<string, Type>();
            _loadStateTypes = new Dictionary<string, Type>();
            _stateMachines = new List<string>();
            _statesTypes = new List<string>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in assembly.GetTypes())
                {
                    if(!t.IsVisible || t.IsGenericType) { continue;}
                    if (t.ImplementsOrInherits(typeof(IMonoStateMachineWithSo)) && t != typeof(IMonoStateMachineWithSo))
                    {
                        _stateMachines.Add(t.Name);
                        _loadedFsmTypes.Add(t.Name, t);
                    }
                    else if (t.ImplementsOrInherits(typeof(SoStateBase)) && t != typeof(SoStateBase))
                    {
                        var baseTypeFullName = t.BaseType.FullName;
                        _statesTypes.Add(baseTypeFullName);
                        _loadStateTypes.Add(baseTypeFullName, t);
                    }
                }
            }
        }
        
        private string FormatState(string state)
        {
            return string.IsNullOrEmpty(state) ? "" : state.Trim().Replace(" ", "");
        }
        
        
        /*
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void CreateAssetWhenReady()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                EditorApplication.delayCall += CreateAssetWhenReady;
                return;
            }

            EditorApplication.delayCall += CreateAssetNow;
        }

        private static void CreateAssetNow()
        {
            bool generate = EditorPrefs.GetBool("ShouldCreateAsset", false);

            if (generate)
            {
                string fDecisionName = EditorPrefs.GetString("FalseDecisionAssetName");
                string tDecisionName = EditorPrefs.GetString("TrueDecisionAssetName");
                string stateName = EditorPrefs.GetString("StateAssetName");

                Type fDecisionType = Type.GetType(fDecisionName);
                Type tDecisionType = Type.GetType(tDecisionName);
                Type stateType = Type.GetType(stateName);

                Debug.Log(fDecisionName + " " + tDecisionName + " " + stateName);
                Debug.Log(fDecisionType +" " + tDecisionType + " " + stateType);
                Debug.Log(fDecisionType.FullName + " " + tDecisionType.Name + " " + stateType.Name);
            }


            EditorPrefs.SetBool("ShouldCreateAsset", false);
            EditorPrefs.SetString("FalseDecisionAssetName", string.Empty);
            EditorPrefs.SetString("TrueDecisionAssetName", string.Empty);
            EditorPrefs.SetString("StateAssetName", string.Empty);
        }
        */

        
    }
}
