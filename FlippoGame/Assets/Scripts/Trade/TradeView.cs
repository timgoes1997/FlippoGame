using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeView : MonoBehaviour, ITradePanel
{
    public Image proposedImage;
    public Image requestedImage;
    public Image statusImage;
    public Sprite acceptedSprite;
    public Sprite pendingSprite;
    
    public Text proposedFlippoText;
    public Text requestedFlippoText;

    public TradeItem item;
    public TradeManager manager;
    public bool accepted;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

        bool isProposer = false;
        if (item.Proposer != null && item.Requester != null)
        {
            isProposer = item.Proposer.Account.Id == PlayerManager.Instance.Account.Id;
        }

        if (item.ProposedFlippo == null && item.RequestedFlippo == null) return;
        if (proposedFlippoText != null) proposedFlippoText.text = (isProposer) ? "#" + item.Proposer.Item.id.ToString() : "#" + item.Requester.Item.id.ToString();
        if (proposedImage != null) proposedImage.sprite = (isProposer) ? item.Proposer.Item.sprite : item.Requester.Item.sprite;
        if (requestedFlippoText != null) requestedFlippoText.text = (isProposer) ? "#" + item.Requester.Item.id.ToString() : "#" + item.Proposer.Item.id.ToString();
        if (requestedImage != null) requestedImage.sprite = (isProposer) ? item.Requester.Item.sprite : item.Proposer.Item.sprite;
        if (statusImage != null && acceptedSprite != null && pendingSprite != null) statusImage.sprite = (item.Accepted) ? acceptedSprite : pendingSprite;
        //if (otherTraderText != null) otherTraderText.text = (isProposer) ? "Trader: " + item.Proposer.Account.ToString() : "Trader: " + item.Requester.Account.Id.ToString();
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
