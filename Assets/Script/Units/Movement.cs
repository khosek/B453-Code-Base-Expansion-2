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

        public Transform cameraTransform;

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
        void Awake()
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

            if (_input == Vector3.zero) return;

            Vector3 cameraForward = cameraTransform.forward;

            cameraForward.y = 0;

            Vector3 cameraRight = cameraTransform.right;

            cameraRight.y = 0;

            Vector3 forwardRelative = _input.z * cameraForward * _speed * Time.deltaTime;

            Vector3 rightRelative = _input.x * cameraRight * _speed * Time.deltaTime;

            Vector3 moveDirection = forwardRelative + rightRelative;


            // _rigidbody.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);

            _rigidbody.velocity = new Vector3(moveDirection.x, _rigidbody.velocity.y, moveDirection.z);

            // _rigidbody.AddForce(new Vector3(moveDirection.x, _rigidbody.velocity.y, moveDirection.z));


        }
    }
}
