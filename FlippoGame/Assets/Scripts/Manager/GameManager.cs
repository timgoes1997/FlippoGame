using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;
    public static GameManager Instance { get { return (instance != null) ? instance : (FindObjectsOfType<GameManager>().Length > 0) 
                ? FindObjectOfType<GameManager>() : (Instantiate(Resources.Load("Manager/GameManager", typeof(GameObject))) as GameObject).GetComponent<GameManager>(); } }

    public List<Collection> collections;

    void Awake()
    {       
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.Portrait;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
