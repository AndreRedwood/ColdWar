using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStats
{
	[SerializeField]
	private int baseHealth;
	public int BaseHealth
	{
		get { return baseHealth; }
	}
	[SerializeField]
	private int baseEvasion;
	public int BaseEvasion
	{
		get { return baseEvasion; }
	}
	[SerializeField]
	private int baseAaccuracy;
	public int BaseAccuracy
	{
		get { return baseAaccuracy; }
	}
}
