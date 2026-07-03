using EndlessRunnerECS.Data.RoadObject;
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
        private readonly RoadObjectData _data;

        public RoadObjectSpawnService(IPool<RoadObjectView> pool, RoadObjectData roadObjectData)
        {
            _pool = pool;
            _data = roadObjectData;
        }

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

            ref CollectableComponent collectable = ref world.GetPool<CollectableComponent>().Add(entity);
            collectable.RadiusX = _data.TriggerRadiusX;
            collectable.RadiusZ = _data.TriggerRadiusZ;

            world.GetPool<RoadObjectComponent>().Add(entity);
            world.GetPool<WorldScrollableTag>().Add(entity);

            return entity;
        }
    }
}
