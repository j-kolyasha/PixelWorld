using System;
using UnityEngine;

namespace Common.MonoBehaviour
{
    public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] assets = Resources.FindObjectsOfTypeAll<T>();

                    if (assets == null || assets.Length == 0)
                    {
                        throw new ArgumentNullException();
                    }

                    if (assets.Length > 1)
                    {
                        throw new ArgumentException();
                    }

                    _instance = assets[0];
                }
                
                return _instance;
            }
        }
    }
}