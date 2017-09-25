using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeItem
{
    private int tradeId;
    private Trade requester;
    private Trade proposer;
    private bool accepted;

    public int ID { get { return tradeId; } }
    public Trade Requester { get { return requester; } }
    public Trade Proposer { get { return proposer; } }
    public Flippo ProposedFlippo { get { return proposer.Item; } }
    public Flippo RequestedFlippo { get { return requester.Item; } }   
    public Account RequesterAccount { get { return proposer.Account; } }
    public Account ProposerAccount { get { return proposer.Account; } }
    public bool Accepted { get { return accepted; } }

    public TradeItem(Trade requester, Trade proposer)
    {
        this.requester = requester;
        this.proposer = proposer;    
    }

    public TradeItem(int tradeId, Trade requester, Trade proposer)
    {
        this.requester = requester;
        this.proposer = proposer;
    }

    public TradeItem(int tradeId, Trade requester, Trade proposer, bool accepted = false)
    {
        this.requester = requester;
        this.proposer = proposer;
        this.accepted = accepted;
    }

    public TradeItem(int tradeId, Flippo proposedFlippo, Flippo requestedFlippo, Account proposer, Account requester)
    {
        this.tradeId = tradeId;
        this.requester = new Trade(requester, requestedFlippo);
        this.proposer = new Trade(proposer, proposedFlippo);
    }

    public TradeItem(int tradeId, Flippo proposedFlippo, Flippo requestedFlippo, Account proposer, Account requester, bool accepted = false)
    {
        this.tradeId = tradeId;
        this.requester = new Trade(requester, requestedFlippo);
        this.proposer = new Trade(proposer, proposedFlippo);
        this.accepted = accepted;
    }

    public TradeItem(Flippo proposedFlippo, Flippo requestedFlippo, Account proposer, Account requester)
    {
        this.requester = new Trade(requester, requestedFlippo);
        this.proposer = new Trade(proposer, proposedFlippo);
    }

    public TradeItem()
    {
        requester = new Trade();
        proposer = new Trade();

    }

    public void SetRequesterAccount(Account a)
    {
        requester.SetAccount(a);
    }

    public void SetProposerAccount(Account a)
    {
        proposer.SetAccount(a);
    }

    public void SetRequestedFlippo(Flippo f)
    {
        requester.SetTradeItem(f);
    }

    public void SetProposedFlippo(Flippo f)
    {
        proposer.SetTradeItem(f);
    }

    public void SetProposer(Trade proposer)
    {
        this.proposer = proposer;
    }

    public void SetRequester(Trade requester)
    {
        this.requester = requester;
    }
}
