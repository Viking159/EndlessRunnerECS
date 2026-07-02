using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerECS.Infrastructure.Pools
{
    public sealed class ViewPool<T> : IObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Queue<T> _instances = new Queue<T>();

        public ViewPool(T prefab, Transform parent, int preloadCount)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < preloadCount; i++)
            {
                T instance = CreateInstance();
                Release(instance);
            }
        }

        public T Get()
        {
            T instance = _instances.Count > 0
                ? _instances.Dequeue()
                : CreateInstance();

            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Release(T instance)
        {
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(_parent);
            _instances.Enqueue(instance);
        }

        private T CreateInstance() => Object.Instantiate(_prefab, _parent);
    }
}
