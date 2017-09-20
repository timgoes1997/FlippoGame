using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerFlippo {
    public PlayerFlippo()
    {

    }

    public PlayerFlippo(int flippoID, int amount)
    {
        this.flippoID = flippoID;
        this.amount = amount;
    }

    public int flippoID;
    public int amount;
}
