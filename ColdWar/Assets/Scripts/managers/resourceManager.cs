using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum WeaponType
{
	_SMG,
	_RF,
};

public class ResourceManager : MonoBehaviour
{
	private static string path = "Assets/Objects/";

	[HideInInspector]
	public IdGiver idGiver = new IdGiver();

	[SerializeField]
	private List<UnitType> unitTypes = new List<UnitType>();
	public List<UnitType> UnitTypes
	{
		get { return unitTypes; }
		set { unitTypes = value; }
	}

	[SerializeField]
	private List<Unit> allUnits = new List<Unit>();
	public List<Unit> AllUnits
	{
		get { return allUnits; }
		set { allUnits = value; }
	}

	private bool isInit = false;

	private void Init()
    {
		string json;
		string[] unitTypesJsons = Directory.GetFiles(path + "unitTypes/", "*.json");
		foreach(string typeJson in unitTypesJsons)
		{
			json = File.ReadAllText(typeJson);
			unitTypes.Add(JsonUtility.FromJson<UnitType>(json));
		}
		unitTypes.TrimExcess();

		isInit = true;
    }

	public void generateUnitTypeTemplate()
	{
		string json;
		unitTypes.Add(new UnitType());
		json = JsonUtility.ToJson(unitTypes[0]);
		File.WriteAllText(path + "unitTypes/classTemplate.json",json);
	}

	public void Start()
	{
		Init();
		Debug.Log(idGiver.giveId());
		Debug.Log(idGiver.giveId());
		Debug.Log(idGiver.giveId());
		Debug.Log(idGiver.giveId());
		Debug.Log(idGiver.giveId());
		//generateUnitTypeTemplate();
		string[] names = { "janek", "tomek", "kuba", "artur", "sergiej", "bob" };
		allUnits.Add(new Unit(names[Random.Range(0, 5)], unitTypes[0]));
	}
}
