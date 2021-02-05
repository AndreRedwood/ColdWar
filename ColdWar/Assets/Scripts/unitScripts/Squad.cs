using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Squad
{
	public string squadName;
	public List<Unit> members = new List<Unit>();
	public List<Magazine> squadMagazines = new List<Magazine>();
}
