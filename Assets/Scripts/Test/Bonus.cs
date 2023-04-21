using UnityEngine;
using System.Collections;
using Assets.Scripts;

namespace Snake.Test
{
    public class Bonus : MonoBehaviour
    {

        public float timeout = 10;

        void Start()
        {
            Destroy(gameObject, timeout);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Snake.tailCount++;
            Destroy(gameObject);
        }
    }
}