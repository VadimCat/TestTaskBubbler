using System;
using UnityEngine;

namespace Client
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public event Action<Vector3> OnRaycastHold;
        public event Action<Vector3> OnRaycastEnd;
    
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    OnRaycastHold?.Invoke(hit.point);
                }
            }
            else if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    OnRaycastEnd?.Invoke(hit.point);
                }
            }
        }

        private void OnDestroy()
        {
            OnRaycastHold = null;
            OnRaycastEnd = null;
        }
    }
}