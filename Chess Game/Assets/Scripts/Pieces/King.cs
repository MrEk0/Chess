using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChessGame
{
    public class King : ChessPiece
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

        public override void ShowPossibleSteps()
        {
            for (int i = 0; i < moveVectors.Length; i++)
            {
                GameObject tile = TileManager.instance.GetStepTile(transform.position, moveVectors[i]);
                if (tile != null)
                {
                    CheckAttackSteps(tile);
                }
            }
        }

        public override void CheckAttackSteps(GameObject tile)
        {
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
