using UnityEngine;


namespace Units
{
    public class ChaseCamera : MonoBehaviour
    {
        private Vector3 _offset;

        private Vector3 _velocity = Vector3.zero;

        public Transform target;

        public float smoothTime = 0.3f;

        private void Start()
        {
            _offset = transform.position - target.position;
        }

        private void LateUpdate()
        {

            Vector3 targetPosition = target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime * Time.deltaTime);


            transform.LookAt(target);
        }
    }

}