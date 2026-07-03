using EndlessRunnerECS.ECS.Components;
using Leopotam.EcsLite;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class ScoreInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            int entity = world.NewEntity();
            ref ScoreComponent score = ref world.GetPool<ScoreComponent>().Add(entity);
            score.Value = 0;
        }
    }
}
