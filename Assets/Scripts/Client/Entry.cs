using Configs;
using Models;
using Presenter;
using UnityEngine;
using View;

namespace Client
{
    public class Entry : MonoBehaviour
    {
        [SerializeField] private Raycaster _raycaster;
        [SerializeField] private GameScreen _gameScreen;

        [SerializeField] private LevelView levelView;
        [SerializeField] private BubbleAreaConfig _bubbleAreaConfig;

        private void Awake()
        {
            new LeverPresenter(new BubbleArea(_bubbleAreaConfig.MinSqrBubbleDistance), _raycaster, _gameScreen, levelView,
                _bubbleAreaConfig);
        }
    }
}