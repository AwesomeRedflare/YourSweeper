using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    public TileManager tileManager;

    public bool hasMine;

    private bool isClicked = false;
    private bool isFlagged = false;

    private int mineNeighbors;

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

        for (int i = 0; i < tileManager.tiles.Count; i++)
        {
            if ((Vector2)tileManager.tiles[i].transform.position == new Vector2(transform.position.x, transform.position.y + 1))
            { //Checks Up
                EvaluateNeighbor(i);
                neighbors.Add(tileManager.tiles[i]);
            }

            if ((Vector2)tileManager.tiles[i].transform.position == new Vector2(transform.position.x, transform.position.y - 1))
            { //Checks Down
                EvaluateNeighbor(i);
                neighbors.Add(tileManager.tiles[i]);
            }

            if ((Vector2)tileManager.tiles[i].transform.position == new Vector2(transform.position.x - 1, transform.position.y))
            { //Checks Left
                EvaluateNeighbor(i);
                neighbors.Add(tileManager.tiles[i]);
            }

            if ((Vector2)tileManager.tiles[i].transform.position == new Vector2(transform.position.x + 1, transform.position.y))
            { //Checks Right
                EvaluateNeighbor(i);
                neighbors.Add(tileManager.tiles[i]);
            }

            if ((Vector2)tileManager.tiles[i].transform.position == new Vector2(transform.position.x - 1, transform.position.y + 1))
            { //Checks UpLeft
                EvaluateNeighbor(i);
                neighbors.Add(tileManager.tiles[i]);
            }

            if ((Vector2)tileManager.tiles[i].transform.position == new Vector2(transform.position.x + 1, transform.position.y + 1))
            { //Checks UpRight
                EvaluateNeighbor(i);
                neighbors.Add(tileManager.tiles[i]);
            }

            if ((Vector2)tileManager.tiles[i].transform.position == new Vector2(transform.position.x - 1, transform.position.y - 1))
            { //Checks DownLeft
                EvaluateNeighbor(i);
                neighbors.Add(tileManager.tiles[i]);
            }

            if ((Vector2)tileManager.tiles[i].transform.position == new Vector2(transform.position.x + 1, transform.position.y - 1))
            { //Checks DownRight
                EvaluateNeighbor(i);
                neighbors.Add(tileManager.tiles[i]);
            }
        }

        if(mineNeighbors == 0)
        {
            for (int i = 0; i < neighbors.Count; i++)
            {
                if(neighbors[i].GetComponent<Mine>().isClicked == false)
                {
                    neighbors[i].GetComponent<Mine>().CheckNeighbors();
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
        if (tileManager.tiles[i].GetComponent<Mine>().hasMine == true)
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
