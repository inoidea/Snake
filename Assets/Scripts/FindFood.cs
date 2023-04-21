using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Profiling.HierarchyFrameDataView;

namespace Assets.Scripts
{
    internal sealed class FindFood : MonoBehaviour
    {
        public ISnakeViewModel _snakeViewModel;
        public FoodView _foodView;
        public GameObject _food;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<SnakeView>(out SnakeView snakeView))
            {
                Destroy(gameObject);
                _foodView.CreateFood(_snakeViewModel);
                _snakeViewModel.ApplyHeight(1);
            }
        }
    }
}
