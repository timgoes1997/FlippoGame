using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeItem
{
    private int tradeId;
    private Flippo requestedFlippo;
    private Flippo proposedFlippo;
    private Account requester;
    private Account proposer;

    public int ID { get { return tradeId; } }
    public Flippo ProposedFlippo { get { return proposedFlippo; } }
    public Flippo RequestedFlippo { get { return requestedFlippo; } }   
    public Account Requester { get { return requester; } }
    public Account Proposer { get { return proposer; } }

    public TradeItem(int tradeId, Flippo proposedFlippo, Flippo requestedFlippo, Account proposer, Account requester)
    {
        this.tradeId = tradeId;
        this.proposedFlippo = proposedFlippo;
        this.requestedFlippo = requestedFlippo;
        this.requester = requester;
        this.proposer = proposer;
    }

    public TradeItem(Flippo proposedFlippo, Flippo requestedFlippo, Account proposer, Account requester)
    {
        this.proposedFlippo = proposedFlippo;
        this.requestedFlippo = requestedFlippo;
        this.requester = proposer;
        this.proposer = requester;
    }

    public TradeItem()
    {
        
    }

    public void SetProposedFlippo(Flippo proposed)
    {
        this.proposedFlippo = proposed;
    }

    public void SetRequestedFlippo(Flippo requested)
    {
        this.requestedFlippo = requested;
    }

    public void SetAccounts(Account proposer, Account requester)
    {
        this.proposer = proposer;
        this.requester = requester;
    }
}
