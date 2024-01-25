using UnityEngine;
using Utils;

namespace Units
{
    public class ArcadeMovement : MonoBehaviour
    {
        [SerializeField] private float _forwardAcceleration = 8f;

        [SerializeField] private float _reverseAcceleration = 4f;

        [SerializeField] private float _maxSpeed = 50f;

        [SerializeField] private float _speedMultiplier = 1000f;

        [SerializeField] private float gravityForce = 10f;

        [SerializeField] private float _turnSpeed = 180f;

        [SerializeField] private float _speedInput = 0f;

        [SerializeField] private float _turnInput = 0f;

        [SerializeField] private string _xAxis = "Horizontal";

        [SerializeField] private string _yAxis = "Vertical";

        [SerializeField] private bool _isGrounded = false;

        private Vector3 _input;

        public new Collider collider;

        public new Rigidbody rigidbody;

        public Transform cameraTransform;

        public Transform groundRayPoint;

        public LayerMask whatIsGround;

        public float groundRayLength = .5f;

        private void GetInputs()
        {
            _input = new Vector3(Input.GetAxis(_xAxis), 0, Input.GetAxis(_yAxis));

            _speedInput = _input.z > 0 ? _input.z * _forwardAcceleration * _speedMultiplier : _input.z < 0 ? _input.z * _reverseAcceleration * _speedMultiplier : 0f;

            _turnInput = _input.x;

            if (_isGrounded)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, _turnInput * _input.z * _turnSpeed * Time.deltaTime, 0f));
            }
        }

        private void Look()
        {
            if (_input == Vector3.zero) return;

            Quaternion newRotation = Quaternion.LookRotation(_input.ToIso(), Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _turnSpeed * Time.deltaTime);
        }

        private void Update()
        {
            GetInputs();
            // Look();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerStay(Collider other)
        {
            _isGrounded = other != null && (((1 << other.gameObject.layer) & whatIsGround) != 0);

            // transform.rotation = Quaternion.FromToRotation(other.gameObject)
            // foreach (ContactPoint contact in other.con)
            // {
            //     Debug.DrawRay(contact.point, contact.normal, Color.white);
            // }

        }

        private void OnTriggerExit(Collider other)
        {
            _isGrounded = false;
        }

        private void Move()
        {
            if (_isGrounded)
            {
                rigidbody.drag = 3f;

                if (Mathf.Abs(_speedInput) > 0)
                {
                    rigidbody.AddForce(transform.forward * _speedInput);
                }
            };

            if (!_isGrounded)
            {
                rigidbody.drag = 0.1f;
                rigidbody.AddForce(Vector3.up * -gravityForce * 100f);
            }


            // Vector3 cameraForward = cameraTransform.forward;

            // cameraForward.y = 0;

            // Vector3 cameraRight = cameraTransform.right;

            // cameraRight.y = 0;

            // Vector3 forwardRelative = _input.z * cameraForward * _maxSpeed * Time.deltaTime;

            // Vector3 rightRelative = _input.x * cameraRight * _maxSpeed * Time.deltaTime;

            // Vector3 moveDirection = forwardRelative + rightRelative;

            // rigidbody.AddForce(new Vector3(moveDirection.x, rigidbody.velocity.y, moveDirection.z));


        }
    }
}
