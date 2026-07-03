using EndlessRunnerECS.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class RoadObjectCollectSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _roadObjectFilter;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<CollectableComponent> _collectablePool;
        private EcsPool<CollectRequestComponent> _collectRequestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<PlayerTag>()
                .Inc<PositionComponent>()
                .End();

            _roadObjectFilter = _world.Filter<RoadObjectComponent>()
                .Inc<PositionComponent>()
                .Inc<CollectableComponent>()
                .Exc<CollectRequestComponent>()
                .End();

            _positionPool = _world.GetPool<PositionComponent>();
            _collectablePool = _world.GetPool<CollectableComponent>();
            _collectRequestPool = _world.GetPool<CollectRequestComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int playerEntity in _playerFilter)
            {
                ref PositionComponent playerPosition = ref _positionPool.Get(playerEntity);

                foreach (int roadObjectEntity in _roadObjectFilter)
                {
                    ref PositionComponent objectPosition = ref _positionPool.Get(roadObjectEntity);
                    ref CollectableComponent collectable = ref _collectablePool.Get(roadObjectEntity);

                    if (!QuickPositionCrossCheck(playerPosition.Value, objectPosition.Value, collectable.RadiusX, collectable.RadiusZ))
                    {
                        continue;
                    }
                    _collectRequestPool.Add(roadObjectEntity);
                }
                return;
            }
        }

        /// <summary>
        /// Быстрое определение пересечения по X-Z, без использования тяжелых функций и физики
        /// </summary>
        private bool QuickPositionCrossCheck(Vector3 v1, Vector3 v2, float radiusX, float radiusZ)
            => Mathf.Abs(v1.x - v2.x) <= radiusX && Mathf.Abs(v1.z - v2.z) <= radiusZ;
    }
}
