using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChessGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject lightTile = null;
        [SerializeField] GameObject darkTile = null;
        [SerializeField] RectTransform board = null;
        //[SerializeField] GameObject piece = null;

        [SerializeField] List<GameObject> whitePieces = null;
        [SerializeField] List<GameObject> blackPieces = null;

        GameObject currentTile;
        float offset;
        int numberOfRowColumn = 8;

        List<GameObject> boardTiles;
        //Dictionary<Vector3, GameObject> cells = new Dictionary<Vector3, GameObject>();//cells
        TileManager tileManager;

        Dictionary<string, GameObject> whitePiecesDict = new Dictionary<string, GameObject>();
        Dictionary<string, GameObject> blackPiecesDict = new Dictionary<string, GameObject>();

        private void Awake()
        {
            offset = lightTile.GetComponent<RectTransform>().rect.height;
            boardTiles = new List<GameObject>();
            tileManager = FindObjectOfType<TileManager>();

        }

        // Start is called before the first frame update
        void Start()
        {
            CreateBoard();
            SortPieces(whitePieces, whitePiecesDict);
            SortPieces(blackPieces, blackPiecesDict);
            SetupPiece();
        }

        private void CreateBoard()
        {
            for (int y = 0; y < numberOfRowColumn; y++)
            {
                for (int x = 0; x < numberOfRowColumn; x++)
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

                    boardTiles.Add(boardTile);
                    tileManager.AddTile(spawnPosition, boardTile);
                    //cells.Add(spawnPosition, boardTile);
                }
            }
        }

        private void SetupPiece()
        {
            Vector3[] tilePositions = tileManager.GetTilePositions();

            for (int i=0; i<2*numberOfRowColumn; i++)
            {
                InstallPiecesOnBoard(i, whitePiecesDict, tilePositions[i]);
                tileManager.SetTileTaken(tilePositions[i]);
            }
          

            for (int i = 0; i < numberOfRowColumn; i++)
            {
                InstallPiecesOnBoard(i, blackPiecesDict, tilePositions[tilePositions.Length - numberOfRowColumn + i]);
                tileManager.SetTileTaken(tilePositions[tilePositions.Length - numberOfRowColumn + i]);
            }

            for (int i = 0; i < numberOfRowColumn; i++)
            {
                InstallPiecesOnBoard(i + numberOfRowColumn, blackPiecesDict,
                    tilePositions[tilePositions.Length - 2*numberOfRowColumn + i]);
                tileManager.SetTileTaken(tilePositions[tilePositions.Length - 2 * numberOfRowColumn + i]);
            }
        }

        private void InstallPiecesOnBoard(int positionIndex, Dictionary<string, GameObject> pieceDict, Vector3 installPos)
        {
            switch (positionIndex)
            {
                case 0:
                case 7:
                    Instantiate(pieceDict["Rook"], installPos, Quaternion.identity, board);
                    break;
                case 1:
                case 6:
                    Instantiate(pieceDict["Knight"], installPos, Quaternion.identity, board);
                    break;
                case 2:
                case 5:
                    Instantiate(pieceDict["Bishop"], installPos, Quaternion.identity, board);
                    break;
                case 3:
                    Instantiate(pieceDict["Queen"], installPos, Quaternion.identity, board);
                    break;
                case 4:
                    Instantiate(pieceDict["King"], installPos, Quaternion.identity, board);
                    break;
                default:
                    Instantiate(pieceDict["Pawn"], installPos, Quaternion.identity, board);
                    break;
            }
        }

        private void SortPieces(List<GameObject> listOfPieces, Dictionary<string, GameObject> sortedList)
        {
            foreach (GameObject obj in listOfPieces)
            {
                string name = obj.name;

                if (name.Contains("Pawn"))
                {
                    sortedList.Add("Pawn", obj);
                }
                else if (name.Contains("King"))
                {
                    sortedList.Add("King", obj);
                }
                else if (name.Contains("Queen"))
                {
                    sortedList.Add("Queen", obj);
                }
                else if (name.Contains("Bishop"))
                {
                    sortedList.Add("Bishop", obj);
                }
                else if (name.Contains("Rook"))
                {
                    sortedList.Add("Rook", obj);
                }
                else if (name.Contains("Knight"))
                {
                    sortedList.Add("Knight", obj);
                }
            }
        }

        //public Dictionary<Vector3, GameObject> GetCells()
        //{
        //    return cells;
        //}
    }
}
