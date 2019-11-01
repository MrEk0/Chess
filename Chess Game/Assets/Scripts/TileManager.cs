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

        List<GameObject> tilesToMove;
        Dictionary<GameObject, PieceColor> takenTilesDict = new Dictionary<GameObject, PieceColor>();

        GameObject selectedPiece;
        GameObject previousSelectedPiece;

        public bool isPieceMoved { get; private set; } = false;

        float height;
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

        public void AddTileTaken(Vector3 position, PieceColor pieceColor)
        {
            GameObject tile = tiles[position];
            takenTilesDict.Add(tile, pieceColor);
        }

        public Vector3[] GetTilePositions()
        {
            return tiles.Keys.ToArray();
        }

        public bool IsTileTaken(GameObject tile)//new important
        {
            if(takenTilesDict.ContainsKey(tile))
            {
                return true;
            }

            return false;
        }

        public bool IsTileExist(GameObject tile)//new
        {
            if (tiles.ContainsValue(tile))
            {
                return true;
            }
            return false;
        }

        public GameObject GetStepTile(Vector3 position, Vector2Int step)//new important
        {
            Vector3 stepPos = new Vector3(position.x + height * step.x, position.y + height * step.y);
            if (tiles.ContainsKey(stepPos))
            {
                GameObject tile = tiles[stepPos];
                return tile;
            }

            return null;
        }

        public void AddTileToMove(GameObject tile)//new
        {
            tilesToMove.Add(tile);
        }

        public void SelectPiece(GameObject piece)//new
        {
            if (selectedPiece != null)
            {
                EatPiece(piece);
                ClearMoveTiles();
            }

            selectedPiece = piece;
        }

        public void MovePiece(GameObject clickedTile)//new
        {
            if (tilesToMove.Contains(clickedTile))
            {
                GameObject targetTile = tilesToMove.First(tile => tile == clickedTile);

                GameObject tileToRemove = tiles[selectedPiece.transform.position];
                selectedPiece.transform.position = targetTile.transform.position;

                //isPieceMoved();//
                //isPieceMoved = true;
                if(selectedPiece.GetComponent<Pawn>()!=null)
                {
                    selectedPiece.GetComponent<Pawn>().HasFirstCall = true;
                }

                takenTilesDict.Remove(tileToRemove);
                takenTilesDict.Add(targetTile, selectedPiece.GetComponent<ChessPiece>().colorType);

                ClearMoveTiles();
            }
        }

        private void EatPiece(GameObject piece)//new
        {
            GameObject tileUnderPiece = tiles[piece.transform.position];

            if (tilesToMove.Contains(tileUnderPiece))
            {
                Destroy(piece);
                MovePiece(tileUnderPiece);
            }
        }

        private void ClearMoveTiles()//new
        {
            selectedPiece = null;
            foreach (GameObject tile in tilesToMove)
            {
                ChangeTileColor(tile, Color.white);
            }
            tilesToMove.Clear();
        }

        public void ChangeTileColor(GameObject tile, Color color)//new important
        {
            tile.GetComponent<Image>().color = color;
        }

        public bool IsPieceTheSameSide(GameObject tile, PieceColor pieceColor)
        {
            if (takenTilesDict[tile]==pieceColor)
            {
                return true;
            }

            return false;
        }

        //public bool isPieceMoved()
        //{
        //    return isMoved;
        //}
    }
}
