using UnityEngine;
using Utils;

namespace Units
{
    public class ArcadeMovement : MonoBehaviour
    {
        private Vector3 _input;

        [SerializeField] private float _forwardAcceleration = 8f;

        [SerializeField] private float _reverseAcceleration = 4f;

        [SerializeField] private float _maxSpeed = 50f;

        [SerializeField] private float _speedMultiplier = 1000f;

        [SerializeField] private float gravityForce = 10f;

        [SerializeField] private float _turnSpeed = 180f;

        [SerializeField] private float _speedInput = 0f;

        [SerializeField] private float _turnInput = 0f;

        [SerializeField] private float _maxWheelTurn = 25f;

        [SerializeField] private string _xAxis = "Horizontal";

        [SerializeField] private string _yAxis = "Vertical";

        [SerializeField] private bool _isGrounded = false;

        [SerializeField] private Rigidbody PlayerRigidbody;

        [SerializeField] private Transform groundRayPoint;

        [SerializeField] private Transform FrontWheel;

        [SerializeField] private Transform RearWheel;

        [SerializeField] private LayerMask WhatIsGround;

        [SerializeField] private float groundRayLength = .5f;

        [SerializeField] private ParticleSystem[] dustTrail;

        [SerializeField] private float maxEmission = 25f;

        [SerializeField] private float emissionRate;

        private void GetInputs()
        {
            _input = new Vector3(Input.GetAxis(_xAxis), 0, Input.GetAxis(_yAxis));

            _speedInput = GetSpeedInput(_input.z);

            _turnInput = _input.x;

            if (_isGrounded)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, _turnInput * _input.z * _turnSpeed * Time.deltaTime, 0f));
            }
        }

        private float GetSpeedInput(float speed)
        {
            if (speed == 0)
                return 0;

            return speed * _speedMultiplier * (speed > 0 ? _forwardAcceleration : _reverseAcceleration);
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
            SlopeRotation();
            // Look();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerStay(Collider other)
        {
            _isGrounded = other != null && (((1 << other.gameObject.layer) & WhatIsGround) != 0);
        }

        private void OnTriggerExit()
        {
            _isGrounded = false;
        }

        private void SlopeRotation()
        {
            RaycastHit hit;

            if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, WhatIsGround))
            {
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }

        private void Move()
        {
            emissionRate = 0f;

            if (_isGrounded)
            {
                PlayerRigidbody.drag = 3f;

                if (Mathf.Abs(_speedInput) > 0)
                {
                    PlayerRigidbody.AddForce(transform.forward * _speedInput);

                    emissionRate = maxEmission;
                }
            }

            if (!_isGrounded)
            {
                PlayerRigidbody.drag = 0.1f;
                PlayerRigidbody.AddForce(Vector3.up * -gravityForce);
            }

            foreach (ParticleSystem particle in dustTrail)
            {
                ParticleSystem.EmissionModule emissionModule = particle.emission;

                emissionModule.rateOverTime = emissionRate;
            }

            Vector3 wheelAngles = FrontWheel.localRotation.eulerAngles;

            FrontWheel.localRotation = Quaternion.Euler(wheelAngles.x, wheelAngles.y, _turnInput * _maxWheelTurn);

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
