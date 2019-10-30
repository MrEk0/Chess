using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;

    [SerializeField] GameObject tilePrefab = null;

    Dictionary<Vector3, GameObject> tiles=new Dictionary<Vector3, GameObject>();
    List<GameObject> possibleTiles;
    List<GameObject> takenTiles;
    //private bool isTileSelected = false;
    GameObject selectedPiece;
    Vector3 targetPoint;

    float height;
    private void Awake()
    {
        height = tilePrefab.GetComponent<RectTransform>().rect.height;
        takenTiles = new List<GameObject>();
    }

    public void AddTile(Vector3 position, GameObject tile)
    {
        tiles.Add(position, tile);
    }

    public void SetTileTaken(Vector3 position)
    {
        GameObject tile = tiles[position];
        tile.GetComponent<Tile>().IsTaken = true;
        takenTiles.Add(tile);
        //print(takenTiles.Count);
    }

    public GameObject GetTiles(Vector3 position)
    {
        GameObject tile = tiles[position];
        
        return tile;
    }

    public Vector3[] GetTilePositions()
    {
        return tiles.Keys.ToArray();
    }

    public void SelectTile(Vector3 tilePosition, List<Vector2Int> moveDirections, GameObject piece)
    {
        if (selectedPiece != null)
        {
            CleanDirections();
        }   

        selectedPiece = piece;
        possibleTiles = new List<GameObject>();
        List<Vector3> positions = new List<Vector3>();

        foreach(Vector2Int vector2Int in moveDirections)
        {
            positions.Add(new Vector3(tilePosition.x + height * vector2Int.x, tilePosition.y + height * vector2Int.y));
        }

        foreach (Vector3 position in positions)
        {
            if (tiles.ContainsKey(position))
            {
                possibleTiles.Add(tiles[position]);
                //tiles[position].GetComponent<Image>().color = Color.blue;
            }
        }

        foreach (GameObject tile in possibleTiles)
        {
            if(takenTiles.Contains(tile))
            {
                tile.GetComponent<Image>().color = Color.red;
            }
            else
            {
                tile.GetComponent<Image>().color = Color.cyan;
            }
        }
    }

    public void TileToMove(GameObject tile)
    {
        if(possibleTiles!=null)
        {
            foreach(GameObject obj in possibleTiles)
            {
                if(obj==tile)
                {
                    targetPoint = tile.transform.position;
                    selectedPiece.transform.position = targetPoint;
                    takenTiles.Remove(selectedPiece);
                    takenTiles.Add(tile);
                    CleanDirections();
                }
            }
        }
    }

    public void CleanDirections()
    {
        foreach (GameObject tile in possibleTiles)
        {
            tile.GetComponent<Image>().color = Color.white;
        }
        selectedPiece = null;
        possibleTiles.Clear();
    }
}
