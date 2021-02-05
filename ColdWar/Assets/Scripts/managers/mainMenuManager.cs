using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuManager : MonoBehaviour
{

	public GeneralGameManager generalGameManager;

    // Start is called before the first frame update
    void Start()
    {
		generalGameManager = GameObject.Find("GeneralManager").GetComponent<GeneralGameManager>();
		generalGameManager.mainMenuManager = this;
    }

	public void Close()
	{
		Application.Quit();
	}

    // Update is called once per frame
    public void loadBattle()
    {
		SceneManager.LoadScene(0);
    }
}
