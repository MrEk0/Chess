using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChessGame
{
    public enum Pieces
    {
        King, Queen, Bishop, Knight, Rook, Pawn
    };

    public abstract class ChessPiece : EventTrigger
    {
        protected bool selected = false;

        //public abstract List<Vector2Int> MoveDirections();
        public abstract void OnMouseDown();
        
    }
}
