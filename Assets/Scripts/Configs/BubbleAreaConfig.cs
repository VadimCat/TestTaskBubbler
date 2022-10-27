using UnityEngine;

namespace Configs
{
    [CreateAssetMenu]
    public class BubbleAreaConfig : ScriptableObject
    {
        [SerializeField] private float minSqrBubbleDistance = .6f;
        [SerializeField] private float height = .1f;
        [SerializeField] private float radius = .3f;
        public float MinSqrBubbleDistance => minSqrBubbleDistance;
        public float Radius => radius;
        public float Height => height;
    }
}