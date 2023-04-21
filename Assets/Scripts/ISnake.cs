using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ISnake
    {
        float CurrentLength { get; set; }
        public List<GameObject> SnakeParts { get; set; }
    }
}
