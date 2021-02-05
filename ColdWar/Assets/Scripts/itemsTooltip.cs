using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class itemsTooltip : MonoBehaviour
{
    public GameObject smallTolltip;

    public Image image;

    public float time;
    public Item itemHeld;
    

    public string whatItem;

    public void getItem()
    {
        this.gameObject.SetActive(true);
        switch (whatItem)
        {
            //case "armor": itemHeld = BattleManager.instance.activeUnit.armor; image.sprite = itemHeld.sprite; break;
            //case "mainWeapon": itemHeld = BattleManager.instance.activeUnit.mainWeapon; image.sprite = itemHeld.sprite; break;
            //case "secondaryWeapon": itemHeld = BattleManager.instance.activeUnit.secondaryWeapon; image.sprite = itemHeld.sprite; break;
        }
    }

    public void noItem()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        //getItem();
    }

    public IEnumerator tooltipShow()
    {
        yield return new WaitForSeconds(time);
        smallTolltip.GetComponentInChildren<TextMeshProUGUI>().text = itemHeld.name;
        smallTolltip.transform.position = Input.mousePosition;
        smallTolltip.SetActive(true);
    }

    public void show()
    {
        StartCoroutine(tooltipShow());
    }

    public IEnumerator hide()
    {
        yield return new WaitForSeconds(0.5f);
        smallTolltip.SetActive(false);
    }

    public void broke()
    {
        StopAllCoroutines();StartCoroutine(hide());
    }
}
