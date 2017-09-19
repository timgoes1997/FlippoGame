using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TradeManager : MonoBehaviour
{
    [SerializeField]
    private string jsonURL = "http://192.168.1.215:8080";

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(GetTrades(1));
        StartCoroutine(CreateAccount());
    }

    IEnumerator GetTrades(int accountID)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://192.168.1.215:8080/trade/item?id=" + accountID);

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
        UnityWebRequest www = UnityWebRequest.Post(jsonURL + "/account/create", form);

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

        UnityWebRequest www = UnityWebRequest.Post(jsonURL + "/trade/create", form);

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
        UnityWebRequest www = UnityWebRequest.Get(jsonURL + "/trade/out?id=" + proposer);

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
        UnityWebRequest www = UnityWebRequest.Get(jsonURL + "/trade/in?id=" + receiver);

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
            int id = jo[0]["id"].Value<int>();
            Debug.Log("Trade: " + id);
            StartCoroutine(AcceptTrade(id, proposer, receiver));
        }
    }


    IEnumerator RespondToTrade(int tradeID, int proposer, int receiver)
    {
        Debug.Log("Respond to trade");
        WWWForm form = new WWWForm();
        form.AddField("tradeId", tradeID);
        form.AddField("response", 1);

        UnityWebRequest www = UnityWebRequest.Post(jsonURL + "/trade/respond", form);

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
        UnityWebRequest www = UnityWebRequest.Get(jsonURL + "/trade/accepted?id=" + proposer);

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
}
