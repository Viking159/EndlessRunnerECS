using EndlessRunnerECS.Data.Player;
using EndlessRunnerECS.Data.World;
using EndlessRunnerECS.ECS.Systems;
using EndlessRunnerECS.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Startup
{
    public sealed class GameStartup : MonoBehaviour
    {
        [SerializeField] private ScrollableView[] _sceneScrollableViews;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private WorldConfig _worldConfig;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            RegisterSystems();
        }

        private void Start() => _systems.Init();

        private void Update() => _systems?.Run();

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;

            _world?.Destroy();
            _world = null;
        }

        private void RegisterSystems() 
            => _systems
                .Add(new SceneScrollableInitSystem(_sceneScrollableViews))
                .Add(new PlayerInitSystem(_playerTransform, PlayerDataMapper.Map(_playerConfig)))
                .Add(new PlayerInputSystem())
                .Add(new VehicleLaneInputSystem())
                .Add(new VehicleMovementSystem())
                .Add(new WorldScrollSystem(WorldDataMapper.Map(_worldConfig)))
                .Add(new TransformSyncSystem());
    }
}
