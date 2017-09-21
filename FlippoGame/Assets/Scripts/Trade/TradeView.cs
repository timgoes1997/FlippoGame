using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeView : MonoBehaviour, ITradePanel
{
    public Image proposedImage;
    public Image requestedImage;
    public Text otherTraderText;

    public TradeItem item;
    public TradeManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetProposedFlippoSprite(Sprite sprite)
    {
        if (proposedImage == null) return;

        proposedImage.sprite = sprite;
    }

    public void SetRequestedFlippoSprite(Sprite sprite)
    {
        if (requestedImage == null) return;

        requestedImage.sprite = sprite;
    }

    public void SetupTradePanel(TradeItem item)
    {
        this.item = item;
        if (proposedImage != null)  proposedImage.sprite = item.Proposed.sprite;
        if (requestedImage != null) requestedImage.sprite = item.Requested.sprite;
        if (otherTraderText != null) otherTraderText.text = "Trader: " + item.Other.Id.ToString();
    }

    public void AcceptTrade()
    {
        if (manager == null) return;
        manager.RespondToTrade(item, true);
    }

    public void DeclineTrade()
    {
        if (manager == null) return;
        manager.RespondToTrade(item, false);
    }
}
