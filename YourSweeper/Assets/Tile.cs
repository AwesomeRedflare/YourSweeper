using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileManager tileManager;

    public bool hasMine;

    private bool isClicked = false;
    private bool isFlagged = false;

    private int mineNeighbors;
    public int id;

    public List<GameObject> neighbors;

    public Text number;

    private SpriteRenderer sp;

    private Color originalColor;
    public Color disabledColor;
    public Color flaggedColor;
    public Color mineColor;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        originalColor = sp.color;
    }

    public void CheckNeighbors()
    {
        isClicked = true;

        GetComponent<SpriteRenderer>().color = disabledColor;

        tileManager.amountOfTiles--;

        if (tileManager.amountOfTiles == 0)
        {
            Debug.Log("You win");
        }

        //Checks top-left tile
        if ((id - tileManager.mapSize.x - 1) >= 0 && (id - tileManager.mapSize.x - 1) < tileManager.tiles.Count && (Vector2)tileManager.tiles[id - (int)tileManager.mapSize.x - 1].transform.position == new Vector2(transform.position.x - 1, transform.position.y + 1))
        {
            Tile neighbor = tileManager.tiles[id - (int)tileManager.mapSize.x - 1].GetComponent<Tile>();

            EvaluateNeighbor(neighbor.id);
            neighbors.Add(neighbor.gameObject);
        }

        //Checks top tile
        if ((id - tileManager.mapSize.x) >= 0 && (id - tileManager.mapSize.x) < tileManager.tiles.Count && (Vector2)tileManager.tiles[id - (int)tileManager.mapSize.x].transform.position == new Vector2(transform.position.x, transform.position.y + 1))
        {
            Tile neighbor = tileManager.tiles[id - (int)tileManager.mapSize.x].GetComponent<Tile>();

            EvaluateNeighbor(neighbor.id);
            neighbors.Add(neighbor.gameObject);
        }

        //Checks top-right tile
        if ((id - tileManager.mapSize.x + 1) >= 0 && (id - tileManager.mapSize.x + 1) < tileManager.tiles.Count && (Vector2)tileManager.tiles[id - (int)tileManager.mapSize.x + 1].transform.position == new Vector2(transform.position.x + 1, transform.position.y + 1))
        {
            Tile neighbor = tileManager.tiles[id - (int)tileManager.mapSize.x + 1].GetComponent<Tile>();

            EvaluateNeighbor(neighbor.id);
            neighbors.Add(neighbor.gameObject);
        }

        //Checks left tile
        if ((id - 1) >= 0 && (id -  1) < tileManager.tiles.Count && (Vector2)tileManager.tiles[id - 1].transform.position == new Vector2(transform.position.x - 1, transform.position.y))
        {
            Tile neighbor = tileManager.tiles[id - 1].GetComponent<Tile>();

            EvaluateNeighbor(neighbor.id);
            neighbors.Add(neighbor.gameObject);
        }

        //Checks right tile
        if ((id + 1) >= 0 && (id + 1) < tileManager.tiles.Count && (Vector2)tileManager.tiles[id + 1].transform.position == new Vector2(transform.position.x + 1, transform.position.y))
        {
            Tile neighbor = tileManager.tiles[id + 1].GetComponent<Tile>();

            EvaluateNeighbor(neighbor.id);
            neighbors.Add(neighbor.gameObject);
        }

        //Checks bottom-left tile
        if ((id + tileManager.mapSize.x - 1) >= 0 && (id + tileManager.mapSize.x - 1) < tileManager.tiles.Count && (Vector2)tileManager.tiles[id + (int)tileManager.mapSize.x - 1].transform.position == new Vector2(transform.position.x - 1, transform.position.y - 1))
        {
            Tile neighbor = tileManager.tiles[id + (int)tileManager.mapSize.x - 1].GetComponent<Tile>();

            EvaluateNeighbor(neighbor.id);
            neighbors.Add(neighbor.gameObject);
        }

        //Checks bottom tile
        if ((id + tileManager.mapSize.x) >= 0 && (id + tileManager.mapSize.x) < tileManager.tiles.Count && (Vector2)tileManager.tiles[id + (int)tileManager.mapSize.x].transform.position == new Vector2(transform.position.x, transform.position.y - 1))
        {
            Tile neighbor = tileManager.tiles[id + (int)tileManager.mapSize.x].GetComponent<Tile>();

            EvaluateNeighbor(neighbor.id);
            neighbors.Add(neighbor.gameObject);
        }

        //Checks bottom-right tile
        if ((id + tileManager.mapSize.x + 1) >= 0 && (id + tileManager.mapSize.x + 1) < tileManager.tiles.Count && (Vector2)tileManager.tiles[id + (int)tileManager.mapSize.x + 1].transform.position == new Vector2(transform.position.x + 1, transform.position.y - 1))
        {
            Tile neighbor = tileManager.tiles[id + (int)tileManager.mapSize.x + 1].GetComponent<Tile>();

            EvaluateNeighbor(neighbor.id);
            neighbors.Add(neighbor.gameObject);
        }

        if (mineNeighbors == 0)
        {
            for (int i = 0; i < neighbors.Count; i++)
            {
                Debug.Log("check");

                if (neighbors[i].GetComponent<Tile>().isClicked == false)
                {
                    neighbors[i].GetComponent<Tile>().CheckNeighbors();
                }
            }
        }
        else
        {
            number.text = mineNeighbors.ToString();
        }
    }

    void EvaluateNeighbor(int i)
    {
        if (tileManager.tiles[i].GetComponent<Tile>().hasMine == true)
        {
            mineNeighbors++;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isClicked == false && isFlagged == false)
        {
            if (hasMine == true)
            {
                Debug.Log("has mine");
                sp.color = mineColor;
                return;
            }

            CheckNeighbors();
        }

        if (Input.GetMouseButtonDown(1) && isClicked == false && sp.color != mineColor)
        {
            if (isFlagged == false)
            {
                isFlagged = true;

                sp.color = flaggedColor;

                return;
            }

            if (isFlagged == true)
            {
                isFlagged = false;

                sp.color = originalColor;
            }
        }
    }
}
