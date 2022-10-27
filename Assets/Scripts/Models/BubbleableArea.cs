using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class BubbleArea
    {
        private readonly float _minSqrBubbleDistance;
        private readonly int _maxBubbleCount;
        private List<Vector3> bubblePoints;
        
        public BubbleArea(float minSqrBubbleDistance)
        {
            _minSqrBubbleDistance = minSqrBubbleDistance;

            bubblePoints = new List<Vector3>(20);
        }

        public void Reset()
        {
            bubblePoints.Clear();
        }
        
        public bool TryAddBubblePoint(Vector3 point)
        {
            bool isSetted = true;
            foreach (var bubble in bubblePoints)
            {
                if (Vector3.SqrMagnitude(point - bubble) < _minSqrBubbleDistance)
                {
                    isSetted = false;
                    break;
                }                
            }

            if (isSetted)
            {
                bubblePoints.Add(point);
            }
            
            return isSetted;
        }
    }
}
