using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeGridHandler : MonoBehaviour {

    public TradeManager tradeManager;

    [SerializeField]
    private GameObject tradePanelObject;

    // Use this for initialization
    void Start()
    {
        tradeManager.GetPendingTrades(this);
        //GenerateGridButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateGridButtons(List<TradeItem> tradeItems)
    {
        /*
        foreach (Flippo f in GameManager.Instance.GetFlippoByPlayerFlippo(PlayerManager.Instance.Inventory.flippos, filter))
        {
            GameObject gridButton = Instantiate(tradePanelObject) as GameObject;
            gridButton.SetActive(true);

            gridButton.GetComponent<ICollectionFlippo>().SetFlippoItem(f, PlayerManager.Instance.Inventory.GetFlippoAmount(f));
            gridButton.transform.SetParent(tradePanelObject.transform.parent, false);
        }*/
    }
}
