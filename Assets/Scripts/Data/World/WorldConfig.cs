using UnityEngine;

namespace EndlessRunnerECS.Data.World
{
    [CreateAssetMenu(fileName = "WorldConfig", menuName = "EndlessRunnerECS/Data/World/WorldConfig")]
    public class WorldConfig : ScriptableObject
    {
        [field: SerializeField, Min(WorldDataMapper.MIN_VALUE)]
        public float ScrollSpeed { get; private set; } = 8f;
        [field: SerializeField]
        public int MinLaneIndex { get; private set; } = -1;
        [field: SerializeField]
        public int MaxLaneIndex { get; private set; } = 1;
    }
}
