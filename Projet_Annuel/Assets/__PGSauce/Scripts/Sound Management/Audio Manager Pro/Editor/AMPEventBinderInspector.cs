using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.Events;
using UnityEngine.Events;
using System;
using System.Reflection;
using PGSauce.AudioManagement.External;
using PGSauce.AudioManagement.External.Utilities;
using System.Linq;
using PGSauce.AudioManagement.External.EventBinding;

namespace PGSauce.AudioManagement.External
{
	namespace EventBinding
	{
		/// <summary>Event binder for the full class.</summary>
		public class ClassEventBinder
		{
			/// <summary>The name of the class.</summary>
			public readonly string ClassName;
			/// <summary>The class type.</summary>
			public readonly Type ClassType;
			/// <summary>All member method groups marked as Bindable.</summary>
			public readonly MethodEventBinder[] BindableMethods;

			/// <summary>Gets the names of all of the bindable methods contained in this ClassEventBinder.</summary>
			public string[] MethodNameList
			{
				get
				{
					//Creates name array
					string[] NameList = new string[BindableMethods.Length];
					for (int i = 0; i < NameList.Length; i++) { NameList[i] = BindableMethods[i].MethodName; }
					return NameList;
				}
			}

			/// <summary>Creates a ClassEventBinder instance to handle binding methods for a specific class.</summary>
			/// <param name="TargetClassType">The class to bind methods for.</param>
			public ClassEventBinder(Type TargetClassType)
			{
				//Copies information
				ClassType = TargetClassType;
				ClassName = TargetClassType.Name;

				//Gets all method overloads marked as Bindable
				MethodInfo[] AllOverloads = TargetClassType.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where((MethodInfo x) => x.GetCustomAttributes(typeof(BindableMethod), false).Length > 0).ToArray();

				//Creates a subset of unique methods, removing extra overloads
				MethodInfo[] UniqueMethods = AllOverloads.DistinctBy((MethodInfo x) => x.Name).OrderBy((MethodInfo x) => x.Name).ToArray();

				//Creates BindableMethods for each method group
				BindableMethods = new MethodEventBinder[UniqueMethods.Length];
				for (int i = 0; i < UniqueMethods.Length; i++) { BindableMethods[i] = new MethodEventBinder(AllOverloads.Where((MethodInfo x) => x.Name == UniqueMethods[i].Name).ToArray()); }
			}

			/// <summary>Creates and binds a new BindedEvent.</summary>
			/// <returns>The BindedEvent.</returns>
			/// <param name="DesiredTriggerType">The desired trigger type.</param>
			/// <param name="DesiredOverload">The method overload to bind.</param>
			/// <param name="MethodParamaterValues">The values to pass as paramaters to the method overload.</param>
			public BindedEvent CreateAndBindEvent(EventTriggerType DesiredTriggerType, OverloadEventBinder DesiredOverload, object[] MethodParamaterValues)
			{
				//Creates invokable action and name
				UnityAction<object> Callback = DesiredOverload.CreateInvokableAction((object[])MethodParamaterValues.Clone());
				string EventName = DesiredOverload.CreateEventName(MethodParamaterValues);
				UnityObjectEvent CallbackEvent = new UnityObjectEvent();
				UnityEventTools.AddPersistentListener(CallbackEvent, Callback);

				//Binds the event
				return new BindedEvent(EventName, DesiredTriggerType, CallbackEvent, ClassType);
			}
		}

		/// <summary>Event binder for the full method group, containing each individual overload.</summary>
		public class MethodEventBinder
		{
			/// <summary>The name of the method.</summary>
			public readonly string MethodName;
			/// <summary>The individual overloads of this method.</summary>
			public readonly OverloadEventBinder[] MethodOverloads;

			/// <summary>Gets the names of all of the overloads contained in this MethodEventBinder.</summary>
			public string[] OverloadNameList
			{
				get
				{
					string[] NameList = new string[MethodOverloads.Length];
					for (int i = 0; i < NameList.Length; i++) { NameList[i] = MethodOverloads[i].FullDisplayName; }
					return NameList;
				}
			}

			/// <summary>Creates a MethodEventBinder to handle event binding for all overloads that are part of this method.</summary>
			/// <param name="FullMethodSet">Array of all of the overloads of this method.</param>
			public MethodEventBinder(MethodInfo[] FullMethodSet)
			{
				//Copies information
				MethodName = FullMethodSet[0].Name;

				//Creates OverloadEventBinders
				MethodOverloads = new OverloadEventBinder[FullMethodSet.Length];
				for (int i = 0; i < FullMethodSet.Length; i++) { MethodOverloads[i] = new OverloadEventBinder(FullMethodSet[i]); }
			}
		}

		/// <summary>Event binder for an individual method overload.</summary>
		public class OverloadEventBinder
		{
			/// <summary>The full display name of the overload for editor displaying.</summary>
			public readonly string FullDisplayName;
			/// <summary>The paramaters taken by this overload.</summary>
			public readonly ParameterInfo[] MethodParamaters;
			/// <summary>Formatted type names of the paramaters.</summary>
			private readonly string[] ParamaterTypeNames;
			/// <summary>The MethodInfo object of this specific overload.</summary>
			private readonly MethodInfo MethodOverload;

			/// <summary>Mapping of simplified names for common system types.<summary>
			private static readonly Dictionary<Type, string> SimplifiedTypeNames = new Dictionary<Type, string>()
			{
				{typeof(int), "int"},
				{typeof(float), "float"},
				{typeof(double), "double"},
				{typeof(long), "long"},
				{typeof(string), "string"},
				{typeof(bool), "bool"}
			};

			/// <summary>Constructs a new OverloadBinder from a specific overload of a MethodInfo.</summary>
			/// <param name="MethodOverload">This overload's method.</param>
			public OverloadEventBinder(MethodInfo MethodOverload)
			{
				//Copies information
				this.MethodOverload = MethodOverload;
				MethodParamaters = MethodOverload.GetParameters();

				//Generates paramater type name list
				ParamaterTypeNames = new string[MethodParamaters.Length];
				for (int i = 0; i < ParamaterTypeNames.Length; i++)
				{
					if (SimplifiedTypeNames.ContainsKey(MethodParamaters[i].ParameterType)) { ParamaterTypeNames[i] = SimplifiedTypeNames[MethodParamaters[i].ParameterType]; }
					else { ParamaterTypeNames[i] = MethodParamaters[i].ParameterType.Name; }
				}

				//Creates editor friendly display name for the overload
				string[] ParamaterDisplayNames = new string[MethodParamaters.Length];
				for (int i = 0; i < ParamaterDisplayNames.Length; i++) { ParamaterDisplayNames[i] = ParamaterTypeNames[i] + " " + MethodParamaters[i].Name; }
				FullDisplayName = MethodOverload.Name + "(" + String.Join(", ", ParamaterDisplayNames) + ")";
			}

			/// <summary>Creates a paramater binded invokable action for this overload.</summary>
			/// <returns>The invokable action.</returns>
			/// <param name="MethodParamaterValues">The values to pass as paramaters to the method overload.</param>
			public UnityAction<object> CreateInvokableAction(params object[] MethodParamaterValues)
			{
				if (MethodParamaterValues.Length != MethodParamaters.Length) { throw new ArgumentException("Incorrect paramater count passed.", "MethodParamaterValues"); }
				return (object TargetObject) => MethodOverload.Invoke(TargetObject, MethodParamaterValues);
			}

			/// <summary>Creates the event name.</summary>
			/// <returns>The event name.</returns>
			/// <param name="MethodParamaterValues">The values to pass as paramaters to the method overload.</param>
			public string CreateEventName(params object[] MethodParamaterValues)
			{
				string[] ParamStrings = new string[MethodParamaterValues.Length];
				for (int i = 0; i < ParamStrings.Length; i++) { ParamStrings[i] = MethodParamaterValues[i] == null ? "null" : MethodParamaterValues[i].ToString(); }
				return MethodOverload.Name + "(" + String.Join(", ", ParamStrings) + ")";
			}
		}
	}

	namespace Editor
	{
	    [CustomEditor(typeof(AMPEventBinder))]
	    public class AMPEventBinderInspector : UnityEditor.Editor
	    {
			private bool CreatingEvent;
			private readonly ClassEventBinder[] ClassBinders = new ClassEventBinder[] { new ClassEventBinder(typeof(MusicManager)), new ClassEventBinder(typeof(PlaylistManager)), new ClassEventBinder(typeof(SFXManager)) };
			private readonly string[] ClassNames = { "MusicManager", "PlaylistManager", "SFXManager" };

			private EventTriggerType SelectedTrigger;
			private int SelectedClassIndex;
			private int SelectedMethodIndex;
			private int SelectedOverloadIndex;
			private OverloadEventBinder LastSelectedOverload;
			private object[] CurrentParams = new object[0];

	        private void OnEnable()
	        {
				CreatingEvent = false;
	        }

	        //Draws Playlist inspector
	        public override void OnInspectorGUI()
	        {
				if (!CreatingEvent) { CreatingEvent |= GUILayout.Button("Create new Event"); }
				else
				{
					
					SelectedTrigger = (EventTriggerType)EditorGUILayout.EnumPopup("Event Trigger Type", SelectedTrigger);


					int NewClassIndex = EditorGUILayout.Popup("Manager", SelectedClassIndex, ClassNames);
					if (NewClassIndex != SelectedClassIndex)
					{
						SelectedMethodIndex = 0;
						SelectedOverloadIndex = 0;
					}
					SelectedClassIndex = NewClassIndex;
					ClassEventBinder SelectedClass = ClassBinders[SelectedClassIndex];


					int NewMethodIndex = EditorGUILayout.Popup("Method", SelectedMethodIndex, SelectedClass.MethodNameList);
					if (NewMethodIndex != SelectedMethodIndex) { SelectedOverloadIndex = 0; }
					SelectedMethodIndex = NewMethodIndex;
					MethodEventBinder SelectedMethod = SelectedClass.BindableMethods[SelectedMethodIndex];
			
					if (SelectedMethod.MethodOverloads.Length > 1) { SelectedOverloadIndex = EditorGUILayout.Popup("Overload", SelectedOverloadIndex, SelectedMethod.OverloadNameList); }
					else { SelectedOverloadIndex = 0; }
					OverloadEventBinder SelectedOverload = SelectedMethod.MethodOverloads[SelectedOverloadIndex];
					EditorGUILayout.LabelField(SelectedOverload.FullDisplayName, EditorStyles.wordWrappedMiniLabel);
					if (SelectedOverload != LastSelectedOverload)
					{
						LastSelectedOverload = SelectedOverload;
						CurrentParams = new object[SelectedOverload.MethodParamaters.Length];
						for (int i = 0; i < CurrentParams.Length; i++)
						{
							ParameterInfo Param = SelectedOverload.MethodParamaters[i];
							Type ParamType = Param.ParameterType;
							if (!DBNull.Value.Equals(Param.DefaultValue)) { CurrentParams[i] = Param.DefaultValue; }
							else
							{
								if (ParamType.IsValueType) { CurrentParams[i] = Activator.CreateInstance(ParamType); }
								else if (ParamType == typeof(string)) { CurrentParams[i] = ""; }
								else { CurrentParams[i] = null; }
							}
						}
					}
					for (int i = 0; i < CurrentParams.Length; i++)
					{
						ParameterInfo Param = SelectedOverload.MethodParamaters[i];
						if (Param.ParameterType == typeof(int)){ CurrentParams[i] = EditorGUILayout.IntField(Param.Name, (int)CurrentParams[i]); }
						else if (Param.ParameterType == typeof(float)){ CurrentParams[i] = EditorGUILayout.FloatField(Param.Name, (float)CurrentParams[i]); }
						else if (Param.ParameterType == typeof(string)){ CurrentParams[i] = EditorGUILayout.TextField(Param.Name, (string)CurrentParams[i]); }
						else if (Param.ParameterType == typeof(bool)){ CurrentParams[i] = EditorGUILayout.Toggle(Param.Name, (bool)CurrentParams[i]); }
						else if (Param.ParameterType == typeof(Vector3)){ CurrentParams[i] = EditorGUILayout.Vector3Field(Param.Name, (Vector3)CurrentParams[i]); }
						else if (typeof(UnityEngine.Object).IsAssignableFrom(Param.ParameterType)) { CurrentParams[i] = EditorGUILayout.ObjectField(Param.Name, (UnityEngine.Object)CurrentParams[i], Param.ParameterType, !(typeof(ScriptableObject).IsAssignableFrom(Param.ParameterType) || EditorUtility.IsPersistent(target))); }
						else { EditorGUILayout.LabelField(CurrentParams[i] == null ? "null" : CurrentParams[i].ToString()); }
					} 

					GUILayout.BeginHorizontal();
					if (GUILayout.Button("Bind Event"))
		            {
						BindedEvent NewBindedEvent = SelectedClass.CreateAndBindEvent(SelectedTrigger, SelectedOverload, CurrentParams);
						//serializedObject.FindProperty("AllEvents").InsertArrayElementAtIndex(0);
						//serializedObject.FindProperty("AllEvents").GetArrayElementAtIndex(0).objectReferenceValue  = NewBindedEvent;
						((AMPEventBinder)target).AllEvents.Add(NewBindedEvent);
						CreatingEvent = false;
		            }
					if (GUILayout.Button("Cancel")) { CreatingEvent = false;  }
					GUILayout.EndHorizontal();
				}

				for (int i = 0; i < ((AMPEventBinder)target).AllEvents.Count; i++){ EditorGUILayout.LabelField((((AMPEventBinder)target).AllEvents[i].EventCallback != null).ToString()); }

	            serializedObject.ApplyModifiedProperties();
	            base.DrawDefaultInspector();
	        }
	    }
	}
}
