using EndlessRunnerECS.Data.Road;
using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Infrastructure.EntitySpawners;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class RoadInitSystem : IEcsInitSystem
    {
        private readonly RoadData _roadData;
        private readonly IEntitySpawnService _spawnService;

        public RoadInitSystem(RoadData roadData, IEntitySpawnService spawnService)
        {
            _roadData = roadData;
            _spawnService = spawnService;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            float nextZ = _roadData.StartZ;

            for (int i = 0; i < _roadData.InitialBlocksCount; i++)
            {
                int entity = _spawnService.Spawn(world, new Vector3(0f, 0f, nextZ));

                ref RoadBlockComponent roadBlock = ref world.GetPool<RoadBlockComponent>().Get(entity);
                nextZ += roadBlock.Length;
            }
        }
    }
}
