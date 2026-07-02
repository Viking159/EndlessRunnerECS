using UnityEngine;

namespace EndlessRunnerECS.Data.World
{
    public static class WorldDataMapper
    {
        public const float MIN_VALUE = 0.1f;

        public static WorldData Map(WorldConfig config)
            => new WorldData
            (
                Mathf.Max(MIN_VALUE, config.ScrollSpeed)
            );
    }
}
