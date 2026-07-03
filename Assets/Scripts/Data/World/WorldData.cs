namespace EndlessRunnerECS.Data.World
{
    public readonly struct WorldData
    {
        public readonly float ScrollSpeed;
        public readonly int MinLaneIndex;
        public readonly int MaxLaneIndex;

        public WorldData(float scrollSpeed, int minLaneIndex, int maxLaneIndex)
        {
            ScrollSpeed = scrollSpeed;
            MinLaneIndex = minLaneIndex;
            MaxLaneIndex = maxLaneIndex;
        }
    }
}
