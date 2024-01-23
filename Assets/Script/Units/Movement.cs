
using UnityEngine;

namespace Units
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _turnSpeed = 360f;
        private Vector3 _input;



        void GetInput()
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        void Look()
        {
            if (_input == Vector3.zero) return;

            // Vector3 relative = transform.position + _input - transform.position;

            Quaternion newRotation = Quaternion.LookRotation(_input.ToIso(), Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _turnSpeed * Time.deltaTime);
        }
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            GetInput();
            Look();
        }

        void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);
        }
    }


    public static class Helpers
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 225, 0));

        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }
}
