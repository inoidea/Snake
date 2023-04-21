using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal sealed class Snake : ISnake
    {
        private List<GameObject> _snakeParts = new List<GameObject>();

        public float CurrentLength { get; set; }
        public List<GameObject> SnakeParts { get { return _snakeParts; } set { _snakeParts = value; } }

        public Snake(float currentLength)
        {
            CurrentLength = currentLength;
        }
    }
}
