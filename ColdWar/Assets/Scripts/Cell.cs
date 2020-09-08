using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    public Vector2Int position;
    public Unit standingUnit;
    public bool isOccupied;
    public bool isSelected;

    public Color color;
    public TextMeshProUGUI showName;
    public TextMeshProUGUI showHP;


    void OnMouseOver()
    {
        if (!isSelected)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (isOccupied)
        {
            showName.text = standingUnit.name;
            showHP.text = standingUnit.HP + "/" + standingUnit.maxHP;
            if(standingUnit.HP < standingUnit.maxHP/4)
            {
                showHP.color = Color.red;
            }
        }
    }

    void OnMouseExit()
    {
        if (!isSelected)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = color;
        }
        showName.text = " ";
        showHP.text = " ";
        showHP.color = Color.white;
    }

    public void select()
    {
        isSelected = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void deselect()
    {
        isSelected = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
