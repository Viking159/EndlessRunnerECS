namespace EndlessRunnerECS.Data.RoadObject
{
    public readonly struct RoadObjectData
    {
        public readonly float SpawnInterval;
        public readonly float SpawnChance;
        public readonly float SpawnDistanceZ;
        public readonly float RecycleZ;

        public RoadObjectData(float spawnInterval, float spawnChance, float spawnDistanceZ, float recycleZ)
        {
            SpawnInterval = spawnInterval;
            SpawnChance = spawnChance;
            SpawnDistanceZ = spawnDistanceZ;
            RecycleZ = recycleZ;
        }
    }
}
