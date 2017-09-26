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

    public List<Flippo> Flippos { get { return flippos; } }

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

    public static void LoadSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}