using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tile;

    public int amountOfMines;
    public int amountOfTiles;

    public Vector2 mapSize;

    public List<GameObject> tiles;

    private void Start()
    {
        //Camera.main.orthographicSize = (mapSize.x / 2) + 1;

        amountOfTiles = ((int)mapSize.x * (int)mapSize.y) - amountOfMines;

        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                Vector2 tilePos = new Vector2((-mapSize.x / 2) + x, (mapSize.y / 2) - y);

                GameObject newTile = Instantiate(tile, tilePos, Quaternion.identity);

                tiles.Add(newTile);

                newTile.GetComponent<Tile>().tileManager = GetComponent<TileManager>();
                newTile.GetComponent<Tile>().id = tiles.Count -1;
            }
        }

        for (int i = 0; i < amountOfMines;)
        {
            int num = Random.Range(0, (int)mapSize.x * (int)mapSize.y);

            if(tiles[num].GetComponent<Tile>().hasMine == false)
            {
                tiles[num].GetComponent<Tile>().hasMine = true;

                i++;
            }
        }
    }
}
