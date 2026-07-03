using UnityEngine;

namespace EndlessRunnerECS.Data.RoadObject
{
    public static class RoadObjectDataMapper
    {
        public const float MIN_SPAWN_INTERVAL = 0.1f;
        public const float MIN_SPAWN_DISTANCE = 1f;

        public static RoadObjectData Map(RoadObjectConfig config)
        {
            return new RoadObjectData
            (
                Mathf.Max(MIN_SPAWN_INTERVAL, config.SpawnInterval),
                Mathf.Clamp01(config.SpawnChance),
                Mathf.Max(MIN_SPAWN_DISTANCE, config.SpawnDistanceZ),
                config.RecycleZ
            );
        }
    }
}
