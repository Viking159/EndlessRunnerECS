using EndlessRunnerECS.Data.RoadObject;
using EndlessRunnerECS.ECS.Components;
using Leopotam.EcsLite;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class RoadObjectSpawnTimerInitSystem : IEcsInitSystem
    {
        private readonly RoadObjectData _data;

        public RoadObjectSpawnTimerInitSystem(RoadObjectData data) 
            => _data = data;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            int entity = world.NewEntity();
            ref RoadObjectSpawnTimerComponent timer = ref world.GetPool<RoadObjectSpawnTimerComponent>().Add(entity);
            timer.TimeLeft = _data.SpawnInterval;
        }
    }
}
