using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeGridHandler : MonoBehaviour {

    public TradeManager tradeManager;

    public Vector2 oldSize = new Vector2(1000, 400);
    public Vector2 currentTradePanelSize = new Vector2(1000, 300);

    public GameObject tradeView;
    public GameObject tradeSeperatorMessageView;
    public GameObject tradeRequestView;
    public GridLayoutGroup grid;

    [SerializeField]
    private GameObject tradePanelObject;

    private List<GameObject> currentChildren;

    // Use this for initialization
    void Start()
    {
        tradeManager.GetAcceptedTrades(this);
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
        if (grid != null) grid.cellSize = request ? oldSize : currentTradePanelSize;

        for (int i = currentChildren.Count - 1; i >= 0; i--)
        {
            Destroy(currentChildren[i]);
            currentChildren.Remove(currentChildren[i]);
        }

        if (tradeItems.Count > 0)
        {
            AddSeperator("History");
        }

        foreach (TradeItem item in tradeItems)
        {
            SetupNewTradeView(request, item);
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

    public void GenerateGridButtons(List<TradeItem> accepted, List<TradeItem> pending, List<TradeItem> recentlyAccepted, bool request = false)
    {
        if (tradeView == null) return;
        if (tradeRequestView == null) return;
        if (currentChildren == null) currentChildren = new List<GameObject>();
        if (grid != null) grid.cellSize = request ? oldSize : currentTradePanelSize;

        for (int i = currentChildren.Count - 1; i >= 0; i--)
        {
            Destroy(currentChildren[i]);
            currentChildren.Remove(currentChildren[i]);
        }

        if (recentlyAccepted.Count > 0) AddSeperator("Binnen gekomen");
        foreach (TradeItem item in recentlyAccepted)
        {
            SetupNewTradeView(request, item);
        }

        if (pending.Count > 0) AddSeperator("Te ontvangen");
        foreach (TradeItem item in pending)
        {
            SetupNewTradeView(request, item);
        }

        if (accepted.Count > 0) AddSeperator("Voltooid");
        foreach (TradeItem item in accepted)
        {
            SetupNewTradeView(request, item);
        }
    }

    private void SetupNewTradeView(bool request, TradeItem item)
    {
        GameObject view = (request) ? Instantiate(tradeRequestView) as GameObject : Instantiate(tradeView) as GameObject;
        currentChildren.Add(view);
        view.SetActive(true);
        view.GetComponent<ITradePanel>().SetupTradePanel(item);
        view.transform.SetParent(tradeView.transform.parent, false);
    }

    private void AddSeperator(string text)
    {
        GameObject sep = Instantiate(tradeSeperatorMessageView) as GameObject;
        currentChildren.Add(sep);
        sep.SetActive(true);
        sep.GetComponent<ISeperator>().SetText(text);
        sep.transform.SetParent(tradeSeperatorMessageView.transform.parent, false);
    }
}
