using EndlessRunnerECS.Data.RoadObject;
using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Infrastructure.Pools;
using EndlessRunnerECS.Views;
using Leopotam.EcsLite;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class RoadObjectRecycleSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<RoadObjectViewComponent> _viewPool;

        private readonly RoadObjectData _data;
        private readonly IPool<RoadObjectView> _pool;

        public RoadObjectRecycleSystem(RoadObjectData data, IPool<RoadObjectView> pool)
        {
            _data = data;
            _pool = pool;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RoadObjectComponent>()
                .Inc<PositionComponent>()
                .Inc<RoadObjectViewComponent>()
                .End();
            _positionPool = _world.GetPool<PositionComponent>();
            _viewPool = _world.GetPool<RoadObjectViewComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref PositionComponent position = ref _positionPool.Get(entity);

                if (position.Value.z > _data.RecycleZ)
                {
                    continue;
                }

                ref RoadObjectViewComponent viewComponent = ref _viewPool.Get(entity);

                _pool.Release(viewComponent.View);
                _world.DelEntity(entity);
            }
        }
    }
}
