using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Infrastructure.Pools;
using EndlessRunnerECS.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.Infrastructure.EntitySpawners
{
    public sealed class RoadBlockSpawnService : IEntitySpawnService
    {
        private readonly IPool<RoadBlockView> _pool;

        public RoadBlockSpawnService(IPool<RoadBlockView> pool) => _pool = pool;

        public int Spawn(EcsWorld world, Vector3 position)
        {
            RoadBlockView view = _pool.Get();
            view.transform.position = position;

            int entity = world.NewEntity();

            ref PositionComponent positionComponent = ref world.GetPool<PositionComponent>().Add(entity);
            positionComponent.Value = position;

            ref TransformViewComponent transformView = ref world.GetPool<TransformViewComponent>().Add(entity);
            transformView.Transform = view.transform;

            ref RoadBlockComponent roadBlock = ref world.GetPool<RoadBlockComponent>().Add(entity);
            roadBlock.Length = view.Length;

            ref RoadBlockViewComponent roadBlockView = ref world.GetPool<RoadBlockViewComponent>().Add(entity);
            roadBlockView.View = view;

            world.GetPool<WorldScrollableTag>().Add(entity);

            return entity;
        }
    }
}
