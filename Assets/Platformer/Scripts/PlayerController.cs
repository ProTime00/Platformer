using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer.Scripts
{
    public class PlayerController : MonoBehaviour
    {

        private Controls _controls;
        private InputAction _pAction;
        private Rigidbody _rigidbody;
        private float _acceleration = 10f;
        public float jumpForce;
        private float maxSpeed = 15f;
        private bool isGrounded = true;

        private void Awake()
        {
            _controls = new Controls();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _pAction = _controls.Player.Move;
            _pAction.Enable();
        }

        private void OnDisable()
        {
            _pAction.Disable();
        }

        private void FixedUpdate()
        {
            
            
            float x = _pAction.ReadValue<float>();
            _rigidbody.velocity += Vector3.right * (x * _acceleration * Time.deltaTime * 10f);

            Collider col = GetComponent<Collider>();
            float halfHeight = col.bounds.extents.y;

            isGrounded = Physics.Raycast(transform.position, Vector3.down, halfHeight);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            if (!isGrounded && Input.GetKey(KeyCode.Space))
            {
                _rigidbody.AddForce(Vector3.up * 18, ForceMode.Force);
            }

            if (Math.Abs(_rigidbody.velocity.x) > maxSpeed)
            {
                Vector3 temp = Vector3.zero;
                if (_rigidbody.velocity.x < 0)
                {
                    temp.x = maxSpeed * -1;
                }
                else
                {
                    temp.x = maxSpeed;
                }
                _rigidbody.velocity = temp;
            }

            float yap = _rigidbody.velocity.x >= 0 ? 1 : -1;
            var rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0f, yap * 90, 0f);
            transform.rotation = rotation;
        }
    }
}
