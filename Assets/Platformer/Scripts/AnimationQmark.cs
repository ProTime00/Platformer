using System;
using UnityEngine;

namespace Platformer.Scripts
{
    public class AnimationQmark : MonoBehaviour
    {
        private Renderer _renderer;
        private int _animTimer = 10;
        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void FixedUpdate()
        {
            if (_animTimer == 0)
            {
                _animTimer = 10;
                _renderer.material.mainTextureOffset += new Vector2(0f, 0.2f);
            }

            _animTimer--;
        }
    }
}
