using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public GameObject collectionPanel;

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MoveCollectionPanelUp()
    {
        collectionPanel.GetComponent<Animator>().SetBool("Up", true);
    }
    public void MoveCollectionPanelDown()
    {
        collectionPanel.GetComponent<Animator>().SetBool("Up", false);
    }
}
