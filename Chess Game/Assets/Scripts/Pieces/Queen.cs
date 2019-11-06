using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChessGame
{
    public class Queen : ChessPiece
    {
        Vector2Int[] moveVectors = new Vector2Int[8]
       {
            new Vector2Int(1,1),
            new Vector2Int(-1,1),
            new Vector2Int(1,-1),
            new Vector2Int(-1,-1),
            new Vector2Int(0,1),
            new Vector2Int(0,-1),
            new Vector2Int(-1,0),
            new Vector2Int(1,0),
       };

        private bool isPreviousTileTaken = false;

        public override void ShowPossibleSteps()
        {
            for (int i = 0; i < moveVectors.Length; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    GameObject tile = TileManager.instance.GetStepTile(transform.localPosition, moveVectors[i] * j);
                    if (tile != null)
                    {
                        CheckAttackSteps(tile);
                    }
                }
                isPreviousTileTaken = false;
            }
        }

        public override void CheckAttackSteps(GameObject tile)
        {
            if (!isPreviousTileTaken)
            {
                if (TileManager.instance.IsTileTaken(tile))
                {
                    isPreviousTileTaken = true;

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
}
