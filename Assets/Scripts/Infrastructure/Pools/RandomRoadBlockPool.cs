using EndlessRunnerECS.Views;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerECS.Infrastructure.Pools
{
    /// <summary>
    /// Управляет получением случайного блока дороги
    /// </summary>
    /// <remarks>
    /// Каждый префаб (тип дороги) имеет свой пул, что позволяет расширять разнообразие
    /// дорог, но с сохранением работы через пул
    /// </remarks>
    public sealed class RandomRoadBlockPool : IPool<RoadBlockView>
    {
        private readonly IPool<RoadBlockView>[] _pools;

        /// <summary>
        /// Словарь привязывает RoadBlockView с пулом, из которого его взяли
        /// для корректного релиза объекта
        /// </summary>
        private readonly Dictionary<RoadBlockView, IPool<RoadBlockView>> _instancePools = new();

        public RandomRoadBlockPool(RoadBlockView[] prefabs, Transform parent, int preloadCountPerPrefab)
        {
            _pools = new ViewPool<RoadBlockView>[prefabs.Length];
            for (int i = 0; i < prefabs.Length; i++)
            {
                _pools[i] = new ViewPool<RoadBlockView>(prefabs[i], parent, preloadCountPerPrefab);
            }
        }

        public RoadBlockView Get()
        {
            int poolIndex = Random.Range(0, _pools.Length);
            RoadBlockView instance = _pools[poolIndex].Get();
            _instancePools[instance] = _pools[poolIndex];
            return instance;
        }

        public void Release(RoadBlockView instance)
        {
            if (!_instancePools.TryGetValue(instance, out IPool<RoadBlockView> pool))
            {
                Debug.LogWarning($"[{nameof(RandomRoadBlockPool)}]: Unable to find road block '{instance.name}' in pools.");
                instance.gameObject.SetActive(false);
                return;
            }

            _instancePools.Remove(instance);
            pool.Release(instance);
        }
    }
}
