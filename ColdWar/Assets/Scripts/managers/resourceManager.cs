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

	public BattleMap map;

	[Header("spriteLists")]
	[SerializeField]
	private List<Sprite> battleBackgrounds = new List<Sprite>();
	public List<Sprite> BattleBackgrounds
	{
		get { return battleBackgrounds; }
	}

	[Header("jsonTemplates")]
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
	private List<BattleMapTemplate> battleMapTemplates = new List<BattleMapTemplate>();
	public List<BattleMapTemplate> BattleMapTemplatesTemplates
	{
		get { return battleMapTemplates; }
	}

	[Header("objects")]
	[SerializeField]
	private List<TerrainFeature> terrainFeatures = new List<TerrainFeature>();
	public List<TerrainFeature> TerrainFeutures
	{
		get { return terrainFeatures; }
	}

	[Header("gameInfo")]
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

	public Sprite findBattleBackgroundSpriteByName(string name)
	{
		foreach(Sprite background in battleBackgrounds)
		{
			if(name == background.name)
			{
				return background;
			}
		}
		Debug.LogError("error 404, missing value (battleBacground)");
		return null;
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
		Debug.LogError("error 404, missing value (weapon template)");
		return null;
	}

	public TerrainFeature findTerrainFeatureByName(string name)
	{
		foreach(TerrainFeature feature in terrainFeatures)
		{
			if(name == feature.Name)
			{
				return feature;
			}
		}
		Debug.LogError("error 404, missing value (terrain feature)");
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

	public void generateBattleMapTemplate()
	{
		string json;
		battleMapTemplates.Add(new BattleMapTemplate());
		json = JsonUtility.ToJson(battleMapTemplates[0]);
		File.WriteAllText(path + "battleMapTemplates/battleMapTemplate.json", json);
	}

	public void Start()
	{
		Init();
		//generateWeaponTemplate();
		//generateUnitTypeTemplate();
		generateBattleMapTemplate();
		map = new BattleMap(this, battleMapTemplates[0]);
		string[] names = { "janek", "tomek", "kuba", "artur", "sergiej", "bob" };
		allUnits.Add(new Unit(names[Random.Range(0, 5)], unitTypes[0],this));
		//Debug.Log(map.Tiles[0,0]+"+"+ map.Tiles[0, 1] + "+"+ map.Tiles[0, 2]);
	}
}
