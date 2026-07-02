namespace EndlessRunnerECS.Data.Player
{
    public readonly struct PlayerData
    {
        public readonly float LaneChangeSpeed;
        public readonly float LaneWidth;

        public PlayerData(float laneChangeSpeed, float laneWidth)
        {
            LaneChangeSpeed = laneChangeSpeed;
            LaneWidth = laneWidth;
        }
    }
}
