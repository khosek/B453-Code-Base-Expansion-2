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

        [SerializeField] private float smoothTime = .60f;

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
                targetAngle += isRotatingLeft ? -45f : 45f;
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

            transform.rotation = Quaternion.Euler(0, currentAngle, 0);

            transform.position = target.position + _offset;


            // ExtensionMethods._isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, currentAngle, 0));


        }
    }
}
