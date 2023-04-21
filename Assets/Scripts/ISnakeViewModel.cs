using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ISnakeViewModel
    {
        ISnake Snake { get; }
        void ApplyHeight(float height);
        event Action<float> OnGrowthChange;
    }
}
