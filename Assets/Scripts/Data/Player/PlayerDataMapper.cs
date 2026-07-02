using UnityEngine;

namespace EndlessRunnerECS.Data.Player
{
    public static class PlayerDataMapper
    {
        public const float MIN_VALUE = 0.1f;

        public static PlayerData Map(PlayerConfig config)
            => new PlayerData
            (
                Mathf.Max(MIN_VALUE, config.LaneChangeSpeed),
                Mathf.Max(MIN_VALUE, config.LaneWidth)
            );
    }
}
