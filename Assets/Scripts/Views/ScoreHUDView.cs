using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunnerECS.Views
{
    public class ScoreHUDView : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private string _mask = "Score: {0}";

        public void UpdateScoreText(int score) => _text.text = string.Format(_mask, score);
    }
}
