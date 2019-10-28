using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject lightTile = null;
    [SerializeField] GameObject darkTile = null;

    GameObject currentTile;
    float offset;

    private void Awake()
    {
        offset = lightTile.GetComponent<RectTransform>().rect.height;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
    }

    private void CreateBoard()
    {
        for(int x=0; x<8; x++)
        {
            for(int y=0; y<8; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    currentTile = darkTile;
                }
                else
                {
                    currentTile = lightTile;
                }
                GameObject boardTile = Instantiate(currentTile, new Vector3(transform.position.x + offset * x,
                    transform.position.y + offset * y, 0),
                    Quaternion.identity, transform);
            }
        }
    }
}
