using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeItem
{
    private int tradeId;
    private Flippo proposed;
    private Flippo requested;
    private Account other;
    private Account yours;

    public int ID { get { return tradeId; } }
    public Flippo Proposed { get { return proposed; } }
    public Flippo Requested { get { return requested; } }   
    public Account Other { get { return other; } }
    public Account Your { get { return yours; } }

    public TradeItem(int tradeId, Flippo proposed, Flippo requested, Account other, Account yours)
    {
        this.tradeId = tradeId;
        this.proposed = proposed;
        this.requested = requested;
        this.other = other;
        this.yours = yours;
    }

    public TradeItem(Flippo proposed, Flippo requested, Account other, Account yours)
    {
        this.proposed = proposed;
        this.requested = requested;
        this.other = other;
        this.yours = yours;
    }

    public TradeItem()
    {
        
    }

    public void SetProposedFlippo(Flippo proposed)
    {
        this.proposed = proposed;
    }

    public void SetRequestedFlippo(Flippo requested)
    {
        this.requested = requested;
    }

    public void SetAccounts(Account yours, Account other)
    {
        this.yours = yours;
        this.other = other;
    }
}
