using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler/*, IPointerEnterHandler, IPointerExitHandler*/
{
    [SerializeField] Color selectedColor;

    public bool IsTaken { get; set; } = false;
    //public bool IsSelected { get; set; } = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        //IsSelected = true;
        TileManager.instance.TileToMove(gameObject);
    }

}
