using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITradeInspection {
    void Continue();
    void Inspect(TradeItem item);
}
