namespace EndlessRunnerECS.Data.Road
{
    public readonly struct RoadData
    {
        public readonly int InitialBlocksCount;
        public readonly float RecycleZ;
        public readonly float StartZ;

        public RoadData(int initialBlocksCount, float recycleZ, float startZ)
        {
            InitialBlocksCount = initialBlocksCount;
            RecycleZ = recycleZ;
            StartZ = startZ;
        }
    }
}
