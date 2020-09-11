using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicatorScript : MonoBehaviour
{
	public TextMeshProUGUI label;
	public float lifeSpawn;
	float life;

	public Color color;

    void Update()
    {
        if(lifeSpawn < life)
		{
			Destroy(this.gameObject);
		}
		life += Time.deltaTime;
		this.gameObject.transform.position = (transform.position + new Vector3(0, Time.deltaTime * 40f, 0));
		label.color = new Color(color.r, color.g, color.b, color.a - (Time.deltaTime * 1));
		color = label.color;
    }
}
