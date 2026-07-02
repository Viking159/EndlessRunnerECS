namespace EndlessRunnerECS.ECS.Components
{
    public struct LaneMovementComponent
    {
        public int CurrentLane;
        public int TargetLane;

        public int MinLane;
        public int MaxLane;

        public float LaneWidth;
        public float LaneChangeSpeed;
    }
}
