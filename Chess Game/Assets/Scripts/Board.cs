using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChessGame
{
    public class Board : MonoBehaviour
    {
        [SerializeField] GameObject lightTile = null;
        [SerializeField] GameObject darkTile = null;
        [SerializeField] RectTransform board = null;

        [Header("White Pieces")]
        [SerializeField] GameObject w_KingPrefab = null;
        [SerializeField] GameObject w_QueenPrefab = null;
        [SerializeField] GameObject w_BishopPrefab = null;
        [SerializeField] GameObject w_KnightPrefab = null;
        [SerializeField] GameObject w_RookPrefab = null;
        [SerializeField] GameObject w_PawnPrefab = null;

        [Header("Black Pieces")]
        [SerializeField] GameObject b_KingPrefab = null;
        [SerializeField] GameObject b_QueenPrefab = null;
        [SerializeField] GameObject b_BishopPrefab = null;
        [SerializeField] GameObject b_KnightPrefab = null;
        [SerializeField] GameObject b_RookPrefab = null;
        [SerializeField] GameObject b_PawnPrefab = null;

        GameObject currentTile;
        float offset;
        int numberOfRowsAndColumns = 8;

        private List<Vector3> spawnPositions;

        private void Awake()
        {
            offset = lightTile.GetComponent<RectTransform>().rect.height;
            spawnPositions = new List<Vector3>();
        }

        // Start is called before the first frame update
        void Start()
        {
            CreateBoard();
            SetupPiece();
        }

        private void CreateBoard()
        {
            for (int y = 0; y < numberOfRowsAndColumns; y++)
            {
                for (int x = 0; x < numberOfRowsAndColumns; x++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        currentTile = darkTile;
                    }
                    else
                    {
                        currentTile = lightTile;
                    }

                    Vector3 spawnPosition = new Vector3(board.position.x + offset * x, board.position.y + offset * y);
                    GameObject boardTile = Instantiate(currentTile, spawnPosition, Quaternion.identity, board);
                    spawnPositions.Add(spawnPosition);

                    TileManager.instance.AddTile(spawnPosition, boardTile);
                }
            }
        }

        private void SetupPiece()
        {
            for (int i = 0; i < 2 * numberOfRowsAndColumns; i++)
            {
                TileManager.instance.AddTakenTile(spawnPositions[i], PieceColor.White);

                switch (i)
                {
                    case 0:
                    case 7:
                        Instantiate(w_RookPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    case 1:
                    case 6:
                        Instantiate(w_KnightPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    case 2:
                    case 5:
                        Instantiate(w_BishopPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    case 3:
                        Instantiate(w_QueenPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    case 4:
                        Instantiate(w_KingPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    default:
                        Instantiate(w_PawnPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                }
            }

            int startNumberForBlack = spawnPositions.Count - 2 * numberOfRowsAndColumns;

            for (int i = startNumberForBlack; i < spawnPositions.Count; i++)
            {
                TileManager.instance.AddTakenTile(spawnPositions[i], PieceColor.Black);

                switch (i)
                {
                    case 63:
                    case 56:
                        Instantiate(b_RookPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    case 62:
                    case 57:
                        Instantiate(b_KnightPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    case 61:
                    case 58:
                        Instantiate(b_BishopPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    case 59:
                        Instantiate(b_QueenPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    case 60:
                        Instantiate(b_KingPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                    default:
                        Instantiate(b_PawnPrefab, spawnPositions[i], Quaternion.identity, board);
                        break;
                }
            }
        }
    }
}
