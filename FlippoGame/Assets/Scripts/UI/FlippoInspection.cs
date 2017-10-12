using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlippoInspection : MonoBehaviour, IFlippoInspection
{
    public Image flippoSprite;

    public string flippoIDDefaultText = "ID: ";
    public string flippoNameDefaultText = "Naam: ";
    public string flippoCollectionDefaultText = "Collectie: ";
    public string flippoRarityDefaultText = "Zeldzaamheid: ";
    public string collectedFlipposDefaultText = "Verzameld: ";

    public Text flippoID;
    public Text flippoName;
    public Text flippoCollection;
    public Text flippoRarity;
    public Text collectedFlippos;
    public bool destroy = true;
    private Flippo flippo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Inspect(Flippo f)
    {
        flippo = f;
        int amount = PlayerManager.Instance.Inventory.GetFlippoAmount(f);

        if (flippoSprite != null) flippoSprite.sprite = f.sprite;
        if (flippoID != null) flippoID.text = flippoIDDefaultText + f.id.ToString();
        if (flippoName != null) flippoName.text = flippoNameDefaultText + f.sprite.name.Substring(3);
        if (flippoCollection != null) flippoCollection.text = flippoCollectionDefaultText + f.collection.ToString();
        if (flippoRarity != null) flippoRarity.text = flippoRarityDefaultText + f.rarity.ToString();
        if (collectedFlippos != null) collectedFlippos.text = collectedFlipposDefaultText + amount.ToString();
    }

    public void Trade()
    {
        if (PlayerManager.Instance.Inventory.HasFlippo(flippo.id)) GameManager.Instance.flippoCache = flippo;
        SceneManager.LoadScene("Trade");
    }

    public void Continue()
    {
        if (destroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
