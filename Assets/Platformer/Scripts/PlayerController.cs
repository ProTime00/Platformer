using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        private float yap = 1;

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
            if (x == 0 && Math.Abs(_rigidbody.velocity.x) > 0)
            {
                var velocity = _rigidbody.velocity;
                velocity.x /= 1.5f;
                _rigidbody.velocity = velocity;
                if (Math.Abs(_rigidbody.velocity.x) < 0.05)
                {
                    var vector3 = _rigidbody.velocity;
                    vector3.x = 0;
                    _rigidbody.velocity = vector3;
                }
            }
            else
            {
                _rigidbody.velocity += Vector3.right * (x * _acceleration * Time.deltaTime * 10f);
            }

            Collider col = GetComponent<Collider>();
            float halfHeight = col.bounds.extents.y;

            isGrounded = Physics.Raycast(transform.position, Vector3.down, halfHeight + 0.1f);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            if (!isGrounded && Input.GetKey(KeyCode.Space))
            {
                _rigidbody.AddForce(Vector3.up * 70, ForceMode.Force);
            }

            if (Math.Abs(_rigidbody.velocity.x) > maxSpeed)
            {
                Vector3 temp = _rigidbody.velocity;
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

            if (_rigidbody.velocity.x > 0)
            {
                yap = 1;
            }
            else if (_rigidbody.velocity.x < 0)
            {
                yap = -1;
            }
            
            var rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0f, yap * 90, 0f);
            transform.rotation = rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Water"))
            {
                SceneManager.LoadScene("LevelParser");
            }

            if (other.CompareTag("Finish"))
            {
                SceneManager.LoadScene("LevelParser2");
            }

            if (other.gameObject.name is "Destroyer")
            {
                Destroy(other.gameObject.transform.parent.gameObject);
                GameManager.gameManager.scoreInt += 100;
                GameManager.gameManager.score.text = "Score\n";
                GameManager.gameManager.score.text += GameManager.gameManager.scoreInt switch
                {
                    < 1000 => "000" + GameManager.gameManager.scoreInt,
                    < 10000 => "00" + GameManager.gameManager.scoreInt,
                    < 100000 => "0" + GameManager.gameManager.scoreInt,
                    _ => GameManager.gameManager.score.text
                };
            }
        }
    }
}
