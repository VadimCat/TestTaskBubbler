using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private MovingPath movingPath;
    [SerializeField] private MeshBubblerView meshView;

    public MeshBubblerView View => meshView;
    public Character Character => character;
    public MovingPath Path => movingPath;
}