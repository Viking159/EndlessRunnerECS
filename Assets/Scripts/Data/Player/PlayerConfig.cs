using UnityEngine;

namespace EndlessRunnerECS.Data.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "EndlessRunnerECS/Data/Player/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {

        [field: SerializeField, Min(PlayerDataMapper.MIN_VALUE)]
        public float LaneChangeSpeed { get; private set; } = 10f;

        [field: SerializeField, Min(PlayerDataMapper.MIN_VALUE)]
        public float LaneWidth { get; private set; } = 3f;
    }
}
