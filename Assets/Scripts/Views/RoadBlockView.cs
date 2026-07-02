using EndlessRunnerECS.Data.Road;
using UnityEngine;

namespace EndlessRunnerECS.Views
{
    public sealed class RoadBlockView : MonoBehaviour
    {
        public float Length => _blockConfig.Length;

        [SerializeField]
        private RoadBlockConfig _blockConfig;
    }
}
