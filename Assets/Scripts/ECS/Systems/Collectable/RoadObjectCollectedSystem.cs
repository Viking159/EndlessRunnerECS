using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Infrastructure.Pools;
using EndlessRunnerECS.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class RoadObjectCollectedSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _collectedObjectsFilter;
        private EcsFilter _scoreFilter;
        private EcsPool<RoadObjectViewComponent> _viewPool;
        private EcsPool<ScoreComponent> _scorePool;

        private readonly IPool<RoadObjectView> _pool;

        public RoadObjectCollectedSystem(IPool<RoadObjectView> pool) 
            => _pool = pool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _collectedObjectsFilter = _world.Filter<RoadObjectComponent>()
                .Inc<RoadObjectViewComponent>()
                .Inc<CollectRequestComponent>()
                .End();

            _scoreFilter = _world.Filter<ScoreComponent>().End();

            _viewPool = _world.GetPool<RoadObjectViewComponent>();
            _scorePool = _world.GetPool<ScoreComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int collectedEntity in _collectedObjectsFilter)
            {
                foreach (int scoreEntity in _scoreFilter)
                {
                    ref ScoreComponent score = ref _scorePool.Get(scoreEntity);
                    score.Value++;
                    Debug.Log($"Score updated: {score.Value}");
                    break;
                }

                ref RoadObjectViewComponent view = ref _viewPool.Get(collectedEntity);

                _pool.Release(view.View);
                _world.DelEntity(collectedEntity);
            }
        }
    }
}
