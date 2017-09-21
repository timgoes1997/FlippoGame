using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITradePanel
{
    void SetProposedFlippoSprite(Sprite sprite);
    void SetRequestedFlippoSprite(Sprite sprite);
    void SetupTradePanel(TradeItem item);
}
