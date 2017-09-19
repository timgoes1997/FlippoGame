using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TradeManager : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetTrades(1));
    }
	
	IEnumerator GetTrades(int accountID)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://192.168.1.215:8080/trade/item?id=" + accountID);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
