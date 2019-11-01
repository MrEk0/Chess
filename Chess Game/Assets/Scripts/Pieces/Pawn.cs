﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ChessGame
{
    public class Pawn : ChessPiece/*, IPointerClickHandler*/
    {
        public bool HasFirstCall { private get; set; } = false;
        Vector2Int[] moveVectors = new Vector2Int[4] { new Vector2Int(-1, 1), new Vector2Int(0,1),
            new Vector2Int(1,1), new Vector2Int(0,2) };

        private bool isPreviousTileTaken = false;

        private void Awake()
        {
            if(colorType==PieceColor.Black)
            {
                for(int i=0; i<moveVectors.Length; i++)
                {
                    moveVectors[i] *= -1;
                }
            }
        }

        public override void ShowPossibleSteps()
        {
            for(int i=0; i<moveVectors.Length; i++)
            {
                GameObject tile = TileManager.instance.GetStepTile(transform.position, moveVectors[i]);
                if(i==0 || i==2)
                {
                    CheckAttackSteps(tile);
                }
                else if(i==1)
                {
                    CheckForwardSteps(tile);
                }
                else
                {
                    CheckDoubleForwardStep(tile);
                }
            }
        }

        private void CheckDoubleForwardStep(GameObject tile)
        {
            if (!HasFirstCall)
            {
                GameObject firstTile = TileManager.instance.GetStepTile(transform.position, moveVectors[1]);

                if (!TileManager.instance.IsTileTaken(firstTile))
                {
                    CheckForwardSteps(tile);
                    //HasFirstCall = TileManager.instance.isPieceMoved();
                }
            }
        }

        private void CheckForwardSteps(GameObject tile)
        {
            if(!TileManager.instance.IsTileExist(tile))
            {
                return;
            }

            if (!TileManager.instance.IsTileTaken(tile))
            {
                TileManager.instance.ChangeTileColor(tile, Color.cyan);
                TileManager.instance.AddTileToMove(tile);
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
                if(TileManager.instance.IsPieceTheSameSide(tile, colorType))
                {
                    return;
                }

                TileManager.instance.ChangeTileColor(tile, Color.red);
                TileManager.instance.AddTileToMove(tile);
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            TileManager.instance.SelectPiece(gameObject);
            ShowPossibleSteps();
        }
    }
}