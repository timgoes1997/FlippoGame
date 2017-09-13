using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Flippo {
    public int id;
    public int amount;
    public bool unlocked;
    public Image flippoImage;
    public UnityEvent questionEvent; //For loading a certain scene or something else.
}
