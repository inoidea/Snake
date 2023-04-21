using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal sealed class SnakeView : MonoBehaviour
    {
        [SerializeField] private GameObject _snakeHeadPref;
        [SerializeField] private GameObject _snakeTailPref;

        private GameObject head;

        public static Action Reload;

        private List<GameObject> _snakeParts;
        private ISnakeViewModel _viewModel;

        public void Initialize(ISnakeViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnGrowthChange += OnGrowthChange;

            // Получить список частей змеи.
            _snakeParts = _viewModel.Snake.SnakeParts;

            head = Instantiate(_snakeHeadPref, Vector3.zero, Quaternion.identity);

            // Добавить передвижение.
            Move snakeMove = head.AddComponent<Move>();
            snakeMove._snakeParts = _viewModel.Snake.SnakeParts;

            _snakeParts.Add(head);

            OnGrowthChange(_viewModel.Snake.CurrentLength);
        }

        private void OnGrowthChange(float currentLength)
        {
            if (currentLength > _snakeParts.Count)
            {
                var snakePartCount = _snakeParts.Count;
                for (int i = 0; i < currentLength - snakePartCount; i++)
                {
                    var lastTailPart = _snakeParts[_snakeParts.Count - 1];

                    GameObject newTailPart = Instantiate(_snakeTailPref, lastTailPart.transform.position - Vector3.up, Quaternion.identity);

                    // К хвосту со 2 элемента добавить тэг для обработки столкновения.
                    if (currentLength > 2)
                        newTailPart.tag = "Suicide";

                    _snakeParts.Add(newTailPart);
                }

                // Увеличить скорость змейки.
                if (head.TryGetComponent<Move>(out Move move))
                {
                    move.speed += 0.3f;

                    if (move.speed > 9)
                        move.tailDelay -= 0.03f;
                    else
                        move.tailDelay -= 0.1f;
                }
;            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Suicide"))
            {
                Debug.Log("Произошло самоубийство.");
                Reload?.Invoke();
            }
        }

        ~SnakeView()
        {
            _viewModel.OnGrowthChange -= OnGrowthChange;
        }
    }
}
