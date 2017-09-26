using System;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    private Sprite defaultSpritePropose;
    private Sprite defaultSpriteRequest;

    public GameObject pendingTradeMenu;
    public GameObject acceptedTradeMenu;
    public GameObject requestedTradeMenu;
    public GameObject declinedTradeMenu;
    public GameObject tradeMenu;
    public GameObject flippoCollectionMenu;

    public GameObject proposedFlippoImage;
    public GameObject requestedFlippoImage;
    public GameObject messageBox;
    public TradeGridHandler handler;

    private bool proposedFlippo = false;
    private TradeItem currentTrade;

    private void Awake()
    {
        defaultSpritePropose = proposedFlippoImage.GetComponent<Image>().sprite;
        defaultSpriteRequest = requestedFlippoImage.GetComponent<Image>().sprite;
    }

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
        //GetPendingTrades(handler);
        GetTradeHistory(handler);
    }

    public void ActivateAcceptedTradeMenu()
    {
        pendingTradeMenu.SetActive(false);
        requestedTradeMenu.SetActive(false);
        declinedTradeMenu.SetActive(false);
        tradeMenu.SetActive(false);
        flippoCollectionMenu.SetActive(false);
        acceptedTradeMenu.SetActive(true);
        GetAcceptedTrades(handler);
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
        flippoCollectionMenu.GetComponent<IGrid>().SetGrid(true);
        flippoCollectionMenu.SetActive(true);
        proposedFlippo = false;
    }

    public void LoadProposedFlippoCollection()
    {
        pendingTradeMenu.SetActive(false);
        acceptedTradeMenu.SetActive(false);
        requestedTradeMenu.SetActive(false);
        declinedTradeMenu.SetActive(false);
        tradeMenu.SetActive(false);
        flippoCollectionMenu.GetComponent<IGrid>().SetGrid(false);
        flippoCollectionMenu.SetActive(true);
        proposedFlippo = true;
    }

    public void Trade()
    {
        if (currentTrade != null && currentTrade.ProposedFlippo != null && currentTrade.RequestedFlippo != null)
        {
            currentTrade.SetProposerAccount(PlayerManager.Instance.Account);
            StartCoroutine(TradeRequest(currentTrade));
        }
    }

    IEnumerator TradeRequest(TradeItem item)
    {
#if UNITY_EDITOR
        Debug.Log("Create trade");
#endif
        WWWForm form = new WWWForm();
        form.AddField("accountId", item.Proposer.Account.Id);
        form.AddField("flippoId", item.Proposer.Item.id);
        form.AddField("otherflippoId", item.Requester.Item.id);

        UnityWebRequest www = UnityWebRequest.Post(Files.JsonURL + "/trade/create", form);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
            ShowError(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
#if UNITY_EDITOR
            Debug.Log(json);
#endif
            PlayerManager.Instance.Inventory.RemoveFlippo(item.ProposedFlippo.id);
            if (json != "")
            {
                JObject jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    int proposedFlippo = jObject["proposerFlippo"]["id"].Value<int>();
                    PlayerManager.Instance.Inventory.AddFlippo(proposedFlippo);
                }
            }
            ActivatePendingTradeMenu(); //toevoegen decline gemaakte proposed trade, als de user een fout maakt kan die namelijk niet zijn flippo terugkrijgen.
            ResetTrade();
        }
    }

    public void ResetTrade()
    {
        proposedFlippoImage.GetComponent<Image>().sprite = defaultSpritePropose;
        requestedFlippoImage.GetComponent<Image>().sprite = defaultSpriteRequest;
        currentTrade = new TradeItem();
    }

    public void SetTradeFlippo(int id)
    {
        if (currentTrade == null) currentTrade = new TradeItem();

        Flippo f = GameManager.Instance.GetFlippoByID(id);
        if (f == null) return;

        if (proposedFlippo)
        {
            currentTrade.SetRequestedFlippo(f);
            if (proposedFlippoImage != null) proposedFlippoImage.GetComponent<Image>().sprite = f.sprite;
        }
        else
        {
            currentTrade.SetProposedFlippo(f);
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
#if UNITY_EDITOR
        Debug.Log("Proposed trade");
#endif

        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/out?id=" + PlayerManager.Instance.Account.Id);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
            ShowError(www.error);
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
                int proposerAccount = item["proposer"]["id"].Value<int>();
                int proposedFlippo = item["proposerFlippo"]["id"].Value<int>();
                //int requesterAccount = item["reciever"]["id"].Value<int>();
                int requestedFlippo = item["receiverFlippo"]["id"].Value<int>();

                Trade proposer = new Trade(new Account(proposerAccount), GameManager.Instance.GetFlippoByID(proposedFlippo));
                Trade requester = new Trade(new Account(-1), GameManager.Instance.GetFlippoByID(requestedFlippo));
                trades.Add(new TradeItem(tradeID, requester, proposer));
            }
            handler.GenerateGridButtons(trades);
        }
    }

    public void GetTradeHistory(TradeGridHandler handler)
    {
        if (PlayerManager.Instance.Account != null && handler != null)
        {
            StartCoroutine(GetTradeHistoryEnumerator(handler));
        }
    }

    IEnumerator GetTradeHistoryEnumerator(TradeGridHandler handler)
    {
#if UNITY_EDITOR
        Debug.Log("Trade history");
#endif

        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/history?id=" + PlayerManager.Instance.Account.Id);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
            ShowError(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
#if UNITY_EDITOR
            Debug.Log(json);
#endif
            var jObject = JObject.Parse(json);
            var jAccepted = jObject["accepted"];
            var jPending = jObject["pending"];
            List<TradeItem> trades = new List<TradeItem>();


            foreach (JObject item in jAccepted.Children())
            {
                //{"id":0,"proposer":{"id":7},"proposerFlippo":{"id":2},"reciever":{"id":2},"receiverFlippo":{"id":6}}
                int tradeID = item.GetValue("id").Value<int>();
                int proposerAccount = item["proposer"]["id"].Value<int>();
                int proposedFlippo = item["proposerFlippo"]["id"].Value<int>();
                //int requesterAccount = item["reciever"]["id"].Value<int>();
                int requestedFlippo = item["receiverFlippo"]["id"].Value<int>();

                Trade proposer = new Trade(new Account(proposerAccount), GameManager.Instance.GetFlippoByID(proposedFlippo));
                Trade requester = new Trade(new Account(-1), GameManager.Instance.GetFlippoByID(requestedFlippo));
                trades.Add(new TradeItem(tradeID, requester, proposer, true));
            }

            foreach (JObject item in jPending.Children())
            {
                //{"id":0,"proposer":{"id":7},"proposerFlippo":{"id":2},"reciever":{"id":2},"receiverFlippo":{"id":6}}
                int tradeID = item.GetValue("id").Value<int>();
                int proposerAccount = item["proposer"]["id"].Value<int>();
                int proposedFlippo = item["proposerFlippo"]["id"].Value<int>();
                //int requesterAccount = item["reciever"]["id"].Value<int>();
                int requestedFlippo = item["receiverFlippo"]["id"].Value<int>();

                Trade proposer = new Trade(new Account(proposerAccount), GameManager.Instance.GetFlippoByID(proposedFlippo));
                Trade requester = new Trade(new Account(-1), GameManager.Instance.GetFlippoByID(requestedFlippo));
                trades.Add(new TradeItem(tradeID, requester, proposer, false));
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
#if UNITY_EDITOR
        Debug.Log("Proposed trade");
#endif

        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/in?id=" + PlayerManager.Instance.Account.Id);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
            ShowError(www.error);
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
                int proposerAccount = item["proposer"]["id"].Value<int>();
                int proposedFlippo = item["proposerFlippo"]["id"].Value<int>();
                //int requesterAccount = item["reciever"]["id"].Value<int>();
                int requestedFlippo = item["receiverFlippo"]["id"].Value<int>();

                Trade proposer = new Trade(new Account(proposerAccount), GameManager.Instance.GetFlippoByID(proposedFlippo));
                Trade requester = new Trade(new Account(-1), GameManager.Instance.GetFlippoByID(requestedFlippo));
                trades.Add(new TradeItem(tradeID, requester, proposer));
            }
            handler.GenerateGridButtons(trades, true);
        }
    }

    public bool RespondToTrade(TradeItem item, bool accepted)
    {
        if (PlayerManager.Instance.Inventory.HasFlippo(item.RequestedFlippo.id) || accepted == false)
        {
            StartCoroutine(RespondToTradeCoroutine(item, accepted));
            return true;
        }
        return false;
    }

    IEnumerator RespondToTradeCoroutine(TradeItem item, bool accepted)
    {
#if UNITY_EDITOR
        Debug.Log("Respond to trade");
#endif
        WWWForm form = new WWWForm();
        form.AddField("tradeId", item.ID);
        form.AddField("response", accepted ? 1 : 0);

        UnityWebRequest www = UnityWebRequest.Post(Files.JsonURL + "/trade/respond", form);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
            ShowError(www.error);
        }
        else
        {
            if (accepted)
            {
                PlayerManager.Instance.Inventory.RemoveFlippo(item.ProposedFlippo.id);
                PlayerManager.Instance.Inventory.AddFlippo(item.RequestedFlippo.id);
            }
            ActivateRequestedTradeMenu();
        }
    }


    public void GetAcceptedTrades(TradeGridHandler handler)
    {
        StartCoroutine(AcceptedTradesEnumerator(handler));
    }

    IEnumerator AcceptedTradesEnumerator(TradeGridHandler handler)
    {
#if UNITY_EDITOR
        Debug.Log("Accepted trades");
#endif
        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/accepted?id=" + PlayerManager.Instance.Account.Id);

        Debug.Log(Files.JsonURL + "/trade/accepted?id=" + PlayerManager.Instance.Account.Id);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
            ShowError(www.error);
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
                int tradeID = item["tradeItem"]["id"].Value<int>();
                int proposerAccount = item["tradeItem"]["proposer"]["id"].Value<int>();
                int proposedFlippo = item["tradeItem"]["proposerFlippo"]["id"].Value<int>();
                int requestedFlippo = item["tradeItem"]["receiverFlippo"]["id"].Value<int>();
                int requesterAccount = item["receiver"]["id"].Value<int>();

                Trade proposer = new Trade(new Account(proposerAccount), GameManager.Instance.GetFlippoByID(proposedFlippo));
                Trade requester = new Trade(new Account(requesterAccount), GameManager.Instance.GetFlippoByID(requestedFlippo));
                trades.Add(new TradeItem(tradeID, requester, proposer));

                PlayerManager.Instance.Inventory.AddFlippo(requestedFlippo);
            }
            handler.GenerateGridButtons(trades, false);
        }
    }

    public void ShowError(string error)
    {
        if (messageBox == null) messageBox = Instantiate(Resources.Load("Messages/MsgBox", typeof(GameObject))) as GameObject;
        
        if(messageBox != null)
        {
            MessageBox msg = messageBox.GetComponent<MessageBox>();
            msg.SetText("Error:" + Environment.NewLine + error + Environment.NewLine + Environment.NewLine + "Make sure you are connected to the internet if you want to trade!");
            messageBox.SetActive(true);
        }
    }

    #region old
    IEnumerator GetTrades(int accountID)
    {
        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/item?id=" + accountID);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log(www.downloadHandler.text);
#endif
        }
    }


    IEnumerator CreateAccount()
    {
#if UNITY_EDITOR
        Debug.Log("Create account");
#endif
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Post(Files.JsonURL + "/account/create", form);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
        }
        else
        {
            string json = www.downloadHandler.text;
#if UNITY_EDITOR
            Debug.Log(json);
#endif
            var jo = JObject.Parse(json);
            int id = jo["id"].Value<int>();
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
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log(www.downloadHandler.text);
#endif
            StartCoroutine(GetProposedTrades(proposer, receiver));
        }
    }

    IEnumerator GetProposedTrades(int proposer, int receiver)
    {
#if UNITY_EDITOR
        Debug.Log("Proposed trade");
#endif
        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/out?id=" + proposer);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            StartCoroutine(GetTradeRequests(proposer, receiver));
        }
    }

    IEnumerator GetTradeRequests(int proposer, int receiver)
    {
#if UNITY_EDITOR
        Debug.Log("Trade requests");
#endif
        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/in?id=" + receiver);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
        }
        else
        {
            string json = www.downloadHandler.text;
#if UNITY_EDITOR
            Debug.Log(json);
#endif
            var jo = JArray.Parse(json);
            if (jo.Count > 0)
            {
                int id = jo[0]["id"].Value<int>();
                StartCoroutine(RespondToTrade(id, proposer, receiver));
            }
        }
    }


    IEnumerator RespondToTrade(int tradeID, int proposer, int receiver)
    {
#if UNITY_EDITOR
        Debug.Log("Respond to trade");
#endif
        WWWForm form = new WWWForm();
        form.AddField("tradeId", tradeID);
        form.AddField("response", 1);

        UnityWebRequest www = UnityWebRequest.Post(Files.JsonURL + "/trade/respond", form);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log(www.downloadHandler.text);
#endif
            StartCoroutine(AcceptTrade(tradeID, proposer, receiver));
        }
    }

    IEnumerator AcceptTrade(int tradeID, int proposer, int receiver)
    {
#if UNITY_EDITOR
        Debug.Log("Accept trade - proposer: " + proposer + " - receiver: " + receiver);
#endif
        UnityWebRequest www = UnityWebRequest.Get(Files.JsonURL + "/trade/accepted?id=" + proposer);

        yield return www.Send();

        if (www.isNetworkError)
        {
#if UNITY_EDITOR
            Debug.Log(www.error);
#endif
        }
        else
        {
            string json = www.downloadHandler.text;
#if UNITY_EDITOR
            Debug.Log(json);
#endif
        }
    }
    #endregion
}
