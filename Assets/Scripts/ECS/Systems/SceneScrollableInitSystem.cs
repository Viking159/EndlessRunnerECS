using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Views;
using Leopotam.EcsLite;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class SceneScrollableInitSystem : IEcsInitSystem
    {
        private readonly ScrollableView[] _scrollableViews;

        public SceneScrollableInitSystem(ScrollableView[] scrollableViews) => _scrollableViews = scrollableViews;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            foreach (ScrollableView scrollableView in _scrollableViews)
            {
                int entity = world.NewEntity();

                ref PositionComponent position = ref world.GetPool<PositionComponent>().Add(entity);
                position.Value = scrollableView.transform.position;

                ref TransformViewComponent transformView = ref world.GetPool<TransformViewComponent>().Add(entity);
                transformView.Transform = scrollableView.transform;

                world.GetPool<WorldScrollableTag>().Add(entity);
            }
        }
    }
}
