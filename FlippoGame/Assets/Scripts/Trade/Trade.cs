using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade {

    private Account account;
    private Flippo item;

    public Account Account { get { return account; } }
    public Flippo Item { get { return item; } }

    public Trade(Account account, Flippo item)
    {
        this.account = account;
        this.item = item;
    }

    public Trade()
    {

    }

    public void SetTradeItem(Flippo item)
    {
        this.item = item;
    }

    public void SetAccount(Account account)
    {
        this.account = account;
    }
}
