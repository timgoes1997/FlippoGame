using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : MonoBehaviour
{

    //properties
    public static PlayerManager Instance
    {
        get
        {
            return instance ?? ((FindObjectsOfType<PlayerManager>().Length > 0)
                    ? FindObjectOfType<PlayerManager>() : (Instantiate(Resources.Load("Manager/PlayerManager", typeof(GameObject))) as GameObject).GetComponent<PlayerManager>());
        }
    }
    public Inventory Inventory { get { return inventory; } }
    public Account Account { get { return acc; } }


    //fields
    private static PlayerManager instance = null;
    [SerializeField]
    private Account acc;
    [SerializeField]
    private Inventory inventory;
    private static SaveSystem ss = new SaveSystem();

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (ss.Load(Files.FlippoData))
        {
            Inventory inv = ss.GetObject<Inventory>();
            if(inv != null)
            {
                inventory = inv;
            }
        }
        if (ss.Load(Files.Account))
        {
            Account account = ss.GetObject<Account>();
            if(account != null)
            {
                acc = account;
            }
        }
        else
        {
            StartCoroutine(CreateAccount());
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// For IOS builds specify that the application exits instead of suspend.
    /// </summary>
    void OnApplicationQuit()
    {
        SaveInventory();
        SaveAccount();
    }

    public void SaveAccount()
    {
        ss.Clear();
        ss.Add(acc);
        ss.Save(Files.Account);
    }

    /// <summary>
    /// Saves the players inventory to a binary file.
    /// Windows path : ~\AppData\LocalLow\Unity\...\FlippoData.dat
    /// </summary>
    public void SaveInventory()
    {
        ss.Clear();
        ss.Add(inventory);
        ss.Save(Files.FlippoData);
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
            acc.Id = id;
        }
    }    
}
