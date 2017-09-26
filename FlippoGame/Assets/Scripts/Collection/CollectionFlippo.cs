using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionFlippo : MonoBehaviour, ICollectionFlippo
{

    [SerializeField]
    private Image myIcon;

    [SerializeField]
    public GameObject gridButton;

    private Flippo flippo;

    public int flippoAmount = 1;
    public Text flippoText;
    public TradeManager tradeManager;

    public GameObject InspectionPanel;

    // Use this for initialization
    void Start()
    {
        myIcon = gridButton.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFlippoItem(Flippo flippo, int flippoAmount = 1)
    {
        if (myIcon == null)
        {
            myIcon = gridButton.GetComponent<Image>();
        }

        flippoText.text = flippoAmount.ToString();
        myIcon.sprite = flippo.sprite;
        this.flippo = flippo;
    }

    public void OnClick()
    {
#if UNITY_EDITOR
        if (flippo != null)
            Debug.Log("Pressed flippo:" + flippo.id);
#endif
        if (tradeManager == null)
        {
            if (InspectionPanel == null)
            {
                InspectionPanel =  Instantiate(Resources.Load("Messages/InspectionPanel", typeof(GameObject)), FindObjectOfType<Canvas>().transform, false) as GameObject;
            }
            
            if(InspectionPanel != null)
            {
                InspectionPanel.SetActive(true);
                InspectionPanel.GetComponent<IInspection>().Inspect(flippo);
            }
            return;
        }

        if (flippo != null) tradeManager.SetTradeFlippo(flippo.id);
    }
}
