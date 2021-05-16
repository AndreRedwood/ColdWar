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
	}
	[SerializeField]
	private List<Weapon> weaponTemplates = new List<Weapon>();
	public List<Weapon> WeaponTemplates
	{
		get { return weaponTemplates; }
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

		string[] weaponTemplatesJsons = Directory.GetFiles(path + "weaponTemplates/", "*.json");
		foreach(string templateJson in weaponTemplatesJsons)
		{
			json = File.ReadAllText(templateJson);
			weaponTemplates.Add(JsonUtility.FromJson<Weapon>(json));
		}
		weaponTemplates.TrimExcess();

		isInit = true;
    }

	public Weapon findWeaponByName(string name)
	{
		foreach(Weapon weapon in weaponTemplates)
		{
			if(name == weapon.Name)
			{
				return weapon;
			}
		}
		Debug.LogError("error 404, missing value");
		return null;
	}

	public void generateUnitTypeTemplate()
	{
		string json;
		unitTypes.Add(new UnitType());
		json = JsonUtility.ToJson(unitTypes[0]);
		File.WriteAllText(path + "unitTypes/classTemplate.json",json);
	}

	public void generateWeaponTemplate()
	{
		string json;
		weaponTemplates.Add(new Weapon());
		json = JsonUtility.ToJson(weaponTemplates[0]);
		File.WriteAllText(path + "weaponTemplates/weaponTemplate.json", json);
	}

	public void Start()
	{
		Init();
		//generateWeaponTemplate();
		//generateUnitTypeTemplate();
		string[] names = { "janek", "tomek", "kuba", "artur", "sergiej", "bob" };
		allUnits.Add(new Unit(names[Random.Range(0, 5)], unitTypes[0],this));
	}
}
