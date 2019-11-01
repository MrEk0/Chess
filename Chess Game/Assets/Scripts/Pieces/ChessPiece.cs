using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChessGame
{
    public enum PieceColor
    {
        White, Black
    };

    public abstract class ChessPiece : MonoBehaviour, IPointerClickHandler
    {
        public PieceColor colorType;

        protected bool isSelected = false;
        public abstract void ShowPossibleSteps();

        public abstract void CheckAttackSteps(GameObject tile);

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            ShowPossibleSteps();
            TileManager.instance.SelectPiece(gameObject);
        }
        
    }
}
