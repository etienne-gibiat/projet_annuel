using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGSauce.Core.GlobalVariables
{
    /// <summary>
    /// Base class for Global Variables. A scriptable Object.
    /// </summary>
    /// <typeparam name="T">Type of the variable (bool, int, etc.)</typeparam>
    public class IGlobalValue<T> : IGlobalValueScriptableObject
    {
        [SerializeField] private T value;

        /// <summary>
        /// The readonly value of the variable
        /// </summary>
        public T Value => value;

        /// <summary>
        /// It is possible to implicitly cast a GlobalVariable to its type like : myInt = myGlobalInt; But it DOES NOT WORK for GLOBAL BOOL !!! (Unity is at fault).
        /// Also it would be a compile time error, so very insidious.
        /// Use with caution.
        /// </summary>
        /// <param name="val">The global variable</param>
        /// <returns>The value of the GlobalVariables casted into the type T</returns>
        public static implicit operator T(IGlobalValue<T> val)
        {
            if (val is GlobalBool)
            {
                throw new ArgumentException($"Can't implicitly cast a GlobalBool to bool");
            }
            return val.Value;
        }
        
        /// <summary>
        /// If the global bool is named Is Dead and the value is true, then it returns "Is Dead = true"
        /// </summary>
        /// <returns>A nice formatted string for the value</returns>
        public override string ToString()
        {
            return $"{name} = {Value.ToString()}";
        }
    }
}

