using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGridHandler : MonoBehaviour, IGrid
{

    public Collection filter = Collection.None;
    public bool inventory = true;

    [SerializeField]
    private GameObject gridButtonTemplate;

    public List<GameObject> currentChildren;

    // Use this for initialization
    void Start()
    {
        GenerateGridButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateGridButtons()
    {
        if (currentChildren == null) currentChildren = new List<GameObject>();

        RemoveCurrentChildren();
        if (inventory)
        {
            GenerateInventory();
        }
        else
        {
            GenerateCollection();
        }
    }

    private void RemoveCurrentChildren()
    {
        for (int i = currentChildren.Count - 1; i >= 0; i--)
        {
            Destroy(currentChildren[i]);
            currentChildren.Remove(currentChildren[i]);
        }
    }

    private void GenerateInventory()
    {
        foreach (Flippo f in GameManager.Instance.GetFlippoByPlayerFlippo(PlayerManager.Instance.Inventory.flippos, filter))
        {
            GameObject gridButton = Instantiate(gridButtonTemplate) as GameObject;
            gridButton.SetActive(true);

            gridButton.GetComponent<ICollectionFlippo>().SetFlippoItem(f, PlayerManager.Instance.Inventory.GetFlippoAmount(f));
            gridButton.transform.SetParent(gridButtonTemplate.transform.parent, false);
            currentChildren.Add(gridButton);
        }
    }

    private void GenerateCollection()
    {
        foreach (Flippo f in GameManager.Instance.Flippos)
        {
            GameObject gridButton = Instantiate(gridButtonTemplate) as GameObject;
            gridButton.SetActive(true);

            gridButton.GetComponent<ICollectionFlippo>().SetFlippoItem(f, PlayerManager.Instance.Inventory.GetFlippoAmount(f));
            gridButton.transform.SetParent(gridButtonTemplate.transform.parent, false);
            currentChildren.Add(gridButton);
        }
    }

    public void SetGrid(bool inventory)
    {
        this.inventory = inventory;
        GenerateGridButtons();
    }
}
