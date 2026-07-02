using UnityEngine;

namespace EndlessRunnerECS.Data.Road
{
    public static class RoadDataMapper
    {
        public const int MIN_INITIAL_BLOCKS_COUNT = 2;

        public static RoadData Map(RoadConfig config) 
            => new RoadData
            (
                Mathf.Max(MIN_INITIAL_BLOCKS_COUNT, config.InitialBlocksCount),
                config.RecycleZ,
                config.StartZ
            );
    }
}
