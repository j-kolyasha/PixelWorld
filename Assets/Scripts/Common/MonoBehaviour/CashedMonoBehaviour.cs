using System.Collections.Generic;
using UnityEngine;

namespace Common.MonoBehaviour
{
    public class CashedMonoBehaviour : UnityEngine.MonoBehaviour
    {
        private List<Component> _cashedComponents = new List<Component>();
        private Transform _transform;
        private GameObject _gameObject;

        public new Transform transform
        {
            get
            {
                if (_transform == null)
                {
                    _transform = base.transform;
                }   

                return _transform;
            }
        }

        public new GameObject gameObject
        {
            get
            {
                if (_gameObject == null)
                {
                    _gameObject = base.gameObject;
                }
                
                return _gameObject;
            }
        }

        public new T GetComponent<T>() where T : Component
        {
            T component = _cashedComponents.Find(c => c is T) as T;
            if (component == null)
            {
                component = base.GetComponent<T>();
                _cashedComponents.Add(component);
            }

            return component;
        }

        private void Awake()
        {
            InheritAwake();
        }

        protected virtual void InheritAwake() { }
        
        private void Start()
        {
            InheritStart();
        }

        protected virtual void InheritStart() { }

        private void OnEnable()
        {
            InheritOnEnable();
        }

        protected virtual void InheritOnEnable() { }
        
        private void OnDestroy()
        {
            InheritOnDestroy();
        }

        protected virtual void InheritOnDestroy() { }
    }
}