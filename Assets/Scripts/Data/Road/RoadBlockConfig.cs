using UnityEngine;

namespace EndlessRunnerECS.Data.Road
{
    [CreateAssetMenu(fileName = "RoadBlockConfig", menuName = "EndlessRunnerECS/Data/Road/RoadBlockConfig")]
    public class RoadBlockConfig : ScriptableObject
    {
        [field: SerializeField, Min(1f)]
        public float Length { get; private set; } = 30f;
    }
}
