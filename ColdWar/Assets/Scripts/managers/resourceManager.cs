using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceManager : MonoBehaviour
{
	public int squadsFormed;
	public List<Squad> playerSquads = new List<Squad>();
	public List<Squad> enemyGroups = new List<Squad>();

	public List<Unit> allUnits = new List<Unit>();


	//unused and probably broken (but maybe working)
	public void buildUnits()
	{
		for(int i=0;i < allUnits.Capacity; i++)
		{
			allUnits[i].initiative = allUnits[i].unitClass.baseInitiative;
			allUnits[i].maxHP = allUnits[i].unitClass.baseHP;
			allUnits[i].evasion = allUnits[i].unitClass.baseEvasion;
			allUnits[i].accuracy = allUnits[i].unitClass.baseAccuracy;
		}
	}
}
