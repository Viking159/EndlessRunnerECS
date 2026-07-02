using EndlessRunnerECS.Data.Player;
using EndlessRunnerECS.Data.Road;
using EndlessRunnerECS.Data.World;
using EndlessRunnerECS.ECS.Systems;
using EndlessRunnerECS.Infrastructure.EntitySpawners;
using EndlessRunnerECS.Infrastructure.Pools;
using EndlessRunnerECS.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.ECS.Startup
{
    public sealed class GameStartup : MonoBehaviour
    {
        [SerializeField] private RoadConfig _roadConfig;
        [SerializeField] private RoadBlockView[] _roadBlockPrefabs;
        [SerializeField] private Transform _roadBlocksParent;
        [SerializeField] private int _roadBlocksPreloadCount = 1;

        [SerializeField] private ScrollableView[] _sceneScrollableViews;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private WorldConfig _worldConfig;

        private EcsWorld _world;
        private EcsSystems _systems;

        private IObjectPool<RoadBlockView> _roadBlockPool;
        private IEntitySpawnService _roadBlockSpawnService;
        private RoadData _roadData;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _roadBlockPool = new RandomRoadBlockPool( _roadBlockPrefabs, _roadBlocksParent, _roadBlocksPreloadCount);
            _roadBlockSpawnService = new RoadBlockSpawnService(_roadBlockPool);
            _roadData = RoadDataMapper.Map(_roadConfig);
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
                .Add(new PlayerInitSystem(_playerTransform, PlayerDataMapper.Map(_playerConfig)))
                .Add(new SceneScrollableInitSystem(_sceneScrollableViews))
                .Add(new RoadInitSystem(_roadData, _roadBlockSpawnService))

                .Add(new PlayerInputSystem())
                .Add(new VehicleLaneInputSystem())
                .Add(new VehicleMovementSystem())

                .Add(new WorldScrollSystem(WorldDataMapper.Map(_worldConfig)))
                .Add(new RoadBlockRecycleSystem(_roadData, _roadBlockPool))
                .Add(new RoadBlockSpawnSystem(_roadData, _roadBlockSpawnService))

                .Add(new TransformSyncSystem());
    }
}
