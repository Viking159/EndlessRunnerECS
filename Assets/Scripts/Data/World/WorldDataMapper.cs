using UnityEngine;

namespace EndlessRunnerECS.Data.World
{
    public static class WorldDataMapper
    {
        public const float MIN_VALUE = 0.1f;

        public static WorldData Map(WorldConfig config)
        {
            int minLaneIndex = config.MinLaneIndex;
            int maxLaneIndex = config.MaxLaneIndex;
            if (minLaneIndex > maxLaneIndex)
            {
                int temp = maxLaneIndex;
                maxLaneIndex = minLaneIndex;
                minLaneIndex = temp;
            }
            return new WorldData
                    (
                        Mathf.Max(MIN_VALUE, config.ScrollSpeed),
                        minLaneIndex,
                        maxLaneIndex
                    );
        }
    }
}
