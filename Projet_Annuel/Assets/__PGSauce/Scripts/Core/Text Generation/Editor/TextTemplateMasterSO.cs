using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;

namespace PGSauce.Core
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Text Template/Text Template Master Singleton")]
    public class TextTemplateMasterSO : SOSingleton<TextTemplateMasterSO>
    {
        [SerializeField] private TextTemplateSO interfaceTemplate;
        [SerializeField] private TextTemplateSO textGeneratorTemplate;
        [SerializeField] private TemplatesData templates;

        [SerializeField, ReadOnly] private string lastInsidePGSauceSubNameSpace;

        public TextTemplateSO TextGeneratorTemplate => textGeneratorTemplate;

        public TextTemplateSO InterfaceTemplate => interfaceTemplate;

        public TemplatesData Templates
        {
            get => templates;
        }

        public string LastInsidePgSauceSubNameSpace
        {
            get => lastInsidePGSauceSubNameSpace;
            set => lastInsidePGSauceSubNameSpace = value;
        }


        [Serializable]
        public struct TemplatesData
        {
            public TextTemplateSO unityEvent;
            public TextTemplateSO pgEvent;
            public TextTemplateSO pgEventListener;
            public TextTemplateSO globalVariable;
            public TextTemplateSO fsmAction;
            public TextTemplateSO fsmDecision;
            public TextTemplateSO fsmState;
            public TextTemplateSO fsmStateController;
            public TextTemplateSO fsmAny;
            public TextTemplateSO strings;
        }
    }
}
