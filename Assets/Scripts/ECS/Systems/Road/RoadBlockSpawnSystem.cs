using EndlessRunnerECS.Data.Road;
using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Infrastructure.EntitySpawners;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class RoadBlockSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<RoadBlockComponent> _roadPool;
        private EcsPool<PositionComponent> _positionPool;

        private readonly RoadData _roadData;
        private readonly IEntitySpawnService _spawnService;

        public RoadBlockSpawnSystem(RoadData roadData, IEntitySpawnService spawnService)
        {
            _roadData = roadData;
            _spawnService = spawnService;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RoadBlockComponent>()
                .Inc<PositionComponent>()
                .End();
            _roadPool = _world.GetPool<RoadBlockComponent>();
            _positionPool = _world.GetPool<PositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            int count = 0;
            //NOTE: farthestEndZ определяет самую дальнюю точку дороги от игрока, считается по активным блокам,
            //что позволяет независеть от порядка entity в ECS фильтре
            float farthestEndZ = _roadData.StartZ;

            foreach (int entity in _filter)
            {
                ref RoadBlockComponent roadBlock = ref _roadPool.Get(entity);
                ref PositionComponent position = ref _positionPool.Get(entity);

                float endZ = position.Value.z + roadBlock.Length;

                if (endZ > farthestEndZ)
                {
                    farthestEndZ = endZ;
                }
                count++;
            }

            while (count < _roadData.InitialBlocksCount)
            {
                int entity = _spawnService.Spawn(_world, new Vector3(0f, 0f, farthestEndZ));

                ref RoadBlockComponent roadBlock = ref _roadPool.Get(entity);
                farthestEndZ += roadBlock.Length;

                count++;
            }
        }
    }
}
