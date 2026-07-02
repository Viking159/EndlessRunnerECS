namespace EndlessRunnerECS.Data.World
{
    public readonly struct WorldData
    {
        public readonly float ScrollSpeed;

        public WorldData(float scrollSpeed) 
            => ScrollSpeed = scrollSpeed;
    }
}
