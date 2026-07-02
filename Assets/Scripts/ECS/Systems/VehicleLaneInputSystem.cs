using EndlessRunnerECS.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class VehicleLaneInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<InputComponent> _inputPool;
        private EcsPool<LaneMovementComponent> _lanePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PlayerTag>()
                .Inc<InputComponent>()
                .Inc<LaneMovementComponent>()
                .End();
            _inputPool = _world.GetPool<InputComponent>();
            _lanePool = _world.GetPool<LaneMovementComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref InputComponent input = ref _inputPool.Get(entity);
                ref LaneMovementComponent lane = ref _lanePool.Get(entity);

                if (input.HorizontalDirection == 0)
                {
                    continue;
                }

                int nextLane = lane.TargetLane + input.HorizontalDirection;
                lane.TargetLane = Mathf.Clamp(nextLane, lane.MinLane, lane.MaxLane);
            }
        }
    }
}
