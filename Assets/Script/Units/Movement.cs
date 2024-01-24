using UnityEngine;
using Utils;

namespace Units
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        [SerializeField] private float _speed = 5f;

        [SerializeField] private float _turnSpeed = 360f;

        [SerializeField] private string _xAxis = "Horizontal";

        [SerializeField] private string _yAxis = "Vertical";

        private Vector3 _input;

        void GetInputs()
        {
            _input = new Vector3(Input.GetAxisRaw(_xAxis), 0, Input.GetAxisRaw(_yAxis));
        }

        void Look()
        {
            if (_input == Vector3.zero) return;

            Quaternion newRotation = Quaternion.LookRotation(_input.ToIso(), Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _turnSpeed * Time.deltaTime);
        }
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            GetInputs();
            Look();
        }

        void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rigidbody.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);

            // _rigidbody.AddForce()
        }
    }
}
