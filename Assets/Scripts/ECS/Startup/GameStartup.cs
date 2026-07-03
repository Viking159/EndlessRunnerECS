using EndlessRunnerECS.Data.Player;
using EndlessRunnerECS.Data.Road;
using EndlessRunnerECS.Data.RoadObject;
using EndlessRunnerECS.Data.World;
using EndlessRunnerECS.ECS.Systems;
using EndlessRunnerECS.ECS.Systems.HUD;
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

        [SerializeField] private RoadObjectConfig _roadObjectConfig;
        [SerializeField] private RoadObjectView _roadObjectPrefab;
        [SerializeField] private Transform _roadObjectsParent;
        [SerializeField] private int _roadObjectsPreloadCount = 1;

        [SerializeField] private ScoreHUDView _scoreView;

        private EcsWorld _world;
        private EcsSystems _systems;

        private IPool<RoadBlockView> _roadBlockPool;
        private IPool<RoadObjectView> _roadObjectPool;
        private IEntitySpawnService _roadBlockSpawnService;
        private IEntitySpawnService _roadObjectSpawnService;
        private RoadData _roadData;
        private WorldData _worldData;
        private RoadObjectData _roadObjectData;
        private PlayerData _playerData;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _roadData = RoadDataMapper.Map(_roadConfig);
            _worldData = WorldDataMapper.Map(_worldConfig);
            _roadObjectData = RoadObjectDataMapper.Map(_roadObjectConfig);
            _playerData = PlayerDataMapper.Map(_playerConfig);

            _roadBlockPool = new RandomRoadBlockPool( _roadBlockPrefabs, _roadBlocksParent, _roadBlocksPreloadCount);
            _roadBlockSpawnService = new RoadBlockSpawnService(_roadBlockPool);

            _roadObjectPool = new ViewPool<RoadObjectView>(_roadObjectPrefab, _roadObjectsParent, _roadObjectsPreloadCount);
            _roadObjectSpawnService = new RoadObjectSpawnService(_roadObjectPool, _roadObjectData);

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

        //NOTE: Сначала обновляем инпут и движение, затем двигаем мир, после обрабатываем release/spawn,
        //сбор, обновляем HUD, в конце синхронизируем трансформы
        private void RegisterSystems() 
            => _systems
                .Add(new ScoreInitSystem())
                .Add(new PlayerInitSystem(_playerTransform, _playerData, _worldData))
                .Add(new SceneScrollableInitSystem(_sceneScrollableViews))
                .Add(new RoadInitSystem(_roadData, _roadBlockSpawnService))
                .Add(new RoadObjectSpawnTimerInitSystem(_roadObjectData))

                .Add(new PlayerInputSystem())
                .Add(new VehicleLaneInputSystem())
                .Add(new VehicleMovementSystem())

                .Add(new WorldScrollSystem(_worldData))
                .Add(new RoadBlockRecycleSystem(_roadData, _roadBlockPool))
                .Add(new RoadBlockSpawnSystem(_roadData, _roadBlockSpawnService))
                .Add(new RoadObjectSpawnSystem(_roadObjectData, _roadObjectSpawnService, _playerData.LaneWidth, _worldData))

                .Add(new RoadObjectCollectSystem())
                .Add(new RoadObjectCollectedSystem(_roadObjectPool))

                .Add(new RoadObjectRecycleSystem(_roadObjectData, _roadObjectPool))
                .Add(new ScoreHUDUpdateSystem(_scoreView))
                .Add(new TransformSyncSystem());
    }
}
