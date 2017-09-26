using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seperator : MonoBehaviour, ISeperator
{
    public Text seperatorText;

    public void SetText(string text)
    {
        if (seperatorText != null) seperatorText.text = text;
    }
}
