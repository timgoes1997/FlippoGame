using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTraderID : MonoBehaviour
{

    public Text text;

    // Use this for initialization
    void Start()
    {
        Account acc = PlayerManager.Instance.Account;
        if (acc != null)
        {
            text.text = acc.Id.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
