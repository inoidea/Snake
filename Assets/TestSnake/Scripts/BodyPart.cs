using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.TestSnake.Scripts
{
    internal class BodyPart : MonoBehaviour
    {
        public GameObject parentObject;
        public SnakeController snakeController;

        void Update()
        {
            parentObject.transform.LookAt(parentObject.transform);
            transform.position = Vector3.Lerp(transform.position, parentObject.transform.position, Time.deltaTime * 3.0f * snakeController.speed);
        }
    }
}
