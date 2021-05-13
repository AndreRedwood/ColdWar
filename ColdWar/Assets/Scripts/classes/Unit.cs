using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	none,
	stun,
	dead
}

[System.Serializable]
public class Unit
{
	[SerializeField]
	private int id;
	public int Id
	{
		get { return id; }
	}
	[SerializeField]
	private string name;
	public string Name
	{
		get { return name; }
	}
	[SerializeField]
	private UnitType unitType;
	public UnitType UnitType
	{
		get { return unitType; }
	}
	[Header("General status")]
	[SerializeField]
	private int level;
	public int Level
	{
		get { return level; }
	}
	[SerializeField]
	private int experience;
	public int Experience
	{
		get { return experience; }
		set { experience += value; }
	}
	[SerializeField]
	private UnitStats unitStats;
	public UnitStats UnitStats
	{
		get { return unitStats; }
	}
	[Header("Actual status")]
	[SerializeField]
	private UnitStats actualStats;
	public UnitStats ActualStats
	{
		get { return actualStats; }
		set { actualStats = value; }
	}
	[SerializeField]
	private State status;
	public State Status
	{
		get { return status; }
		set { status = value; }
	}

	public Unit(string newName,UnitType newUnitType)
	{
		id = Random.Range(0, 9999);
		level = 1;
		experience = 0;
		name = newName;
		unitType = newUnitType;
		unitStats = unitType.BaseStats;
	}
}
