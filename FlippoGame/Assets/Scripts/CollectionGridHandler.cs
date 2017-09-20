using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGridHandler : MonoBehaviour {

    public Collection filter = Collection.None;

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
        foreach (Flippo f in GameManager.Instance.GetFlippoByPlayerFlippo(PlayerManager.Instance.Inventory.flippos, filter))
        {
            GameObject gridButton = Instantiate(gridButtonTemplate) as GameObject;
            gridButton.SetActive(true);

            gridButton.GetComponent<CollectionFlippo>().SetFlippoItem(f, PlayerManager.Instance.Inventory.GetFlippoAmount(f));
            gridButton.transform.SetParent(gridButtonTemplate.transform.parent, false);
        }
    }
}
