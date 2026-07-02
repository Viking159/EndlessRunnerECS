using EndlessRunnerECS.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class VehicleMovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<LaneMovementComponent> _lanePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<PlayerTag>()
                .Inc<PositionComponent>()
                .Inc<LaneMovementComponent>()
                .End();

            _positionPool = _world.GetPool<PositionComponent>();
            _lanePool = _world.GetPool<LaneMovementComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            foreach (int entity in _filter)
            {
                ref PositionComponent position = ref _positionPool.Get(entity);
                ref LaneMovementComponent lane = ref _lanePool.Get(entity);

                float targetX = lane.TargetLane * lane.LaneWidth;
                position.Value.x = Mathf.MoveTowards(position.Value.x, targetX, lane.LaneChangeSpeed * deltaTime);

                if (Mathf.Approximately(position.Value.x, targetX))
                {
                    lane.CurrentLane = lane.TargetLane;
                }
            }
        }
    }
}
