using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChessGame
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] Canvas canvas = null;
        [SerializeField] GameObject gameOverPanel = null;

        private void OnEnable()
        {
            TileManager.instance.OnKingDead += ShowGameOverPanel;
        }

        private void OnDisable()
        {
            TileManager.instance.OnKingDead -= ShowGameOverPanel;
        }

        private void ShowGameOverPanel()
        {
            Instantiate(gameOverPanel, canvas.transform);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
