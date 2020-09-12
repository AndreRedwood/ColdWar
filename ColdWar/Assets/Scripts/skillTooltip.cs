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

	public bool hasAmmo = true;

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
            case skillType.attackRow:
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

    public void executeSkill(Unit user, Unit target, TargetingPanel panel)
    {
        if (user.actionsLeft < skillHeld.actionCost)
        {
            Debug.Log("Lack of action points!");
            return;
        }

		hasAmmo = true;

        switch (skillHeld.type)
        {
            case skillType.attack: attackSkill(user, target, skillHeld, panel.transform.position); break;
            case skillType.attackRow: attackRowSkill(user, target, skillHeld, panel.transform.position); break;
        }

		//Debug.Log(skillHeld.actionCost);
		if (hasAmmo)
		{
			battleManager.comsumeActionPoints(skillHeld.actionCost);
			battleManager.actionBipsControl();
		}
		battleManager.panel.SetActive(false);
    }

	public GameObject damageIndPrefab;
	public GameObject UIPanel;

    public void attackSkill(Unit user ,Unit target, Skill skillUsed, Vector2 position)
    {

        float oneShootDamage = (skillUsed.damageMultiplayer * user.mainWeapon.damage) - target.armor.armorBonus;
        float attacksCount = skillUsed.attackCount * user.mainWeapon.shoots;
        float accuracy = skillUsed.accuracyMultiplayer * (user.accuracy + user.mainWeapon.weaponAccuracyBonus);
        float targetDogde = target.evasion + target.armor.dogdeBonus;
        float damageDealed = 0;

        if (user.mainWeaponAmmo < attacksCount)
        {
            Debug.Log("Lack of ammunition!");
			hasAmmo = false;
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
			//Debug.Log("MISS!");
			//battleManager.damageEffect("MISS!", position);
			GameObject tmpDamPop = Instantiate(damageIndPrefab, new Vector3 (position.x, position.y + 50), Quaternion.identity);
			tmpDamPop.GetComponent<DamageIndicatorScript>().label.text = "MISS!";
			tmpDamPop.transform.SetParent(UIPanel.transform);
		}
        else
        {
            //Debug.Log(damageDealed);
            //battleManager.damageEffect(damageDealed.ToString(), position);
			GameObject tmpDamPop = Instantiate(damageIndPrefab, new Vector3(position.x, position.y + 50), Quaternion.identity);
			tmpDamPop.GetComponent<DamageIndicatorScript>().label.text = damageDealed.ToString();
			tmpDamPop.transform.SetParent(UIPanel.transform);
		}
        battleManager.ammoCounters[0].updateCounter(battleManager.activeUnit);
        battleManager.killCheck(target);
        //Debug.Log(user.mainWeaponAmmo);
    }

    public void attackRowSkill(Unit user, Unit target, Skill skillUsed, Vector2 position)
    {
        Unit secondTarget = null;
        for (int i = 0; i < battleManager.engagedUnits.Capacity; i++)
        {
            //Debug.Log("ichi");
            if (battleManager.engagedUnits[i].position.y == target.position.y && battleManager.engagedUnits[i].position.x != target.position.x)
            {
                Debug.Log("Sec");
                secondTarget = battleManager.engagedUnits[i];
            }
        }

        float oneShootDamage = (skillUsed.damageMultiplayer * user.mainWeapon.damage) - target.armor.armorBonus;
        float attacksCount = skillUsed.attackCount * user.mainWeapon.shoots;
        float accuracy = skillUsed.accuracyMultiplayer * (user.accuracy + user.mainWeapon.weaponAccuracyBonus);
        float targetDogde = target.evasion + target.armor.dogdeBonus;
        float targetDogdeSecond = 0;
        if (secondTarget != null)
            targetDogdeSecond = secondTarget.evasion + secondTarget.armor.dogdeBonus;
        float damageDealed = 0;
        float damageDealedSecond = 0;

        if (user.mainWeaponAmmo < attacksCount)
        {
            Debug.Log("Lack of ammunition!");
            return;
        }
        //Debug.Log("Max Damage " + oneShootDamage * attacksCount);
        for (int i = 0; i < attacksCount; i++)
        {
            if (i + 1 < attacksCount && secondTarget != null)
            {
                Debug.Log("Halo");
                if (Random.Range(0, accuracy + targetDogde) <= accuracy)
                {
                    damageDealed += oneShootDamage;
                }
                user.mainWeaponAmmo--;
                if (Random.Range(0, accuracy + targetDogdeSecond) <= accuracy)
                {
                    damageDealedSecond += oneShootDamage;
                }
                user.mainWeaponAmmo--;
            }
            else if(i < attacksCount)
            {
                if (Random.Range(0, accuracy + targetDogde) <= accuracy)
                {
                    damageDealed += oneShootDamage;
                }
                user.mainWeaponAmmo--;
            }
        }
        //Debug.Log("Damage deaded " + damageDealed);
        target.HP -= (int)damageDealed;
        if(secondTarget != null)
        {
            secondTarget.HP -= (int)damageDealed;
        }
        //if (damageDealed == 0)
        //{
        //    //Debug.Log("MISS!");
        //    battleManager.damageEffect("MISS!");
        //}
        //else
        //{
        //    //Debug.Log(damageDealed);
        //    battleManager.damageEffect(damageDealed.ToString());
        //}
        battleManager.ammoCounters[0].updateCounter(battleManager.activeUnit);
        battleManager.killCheck(target);
        //Debug.Log(user.mainWeaponAmmo);
    }
}
