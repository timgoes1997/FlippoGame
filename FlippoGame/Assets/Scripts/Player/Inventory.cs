using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory {
    public int Id;
    public List<PlayerFlippo> flippos;	

    public bool AddFlippo(int id)
    {
        foreach(PlayerFlippo f in flippos)
        {
            if(f.flippoID == id)
            {
                f.amount++;
                return true;
            }
        }

        flippos.Add(new PlayerFlippo(id, 1));
        return true;
    }
}
