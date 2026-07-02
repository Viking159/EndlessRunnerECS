using EndlessRunnerECS.Views;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerECS.Infrastructure.Pools
{
    public sealed class RandomRoadBlockPool : IObjectPool<RoadBlockView>
    {
        private readonly IObjectPool<RoadBlockView>[] _pools;
        private readonly Dictionary<RoadBlockView, IObjectPool<RoadBlockView>> _instancePools = new();

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
            if (!_instancePools.TryGetValue(instance, out IObjectPool<RoadBlockView> pool))
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
