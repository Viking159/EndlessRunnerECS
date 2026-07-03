using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Infrastructure.Pools;
using EndlessRunnerECS.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.Infrastructure.EntitySpawners
{
    public sealed class RoadObjectSpawnService : IEntitySpawnService
    {
        private readonly IPool<RoadObjectView> _pool;

        public RoadObjectSpawnService(IPool<RoadObjectView> pool) 
            => _pool = pool;

        public int Spawn(EcsWorld world, Vector3 position)
        {
            RoadObjectView view = _pool.Get();
            view.transform.position = position;

            int entity = world.NewEntity();

            ref PositionComponent positionComponent = ref world.GetPool<PositionComponent>().Add(entity);
            positionComponent.Value = position;

            ref TransformViewComponent transformView = ref world.GetPool<TransformViewComponent>().Add(entity);
            transformView.Transform = view.transform;

            ref RoadObjectViewComponent viewComponent = ref world.GetPool<RoadObjectViewComponent>().Add(entity);
            viewComponent.View = view;

            world.GetPool<RoadObjectComponent>().Add(entity);
            world.GetPool<WorldScrollableTag>().Add(entity);

            return entity;
        }
    }
}
