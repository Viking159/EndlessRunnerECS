using EndlessRunnerECS.Data.RoadObject;
using EndlessRunnerECS.Data.World;
using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Infrastructure.EntitySpawners;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class RoadObjectSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float SPAWN_POINT_Y = 0.5f;

        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<RoadObjectSpawnTimerComponent> _timerPool;

        private readonly RoadObjectData _data;
        private readonly IEntitySpawnService _spawnService;
        private readonly float _laneWidth;
        private readonly WorldData _worldData;

        public RoadObjectSpawnSystem(RoadObjectData data, IEntitySpawnService spawnService, float laneWidth, WorldData worldData)
        {
            _data = data;
            _spawnService = spawnService;
            _laneWidth = laneWidth;
            _worldData = worldData;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RoadObjectSpawnTimerComponent>().End();
            _timerPool = _world.GetPool<RoadObjectSpawnTimerComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            if (!TickTimer() || Random.value > _data.SpawnChance)
            {
                return;
            }

            int lane = Random.Range(_worldData.MinLaneIndex, _worldData.MaxLaneIndex + 1);
            Vector3 spawnPosition = new Vector3(lane * _laneWidth, SPAWN_POINT_Y, _data.SpawnDistanceZ);
            _spawnService.Spawn(_world, spawnPosition);
        }

        private bool TickTimer()
        {
            foreach (int entity in _filter)
            {
                ref RoadObjectSpawnTimerComponent timer = ref _timerPool.Get(entity);

                timer.TimeLeft -= Time.deltaTime;

                if (timer.TimeLeft > 0f)
                {
                    return false;
                }

                timer.TimeLeft = _data.SpawnInterval;
                return true;
            }

            return false;
        }
    }
}
