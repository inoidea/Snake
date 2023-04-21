using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private SnakeView _snakeView;
        [SerializeField] private FoodView _foodView;
        [SerializeField] private Transform _topWall;
        [SerializeField] private Transform _rightWall;
        [SerializeField] private Transform _leftWall;
        [SerializeField] private Transform _bottomWall;
        [SerializeField] private GameObject _reloadPanel;
        [SerializeField] private float _wellsWidth = 1;

        private void Awake()
        {
            Time.timeScale = 1;
            _reloadPanel.SetActive(false);
        }

        private void Start()
        {
            var snake = new Snake(2);
            var viewModel = new SnakeViewModel(snake);
            _snakeView.Initialize(viewModel);
            _foodView.Initialize(viewModel, GetBounds());

            SnakeView.Reload += Reload;
        }

        private (Vector2 max, Vector2 min) GetBounds() {
            Vector2 min = new Vector2(_leftWall.position.x + _wellsWidth, _bottomWall.position.y + _wellsWidth);
            Vector2 max = new Vector2(_rightWall.position.x - _wellsWidth, _topWall.position.y - _wellsWidth);

            return (max, min);
        }

        private void Reload() {
            // Поставить игру на паузу.
            Time.timeScale = 0;

            // Отобразить панель с перезагрузкой игры.
            _reloadPanel.SetActive(true);
        }

        private void OnDestroy()
        {
            SnakeView.Reload -= Reload;
        }
    }
}