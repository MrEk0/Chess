using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChessGame
{
    public enum PieceColor
    {
        White, Black /*King, Queen, Bishop, Knight, Rook, Pawn*/
    };

    public abstract class ChessPiece : MonoBehaviour, IPointerClickHandler /*: EventTrigger*/
    {
        public PieceColor colorType;

        //protected Vector2Int[] moveVectors;
        public abstract void ShowPossibleSteps();

        public abstract void CheckAttackSteps(GameObject tile);

        public abstract void OnPointerClick(PointerEventData eventData);
        
    }
}
