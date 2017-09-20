using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionFlippo : MonoBehaviour {

    [SerializeField]
    private Image myIcon;

    [SerializeField]
    public GameObject gridButton;

    private Flippo flippo;

	// Use this for initialization
	void Start () {
        myIcon = gridButton.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetFlippoItem(Flippo flippo)
    {
        if(myIcon == null)
        {
            myIcon = gridButton.GetComponent<Image>();
        }

        myIcon.sprite = flippo.sprite;
        this.flippo = flippo;
    }

    public void OnClick()
    {
#if UNITY_EDITOR
        if(flippo != null)
            Debug.Log("Pressed flippo:" + flippo.id);
#endif
    }
}
