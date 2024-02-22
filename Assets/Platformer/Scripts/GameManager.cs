using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Platformer.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public Text time;
        public Text coins;
        private int _coinsCount;
        private float _timeCount = 400 * 50;
        public Camera cam;

        private void FixedUpdate()
        {
            if (_timeCount <= 0)
            {
                Debug.Log("GAME OVER TIME ENDED");
            }
            else
            {
                _timeCount -= 2.5f;
                time.text = $"TIME\n{Math.Round(_timeCount / 50)}";
            }
            
            //Check for mouse click 
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
            {
                RaycastHit raycastHit;
                if (cam is not null)
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out raycastHit))
                    {
                        if (raycastHit.transform is not null)
                        {
                            CurrentClickedGameObject(raycastHit.transform.gameObject);
                        }
                    }
                }
            }
        }

        private void CurrentClickedGameObject(GameObject gameObjectRaycasted)
        {
            if (gameObjectRaycasted.name is "Brick(Clone)")
            {
                Destroy(gameObjectRaycasted);
            }
            else if(gameObjectRaycasted.name is "Question(Clone)")
            {
                _coinsCount += 1;
                coins.text = _coinsCount.ToString();
            }
        }
    }
}