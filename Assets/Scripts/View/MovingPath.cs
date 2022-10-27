using UnityEngine;

public class MovingPath : MonoBehaviour
{
    [SerializeField] private Transform[] path;

    public Transform[] Path => path;
}
