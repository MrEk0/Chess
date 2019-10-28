using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject lightTile = null;
    [SerializeField] GameObject darkTile = null;
    [SerializeField] RectTransform board = null;
    [SerializeField] GameObject piece = null;

    [SerializeField] List<Sprite> whitePieces = null;
    [SerializeField] List<Sprite> blackPieces = null;
    //[SerializeField] Sprite whitePawn = null;
    //[SerializeField] Sprite darkPawn = null;

    //[SerializeField] Sprite whiteKing = null;
    //[SerializeField] Sprite whiteQueen = null;
    //[SerializeField] Sprite whiteBishop = null;
    //[SerializeField] Sprite whiteKnight = null;
    //[SerializeField] Sprite whiteRook = null;

    //[SerializeField] Sprite darkKing = null;
    //[SerializeField] Sprite darkQueen = null;
    //[SerializeField] Sprite darkBishop = null;
    //[SerializeField] Sprite darkKnight = null;
    //[SerializeField] Sprite darkRook = null;

    GameObject currentTile;
    float offset;
    int numberOfRowColumn = 8;

    List<GameObject> boardTiles;
    Dictionary<string, Sprite> whitePiecesDict = new Dictionary<string, Sprite>();
    Dictionary<string, Sprite> blackPiecesDict = new Dictionary<string, Sprite>();

    private void Awake()
    {
        offset = lightTile.GetComponent<RectTransform>().rect.height;
        boardTiles = new List<GameObject>();

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
        for(int y=0; y<numberOfRowColumn; y++)
        {
            for(int x=0; x<numberOfRowColumn; x++)
            {
                if ((x + y) % 2 == 0)
                {
                    currentTile = darkTile;
                }
                else
                {
                    currentTile = lightTile;
                }

                GameObject boardTile = Instantiate(currentTile, new Vector3(board.position.x + offset * x,
                    board.position.y + offset * y, 0),
                    Quaternion.identity, board);

                boardTiles.Add(boardTile);
            }
        }
    }

    private void SetupPiece()
    {
        for (int i = 0; i < 2 * numberOfRowColumn; i++)
        {
            InstallPiecesOnBoard(i, whitePiecesDict, boardTiles[i].transform.position);
        }

        for (int i = 0; i < numberOfRowColumn; i++)
        {
            InstallPiecesOnBoard(i, blackPiecesDict, boardTiles[boardTiles.Count-numberOfRowColumn+i].transform.position);
        }

        for (int i = 0; i < numberOfRowColumn; i++)
        {
            InstallPiecesOnBoard(i+numberOfRowColumn, blackPiecesDict, boardTiles[boardTiles.Count - 2*numberOfRowColumn + i].transform.position);
        }

        //int blackPieceSpawnIndex;
        //if (i < numberOfRowColumn)
        //{
        //    blackPieceSpawnIndex = boardTiles.Count - numberOfRowColumn + i;
        //}
        //else
        //{
        //    blackPieceSpawnIndex = boardTiles.Count - numberOfRowColumn - i;
        //}
        //InstallPiecesOnBoard(i, blackPiecesDict, boardTiles[blackPieceSpawnIndex].transform.position);


    }

    private void InstallPiecesOnBoard(int positionIndex, Dictionary<string, Sprite> pieceDict, Vector3 installPos)
    {
        GameObject chessPiece = Instantiate(piece, installPos, Quaternion.identity, board);
        switch (positionIndex)
        {
            case 0:
            case 7:
                chessPiece.GetComponent<Image>().sprite = pieceDict["Rook"];
                break;
            case 1:
            case 6:
                chessPiece.GetComponent<Image>().sprite = pieceDict["Knight"];
                break;
            case 2:
            case 5:
                chessPiece.GetComponent<Image>().sprite = pieceDict["Bishop"];
                break;
            case 3:
                chessPiece.GetComponent<Image>().sprite = pieceDict["Queen"];
                break;
            case 4:
                chessPiece.GetComponent<Image>().sprite = pieceDict["King"];
                break;
            default:
                chessPiece.GetComponent<Image>().sprite = pieceDict["Pawn"];
                break;
        }
    }

    private void SortPieces(List<Sprite> listOfPieces, Dictionary<string, Sprite> sortedList)
    {
        foreach (Sprite sprite in listOfPieces)
        {
            string name = sprite.name;
            
            if(name.Contains("Pawn"))
            {
                sortedList.Add("Pawn", sprite);
            }
            else if (name.Contains("King"))
            {
                sortedList.Add("King", sprite);
            }
            else if (name.Contains("Queen"))
            {
                sortedList.Add("Queen", sprite);
            }
            else if (name.Contains("Bishop"))
            {
                sortedList.Add("Bishop", sprite);
            }
            else if (name.Contains("Rook"))
            {
                sortedList.Add("Rook", sprite);
            }
            else if (name.Contains("Knight"))
            {
                sortedList.Add("Knight", sprite);
            }
        }
    }
}
