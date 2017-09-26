using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFlippo : MonoBehaviour {

    public bool getNewQuestion;
	// Use this for initialization
	void Start () {
        getNewQuestion = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void StartAnimation()
    {
        GetComponent<Animation>().Play();
    }

    public void DestroyFlippo()
    {
        if (getNewQuestion)
        {
            FindObjectOfType<QuizManager>().GetQuestion();
        }
        Destroy(this.gameObject);
    }
}
