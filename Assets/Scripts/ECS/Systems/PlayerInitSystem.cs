using EndlessRunnerECS.Data.Player;
using EndlessRunnerECS.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Systems
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        private readonly Transform _playerTransform;
        private readonly PlayerData _playerData;

        public PlayerInitSystem(Transform playerTransform, PlayerData playerData)
        {
            _playerTransform = playerTransform;
            _playerData = playerData;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            int entity = world.NewEntity();

            world.GetPool<PlayerTag>().Add(entity);

            ref PositionComponent position = ref world.GetPool<PositionComponent>().Add(entity);
            position.Value = _playerTransform.position;

            ref LaneMovementComponent laneMovement = ref world.GetPool<LaneMovementComponent>().Add(entity);
            InitLaneMovement(ref laneMovement);

            world.GetPool<InputComponent>().Add(entity);

            ref TransformViewComponent view = ref world.GetPool<TransformViewComponent>().Add(entity);
            view.Transform = _playerTransform;
        }

        private void InitLaneMovement(ref LaneMovementComponent laneMovement)
        {
            laneMovement.CurrentLane = 0;
            laneMovement.TargetLane = 0;
            laneMovement.MinLane = -1;
            laneMovement.MaxLane = 1;
            laneMovement.LaneWidth = _playerData.LaneWidth;
            laneMovement.LaneChangeSpeed = _playerData.LaneChangeSpeed;
        }
    }
}
