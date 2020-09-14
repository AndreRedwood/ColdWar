using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager : MonoBehaviour
{
	public mainMenuManager mainMenuManager;
	public BattleManager battleManager;
	

    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(this.gameObject);
    }

	public void initializeBattle(BattleManager battleManagerTemp)
	{
		battleManager = battleManagerTemp;

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
