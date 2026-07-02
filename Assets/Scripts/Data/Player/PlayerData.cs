namespace EndlessRunnerECS.Data.Player
{
    public struct PlayerData
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
