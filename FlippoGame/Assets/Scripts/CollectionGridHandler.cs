using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGridHandler : MonoBehaviour {

    [SerializeField]
    private GameObject gridButtonTemplate;

	// Use this for initialization
	void Start () {
        GenerateGridButtons();        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GenerateGridButtons()
    {
        Collection c = GameManager.Instance.collections[0];
        foreach (Flippo f in c.flippos)
        {
            GameObject gridButton = Instantiate(gridButtonTemplate) as GameObject;
            gridButton.SetActive(true);

            gridButton.GetComponent<CollectionFlippo>().SetFlippoItem(f);
            gridButton.transform.SetParent(gridButtonTemplate.transform.parent, false);
        }
    }
}
