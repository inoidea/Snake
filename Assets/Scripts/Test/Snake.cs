using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Random;

namespace Snake.Test
{
    public class Snake : MonoBehaviour
    {

        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;
        public KeyCode up = KeyCode.W;
        public KeyCode down = KeyCode.S;

        public int size = 32;
        public float shift = 1;
        public float timeoutMove = 0.5f;
        public float timeoutBonus = 5;
        public GameObject _tail;
        public GameObject _bonus;
        private float curTimeout;
        private Vector3[,] pos;
        private List<GameObject> tail;
        private Vector3 tail_pos;
        private Vector3 tail_last;
        private int t_Count;
        private float h, v;

        public static int tailCount;
        public static bool lose;

        void Start()
        {
            lose = false;
            tailCount = 1;
            t_Count = tailCount;
            tail = new List<GameObject>();
            tail.Add(this.gameObject);
            float posX = -shift * size / 2 - shift;
            float posY = Mathf.Abs(posX);
            float Xreset = posX;
            pos = new Vector3[size, size];
            for (int y = 0; y < size; y++)
            {
                posY -= shift;
                for (int x = 0; x < size; x++)
                {
                    posX += shift;
                    pos[x, y] = new Vector3(posX, posY, 0);
                }
                posX = Xreset;
            }

            tail[0].transform.position = pos[size / 2, size / 2];
            StartCoroutine(AddBonus());
        }

        IEnumerator AddBonus()
        {
            GameObject clone = Instantiate(_bonus) as GameObject;
            clone.transform.position = pos[Range(0, size), Range(0, size)];
            yield return new WaitForSeconds(timeoutBonus);
            if (!lose) StartCoroutine(AddBonus());
        }

        void Move(int count)
        {
            tail_last = tail[tail.Count - 1].transform.position;

            tail_pos = tail[0].transform.position;
            tail[0].transform.position = new Vector3(tail_pos.x + h, tail_pos.y + v, 0);

            Vector3 tmp = Vector3.zero;

            for (int j = 1; j < count; j++)
            {
                tmp = tail[j].transform.position;
                tail[j].transform.position = tail_pos;
                tail_pos = tmp;
            }
        }

        void Update()
        {
            curTimeout += Time.deltaTime;
            if (curTimeout > timeoutMove)
            {
                curTimeout = 0;
                Move(tailCount);
            }

            if (t_Count != tailCount)
            {
                GameObject clone = Instantiate(_tail) as GameObject;
                clone.name = "Tail_" + tail.Count;
                clone.transform.position = tail_last;
                tail.Add(clone);
            }
            t_Count = tailCount;

            if (Input.GetKeyDown(left))
            {
                h = -shift;
                v = 0;
            }
            else if (Input.GetKeyDown(right))
            {
                h = shift;
                v = 0;
            }
            else if (Input.GetKeyDown(down))
            {
                v = -shift;
                h = 0;
            }
            else if (Input.GetKeyDown(up))
            {
                v = shift;
                h = 0;
            }

            if (lose)
            {
                Debug.Log("Вы проиграли!");
                enabled = false;
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag == "Player")
            {
                lose = true;
            }
        }
    }
}