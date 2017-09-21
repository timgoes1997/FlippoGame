using System;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    public GameObject pendingTradeMenu;
    public GameObject acceptedTradeMenu;
    public GameObject requestedTradeMenu;
    public GameObject declinedTradeMenu;
    public GameObject tradeMenu;
    public GameObject flippoCollectionMenu;

    public GameObject proposedFlippoImage;
    public GameObject requestedFlippoImage;
    public TradeGridHandler handler;
    public Text otherTraderId;

    private bool proposedFlippo = false;
    private TradeItem currentTrade;

    // Use this for initialization
    void Start()
    {
        currentTrade = new TradeItem();
        //StartCoroutine(GetTrades(1));
        //StartCoroutine(CreateAccount());
    }

    public void ActivatePendingTradeMenu()
    {
        acceptedTradeMenu.SetActive(false);
        requestedTradeMenu.SetActive(false);
        declinedTradeMenu.SetActive(false);
        tradeMenu.SetActive(false);
        flippoCollectionMenu.SetActive(false);
        pendingTradeMenu.SetActive(true);
        GetPendingTrades(handler);
    }

    public void ActivateAcceptedTradeMenu()
    {
        pendingTradeMenu.SetActive(false);
        requestedTradeMenu.SetActive(false);
        declinedTradeMenu.SetActive(false);
        tradeMenu.SetActive(false);
        flippoCollectionMenu.SetActive(false);
        acceptedTradeMenu.SetActive(true);
    }

    public void ActivateRequestedTradeMenu()
    {
        pendingTradeMenu.SetActive(false);
        acceptedTradeMenu.SetActive(false);
        declinedTradeMenu.SetActive(false);
        tradeMenu.SetActive(false);
        flippoCollectionMenu.SetActive(false);
        requestedTradeMenu.SetActive(true);
        GetRequestedTrades(handler);
    }

    public void ActivateDeclinedTradeMenu()
    {
        pendingTradeMenu.SetActive(false);
        acceptedTradeMenu.SetActive(false);
        requestedTradeMenu.SetActive(false);
        tradeMenu.SetActive(false);
        flippoCollectionMenu.SetActive(false);
        declinedTradeMenu.SetActive(true);
    }

    public void ActivateTradeMenu()
    {
        pendingTradeMenu.SetActive(false);
        acceptedTradeMenu.SetActive(false);
        requestedTradeMenu.SetActive(false);
        declinedTradeMenu.SetActive(false);
        flippoCollectionMenu.SetActive(false);
        tradeMenu.SetActive(true);
    }

    public void LoadRequestFlippoCollection()
    {
        pendingTradeMenu.SetActive(false);
        acceptedTradeMenu.SetActive(false);
        requestedTradeMenu.SetActive(false);
        declinedTradeMenu.SetActive(false);
        tradeMenu.SetActive(false);
        flippoCollectionMenu.SetActive(true);
        proposedFlippo = true;
    }

    public void LoadProposedFlippoCollection()
    {
        pendingTradeMenu.SetActive(false);
        acceptedTradeMenu.SetActive(false);
        requestedTradeMenu.SetActive(false);
        declinedTradeMenu.SetActive(false);
        tradeMenu.SetActive(false);
        flippoCollectionMenu.SetActive(true);
        proposedFlippo = false;
    }

    public void Trade()
    {
        string otherID = otherTraderId.text;
        if (otherID == "") return; //Error message laten zien

        int otherTrader = int.Parse(otherID);

        if (currentTrade != null && currentTrade.Proposed != null && currentTrade.Requested != null && PlayerManager.Instance.Account.Id != otherTrader && otherTrader > 0)
        {
            currentTrade.SetAccounts(PlayerManager.Instance.Account, new Account(otherTrader));
            StartCoroutine(TradeRequest(currentTrade));
        }
    }

    IEnumerator TradeRequest(TradeItem item)
    {
        Debug.Log("Create trade");
        WWWForm form = new WWWForm();
        form.AddField("accountId", item.Your.Id);
        form.AddField("flippoId", item.Proposed.id);
        form.AddField("otheraccId", item.Other.Id);
        form.AddField("otherflippoId", item.Requested.id);

        UnityWebRequest www = UnityWebRequest.Post(Files.JsonURL + "/trade/create", form);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            //failed message
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            PlayerManager.Instance.Inventory.RemoveFlippo(item.Proposed.id);
            ActivatePendingTradeMenu(); //toevoegen decline gemaakte proposed trade, als de user een fout maakt kan die namelijk niet zijn flippo terugkrijgen.
            currentTrade = new TradeItem();
        }
    }

    public void SetTradeFlippo(int id)
    {
        if (currentTrade == null) currentTrade = new TradeItem();

        Flippo f = GameManager.Instance.GetFlippoByID(id);
        if (f == null) return;

        if (proposedFlippo)
        {
            currentTrade.SetProposedFlippo(f);
            if (proposedFlippoImage != null) proposedFlippoImage.GetComponent<Image>().sprite = f.sprite;
        }
        else
        {
            currentTrade.SetRequestedFlippo(f);
            if (requestedFlippoImage != null) requestedFlippoImage.GetComponent<Image>().sprite = f.sprite;
        }
        ActivateTradeMenu();
    }


    public void GetPendingTrades(TradeGridHandler handler)
    {
        if (PlayerManager.Instance.Account != null && handler != null)
        {
            StartCoroutine(GetPendingTradesEnumerator(PlayerManager.Instance.Account.Id, handler));
        }
    }

    IEnumerator GetPendingTradesEnumerator(int proposerId, TradeGridHandler handler)
    {
        Debug.Log("Proposed trade");

        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/out?id=" + PlayerManager.Instance.Account.Id);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
#if UNITY_EDITOR
            Debug.Log(json);
#endif
            var jArray = JArray.Parse(json);
            List<TradeItem> trades = new List<TradeItem>();
            foreach (JObject item in jArray.Children())
            {
                //{"id":0,"proposer":{"id":7},"proposerFlippo":{"id":2},"reciever":{"id":2},"receiverFlippo":{"id":6}}
                int tradeID = item.GetValue("id").Value<int>();
                int yourID = item["proposer"]["id"].Value<int>();
                int flippoID = item["proposerFlippo"]["id"].Value<int>();
                int receiverID = item["reciever"]["id"].Value<int>();
                int receiverFlippoID = item["receiverFlippo"]["id"].Value<int>();
                Debug.Log(tradeID + "-" + yourID + "-" + flippoID + "-" + receiverID + "-" + receiverFlippoID);

                trades.Add(new TradeItem(tradeID, GameManager.Instance.GetFlippoByID(flippoID), GameManager.Instance.GetFlippoByID(receiverFlippoID), new Account(receiverID), new Account(yourID)));
            }
            handler.GenerateGridButtons(trades);
        }
    }

    public void GetRequestedTrades(TradeGridHandler handler)
    {
        if (PlayerManager.Instance.Account != null && handler != null)
        {
            StartCoroutine(GetRequestedTradesEnumerator(PlayerManager.Instance.Account.Id, handler));
        }
    }

    IEnumerator GetRequestedTradesEnumerator(int proposerId, TradeGridHandler handler)
    {
        Debug.Log("Proposed trade");

        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/in?id=" + PlayerManager.Instance.Account.Id);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
#if UNITY_EDITOR
            Debug.Log(json);
#endif
            var jArray = JArray.Parse(json);
            List<TradeItem> trades = new List<TradeItem>();
            foreach (JObject item in jArray.Children())
            {
                //{"id":0,"proposer":{"id":7},"proposerFlippo":{"id":2},"reciever":{"id":2},"receiverFlippo":{"id":6}}
                int tradeID = item.GetValue("id").Value<int>();
                int yourID = item["proposer"]["id"].Value<int>();
                int flippoID = item["proposerFlippo"]["id"].Value<int>();
                int receiverID = item["reciever"]["id"].Value<int>();
                int receiverFlippoID = item["receiverFlippo"]["id"].Value<int>();
                Debug.Log(tradeID + "-" + yourID + "-" + flippoID + "-" + receiverID + "-" + receiverFlippoID);

                trades.Add(new TradeItem(tradeID,
                    GameManager.Instance.GetFlippoByID(receiverFlippoID),
                    GameManager.Instance.GetFlippoByID(flippoID),
                    new Account(yourID),
                    new Account(receiverID)));
            }
            handler.GenerateGridButtons(trades, true);
        }
    }

    public bool RespondToTrade(TradeItem item, bool accepted)
    {
        if (PlayerManager.Instance.Inventory.HasFlippo(item.Proposed.id))
        {
            StartCoroutine(RespondToTradeCoroutine(item, accepted));
            return true;
        }
        return false;
    }

    IEnumerator RespondToTradeCoroutine(TradeItem item, bool accepted)
    {
        Debug.Log("Respond to trade");
        WWWForm form = new WWWForm();
        form.AddField("tradeId", item.ID);
        form.AddField("response", accepted ? 1 : 0);

        UnityWebRequest www = UnityWebRequest.Post(Files.JsonURL + "/trade/respond", form);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            PlayerManager.Instance.Inventory.RemoveFlippo(item.Proposed.id);
            PlayerManager.Instance.Inventory.AddFlippo(item.Requested.id);
            ActivateRequestedTradeMenu();
        }
    }

    #region old
    IEnumerator GetTrades(int accountID)
    {
        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/item?id=" + accountID);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }


    IEnumerator CreateAccount()
    {
        Debug.Log("Create account");
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Post(Files.JsonURL + "/account/create", form);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            Debug.Log(json);
            var jo = JObject.Parse(json);
            int id = jo["id"].Value<int>();
            Debug.Log(id);
            StartCoroutine(CreateTrades(id, id + 1));
        }
    }

    IEnumerator CreateTrades(int proposer, int receiver)
    {
        Debug.Log("Create trade");
        WWWForm form = new WWWForm();
        form.AddField("accountId", proposer);
        form.AddField("flippoId", 2);
        form.AddField("otheraccId", receiver);
        form.AddField("otherflippoId", 3);

        UnityWebRequest www = UnityWebRequest.Post(Files.JsonURL + "/trade/create", form);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            StartCoroutine(GetProposedTrades(proposer, receiver));
        }
    }

    IEnumerator GetProposedTrades(int proposer, int receiver)
    {
        Debug.Log("Proposed trade");
        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/out?id=" + proposer);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            StartCoroutine(GetTradeRequests(proposer, receiver));
        }
    }

    IEnumerator GetTradeRequests(int proposer, int receiver)
    {
        Debug.Log("Trade requests");
        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/in?id=" + receiver);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            Debug.Log(json);
            var jo = JArray.Parse(json);
            if (jo.Count > 0)
            {
                int id = jo[0]["id"].Value<int>();
                Debug.Log("Trade: " + id);
                StartCoroutine(RespondToTrade(id, proposer, receiver));
            }
        }
    }


    IEnumerator RespondToTrade(int tradeID, int proposer, int receiver)
    {
        Debug.Log("Respond to trade");
        WWWForm form = new WWWForm();
        form.AddField("tradeId", tradeID);
        form.AddField("response", 1);

        UnityWebRequest www = UnityWebRequest.Post(Files.JsonURL + "/trade/respond", form);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            StartCoroutine(AcceptTrade(tradeID, proposer, receiver));
        }
    }

    IEnumerator AcceptTrade(int tradeID, int proposer, int receiver)
    {
        Debug.Log("Accept trade - proposer: " + proposer + " - receiver: " + receiver);
        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/accepted?id=" + proposer);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            Debug.Log(json);
        }
    }
    #endregion
}
