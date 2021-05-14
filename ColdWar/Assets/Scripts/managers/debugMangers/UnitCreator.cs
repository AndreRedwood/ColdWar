using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ResourceManager))]
public class UnitCreator : MonoBehaviour
{
	private ResourceManager resourceManager;

	[Header("Scene Connections")]
	[SerializeField]
	TextMeshProUGUI unitNameDisplay;
	[SerializeField]
	TextLocaliser unitTypeDisplay;
	[SerializeField]
	TextLocaliser unitTypeDescrDisplay;

	[SerializeField]
	TMP_Dropdown unitTypeDropdown;
	

	[Header("Creation Data")]
	[SerializeField]
	private string temponaryName;
	[SerializeField]
	private UnitType temponaryType;

	private bool isInit = false;
	private void Init()
	{
		resourceManager = GetComponent<ResourceManager>();

		generateDropdown();

		temponaryName = "nameless";
		temponaryType = resourceManager.UnitTypes[0];

		unitNameDisplay.text = temponaryName;
		unitTypeDisplay.Key = temponaryType.Name;
		unitTypeDescrDisplay.Key = temponaryType.Description;

		isInit = true;
	}

	public void outsideInit()
	{
		if (!isInit) { Init(); }
	}

	public void changeName(string name)
	{
		if (!isInit) { Init(); }
		temponaryName = name;
		unitNameDisplay.text = temponaryName;
	}

	public void changeType(int typeIndex)
	{
		if (!isInit) { Init(); }
		temponaryType = resourceManager.UnitTypes[typeIndex];
		unitTypeDisplay.Key = temponaryType.Name;
		unitTypeDescrDisplay.Key = temponaryType.Description;
		//unitTypeDropdown.value = typeIndex;
		//Debug.Log(typeIndex);
	}

	public void createUnit()
	{
		if (!isInit) { Init(); }
		resourceManager.AllUnits.Add(new Unit(temponaryName, temponaryType));
	}

	void generateDropdown()
	{
		unitTypeDropdown.ClearOptions();
		LocalisationSystem loc = new LocalisationSystem();
		List<string> optionLabels = new List<string>();
		//Debug.Log(resourceManager.UnitTypes.Capacity);
		for ( int i=0; i < resourceManager.UnitTypes.Capacity; i++ )
		{
			optionLabels.Add(LocalisationSystem.GetLocalisedValue(resourceManager.UnitTypes[i].Name));
		}
		unitTypeDropdown.AddOptions(optionLabels);
	}
}
