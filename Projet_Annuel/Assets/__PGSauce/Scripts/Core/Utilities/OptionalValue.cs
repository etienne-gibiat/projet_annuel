using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Core.Utilities
{
    [Serializable]
    public class OptionalValue<T>
    {
        [Tooltip("Check this to set a value for this instance")]
        [SerializeField]
        private bool useValue;

        [Tooltip("The value of this instance")]
        [SerializeField, ShowIf("UseValue")]
        private T value;

        /// <summary>
        /// Gets or sets whether to use the value of this instance.
        /// </summary>
        /// <value><c>true</c> if this value should be used; otherwise, <c>false</c>.</value>
        public bool UseValue
        {
        	get => useValue;
            set => useValue = value;
        }

        /// <summary>
        /// The value of this instance. It should only be used if UseValue is true. Otherwise, some
        /// other value should be used, or code that does not need it must be executed instead.
        /// </summary>
        /// <value>The value of this Optional instance.</value>
        /// <example>This shows a typical example of how to use this class.
        /// <code>
        /// if (optionalMaterial.UseValue)
        /// {
        ///		renderer.material = material;
        /// } //else do not modify the material.
        /// </code></example>
        public T Value
        {
        	get => value;
            set => this.value = value;
        }

        public override string ToString()
        {
            return UseValue ? Value.ToString() : "[No Value]";
        }
    }
}