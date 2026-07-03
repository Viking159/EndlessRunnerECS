using EndlessRunnerECS.Data.World;
using EndlessRunnerECS.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Systems
{
    /// <summary>
    /// Управляет смещением объектов в сторону игрока
    /// </summary>
    /// <remarks>
    /// Игрок остается у координат (0, 0), иммитация движения игрока вперед производится за счет
    /// движения мира к игроку. Реализовано для исключения возможных ошибок
    /// при больших значениях координат.
    /// </remarks>
    public sealed class WorldScrollSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<PositionComponent> _positionPool;

        private readonly WorldData _worldData;

        public WorldScrollSystem(WorldData worldData) => _worldData = worldData;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<WorldScrollableTag>()
                .Inc<PositionComponent>()
                .End();
            _positionPool = _world.GetPool<PositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            float offset = _worldData.ScrollSpeed * Time.deltaTime;
            foreach (int entity in _filter)
            {
                ref PositionComponent position = ref _positionPool.Get(entity);
                position.Value.z -= offset;
            }
        }
    }
}
