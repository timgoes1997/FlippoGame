using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{

    public Text text;
    public bool destroy = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string message)
    {
        text.text = message;
    }

    public void Continue()
    {
        if (!destroy)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(this);
        }

    }
}
