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

        [SerializeField] private bool _isGrounded = true;

        [SerializeField] private Rigidbody PlayerRigidbody;

        [SerializeField] private Transform groundRayPoint;

        [SerializeField] private Transform FrontWheel;

        [SerializeField] private Transform RearWheel;

        [SerializeField] private LayerMask WhatIsGround;

        [SerializeField] private float groundRayLength = .5f;

        [SerializeField] private ParticleSystem[] dustTrail;

        [SerializeField] private float maxEmission = 25f;

        [SerializeField] private float emissionRate;

        [SerializeField] private float boostMultiplier;

        private float heatPercent = 0.0f;

        private bool overheating = false;

        private void GetInputs()
        {
            _input = new Vector3(Input.GetAxis(_xAxis), 0, Input.GetAxis(_yAxis));


            if (Input.GetButton("Fire1"))
            {
                _input = new Vector3(Input.GetAxis(_xAxis), 0, 1);
            }

            if (Input.GetButton("Fire2"))
            {
                _input = new Vector3(Input.GetAxis(_xAxis), 0, -1);
            }

            _speedInput = GetSpeedInput(_input.z);

            if (overheating)
            {
                _speedInput *= 0;
            }
            else if(Input.GetButton("Boost"))
            {
                _speedInput *= boostMultiplier;
            }

            _turnInput = _input.x;
        }

        private float GetSpeedInput(float speed)
        {
            if (speed == 0)
                return 0;

            return speed * _speedMultiplier * (speed > 0 ? _forwardAcceleration : _reverseAcceleration);
        }

        private void Update()
        {
            CheckHeat();
            GetInputs();
            SlopeRotation();
        }

        private void FixedUpdate()
        {
            MoveAndRotate();
        }


        private void Start()
        {
            PlayerRigidbody.sleepThreshold = 0.0f;
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

        private void MoveAndRotate()
        {
            emissionRate = 0f;

            if (_isGrounded)
            {
                PlayerRigidbody.drag = 3f;

                if (Mathf.Abs(_speedInput) > 0)
                {
                    Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, _turnInput * _input.z * _turnSpeed, 0) * Time.fixedDeltaTime);

                    PlayerRigidbody.MoveRotation(PlayerRigidbody.rotation * deltaRotation);

                    Vector3 newVelocity = transform.forward * _speedInput;

                    PlayerRigidbody.velocity = new Vector3(newVelocity.x * Time.fixedDeltaTime, PlayerRigidbody.velocity.y, newVelocity.z * Time.fixedDeltaTime);

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
        }

        public void CheckHeat()
        {

            if (!Input.GetButton("Boost") || overheating)
            {
                heatPercent -= 10.0f * Time.deltaTime;
                if (heatPercent <= 0)
                {
                    overheating = false;
                    heatPercent = 0.0f;
                }
            }
            else
            {
                heatPercent += 20.0f * Time.deltaTime;
                if (heatPercent >= 100) 
                {
                    overheating = true;
                    heatPercent = 100.0f;
                }
            }
        }
    }
}
