using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Random;

namespace Assets.Scripts
{
    internal sealed class FoodView : MonoBehaviour
    {
        private Vector2 _max;
        private Vector2 _min;

        private ISnakeViewModel _viewModel;

        public void Initialize(ISnakeViewModel viewModel, (Vector2 max, Vector2 min) bounds)
        {
            _viewModel = viewModel;
            _max = bounds.max;
            _min = bounds.min;

            CreateFood(viewModel);
        }

        public void CreateFood(ISnakeViewModel viewModel) {
            float foodWidth = transform.localScale.x;

            float x = Range(_min.x + foodWidth, _max.x - foodWidth);
            float y = Range(_min.y + foodWidth, _max.y - foodWidth);

            GameObject food = Instantiate(gameObject, new Vector2(x, y), Quaternion.identity);
            FindFood findFood = food.AddComponent<FindFood>();
            findFood._snakeViewModel = viewModel;
            findFood._food = gameObject;
            findFood._foodView = this;
        }
    }
}
