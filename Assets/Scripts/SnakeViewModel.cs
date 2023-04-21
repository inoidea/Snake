using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    internal sealed class SnakeViewModel : ISnakeViewModel
    {
        public event Action<float> OnGrowthChange;
        public ISnake Snake { get; }

        public SnakeViewModel(ISnake snake) {
            Snake = snake;
        }

        public void ApplyHeight(float height)
        {
            Snake.CurrentLength += height;
            OnGrowthChange?.Invoke(Snake.CurrentLength);
        }
    }
}
