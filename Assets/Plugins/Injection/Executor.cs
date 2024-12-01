using System.Collections.Generic;
using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Plugins.Injection
{
    public class Executor : MonoBehaviour
    {
        private List<IAwake> _awakeInstances;
        private List<IUpdate> _updateInstances;
        private List<IFixedUpdate> _fixedUpdateInstances;
        private List<ILateUpdate> _lateUpdateInstances;

        public static Executor Instance;
        
        private void Awake()
        {
            Instance = this;
            
            _awakeInstances = new List<IAwake>();
            _updateInstances = new List<IUpdate>();
            _fixedUpdateInstances = new List<IFixedUpdate>();
            _lateUpdateInstances = new List<ILateUpdate>();
        }

        private void Start()
        {
            foreach (var item in _awakeInstances)
            {
                item.OnAwake();
            }
        }

        private void Update()
        {
            foreach (var item in _updateInstances)
            {
                item.OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            foreach (var item in _fixedUpdateInstances)
            {
                item.OnFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            foreach (var item in _lateUpdateInstances)
            {
                item.OnLateUpdate();
            }
        }

        public void AddAwake(IAwake instance)
        {
            _awakeInstances.Add(instance);
        }

        public void AddUpdate(IUpdate instance)
        {
            _updateInstances.Add(instance);
        }

        public void AddFixedUpdate(IFixedUpdate instance)
        {
            _fixedUpdateInstances.Add(instance);
        }

        public void AddLateUpdate(ILateUpdate instance)
        {
            _lateUpdateInstances.Add(instance);
        }
    }
}