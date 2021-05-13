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
	
	[SerializeField]
	TextLocaliser unitTypeNameDisplay;

	[SerializeField]
	List<UnitType> unitTypes = new List<UnitType>();


	[SerializeField]
	List<Unit> allUnits = new List<Unit>();

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
		//generateUnitTypeTemplate();
		string[] names = { "janek", "tomek", "kuba", "artur", "sergiej", "bob" };
		allUnits.Add(new Unit(names[Random.Range(0, 5)], unitTypes[0]));
		unitTypeNameDisplay.setKey(unitTypes[0].Name);
	}
}
