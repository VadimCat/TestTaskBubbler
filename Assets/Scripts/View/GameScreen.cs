using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private GameView gameView;

    public GameView View => gameView;
}