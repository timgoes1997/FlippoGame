using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeGridHandler : MonoBehaviour {

    public TradeManager tradeManager;

    public GameObject tradeView;
    public GameObject tradeRequestView;
    public GridLayoutGroup grid;

    [SerializeField]
    private GameObject tradePanelObject;

    private List<GameObject> currentChildren;

    // Use this for initialization
    void Start()
    {
        tradeManager.GetPendingTrades(this);
        if (tradePanelObject == null) tradePanelObject = gameObject;
        //GenerateGridButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateGridButtons(List<TradeItem> tradeItems, bool request = false)
    {
        if (tradeView == null) return;
        if (tradeRequestView == null) return;
        if (currentChildren == null) currentChildren = new List<GameObject>();
        if (grid != null) grid.cellSize = request ? new Vector2(1000, 400) : new Vector2(1000, 300);

        for (int i = currentChildren.Count - 1; i >= 0; i--)
        {
            Destroy(currentChildren[i]);
            currentChildren.Remove(currentChildren[i]);
        }

        foreach (TradeItem item in tradeItems)
        {
            GameObject view = (request) ? Instantiate(tradeRequestView) as GameObject : Instantiate(tradeView) as GameObject;
            currentChildren.Add(view);
            view.SetActive(true);
            view.GetComponent<ITradePanel>().SetupTradePanel(item);
            view.transform.SetParent(tradeView.transform.parent, false);
        }

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
