using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliser : MonoBehaviour
{
	TextMeshProUGUI texField;
	public string key;

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

	public void setKey(string newKey)
	{
		key = newKey;
		refresh();
	}
}
