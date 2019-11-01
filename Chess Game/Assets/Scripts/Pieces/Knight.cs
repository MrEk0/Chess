using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChessGame
{
    public class Knight:ChessPiece
    {
        Vector2Int[] moveVectors = new Vector2Int[8]
        {
            new Vector2Int(1,2),
            new Vector2Int(-1,2),
            new Vector2Int(1,-2),
            new Vector2Int(-1,-2),
            new Vector2Int(2,1),
            new Vector2Int(2,-1),
            new Vector2Int(-2,1),
            new Vector2Int(-2,-1),
        };   

        //public override void OnPointerClick(PointerEventData eventData)
        //{
        //    TileManager.instance.SelectPiece(gameObject);
        //    ShowPossibleSteps();
        //}

        public override void ShowPossibleSteps()
        {
            for (int i = 0; i < moveVectors.Length; i++)
            {
                GameObject tile = TileManager.instance.GetStepTile(transform.position, moveVectors[i]);
                CheckAttackSteps(tile);
            }
        }

        public override void CheckAttackSteps(GameObject tile)
        {
            if (!TileManager.instance.IsTileExist(tile))
            {
                return;
            }

            if (TileManager.instance.IsTileTaken(tile))
            {
                if (TileManager.instance.IsPieceTheSameSide(tile, colorType))
                {
                    return;
                }

                TileManager.instance.ChangeTileColor(tile, Color.red);
                TileManager.instance.AddTileToMove(tile);
            }
            else
            {
                TileManager.instance.ChangeTileColor(tile, Color.cyan);
                TileManager.instance.AddTileToMove(tile);
            }
        }
    }
}