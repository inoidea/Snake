using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    enum Rotation
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    internal sealed class Move : MonoBehaviour
    {
        public List<GameObject> _snakeParts;
        public float speed = 0.8f;
        public float tailDelay = 1.2f;
        public float tailDelayDefault = 0.4f;

        [SerializeField] private float followDistance = 30f;

        private Rotation rotation;
        private Vector3 direction;

        private void Awake()
        {
            rotation = Rotation.UP;
        }

        private void Update()
        {
            MoveHead(speed);
            StartCoroutine("MoveTail");
            Rotate();
        }

        private void MoveHead(float speed)
        {
            switch (rotation)
            {
                case Rotation.UP:
                    direction = Vector3.up;
                    break;
                case Rotation.DOWN:
                    direction = Vector3.down;
                    break;
                case Rotation.LEFT:
                    direction = Vector3.left;
                    break;
                case Rotation.RIGHT:
                    direction = Vector3.right;
                    break;
                default: direction = Vector3.up;
                    break;
            }

            transform.position = transform.position + direction * speed * Time.deltaTime;
        }

        IEnumerator MoveTail()
        {
            Vector3 prevPosition = _snakeParts[0].transform.position;

            for (int i = 0; i < _snakeParts.Count; i++)
            {
                var tailTransform = _snakeParts[i].transform;
                Vector3 curPosition = tailTransform.position;
                tailTransform.position = Vector3.Lerp(tailTransform.position, prevPosition, followDistance);
                prevPosition = curPosition;

                if (speed > 4)
                    tailDelayDefault = 0.3f;
                if (speed > 5.5f)
                    tailDelayDefault = 0.2f;
                if (speed > 11f)
                    tailDelayDefault = 0.1f;

                if (tailDelay < tailDelayDefault)
                    tailDelay = tailDelayDefault;

                yield return new WaitForSeconds(tailDelay);
            }
        }

        private void Rotate()
        {
            switch (rotation)
            {
                case Rotation.LEFT:
                case Rotation.RIGHT:
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                        rotation = Rotation.DOWN;
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                        rotation = Rotation.UP;
                    break;
                case Rotation.UP:
                case Rotation.DOWN:
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        rotation = Rotation.LEFT;
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                        rotation = Rotation.RIGHT;
                    break;
            }
        }
    }
}
