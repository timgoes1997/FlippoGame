using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //properties
    public static GameManager Instance
    {
        get
        {
            return instance ?? ((FindObjectsOfType<GameManager>().Length > 0)
                        ? FindObjectOfType<GameManager>() : (Instantiate(Resources.Load("Manager/GameManager", typeof(GameObject))) as GameObject).GetComponent<GameManager>());
        }
    }

    //fields
    private static GameManager instance = null;

    [SerializeField]
    private List<Flippo> flippos;
    public int AmountOfFlippos { get { return flippos.Count; } }

    public int CommonChance = 75;
    public int UnCommonChance = 15;
    public int RareChance = 7;
    public int legendaryChance = 3;

    public List<Flippo> Flippos { get { return flippos; } }

    public bool canSwipe = true;
    public Flippo flippoCache;
    public bool gotTradeMessage = false;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Flippo GetFlippoByID(int id)
    {
        foreach (Flippo f in flippos)
        {
            if (f.id == id)
            {

                return f;
            }
        }
        return null;
    }

    public List<Flippo> GetFlippoByPlayerFlippo(List<PlayerFlippo> playerFlippos, Collection filter)
    {
        List<Flippo> pFlippos = new List<Flippo>();

        foreach (Flippo f in flippos)
        {
            foreach (PlayerFlippo pf in playerFlippos)
            {
                if (f.id == pf.flippoID) // && (filter == Collection.None || f.collection == filter)
                {
                    pFlippos.Add(f);
                    if (flippos.Count == pFlippos.Count)
                    {
                        return pFlippos;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return pFlippos;
    }

    public Flippo GetRandomFlippo()
    {
        int total = CommonChance + UnCommonChance + RareChance + legendaryChance;
        int randomChance = Random.Range(0, total);
        if (randomChance <= CommonChance) return GetRandomFlippoByRarity(Rarity.Common);
        if (randomChance <= CommonChance + UnCommonChance) return GetRandomFlippoByRarity(Rarity.UnCommon);
        if (randomChance <= CommonChance + UnCommonChance + RareChance) return GetRandomFlippoByRarity(Rarity.Rare);
        return GetRandomFlippoByRarity(Rarity.Legendary);
    }

    public Flippo GetRandomFlippoByRarity(Rarity rarity)
    {
#if UNITY_EDITOR
        Debug.Log("Received flippo with rarity:" + rarity.ToString());
#endif
        List<Flippo> rarityFlippos = GetFlipposByRarity(rarity);
        return rarityFlippos[Random.Range(0, rarityFlippos.Count)];
    }

    public List<Flippo> GetFlipposByRarity(Rarity rarity)
    {
        List<Flippo> rarityFlippos = new List<Flippo>();
        foreach (Flippo f in flippos)
        {
            if (f.rarity == rarity)
            {
                rarityFlippos.Add(f);
            }
        }
        return rarityFlippos;
    }

    public static void LoadSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}