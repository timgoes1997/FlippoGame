using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeInspection : MonoBehaviour, ITradeInspection
{
    private Sprite defaultProposerSprite;
    private Sprite defaultRequesterSprite;

    public Sprite tradeAccepted;
    public Sprite tradePending;
    public Image tradeStatus;
    public Text tradeStatusText;
    public Text tradeIDText;

    public Text proposerAccount;
    public Text requesterAccount;
    public CollectionFlippo requested;
    public CollectionFlippo proposed;

    public bool destroy = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Continue()
    {
        if (destroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Inspect(TradeItem item)
    {
        bool isProposer = false;
        if (item.Proposer != null && item.Requester != null)
        {
            isProposer = item.Proposer.Account.Id == PlayerManager.Instance.Account.Id;
        }
        else
        {
            return;
        }

        if (tradeAccepted != null && tradePending != null)
        {
            tradeStatus.sprite = (item.Accepted) ? tradeAccepted : tradePending;
            if (tradeStatusText != null) tradeStatusText.text = (item.Accepted) ? "Voltooid" : "In afwachting";
            if (tradeIDText.text != null) tradeIDText.text = "Trade #" + item.ID.ToString();
        }
        else
        {
            return;
        }

        if (requested != null) requested.SetFlippoItem((isProposer) ? item.RequestedFlippo : item.ProposedFlippo);
        if (proposed != null) proposed.SetFlippoItem((isProposer) ? item.ProposedFlippo : item.RequestedFlippo);
        if (proposerAccount != null) proposerAccount.text = (item.Proposer != null && item.Proposer.Account != null) ? 
                (isProposer) ? "Jij" : (item.Requester.Account.Id > 0) ? item.Requester.Account.Id.ToString() : "Geen"
                    : "??";
        if (requesterAccount != null) requesterAccount.text = (item.Requester != null && item.Requester.Account != null) ? 
                (isProposer) ? (item.Requester.Account.Id > 0) ? item.Requester.Account.Id.ToString() : "Geen" : "Jij" 
                    : "??";
    }
}
