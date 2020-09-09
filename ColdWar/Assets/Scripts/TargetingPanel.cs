using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingPanel : MonoBehaviour
{
    public BattleManager battleManager;

    public Vector2Int position;
    public Cell connetedCell;

    public Unit targetHere;


    public void connectToTarget()
    {
        for (int i = 0; i < battleManager.engagedUnits.Capacity; i++)
        {
            //Debug.Log("ichi");
            if (battleManager.engagedUnits[i].position.Equals(position))
            {
                //Debug.Log("ni");
                //attackSkill(battleManager.activeUnit, battleManager.engagedUnits[i], skillHeld);
                targetHere = battleManager.engagedUnits[i];
            }
        }
    }

    public void buttonExecute()
    {
        battleManager.selectedSkill.executeSkill(battleManager.activeUnit,targetHere,this);
    }
}
