using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ChessGame
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] Canvas canvas = null;
        [SerializeField] GameObject gameOverPanel = null;
        [SerializeField] GameObject nextTurnPanel = null;
        [SerializeField] float timeForNextTurnPanel = 1f;

        string nextPieceColor;

        private void OnEnable()
        {
            TileManager.instance.OnKingDead += ShowGameOverPanel;
            TileManager.instance.OnPieceMoved += ShowNextTurnPanel;
        }

        private void OnDisable()
        {
            TileManager.instance.OnKingDead -= ShowGameOverPanel;
            TileManager.instance.OnPieceMoved -= ShowNextTurnPanel;
        }

        private void ShowGameOverPanel()
        {
            Instantiate(gameOverPanel, canvas.transform);
        }

        private void ShowNextTurnPanel(string tag)
        {
            nextPieceColor = (tag == "White") ? "black" : "white";

            StartCoroutine(NextTurn(nextPieceColor));
        }

        IEnumerator NextTurn(string pieceColor)
        {
            nextTurnPanel.SetActive(true);
            nextTurnPanel.GetComponentInChildren<Text>().text = string.Format("Next turn is {0} pieces", pieceColor);

            yield return new WaitForSeconds(timeForNextTurnPanel);
            nextTurnPanel.SetActive(false);
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
