using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PGSauce.Core.PGEditor
{
    public class EventGenerationAction : OdinWindowBaseCodeGenerationAction<EventCodeGeneratorOdinWindow>
    {
        public EventGenerationAction(EventCodeGeneratorOdinWindow window) : base(window)
        {
        }

        protected override void GenerateAction()
        {
            Window.GenerateEvent();
        }

        protected override (bool isOK, string errorMessage) VerifyCriticalData()
        {
            return Window.VerifyCriticalData();
        }
    }
}
