using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeView : MonoBehaviour, ITradePanel
{
    public Image proposedImage;
    public Image requestedImage;
    public Text otherTraderText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetProposedFlippoSprite(Sprite sprite)
    {
        if (proposedImage == null) return;

        proposedImage.sprite = sprite;
    }

    public void SetRequestedFlippoSprite(Sprite sprite)
    {
        if (requestedImage == null) return;

        requestedImage.sprite = sprite;
    }

    public void SetupTradePanel(TradeItem item)
    {
        if(proposedImage != null)  proposedImage.sprite = item.Proposed.sprite;
        if (requestedImage != null) requestedImage.sprite = item.Requested.sprite;
        if (otherTraderText != null) otherTraderText.text = item.Other.Id.ToString();
    }
}
