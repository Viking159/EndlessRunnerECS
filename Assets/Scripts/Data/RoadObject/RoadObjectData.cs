namespace EndlessRunnerECS.Data.RoadObject
{
    public readonly struct RoadObjectData
    {
        public readonly float SpawnInterval;
        public readonly float SpawnChance;
        public readonly float SpawnDistanceZ;
        public readonly float RecycleZ;
        public readonly float TriggerRadiusX;
        public readonly float TriggerRadiusZ;

        public RoadObjectData(float spawnInterval, float spawnChance, float spawnDistanceZ, float recycleZ,
            float triggerRadiusX, float triggerRadiusZ)
        {
            SpawnInterval = spawnInterval;
            SpawnChance = spawnChance;
            SpawnDistanceZ = spawnDistanceZ;
            RecycleZ = recycleZ;
            TriggerRadiusX = triggerRadiusX;
            TriggerRadiusZ = triggerRadiusZ;
        }
    }
}
