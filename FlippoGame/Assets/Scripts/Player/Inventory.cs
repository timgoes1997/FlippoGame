using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory {
    public int Id;
    public List<PlayerFlippo> flippos;
    public PlayerFlippo lastAddedFlippo;

    public bool HasFlippo(int id)
    {
        foreach(PlayerFlippo f in flippos)
        {
            if(f.flippoID == id)
            {
                return true;
            }
        }
        return false;
    }

    public bool AddFlippo(int id)
    {
        foreach(PlayerFlippo f in flippos)
        {
            if(f.flippoID == id)
            {
                lastAddedFlippo = f;
                f.amount++;
                return true;
            }
        }

        flippos.Add(new PlayerFlippo(id, 1));
        return true;
    }

    public bool RemoveFlippo(int id)
    {
        for (int i = 0; i < flippos.Count; i++)
        {
            if (flippos[i].flippoID == id)
            {
                if (flippos[i].amount > 1)
                {
                    flippos[i].amount--;
                    return true;
                }
                else
                {
                    flippos.Remove(flippos[i]);
                    return true;
                }
            }
        }
        return false;
    }

    public int GetFlippoAmount(Flippo flippo)
    {
        foreach(PlayerFlippo f in flippos)
        {
            if(flippo.id == f.flippoID)
            {
                return f.amount;
            }
        }
        return 0;
    }
}
