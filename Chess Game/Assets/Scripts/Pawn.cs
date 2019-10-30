using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ChessGame
{
    public class Pawn : MonoBehaviour, IPointerClickHandler
    {
        //public Color selectedColor=new Color(0,0,0,1);

        //float height;
        float callsCount = 0;
        //TileManager tileManager;
        Vector2Int[] moveVectors = new Vector2Int[4] { new Vector2Int(-1, 1), new Vector2Int(0,1), new Vector2Int(1,1), new Vector2Int(0,2) };

        private void ShowDirections()
        {
            List<Vector2Int> moveDirections = new List<Vector2Int>();
            foreach (Vector2Int vector2Int in moveVectors)
            {
                moveDirections.Add(vector2Int);
            }

            if (callsCount == 0)
            {
                TileManager.instance.SelectTile(transform.position, moveDirections, gameObject);
                callsCount++;
            }
            else
            {
                moveDirections.RemoveAt(moveDirections.Count-1);
                TileManager.instance.SelectTile(transform.position, moveDirections, gameObject);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ShowDirections();      
        }
    }
}
