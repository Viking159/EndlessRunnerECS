using UnityEngine;

namespace EndlessRunnerECS.Data.RoadObject
{
    [CreateAssetMenu(fileName = "RoadObjectConfig",menuName = "EndlessRunnerECS/Data/RoadObject/RoadObjectConfig")]
    public sealed class RoadObjectConfig : ScriptableObject
    {
        [field: SerializeField, Min(RoadObjectDataMapper.MIN_SPAWN_INTERVAL)]
        public float SpawnInterval { get; private set; } = 1f;

        [field: SerializeField, Range(0f, 1f)]
        public float SpawnChance { get; private set; } = 0.7f;

        [field: SerializeField, Min(RoadObjectDataMapper.MIN_SPAWN_DISTANCE)]
        public float SpawnDistanceZ { get; private set; } = 40f;

        [field: SerializeField]
        public float RecycleZ { get; private set; } = -15f;

        [field: SerializeField, Min(RoadObjectDataMapper.MIN_TRIGGER_RADIUS)]
        public float TriggerRadiusX { get; private set; } = 1f;

        [field: SerializeField, Min(RoadObjectDataMapper.MIN_TRIGGER_RADIUS)]
        public float TriggerRadiusZ { get; private set; } = 1f;
    }
}
