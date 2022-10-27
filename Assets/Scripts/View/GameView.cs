using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button resetButton;

        public Button PlayButton => playButton;
        public Button ResetButton => resetButton;

        private void OnDestroy()
        {
            playButton.onClick = null;
            resetButton.onClick = null;
        }
    }
}