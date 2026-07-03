using EndlessRunnerECS.ECS.Components;
using EndlessRunnerECS.Views;
using Leopotam.EcsLite;

namespace EndlessRunnerECS.ECS.Systems.HUD
{
    public sealed class ScoreHUDUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<ScoreComponent> _scorePool;
        private int _lastScore = -1;

        private readonly ScoreHUDView _scoreView;

        public ScoreHUDUpdateSystem(ScoreHUDView scoreView) => _scoreView = scoreView;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ScoreComponent>().End();
            _scorePool = _world.GetPool<ScoreComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref ScoreComponent score = ref _scorePool.Get(entity);
                if (score.Value != _lastScore)
                {
                    _lastScore = score.Value;
                    _scoreView.UpdateScoreText(_lastScore);
                }
                return;
            }
        }
    }
}
