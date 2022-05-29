using System;
using System.Globalization;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LeTai.TrueShadow.Editor
{
public class EditorProperty
{
    public readonly SerializedProperty serializedProperty;

    readonly SerializedObject   serializedObject;
    readonly PropertyInfo       property;
    readonly SerializedProperty dirtyFlag;

    public EditorProperty(SerializedObject obj, string name)
    {
        var propertyName = char.ToLower(name[0], CultureInfo.InvariantCulture) + name.Substring(1);

        serializedObject   = obj;
        serializedProperty = serializedObject.FindProperty(propertyName);
        property           = serializedObject.targetObject.GetType().GetProperty(name);

        if (serializedProperty == null)
        {
            var it       = serializedObject.GetIterator().Copy();
            var allProps = "";
            while (it.Next(true))
            {
                allProps += it.name + "\n";
            }

            Debug.LogError($"True Shadow error! Please Report: Missing\t {propertyName} from:\n{allProps}");
        }

        dirtyFlag = serializedObject.FindProperty("modifiedFromInspector");
    }

    public void Draw()
    {
        using (var scope = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.PropertyField(serializedProperty);


            if (scope.changed)
            {
                dirtyFlag.boolValue = true;
                serializedObject.ApplyModifiedProperties();

                foreach (var target in serializedObject.targetObjects)
                {
                    switch (serializedProperty.propertyType)
                    {
                    case SerializedPropertyType.Float:
                        property.SetMethod.Invoke(target, new object[] {serializedProperty.floatValue});
                        break;
                    case SerializedPropertyType.Enum:
                        property.SetMethod.Invoke(target, new object[] {serializedProperty.enumValueIndex});
                        break;
                    case SerializedPropertyType.Boolean:
                        property.SetMethod.Invoke(target, new object[] {serializedProperty.boolValue});
                        break;
                    case SerializedPropertyType.Color:
                        property.SetMethod.Invoke(target, new object[] {serializedProperty.colorValue});
                        break;
                    default: throw new NotImplementedException();
                    }
                }
            }
        }
    }
}
}
