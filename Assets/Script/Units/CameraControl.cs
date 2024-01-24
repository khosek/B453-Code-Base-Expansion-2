using UnityEngine;
using Utils;

namespace Units
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private float targetAngle = 45f;

        [SerializeField] private float currentAngle = 0f;

        [SerializeField] private float rotationSpeed = 5f;

        [SerializeField] private string Fire2 = "Fire2";

        [SerializeField] private string Fire3 = "Fire3";

        private Vector3 _offset;

        [SerializeField] private Transform target;

        [SerializeField] private float smoothTime;

        [SerializeField] private Vector3 _currentVelocity = Vector3.zero;

        private void Awake()
        {
            _offset = transform.position - target.position;
        }

        private void Update()
        {
            bool isRotatingLeft = Input.GetButtonDown(Fire2);
            bool isRotatingRight = Input.GetButtonDown(Fire3);


            if (isRotatingLeft || isRotatingRight)
            {
                targetAngle += isRotatingLeft ? -45f : isRotatingRight ? 45f : 0;
            }

            if (targetAngle < 0)
            {
                targetAngle += 360;
            }

            if (targetAngle > 360)
            {
                targetAngle -= 360;
            }

            currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(30, currentAngle, 0);

            // ExtensionMethods._skewedDegree = currentAngle;
            ExtensionMethods._isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, currentAngle, 0));


        }

        private void LateUpdate()
        {
            Vector3 targetPosition = target.position + _offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        }

    }
}