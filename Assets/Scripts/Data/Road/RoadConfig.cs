using UnityEngine;

namespace EndlessRunnerECS.Data.Road
{
    [CreateAssetMenu(fileName = "RoadConfig", menuName = "EndlessRunnerECS/Data/Road/RoadConfig")]
    public class RoadConfig : ScriptableObject
    {
        [field: SerializeField, Min(RoadDataMapper.MIN_INITIAL_BLOCKS_COUNT)]
        public int InitialBlocksCount { get; private set; } = 3;

        [field: SerializeField]
        public float RecycleZ { get; private set; } = -35f;

        [field: SerializeField]
        public float StartZ { get; private set; } = 0f;
    }
}
