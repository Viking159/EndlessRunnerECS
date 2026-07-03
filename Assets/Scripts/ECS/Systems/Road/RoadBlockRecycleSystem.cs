using EndlessRunnerECS.Data.Road;
using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Infrastructure.Pools;
using EndlessRunnerECS.Views;
using Leopotam.EcsLite;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class RoadBlockRecycleSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<RoadBlockComponent> _roadPool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<RoadBlockViewComponent> _viewPool;

        private readonly RoadData _roadData;
        private readonly IPool<RoadBlockView> _pool;

        public RoadBlockRecycleSystem(RoadData roadData, IPool<RoadBlockView> pool)
        {
            _roadData = roadData;
            _pool = pool;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RoadBlockComponent>()
                .Inc<PositionComponent>()
                .Inc<RoadBlockViewComponent>()
                .End();
            _positionPool = _world.GetPool<PositionComponent>();
            _roadPool = _world.GetPool<RoadBlockComponent>();
            _viewPool = _world.GetPool<RoadBlockViewComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref PositionComponent position = ref _positionPool.Get(entity);
                ref RoadBlockComponent roadBlock = ref _roadPool.Get(entity);

                float endZ = position.Value.z + roadBlock.Length;

                if (endZ > _roadData.RecycleZ)
                {
                    continue;
                }

                ref RoadBlockViewComponent view = ref _viewPool.Get(entity);

                _pool.Release(view.View);
                _world.DelEntity(entity);
            }
        }
    }
}
