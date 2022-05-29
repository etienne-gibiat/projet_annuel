using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace VoidToolSpace
{
    internal enum MessagePriorityLevel
    {
        High = 0,
        Question,
        Normal,
        Low,
    }

    internal sealed class VisualStudioTaskListView : EditorWindow
    {
        #region validation

        private const string pref_Show_DebugLog = "EditorPref_Show_Debug_On_AssetLoad";

        [MenuItem("Tools/Visual Studio Task List View/Show Debug ON")]
        private static void VSPrefOnClicked()
        {
            EditorPrefs.SetBool(pref_Show_DebugLog, true);
            Debug.Log("Enabled Debug messages for VSTaskListView");
        }

        [MenuItem("Tools/Visual Studio Task List View/Show Debug ON", true)]
        private static bool VSPrefOn_validate()
        {
            return !EditorPrefs.GetBool(pref_Show_DebugLog, false);
        }

        [MenuItem("Tools/Visual Studio Task List View/Show Debug OFF")]
        private static void VSPrefOffClicked()
        {
            EditorPrefs.SetBool(pref_Show_DebugLog, false);
            Debug.Log("Disabled Debug messages for VSTaskListView");
        }

        [MenuItem("Tools/Visual Studio Task List View/Show Debug OFF", true)]
        private static bool VSPrefOff_validate()
        {
            return EditorPrefs.GetBool(pref_Show_DebugLog, false);
        }

        #endregion validation

        private static string[] TabOptions = new string[]
        {
            "Order By File",
            "Order By Priority",
        };

        private static int TabIndex = 0;

        private const string m_exclamation = "//! ";
        private const string m_Question = "//? ";
        private const string m_TODO = "//TODO ";
        private const string m_X = "//X ";

        private const string m_fileEnd = ".cs";

        private static readonly string m_Newline = System.Environment.NewLine;

        private static readonly string[] m_TodoTypes = new string[]
        { m_exclamation, m_Question, m_TODO, m_X };

        private static MessagePriorityLevel m_priorityLevel = MessagePriorityLevel.Normal;
        private static List<GuiData> m_TodoList = new List<GuiData>();

        private Vector2 scrollPos = Vector2.zero;
        private static string m_buttonContext = string.Empty;
        private static readonly string m_refresh = "Refresh";
        private static readonly string m_loading = "Loading...";

        [MenuItem("Tools/Visual Studio Task List View/Open TaskList %T", priority = 1)]
        private static void ShowWindow()
        {
            VisualStudioTaskListView window = GetWindow<VisualStudioTaskListView>();
            window.titleContent = new GUIContent("VS TaskList");
            window.minSize = new Vector2(300, 100);

            window.Show();
            m_buttonContext = m_refresh;
        }

        private void OnGUI()
        {
            try
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos);

                TabIndex = GUILayout.Toolbar(TabIndex, TabOptions);

                switch (TabIndex)
                {
                    case 0:
                    {
                        OrderByFileGUI();
                        break;
                    }
                    case 1:
                    {
                        OrderByPriorityGUI();
                        break;
                    }
                    default:
                        break;
                }

                EditorGUILayout.EndScrollView();

                if (GUILayout.Button(m_buttonContext))
                {
                    m_buttonContext = m_loading;
                    m_TodoList.Clear();
                    LookThroughScripts();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void OrderByPriorityGUI()
        {
            for (int i = 0; i < m_TodoTypes.Length; i++)
            {
                switch (m_TodoTypes[i])
                {
                    case m_exclamation:
                        {
                            GUI.backgroundColor = Color.red;
                            GUI.contentColor = Color.yellow;
                            break;
                        }
                    case m_Question:
                        {
                            GUI.backgroundColor = Color.magenta;
                            GUI.contentColor = Color.white;
                            break;
                        }
                    case m_TODO:
                        {
                            GUI.backgroundColor = Color.cyan;
                            GUI.contentColor = Color.white;
                            break;
                        }
                    case m_X:
                        {
                            GUI.backgroundColor = Color.grey;
                            GUI.contentColor = Color.white;
                            break;
                        }
                }
                m_priorityLevel = (MessagePriorityLevel)i;
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox($"{m_priorityLevel}", MessageType.None);

                foreach (GuiData item in m_TodoList)
                {
                    if (item.MessageLevel == (MessagePriorityLevel)i)
                    {
                        if (GUILayout.Button($"{item.messageText}   ({item.fileName} at line: {item.lineNumber})", EditorStyles.miniTextField))
                        {
                            AssetDatabase.OpenAsset(FromRelativePathToInstanceID(item.path), item.lineNumber);
                        }
                    }
                }
            }
        }

        private void OrderByFileGUI()
        {
            string prevFile = "";

            if (m_TodoList.Count > 0)
            {
                foreach (GuiData item in m_TodoList)
                {
                    if (prevFile != item.fileName)
                    {
                        prevFile = item.fileName;

                        EditorGUILayout.Space();
                        GUILayout.BeginHorizontal();

                        GUI.contentColor = Color.white;
                        GUI.backgroundColor = Color.clear;

                        GUILayout.Label($"In Script \"{item.fileName}\"", EditorStyles.boldLabel);
                        if (GUILayout.Button("Open File"))
                        {
                            AssetDatabase.OpenAsset(FromRelativePathToInstanceID(item.path));
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUI.contentColor = Color.white;
                    GUI.backgroundColor = Color.clear;

                    switch (item.MessageLevel)
                    {
                        case MessagePriorityLevel.High:
                            {
                                GUI.backgroundColor = Color.red;
                                GUI.contentColor = Color.yellow;
                                break;
                            }
                        case MessagePriorityLevel.Question:
                            {
                                GUI.backgroundColor = Color.magenta;
                                GUI.contentColor = Color.white;
                                break;
                            }
                        case MessagePriorityLevel.Normal:
                            {
                                GUI.backgroundColor = Color.cyan;
                                GUI.contentColor = Color.white;
                                break;
                            }
                        case MessagePriorityLevel.Low:
                            {
                                GUI.backgroundColor = Color.white;
                                GUI.contentColor = Color.white;
                                break;
                            }
                    }

                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button($"{item.messageText} (at line {item.lineNumber})", EditorStyles.miniTextField))
                    {
                        AssetDatabase.OpenAsset(FromRelativePathToInstanceID(item.path), item.lineNumber);
                    }

                    GUILayout.EndHorizontal();
                }
            }

            GUI.backgroundColor = Color.clear;
            GUI.contentColor = Color.white;

            // TODO change refresh lable to "loading" while its searching
        }

        internal static void SearchScripts() => LookThroughScripts();

        private static void LookThroughScripts()
        {
            //replace this
            Dictionary<string, string> lookUp = new Dictionary<string, string>();

            string[] path = Directory.GetFiles($"{GetProjectFolder()}Assets\\", $"*{m_fileEnd}", SearchOption.AllDirectories);

            for (int i = 0; i < path.Length; i++)
            {
                string _filepath = path[i];

                if (_filepath.EndsWith(m_fileEnd))
                {
                    using (StreamReader sr = new StreamReader(_filepath))
                    {
                        lookUp.Add(_filepath, sr.ReadToEnd());
                    }
                }
            }

            foreach (string script in lookUp.Values)
            {
                string _sub = string.Empty;
                int _lineCount = 1;
                foreach (string _todoType in m_TodoTypes)
                {
                    _sub = script;
                    while (_sub.Contains(_todoType))
                    {
                        int _start = _sub.IndexOf(_todoType) + _todoType.Length;
                        int _end = _sub.Substring(_start).IndexOf(m_Newline);

                        if (_end < 0) _end = 0;

                        //checks if its empty
                        if (_sub.Substring(_start, _end) == "\";")
                        {
                            _sub = _sub.Substring(_start + _end);
                        }
                        else
                        {
                            //count line numbers
                            for (int j = 0; j < _start + _end; j++)
                            {
                                if ($"{_sub[j]}{_sub[j + 1]}" == m_Newline)
                                {
                                    _lineCount++;
                                }
                            }

                            switch (_todoType)
                            {
                                case m_exclamation:
                                    {
                                        m_priorityLevel = MessagePriorityLevel.High;
                                        break;
                                    }
                                case m_TODO:
                                    {
                                        m_priorityLevel = MessagePriorityLevel.Normal;
                                        break;
                                    }
                                case m_Question:
                                    {
                                        m_priorityLevel = MessagePriorityLevel.Question;
                                        break;
                                    }
                                case m_X:
                                    {
                                        m_priorityLevel = MessagePriorityLevel.Low;
                                        break;
                                    }
                                default:
                                    {
                                        Debug.Log("something went wrong... " + _todoType);
                                        break;
                                    }
                            }

                            GuiData _data = new GuiData
                            {
                                MessageLevel = m_priorityLevel,
                                messageText = (_end > 1 ? _sub.Substring(_start, _end) : _sub.Substring(_start)),
                                lineNumber = _lineCount,
                                path = ExtentionFunctions.GetKeyByValueInput(lookUp, script),
                            };

                            _data.fileName = GetFileName(_data.path);
                            m_TodoList.Add(_data);

                            _sub = _sub.Substring(_start + _end);
                        }
                    }
                }
            }

            if (EditorPrefs.GetBool(pref_Show_DebugLog))
            {
                Debug.Log("Finished looking through the scripts");
            }

            m_buttonContext = m_refresh;
        }

        #region help functions

        /// <summary>
        /// gets the instance id using the guid using the relative path of the file
        /// </summary>
        /// <param name="_relativePath">file path from within unity's "Assets" folder</param>
        /// <returns>instance id of the file</returns>
        private static int GetInstance_IdFromGUIDAtRelativePath(string _relativePath)
        {
            string t = AssetDatabase.AssetPathToGUID(_relativePath);
            return AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetDatabase.GUIDToAssetPath(t)).GetInstanceID();
        }

        /// <returns>project folder before the "Assets" folder</returns>
        private static string GetProjectFolder()
        {
            string[] s = Application.dataPath.Split('/');
            string projectPath = "";
            for (int i = 0; i < s.Length - 1; i++)
            {
                projectPath += $"{s[i]}\\";
            }
            return projectPath;
        }

        /// <param name="_fullPath">file path (relative and full)</param>
        /// <returns>file name including extention</returns>
        private static string GetFileName(string _fullPath)
        {
            string[] s = _fullPath.Split('\\');
            return s[s.Length - 1];
        }

        /// <summary>
        /// gets the path of the file from within the unity "Assets" folder
        /// </summary>
        /// <param name="fullPath">Full filepath on the storage medium</param>
        /// <returns>the path from the "Assets" folder to the file</returns>
        private static string GetRelativePath(string fullPath)
        {
            string[] s = fullPath.Split('\\');
            string output = "";
            int k = 0;
            //cuts off every thing before "Assets"
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == "Assets")
                {
                    output += $"{s[i]}\\";
                    k = i;
                    break;
                }
            }
            //formats the string correctly
            for (int j = 1; j + k <= s.Length; j++)
            {
                if (j + k < s.Length - 1)
                {
                    output += $"{s[j + k]}\\";
                }
                else
                {
                    output += $"{s[j + k]}";
                    break;
                }
            }

            return output;
        }

        /// <summary>gets the InstanceId from the Relative Path used by the AssetDatabase</summary>
        /// <param name="_relativePath">key value of outlook Dictionary</param>
        /// <returns>Returns the InstanceID of the c# file</returns>
        private static int FromRelativePathToInstanceID(string _relativePath)
        {
            string relativepath = GetRelativePath(_relativePath);
            return GetInstance_IdFromGUIDAtRelativePath(relativepath);
        }

        #endregion help functions
    }

    internal static class ExtentionFunctions
    {
        internal static T GetKeyByValueInput<T, W>(this Dictionary<T, W> dict, W val)
        {
            T key = default;
            foreach (KeyValuePair<T, W> pair in dict)
            {
                if (EqualityComparer<W>.Default.Equals(pair.Value, val))
                {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }
    }

    internal struct GuiData
    {
        internal string fileName;
        internal string messageText;
        internal int lineNumber;
        internal MessagePriorityLevel MessageLevel;
        internal string path;
    }

    [InitializeOnLoad]
    internal sealed class InitializeScriptsOnLoad
    {
        static InitializeScriptsOnLoad()
        {
            try
            {
                VisualStudioTaskListView.SearchScripts();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}

