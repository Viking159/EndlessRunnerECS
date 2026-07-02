using UnityEngine;

namespace EndlessRunnerECS.Data.World
{
    [CreateAssetMenu(fileName = "WorldConfig", menuName = "EndlessRunnerECS/Data/World/WorldConfig")]
    public class WorldConfig : ScriptableObject
    {
        [field: SerializeField, Min(WorldDataMapper.MIN_VALUE)]
        public float ScrollSpeed { get; private set; } = 8f;
    }
}
