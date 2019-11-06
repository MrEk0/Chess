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
        [SerializeField] RectTransform startPos = null;//fdsfsf
        [SerializeField] float timeToSpawn = 0.3f;

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
        RectTransform rectTransform;//new
        Canvas canvas;
        float offset;
        int numberOfRowsAndColumns = 8;

        private List<Vector3> spawnPositions;

        private void Awake()
        {
            offset = lightTile.GetComponent<RectTransform>().rect.height;
            rectTransform = GetComponent<RectTransform>();
            spawnPositions = new List<Vector3>();
            canvas = GetComponentInParent<Canvas>();//new
        }

        // Start is called before the first frame update
        void Start()
        {
            CreateBoard();
            StartCoroutine(SetUpWhitePieces());
            StartCoroutine(SetUpBlackPieces());
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

                    //Vector3 spawnPosition = new Vector3(startPos.position.x + offset * x, startPos.position.y + offset * y);
                    Vector3 spawnPosition = new Vector3(startPos.localPosition.x + offset * x,
                        startPos.localPosition.y + offset * y);//
                    //print(spawnPosition);
                    GameObject boardTile = Instantiate(currentTile, spawnPosition, Quaternion.identity/*, rectTransform*/);
                    boardTile.transform.SetParent(transform, false);
                    
                    //boardTile.transform.localPosition = spawnPosition;//
                    spawnPositions.Add(spawnPosition);

                    TileManager.instance.AddTile(spawnPosition, boardTile);
                }
            }
        }

        private IEnumerator SetUpWhitePieces()
        {
            GameObject chessPiece;

            for (int i = 0; i < 2 * numberOfRowsAndColumns; i++)
            {
                TileManager.instance.AddTakenTile(spawnPositions[i], PieceColor.White);
                yield return new WaitForSeconds(timeToSpawn);

                switch (i)
                {
                    case 0:
                    case 7:
                        chessPiece=Instantiate(w_RookPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    case 1:
                    case 6:
                        chessPiece = Instantiate(w_KnightPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    case 2:
                    case 5:
                        chessPiece = Instantiate(w_BishopPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    case 3:
                        chessPiece = Instantiate(w_QueenPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    case 4:
                        chessPiece = Instantiate(w_KingPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    default:
                        chessPiece = Instantiate(w_PawnPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                }
                //chessPiece.transform.SetParent(transform, false);
                chessPiece.transform.localPosition = spawnPositions[i];
            }
        }

        private IEnumerator SetUpBlackPieces()
        {
            int startNumberForBlack = spawnPositions.Count - 2 * numberOfRowsAndColumns;
            GameObject chessPiece;

            for (int i = startNumberForBlack; i < spawnPositions.Count; i++)
            {
                TileManager.instance.AddTakenTile(spawnPositions[i], PieceColor.Black);
                yield return new WaitForSeconds(timeToSpawn);

                switch (i)
                {
                    case 63:
                    case 56:
                        chessPiece=Instantiate(b_RookPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    case 62:
                    case 57:
                        chessPiece=Instantiate(b_KnightPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    case 61:
                    case 58:
                        chessPiece=Instantiate(b_BishopPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    case 59:
                        chessPiece=Instantiate(b_QueenPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    case 60:
                        chessPiece=Instantiate(b_KingPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                    default:
                        chessPiece=Instantiate(b_PawnPrefab, spawnPositions[i], Quaternion.identity, transform);
                        break;
                }
                chessPiece.transform.localPosition = spawnPositions[i];
            }
        }


    }
}
