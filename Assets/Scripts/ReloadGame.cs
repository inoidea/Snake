using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal sealed class ReloadGame : MonoBehaviour
    {
        [SerializeField] private Button _reloadBtn;

        private void Start()
        {
            _reloadBtn.onClick.AddListener(Reload);
        }

        private void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}