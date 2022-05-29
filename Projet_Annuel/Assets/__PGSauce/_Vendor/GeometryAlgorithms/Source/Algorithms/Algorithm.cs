using System;

namespace Jobberwocky.GeometryAlgorithms.GeometryAlgorithms.Source.Algorithms
{
    public abstract class Algorithm<T> where T : class
    {
        private static readonly object _object = new object();

        private static T _instance = null;

        public static T Instance {
            get {
                lock (_object) {
                    if (_instance == null) {
                        _instance = CreateInstanceOfT();
                    }

                    return _instance;
                }
            }
        }
        
        private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }
    }
}