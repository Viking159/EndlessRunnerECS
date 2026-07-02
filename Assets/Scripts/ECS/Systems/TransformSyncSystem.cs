using EndlessRunnerECS.ECS.Components;
using Leopotam.EcsLite;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class TransformSyncSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<TransformViewComponent> _viewPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<PositionComponent>()
                .Inc<TransformViewComponent>()
                .End();

            _positionPool = _world.GetPool<PositionComponent>();
            _viewPool = _world.GetPool<TransformViewComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref PositionComponent position = ref _positionPool.Get(entity);
                ref TransformViewComponent view = ref _viewPool.Get(entity);

                view.Transform.position = position.Value;
            }
        }
    }
}
