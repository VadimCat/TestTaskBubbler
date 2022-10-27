using Client;
using Configs;
using Models;
using UnityEngine;
using View;

namespace Presenter
{
    public class LeverPresenter
    {
        private readonly BubbleArea _bubbleArea;
        private readonly Raycaster _raycaster;
        private readonly GameScreen _gameScreen;
        private readonly LevelView _levelView;
        private readonly BubbleAreaConfig _bubbleAreaConfig;

        private bool isCharacterMovementEnabled = true;
        private bool isBubbleCreationEnabled = true;

        public LeverPresenter(BubbleArea bubbleArea, Raycaster raycaster, GameScreen gameScreen, LevelView levelView,
            BubbleAreaConfig bubbleAreaConfig)
        {
            _bubbleArea = bubbleArea;
            _raycaster = raycaster;
            _gameScreen = gameScreen;
            _levelView = levelView;
            _bubbleAreaConfig = bubbleAreaConfig;

            levelView.View.InitComputeShader(bubbleAreaConfig.Radius, bubbleAreaConfig.Height);
        
            raycaster.OnRaycastHold += DisableMoving;
            raycaster.OnRaycastEnd += EnableMoving;

            raycaster.OnRaycastHold += MoveBubble;
            raycaster.OnRaycastEnd += SetBubble;

            _levelView.Character.OnPathEmpty += EnableBubbleCreation;
        
            _gameScreen.View.PlayButton.onClick.AddListener(PlayCharacterMovement);
            _gameScreen.View.ResetButton.onClick.AddListener(ResetBubbleArea);
        }

        private void ResetBubbleArea()
        {
            _bubbleArea.Reset();
            _levelView.View.Reset();
        }

        private void EnableBubbleCreation()
        {
            isBubbleCreationEnabled = true;
        }

        private void PlayCharacterMovement()
        {
            if (!isCharacterMovementEnabled)
                return;

            isBubbleCreationEnabled = false;
            _levelView.Character.SetPath(_levelView.Path.Path);
        }

        private void SetBubble(Vector3 point)
        {
            if (isBubbleCreationEnabled && _bubbleArea.TryAddBubblePoint(point))
            {
                _levelView.View.AddSavedBubble(point);
            }
            else if(!_bubbleArea.TryAddBubblePoint(point))
            {
                _levelView.View.MoveBubble(Vector3.one * float.MaxValue);
            }
        }

        private void MoveBubble(Vector3 point)
        {
            if (!isBubbleCreationEnabled)
                return;

            _levelView.View.MoveBubble(point);
        }

        private void EnableMoving(Vector3 obj)
        {
            isCharacterMovementEnabled = true;
        }

        private void DisableMoving(Vector3 obj)
        {
            isCharacterMovementEnabled = false;
        }
    }
}