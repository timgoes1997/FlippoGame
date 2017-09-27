using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlippoHelp : MonoBehaviour {

    public GameObject panel;
    public Text panelText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetFlippoAmount()
    {
        string tempText = PlayerManager.Instance.Inventory.flippos.Count + " / " + GameManager.Instance.Flippos.Count;
        panelText.text = tempText;
        panel.SetActive(true);
    }
}
