using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    void Awake()
    {
        instance = this;
    }

    public List<Unit> engagedUnits = new List<Unit>();
    public List<Unit> engagedUnitsWaiting = new List<Unit>();
    public List<Unit> engagedUnitsActivated = new List<Unit>();
    public List<Unit> playerUnits = new List<Unit>();
    public int playersKilled;
    public List<Unit> enemyUnits = new List<Unit>();
    public int enemyKilled;
    public Unit activeUnit;
    public int activeUnitID;
    public Cell[] battlefieldCells;
    public Cell[] battlefieldCellsByUnits;

    public GameObject[] actionBips;

    public Image activePortrain;
    public TextMeshProUGUI activeName;
    public Image[] skillsButtons;
    public itemsTooltip[] itemsSlots;
    public AmmoCounter[] ammoCounters;

    public GameObject panel;
    public GameObject[] subPanels;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    public TextMeshProUGUI activeIniciative;
    public TextMeshProUGUI activeHP;

    void unitsPositioning()
    {
        for (int i = 0; i < engagedUnits.Count; i++)
        {
            for (int y = 0; y < battlefieldCells.Length; y++)
            {
                if (engagedUnits[i].position == battlefieldCells[y].position)
                {
                    battlefieldCells[y].isOccupied = true;
                    battlefieldCells[y].standingUnit = engagedUnits[i];
                    engagedUnits[i].transform.position = battlefieldCells[y].transform.position;
                    battlefieldCellsByUnits[i] = battlefieldCells[y];
                }
            }
        }
    }

    public void actionBipsControl()
    {
        actionBips[0].SetActive(false);
        actionBips[1].SetActive(false);
        actionBips[2].SetActive(false);
        actionBips[3].SetActive(false);
        if (activeUnit.actionsLeft > 0)
        {
            actionBips[0].SetActive(true);
        }
        if (activeUnit.actionsLeft > 1)
        {
            actionBips[1].SetActive(true);
        }
        if (activeUnit.actionsLeft > 2)
        {
            actionBips[2].SetActive(true);
        }
        if (activeUnit.actionsLeft > 3)
        {
            actionBips[3].SetActive(true);
        }
    }

    public void refreshLists()
    {
        playerUnits.Clear();
        enemyUnits.Clear();
        List<Unit> tempList = new List<Unit>();
        tempList = engagedUnits;
        while (tempList.Capacity > 1)
        {
            tempList = tempList.OrderByDescending(w => w.initiative).ToList();
            if (!tempList[0].isAIControlled)
            {
                //Debug.Log("!# " + tempList[0].isAIControlled + "_" + tempList[0].name);
                playerUnits.Add(tempList[0]);
            }
            else
            {
                enemyUnits.Add(tempList[0]);
            }
            tempList.Remove(tempList[0]);
        }
    }


    public void switchActiveUnit(int id)
    {

        if (enemyUnits.Capacity - enemyKilled < 1)
        {
            winPanel.SetActive(true);
            return;
        }

        engagedUnitsWaiting = engagedUnitsWaiting.OrderByDescending(w => w.initiative).ToList();
        activeUnit = engagedUnitsWaiting[0];
        engagedUnitsWaiting.Remove(activeUnit);
        unitsPositioning();
        activeUnit.actionsLeft = 4;
        if (activeUnit.isDead)
        {
            if (engagedUnitsWaiting.Capacity > 1)
            {
                activeUnitID++;
                switchActiveUnit(activeUnitID);
                return;
            }
            else
            {
                roundStarting();
            }
        }
        for (int i = 0; i < skillsButtons.Length; i++)
        {
            skillsButtons[i].GetComponent<skillTooltip>().refreshSkills();
        }
        battlefieldCellsByUnits[id].select();
        actionBipsControl();
        activePortrain.sprite = activeUnit.portraiat;
        activeName.text = activeUnit.name;
        activeIniciative.text = "Inicjatywa " + activeUnit.initiative;
        activeHP.text = activeUnit.HP + "/" + activeUnit.maxHP;
        if (activeUnit.armor != null)
            itemsSlots[0].getItem();
        else
            itemsSlots[0].noItem();
        if (activeUnit.mainWeapon != null)
        {
            itemsSlots[1].getItem();
            ammoCounters[0].typeSwitch(activeUnit.mainWeapon.magazine);
            ammoCounters[0].updateCounter(activeUnit);
        }
        else
            itemsSlots[1].noItem();
        if (activeUnit.secondaryWeapon!=null)
            itemsSlots[2].getItem();
        else
            itemsSlots[2].noItem();
        
        if (activeUnit.isAIControlled)
        {
            int randomID = Random.Range(0, playerUnits.Capacity - playersKilled);
            Debug.Log(randomID);
            Debug.Log(playerUnits.Capacity - playersKilled);
            if (playerUnits.Capacity - playersKilled < 1)
            {
                gameOverPanel.SetActive(true);
                return;
            }
            if (activeUnit.mainWeaponAmmo < activeUnit.mainWeapon.shoots)
            {
                reload();
                reload();
            }
            else
            {
                skillsButtons[0].GetComponent<skillTooltip>().attackSkill(activeUnit, playerUnits[randomID], activeUnit.skills[0]);
            }
            if (playerUnits.Capacity - playersKilled < 1)
            {
                gameOverPanel.SetActive(true);
                return;
            }
            if (enemyUnits.Capacity < 1)
            {
                winPanel.SetActive(true);
                return;
            }
            //Debug.Log(activeUnit.actionsLeft);
            if (activeUnit.mainWeaponAmmo < activeUnit.mainWeapon.shoots)
            {
                reload();
                reload();
            }
            else
            {
                randomID = Random.Range(0, playerUnits.Capacity - playersKilled);
                skillsButtons[0].GetComponent<skillTooltip>().attackSkill(activeUnit, playerUnits[randomID], activeUnit.skills[0]);
            }
            //Debug.Log(activeUnit.actionsLeft);
        }

    }

    public void deadCheck()
    {
        engagedUnits.Clear();
        while (engagedUnitsActivated.Capacity > 1)
        {
            engagedUnitsActivated = engagedUnitsActivated.OrderByDescending(w => w.initiative).ToList();
            if (!engagedUnitsActivated[0].isDead)
            {
                engagedUnits.Add(engagedUnitsActivated[0]);
            }
            else if (!engagedUnitsActivated[0].isAIControlled)
            {
                //DestroyImmediate(engagedUnitsActivated[0].gameObject);

                playersKilled--;
            }
            else
            {
                //DestroyImmediate(engagedUnitsActivated[0].gameObject);
            }
            engagedUnitsActivated.Remove(engagedUnitsActivated[0]);
        }
        //engagedUnitsActivated.Clear();

        //for (int i = 0; i + 1 < engagedUnits.Capacity; i++)
        //{
        //    Debug.Log(engagedUnits.Capacity);
        //    engagedUnitsActivated[i] = engagedUnits[i];
        //}
        //engagedUnits.Clear();
        //for (int i = 0; i < engagedUnitsActivated.Capacity; i++)
        //{
        //    if (engagedUnitsActivated[i] != null)
        //    {
        //        if (!engagedUnitsActivated[i].isDead)
        //        {
        //            Debug.Log("YEP");
        //            engagedUnits.Add(engagedUnitsActivated[i]);
        //        }
        //    }
        //}
    }
    void Start()
    {
        instance = this;
        refreshLists();
        engagedUnits = engagedUnits.OrderByDescending(w => w.initiative).ToList();
        if (playerUnits.Capacity < 1)
        {
            gameOverPanel.SetActive(true);
            return;
        }
        if (enemyUnits.Capacity < 1)
        {
            winPanel.SetActive(true);
            return;
        }
        activeUnitID = 0;
        engagedUnitsWaiting = engagedUnits;
        engagedUnitsActivated.Clear();
        switchActiveUnit(activeUnitID);
    }

    public void roundStarting()
    {
        deadCheck();
        Debug.Log("                "+enemyUnits.Capacity);
        if (enemyUnits.Capacity - enemyKilled < 1)
        {
            winPanel.SetActive(true);
            return;
        }
        refreshLists();
        activeUnitID = 0;
        engagedUnitsWaiting = engagedUnits;
        engagedUnitsActivated.Clear();

        switchActiveUnit(activeUnitID);
    }
    
    public void skillTargeting(int skillID)
    {
        panel.SetActive(true);
        for(int i=0;i<subPanels.Length;i++)
        {
            subPanels[i].SetActive(false);
        }
        for (int i = 0; i < activeUnit.skills[skillID].targetsCells.Length; i++)
        {
            for (int y = 0; y < subPanels.Length; y++)
            {
                if (activeUnit.skills[skillID].targetsCells[i].Equals(subPanels[y].GetComponent<TargetingPanel>().position))
                {
                    for(int z=0;z< battlefieldCells.Length;z++)
                    {
                        if(activeUnit.skills[skillID].targetsCells[i].Equals(battlefieldCells[z].position))
                        {
                            //Debug.Log(battlefieldCells[z].isOccupied);
                            if (battlefieldCells[z].isOccupied)
                            {
                                subPanels[y].SetActive(true);
                            }
                        }
                    }
                    
                }
            }
        }
    }

    public void passmove()
    {
        comsumeActionPoints(8);
    }

    public void reload()
    {
        if (activeUnit.mainWeaponMagazines > 0)
        {
            activeUnit.mainWeaponAmmo = activeUnit.mainWeapon.ammoCapacity;
            activeUnit.mainWeaponMagazines--;
            ammoCounters[0].updateCounter(activeUnit);
            comsumeActionPoints(1);
        }
    }
    
    public void killCheck(Unit target)
    {
        if (target.HP < 1)
        {
            target.gameObject.SetActive(false);
            target.isDead = true;
            if(!target.isAIControlled)
            {
                playersKilled++;
            }
            else
            {
                enemyKilled++;
            }
        }
    }

    public void comsumeActionPoints(int amount)
    {
        activeUnit.actionsLeft-=amount;
        actionBipsControl();
        endMoveCheck();
    }

    public void endMoveCheck()
    {
        if (activeUnit.actionsLeft < 1)
        {
            //Debug.Log(activeUnitID + " acted");
            panel.SetActive(false);
            battlefieldCellsByUnits[activeUnitID].deselect();
            engagedUnitsActivated.Add(activeUnit);
            //Debug.Log("CAPACITY "+engagedUnitsWaiting.Capacity);
            if (engagedUnitsWaiting.Capacity > 1)
            {
                switchActiveUnit(activeUnitID);
                //Debug.Log(activeUnit.actionsLeft);
            }
            else
            {
                roundStarting();
            }
        }
    }

    void Update()
    {
        //.
    }
}
