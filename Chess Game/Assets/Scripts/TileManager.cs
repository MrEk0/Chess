using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace ChessGame
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager instance;

        [SerializeField] GameObject tilePrefab = null;

        Dictionary<Vector3, GameObject> tiles = new Dictionary<Vector3, GameObject>();
        Dictionary<GameObject, PieceColor> takenTilesDict = new Dictionary<GameObject, PieceColor>();
        List<GameObject> tilesToMove;     
        GameObject selectedPiece;

        float height;

        public PieceColor NextTurnColor { get; set; } = PieceColor.White;

        private void Awake()
        {
            instance = this;

            height = tilePrefab.GetComponent<RectTransform>().rect.height;
            tilesToMove = new List<GameObject>();
        }

        public void AddTile(Vector3 position, GameObject tile)
        {
            tiles.Add(position, tile);
        }

        public void AddTakenTile(Vector3 position, PieceColor pieceColor)
        {
            GameObject tile = tiles[position];
            takenTilesDict.Add(tile, pieceColor);
        }

        public bool IsTileTaken(GameObject tile)
        {
            if(takenTilesDict.ContainsKey(tile))
            {
                return true;
            }

            return false;
        }

        public GameObject GetStepTile(Vector3 position, Vector2Int step)
        {
            Vector3 stepPos = new Vector3(position.x + height * step.x, position.y + height * step.y);
            if (tiles.ContainsKey(stepPos))
            {
                GameObject tile = tiles[stepPos];
                return tile;
            }

            return null;
        }

        public void AddTileToMove(GameObject tile)
        {
            tilesToMove.Add(tile);
        }
        public void ChangeTileColor(GameObject tile, Color color)
        {
            tile.GetComponent<Image>().color = color;
        }

        public void SelectPiece(GameObject piece)
        {
            if (selectedPiece != null)
            {
                TryToEat(piece);
                ClearMoveTiles();
            }

            selectedPiece = piece;
        }

        private void TryToEat(GameObject piece)
        {
            GameObject tileUnderPiece = tiles[piece.transform.position];

            if (tilesToMove.Contains(tileUnderPiece))
            {
                takenTilesDict.Remove(tileUnderPiece);
                Destroy(piece);
                MovePiece(tileUnderPiece);
            }
        }

        public void MovePiece(GameObject clickedTile)
        {
            if (tilesToMove.Contains(clickedTile))
            {
                GameObject targetTile = tilesToMove.First(tile => tile == clickedTile);

                GameObject tileToRemove = tiles[selectedPiece.transform.position];
                selectedPiece.transform.position = targetTile.transform.position;

                if (selectedPiece.GetComponent<Pawn>() != null)
                {
                    selectedPiece.GetComponent<Pawn>().isMoved = true;
                }

                takenTilesDict.Remove(tileToRemove);
                takenTilesDict.Add(targetTile, selectedPiece.GetComponent<ChessPiece>().colorType);

                if (selectedPiece.GetComponent<ChessPiece>().colorType == PieceColor.White)
                    NextTurnColor = PieceColor.Black;
                else
                    NextTurnColor = PieceColor.White;

                //ClearMoveTiles();
            }
            //else
            //{
            ClearMoveTiles();
            //}
        }

        public void ClearMoveTiles()
        {
            selectedPiece = null;
            foreach (GameObject tile in tilesToMove)
            {
                ChangeTileColor(tile, Color.white);
            }
            tilesToMove.Clear();
        }

        public bool IsPieceTheSameSide(GameObject tile, PieceColor pieceColor)
        {
            if (takenTilesDict[tile]==pieceColor)
            {
                return true;
            }

            return false;
        }
    }
}
