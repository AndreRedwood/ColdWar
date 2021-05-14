using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliser : MonoBehaviour
{
	TextMeshProUGUI texField;
	[SerializeField]
	private string key;
	public string Key
	{
		get { return key; }
		set { key = value; refresh(); }
	}

	private bool isInit = false;

	private void Init()
	{
		texField = GetComponent<TextMeshProUGUI>();

		isInit = true;
	}

	void Start()
	{
		refresh();
	}

	private void refresh()
	{
		if (!isInit) { Init(); }
		LocalisationSystem loc = new LocalisationSystem();
		string value = LocalisationSystem.GetLocalisedValue(key);
		texField.text = value;
    }
}
