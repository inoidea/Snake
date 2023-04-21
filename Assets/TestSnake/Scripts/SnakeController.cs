using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.TestSnake.Scripts
{
    public class SnakeController : MonoBehaviour
    {
        List<GameObject> parts = new List<GameObject>();
        float currentRotation = 0.0f;

        public GameObject bodyPart;

        public float speed = 1f;
        public float speedRotation = 100.0f;

        FoodManager foodManager;

        private void Start()
        {
            parts.Add(gameObject);
            foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        }

        private void Update()
        {
            currentRotation -= Input.GetAxis("Horizontal") * speedRotation * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("FoodItem"))
            {
                Destroy(collision.gameObject);
                GameObject parent = parts[parts.Count - 1];
                GameObject bodyPartObj = GameObject.Instantiate(bodyPart, parent.transform.position, Quaternion.identity) as GameObject;
                BodyPart bodyPartScript = bodyPartObj.GetComponent<BodyPart>();
                bodyPartScript.parentObject = parent;
                bodyPartScript.snakeController = GetComponent<SnakeController>();

                this.parts.Add(bodyPartObj);
                speed += 0.1f;
                foodManager.Add();
            }
        }
    }
}