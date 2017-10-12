using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{

    QuizQuestion[] quizQuestions;
    int randomIndex;

    Canvas canvas;

    public Image questionImage;
    public Text questionText;
    public Text AnswerTextA;
    public Text AnswerTextB;
    public Text AnswerTextC;

    public GameObject panelOk;
    public GameObject panelBad;
    public GameObject lockstreen;

    private int previousLength = 0;
    private bool initial = true;
    public GameObject dubbelPanel;
    public CollectionFlippo inspectFlippo;
    public bool rightAnswer;

    // Use this for initialization
    void Start()
    {
        previousLength = PlayerManager.Instance.Inventory.flippos.Count;
        canvas = FindObjectOfType<Canvas>();
        MakeQuiz();
        GetQuestion();
    }

    public void GetQuestion()
    {
        int newLength = PlayerManager.Instance.Inventory.flippos.Count;
        if (previousLength == newLength && rightAnswer && !initial)
        {
            dubbelPanel.SetActive(true);
            PlayerFlippo last = PlayerManager.Instance.Inventory.lastAddedFlippo;
            if (last != null && !GameManager.Instance.gotTradeMessage) {
                Flippo flippo = GameManager.Instance.GetFlippoByID(last.flippoID);
                GameManager.Instance.flippoCache = flippo;
                inspectFlippo.SetFlippoItem(flippo);
            }
        }
        else
        {
            previousLength = newLength;
            if (initial) initial = false;
        }


        if (CheckIfAllCompleted())
        {
            // No questions left
            for (int i = 0; i < quizQuestions.Length; i++) //
            {
                quizQuestions[i].completed = false;
            }
            Debug.Log("all questions complete");
        }
        //else
        //{
        do
        {
            randomIndex = Random.Range(0, quizQuestions.Length); //
        } while (quizQuestions[randomIndex].completed);

        //TODO: do somthing with the question
        LoadQuestion(randomIndex);
        //}

        lockstreen.SetActive(false);
    }

    void LoadQuestion(int index)
    {
        questionImage.sprite = Resources.Load<Sprite>("Vragen/" + quizQuestions[index].imageName);
        questionText.text = quizQuestions[index].questionText;
        AnswerTextA.text = quizQuestions[index].answerAText;
        AnswerTextB.text = quizQuestions[index].answerBText;
        AnswerTextC.text = quizQuestions[index].answerCText;
    }

    public void MakeAnswer(string answer)
    {
        Answer multiAnswer = Answer.A;
        switch (answer)
        {
            case "B":
                multiAnswer = Answer.B;
                break;
            case "C":
                multiAnswer = Answer.C;
                break;
        }


        if (multiAnswer == quizQuestions[randomIndex].answer)
        {
            quizQuestions[randomIndex].completed = true;
            panelOk.SetActive(true);
            rightAnswer = true;
        }
        else
        {
            panelBad.SetActive(true);
            quizQuestions[randomIndex].completed = true;
            rightAnswer = false;
        }
    }

    bool CheckIfAllCompleted()
    {
        foreach (QuizQuestion question in quizQuestions)
        {
            if (!question.completed)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator GetExtraFlippo(float sec)
    {
        yield return new WaitForSeconds(sec);
        Flippo f2 = GameManager.Instance.GetRandomFlippo();
        PlayerManager.Instance.Inventory.AddFlippo(f2.id);
        GameObject flippo2 = Instantiate(Resources.Load("newFlippo"), canvas.transform, false) as GameObject;
        flippo2.GetComponent<Image>().sprite = f2.sprite;
        flippo2.GetComponent<NewFlippo>().StartAnimation(true);

    }

    public void GetRandomFlippos()
    {
        int randomNr = Random.Range(0, 4);
        Flippo f1 = GameManager.Instance.GetRandomFlippo();
        PlayerManager.Instance.Inventory.AddFlippo(f1.id);
        GameObject flippo1 = Instantiate(Resources.Load("newFlippo"), canvas.transform, false) as GameObject;
        flippo1.GetComponent<Image>().sprite = f1.sprite;
        if (randomNr == 3)
        {
            flippo1.GetComponent<NewFlippo>().StartAnimation(false);
        }
        else
        {
            flippo1.GetComponent<NewFlippo>().StartAnimation(true);
        }


        if (randomNr == 3)
        {
            StartCoroutine(GetExtraFlippo(1.5f));
        }
    }
    void MakeQuiz()
    {
        quizQuestions = new QuizQuestion[27]; //
        for (int i = 0; i < quizQuestions.Length; i++) //
        {
            quizQuestions[i] = new QuizQuestion();
        }
        quizQuestions[0].questionText = "Waar zijn deze poppetjes van?";
        quizQuestions[0].answerAText = "Pokémon";
        quizQuestions[0].answerBText = "MSN";
        quizQuestions[0].answerCText = "Hyves";
        quizQuestions[0].answer = Answer.B;
        quizQuestions[0].imageName = "imgMSN";
        quizQuestions[1].questionText = "Wie was de rappende neef van Ali B?";
        quizQuestions[1].answerAText = "Brainpower";
        quizQuestions[1].answerBText = "2pac";
        quizQuestions[1].answerCText = "Yes-R";
        quizQuestions[1].answer = Answer.C;
        quizQuestions[1].imageName = "imgAliB";
        quizQuestions[2].questionText = "Wie is deze pokemon?";
        quizQuestions[2].answerAText = "Bulbasaur";
        quizQuestions[2].answerBText = "Pikachu";
        quizQuestions[2].answerCText = "Diglett";
        quizQuestions[2].answer = Answer.B;
        quizQuestions[2].imageName = "imgPikachu";
        quizQuestions[3].questionText = "Welke rapper past bij dit plaatje?";
        quizQuestions[3].answerAText = "50 cent";
        quizQuestions[3].answerBText = "Eminem";
        quizQuestions[3].answerCText = "Lil Kleine";
        quizQuestions[3].answer = Answer.B;
        quizQuestions[3].imageName = "imgMM";
        quizQuestions[4].questionText = "Wat betekenen de 2 R’s van ‘R+R = Back’?";
        quizQuestions[4].answerAText = "Rummikub en Ruzzle";
        quizQuestions[4].answerBText = "Respect en Reactie";
        quizQuestions[4].answerCText = "Rood en Rond";
        quizQuestions[4].answer = Answer.B;
        quizQuestions[4].imageName = "imgHyves";
        quizQuestions[5].questionText = "Waar is deze dansende banaan van?";
        quizQuestions[5].answerAText = "The Sims";
        quizQuestions[5].answerBText = "Hyves";
        quizQuestions[5].answerCText = "Chatroulette";
        quizQuestions[5].answer = Answer.B;
        quizQuestions[5].imageName = "imgBanaan";
        quizQuestions[6].questionText = "Hoe heette het virtueel figuur op MSN waarmee je kon chatten?";
        quizQuestions[6].answerAText = "Chatman";
        quizQuestions[6].answerBText = "Chatwoman";
        quizQuestions[6].answerCText = "Chatchild";
        quizQuestions[6].answer = Answer.A;
        quizQuestions[6].imageName = "imgChatman";
        quizQuestions[7].questionText = " Wat is de naam van dit product?";
        quizQuestions[7].answerAText = "Diddl";
        quizQuestions[7].answerBText = "Lidl";
        quizQuestions[7].answerCText = "Twizzl";
        quizQuestions[7].answer = Answer.A;
        quizQuestions[7].imageName = "imgDiddl";
        quizQuestions[8].questionText = "Van wie is deze caravan?";
        quizQuestions[8].answerAText = "Rocket power";
        quizQuestions[8].answerBText = "Bassie en Adriaan";
        quizQuestions[8].answerCText = "Suske en Wiske";
        quizQuestions[8].answer = Answer.B;
        quizQuestions[8].imageName = "imgCaravan";
        quizQuestions[9].questionText = "Wie is deze pokemon?";
        quizQuestions[9].answerAText = "Mewtwo";
        quizQuestions[9].answerBText = "Snorlax";
        quizQuestions[9].answerCText = "Onix";
        quizQuestions[9].answer = Answer.B;
        quizQuestions[9].imageName = "imgSnorlax";
        quizQuestions[10].questionText = "Wie won de eerste Idols?";
        quizQuestions[10].answerAText = "Jim";
        quizQuestions[10].answerBText = "Jamai";
        quizQuestions[10].answerCText = "Boris";
        quizQuestions[10].answer = Answer.B;
        quizQuestions[10].imageName = "imgIdols";
        quizQuestions[11].questionText = "Welke karakter hoort niet bij die van de Looney Tunes?";
        quizQuestions[11].answerAText = "Road Runner";
        quizQuestions[11].answerBText = "Pepe le pew";
        quizQuestions[11].answerCText = "Zazoe";
        quizQuestions[11].answer = Answer.C;
        quizQuestions[11].imageName = "imgLooneyTunes";
        quizQuestions[12].questionText = "Vul het ontbrekende woord in: Whats with …";
        quizQuestions[12].answerAText = "Randy";
        quizQuestions[12].answerBText = "Andy";
        quizQuestions[12].answerCText = "Ferdy";
        quizQuestions[12].answer = Answer.B;
        quizQuestions[12].imageName = "imgAndy";
        quizQuestions[13].questionText = "Hoe heet de roodharige Totally Spice?";
        quizQuestions[13].answerAText = "Sam";
        quizQuestions[13].answerBText = "Alex";
        quizQuestions[13].answerCText = "Clover";
        quizQuestions[13].answer = Answer.A;
        quizQuestions[13].imageName = "imgTotallySpice";
        quizQuestions[14].questionText = "Hoe wordt dit meisje ook wel genoemd?";
        quizQuestions[14].answerAText = "Blondie";
        quizQuestions[14].answerBText = "Beugelbekkie";
        quizQuestions[14].answerCText = "Angsthaas";
        quizQuestions[14].answer = Answer.B;
        quizQuestions[14].imageName = "imgBeugelBekkie";
        quizQuestions[15].questionText = "Hoe werd hij genoemd bij rocket power?";
        quizQuestions[15].answerAText = "Newbie";
        quizQuestions[15].answerBText = "The New Guy";
        quizQuestions[15].answerCText = "Squid";
        quizQuestions[15].answer = Answer.C;
        quizQuestions[15].imageName = "imgSquid";
        quizQuestions[16].questionText = "Op welke bal leek het hoofd van ‘Hey Arnold’?";
        quizQuestions[16].answerAText = "Basketbal";
        quizQuestions[16].answerBText = "Rugbybal";
        quizQuestions[16].answerCText = "Tennisbal";
        quizQuestions[16].answer = Answer.B;
        quizQuestions[16].imageName = "imgArnold";
        quizQuestions[17].questionText = "Op welk vervoersmiddel staan de boys van O’zone in het nummer ‘Dragoste din tei’?";
        quizQuestions[17].answerAText = "Trein";
        quizQuestions[17].answerBText = "Vliegtuig";
        quizQuestions[17].answerCText = "Speedboot";
        quizQuestions[17].answer = Answer.B;
        quizQuestions[17].imageName = "imgOzone";
        quizQuestions[18].questionText = "Hoe heette de munteenheid die gebruikt werd in Nederland voor de Euro?";
        quizQuestions[18].answerAText = "Franken";
        quizQuestions[18].answerBText = "Kronen";
        quizQuestions[18].answerCText = "Gulden";
        quizQuestions[18].answer = Answer.C;
        quizQuestions[18].imageName = "imgGeld";
        quizQuestions[19].questionText = "Welk nummer is niet van de Backstreet Boys?";
        quizQuestions[19].answerAText = "Nothing ever happens";
        quizQuestions[19].answerBText = "I want it that way";
        quizQuestions[19].answerCText = "Anywhere for you";
        quizQuestions[19].answer = Answer.A;
        quizQuestions[19].imageName = "imgBackstreet";
        quizQuestions[20].questionText = "Wie zie je op de foto?";
        quizQuestions[20].answerAText = "Chester";
        quizQuestions[20].answerBText = "Cheetos";
        quizQuestions[20].answerCText = "Cherder";
        quizQuestions[20].answer = Answer.A;
        quizQuestions[20].imageName = "imgChester";
        quizQuestions[21].questionText = "Welk nummer is niet van Britney Spears?";
        quizQuestions[21].answerAText = "Stronger";
        quizQuestions[21].answerBText = "Toxic";
        quizQuestions[21].answerCText = "Because of you";
        quizQuestions[21].answer = Answer.C;
        quizQuestions[21].imageName = "imgBritney";
        quizQuestions[22].questionText = "In welk jaar is Windows XP uitgekomen?";
        quizQuestions[22].answerAText = "2001";
        quizQuestions[22].answerBText = "2002";
        quizQuestions[22].answerCText = "2003";
        quizQuestions[22].answer = Answer.A;
        quizQuestions[22].imageName = "imgWindowsXP";
        quizQuestions[23].questionText = "Hoe heette de eerste Harry Potter film?";
        quizQuestions[23].answerAText = "De geheime kamer";
        quizQuestions[23].answerBText = "De vuurbeker";
        quizQuestions[23].answerCText = "De steen der wijzen";
        quizQuestions[23].answer = Answer.C;
        quizQuestions[23].imageName = "imgHarry";
        quizQuestions[24].questionText = "Wanneer kwam de titanic film uit?";
        quizQuestions[24].answerAText = "1997";
        quizQuestions[24].answerBText = "1998";
        quizQuestions[24].answerCText = "2000";
        quizQuestions[24].answer = Answer.A;
        quizQuestions[24].imageName = "imgTitanic";
        quizQuestions[25].questionText = "Wat is de oorspronkelijke naam van dit wezen in Lord of the Rings?";
        quizQuestions[25].answerAText = "Gandalf";
        quizQuestions[25].answerBText = "Gollum";
        quizQuestions[25].answerCText = "Sméagol";
        quizQuestions[25].answer = Answer.C;
        quizQuestions[25].imageName = "imgSmeagol";
        quizQuestions[26].questionText = "Hoeveel seizoenen van Friends zijn er?";
        quizQuestions[26].answerAText = "12";
        quizQuestions[26].answerBText = "11";
        quizQuestions[26].answerCText = "10";
        quizQuestions[26].answer = Answer.C;
        quizQuestions[26].imageName = "imgFriends";
        //
        /*
        quizQuestions[25].questionText = "";
        quizQuestions[25].answerAText = "";
        quizQuestions[25].answerBText = "";
        quizQuestions[25].answerCText = "";
        quizQuestions[25].answer = Answer.A;
        quizQuestions[25].imageName = ""; //*/
    }
}

public enum Answer
{
    A,
    B,
    C
}