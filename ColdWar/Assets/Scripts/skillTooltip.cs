using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class skillTooltip : MonoBehaviour
{
    public BattleManager battleManager;

    public GameObject skillTolltip;
    public TextMeshProUGUI skillNamePlace;
    public TextMeshProUGUI skillDamagePlace;
    public TextMeshProUGUI skillAccuracyPlace;
    public TextMeshProUGUI skillAmmoCostPlace;
    public GameObject[] actionBips;



    public float time;
    public Skill skillHeld;

    public int skillID;

    public void getSkill()
    {
        skillHeld = BattleManager.instance.activeUnit.skills[skillID];
    }

    public void refreshSkills()
    {
        if (BattleManager.instance.activeUnit.skills.Length > skillID)
        {
            if (BattleManager.instance.activeUnit.skills[skillID] != null)
            {
                this.gameObject.SetActive(true);
                getSkill();
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        //.
    }

    void actionBipsControl()
    {
        actionBips[0].SetActive(false);
        actionBips[1].SetActive(false);
        actionBips[2].SetActive(false);
        actionBips[3].SetActive(false);
        if (skillHeld.actionCost > 0)
        {
            actionBips[0].SetActive(true);
        }
        if (skillHeld.actionCost > 1)
        {
            actionBips[1].SetActive(true);
        }
        if (skillHeld.actionCost > 2)
        {
            actionBips[2].SetActive(true);
        }
        if (skillHeld.actionCost > 3)
        {
            actionBips[3].SetActive(true);
        }
    }

    public IEnumerator tooltipShow()
    {
        yield return new WaitForSeconds(time);
        skillNamePlace.text = skillHeld.name;
        switch (skillHeld.type)
        {
            case skillType.attack : 
                skillDamagePlace.text = skillHeld.attackCount * BattleManager.instance.activeUnit.mainWeapon.shoots + "x" + (int)(skillHeld.damageMultiplayer * BattleManager.instance.activeUnit.mainWeapon.damage);
            skillAccuracyPlace.text = " " + (int)(skillHeld.accuracyMultiplayer * battleManager.activeUnit.mainWeapon.weaponAccuracyBonus + battleManager.activeUnit.accuracy) + " ";
            skillAmmoCostPlace.text = skillHeld.attackCount * BattleManager.instance.activeUnit.mainWeapon.shoots + " "; break;
            case skillType.selfBuff:
                skillDamagePlace.text = skillHeld.buffedStat;
                skillAmmoCostPlace.text = (skillHeld.buffMultiplayer - 1f) * 10 + "% " + skillHeld.buffLasting + " turns"; break;
        }
        skillTolltip.transform.position = Input.mousePosition;
        actionBipsControl();
        skillTolltip.SetActive(true);
    }

    public void show()
    {
        StartCoroutine(tooltipShow());
    }

    public IEnumerator hide()
    {
        yield return new WaitForSeconds(0.5f);
        skillTolltip.SetActive(false);
    }

    public void broke()
    {
        StopAllCoroutines();
        StartCoroutine(hide());
    }

    public void executeSkill(int targetID)
    {
        //Debug.Log(((skillHeld.damageMultiplayer * battleManager.activeUnit.mainWeapon.damage) * (battleManager.activeUnit.mainWeapon.shoots * skillHeld.attackCount)));

        for(int i=0;i<battleManager.engagedUnits.Capacity;i++)
        {
            //Debug.Log("ichi");
            if(battleManager.engagedUnits[i].position.Equals(battleManager.subPanels[targetID].GetComponent<TargetingPanel>().position))
            {
                //Debug.Log("ni");
                attackSkill(battleManager.activeUnit,battleManager.engagedUnits[i],skillHeld);
            }
        }
    }

    public void attackSkill(Unit user ,Unit target, Skill skillUsed)
    {
        if (user.actionsLeft < skillHeld.actionCost)
        {
            Debug.Log("Lack of aaction points!");
            return;
        }

        float oneShootDamage = (skillUsed.damageMultiplayer * user.mainWeapon.damage) - target.armor.armorBonus;
        float attacksCount = skillUsed.attackCount * user.mainWeapon.shoots;
        float accuracy = skillUsed.accuracyMultiplayer * (user.accuracy + user.mainWeapon.weaponAccuracyBonus);
        float targetDogde = target.evasion + target.armor.dogdeBonus;
        float damageDealed = 0;

        if (user.mainWeaponAmmo < attacksCount)
        {
            Debug.Log("Lack of ammunition!");
            return;
        }

        //Debug.Log("Max Damage " + oneShootDamage * attacksCount);
        for (int i=0; i<attacksCount;i++)
        {
            if(Random.Range(0,accuracy+targetDogde) <= accuracy)
            {
                damageDealed += oneShootDamage;
            }
            user.mainWeaponAmmo--;
        }
        //Debug.Log("Damage deaded " + damageDealed);
        target.HP -= (int)damageDealed;
        if(damageDealed == 0)
        {
            Debug.Log("MISS!");
        }
        else
        {
            Debug.Log(damageDealed);
        }
        //Debug.Log(skillHeld.actionCost);
        battleManager.comsumeActionPoints(skillHeld.actionCost);
        battleManager.actionBipsControl();
        battleManager.ammoCounters[0].updateCounter(battleManager.activeUnit);
        battleManager.killCheck(target);
        //Debug.Log(user.mainWeaponAmmo);
    }
}
