using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour {

    QuizQuestion[] quizQuestions;
    int randomIndex;

    // Use this for initialization
    void Start () {
        MakeQuiz();
    }

    public void GetQuestion()
    {
        if (!CheckIfAllCompleted())
        {
            // No questions left
        }
        else
        {
            do
            {
                randomIndex = Random.Range(0, 19);
            } while (!quizQuestions[randomIndex].completed);

            //TODO: do somthing with the question
        }
        
       
    } 

    public void MakeAnswer(string answerText)
    {
        if(answerText == quizQuestions[randomIndex].answerText)
        {
            quizQuestions[randomIndex].completed = true;
            //TODO: is right
        }
        else
        {
            //TODO: is wrong
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
    void MakeQuiz()
    {
        quizQuestions = new QuizQuestion[20];
        quizQuestions[0].questionText = "Waar zijn deze poppetjes van?";
        quizQuestions[0].answerAText = "Pokémon";
        quizQuestions[0].answerBText = "MSN";
        quizQuestions[0].answerCText = "Hyves";
        quizQuestions[0].answerText = "MSN";
        quizQuestions[0].imageName = "imgMSN";
        quizQuestions[1].questionText = "Wie was de rappende neef van Ali B?";
        quizQuestions[1].answerAText = "Brainpower";
        quizQuestions[1].answerBText = "2pac";
        quizQuestions[1].answerCText = "Yes-R";
        quizQuestions[1].answerText = "Yes-R";
        quizQuestions[1].imageName = "";
        quizQuestions[2].questionText = "Wie is deze pokemon?";
        quizQuestions[2].answerAText = "Bulbasaur";
        quizQuestions[2].answerBText = "Pikachu";
        quizQuestions[2].answerCText = "Diglett";
        quizQuestions[2].answerText = "Pikachu";
        quizQuestions[2].imageName = "imgPikachu";
        quizQuestions[3].questionText = "Aan welke rapper denk je bij dit plaatje?";
        quizQuestions[3].answerAText = "50 cent";
        quizQuestions[3].answerBText = "Eminem";
        quizQuestions[3].answerCText = "Lil Kleine";
        quizQuestions[3].answerText = "Eminem";
        quizQuestions[3].imageName = "imgMM";
        quizQuestions[4].questionText = "Wat betekenen de 2 R’s van ‘R+R = Back’?";
        quizQuestions[4].answerAText = "Rummikub en Ruzzle";
        quizQuestions[4].answerBText = "Respect en Reactie";
        quizQuestions[4].answerCText = "Rood en Rond";
        quizQuestions[4].answerText = "Respect en Reactie";
        quizQuestions[4].imageName = "";
        quizQuestions[5].questionText = "Waar is deze dansende banaan van?";
        quizQuestions[5].answerAText = "The Sims";
        quizQuestions[5].answerBText = "Hyves";
        quizQuestions[5].answerCText = "Chatroulette";
        quizQuestions[5].answerText = "Hyves";
        quizQuestions[5].imageName = "imgBanaan";
        quizQuestions[6].questionText = "Hoe heette het virtueel figuur op MSN waarmee je kon chatten?";
        quizQuestions[6].answerAText = "Chatman";
        quizQuestions[6].answerBText = "Chatwoman";
        quizQuestions[6].answerCText = "Chatchild";
        quizQuestions[6].answerText = "Chatman";
        quizQuestions[6].imageName = "";
        quizQuestions[7].questionText = " Wat is de naam van dit product?";
        quizQuestions[7].answerAText = "Diddl";
        quizQuestions[7].answerBText = "Lidl";
        quizQuestions[7].answerCText = "Twizzl";
        quizQuestions[7].answerText = "Diddl";
        quizQuestions[7].imageName = "imgDiddl";
        quizQuestions[8].questionText = "Van wie is deze caravan?";
        quizQuestions[8].answerAText = "Rocket power";
        quizQuestions[8].answerBText = "Bassie en Adriaan";
        quizQuestions[8].answerCText = "Suske en Wiske";
        quizQuestions[8].answerText = "Bassie en Adriaan";
        quizQuestions[8].imageName = "imgCaravan";
        quizQuestions[9].questionText = "Wie is deze pokemon?";
        quizQuestions[9].answerAText = "Mewtwo";
        quizQuestions[9].answerBText = "Snorlax";
        quizQuestions[9].answerCText = "Onix";
        quizQuestions[9].answerText = "Snorlax";
        quizQuestions[9].imageName = "imgSnorlax";
        quizQuestions[10].questionText = "Wie won de eerste Idols?";
        quizQuestions[10].answerAText = "Jim";
        quizQuestions[10].answerBText = "Jamai";
        quizQuestions[10].answerCText = "Boris";
        quizQuestions[10].answerText = "Jamai";
        quizQuestions[10].imageName = "";
        quizQuestions[11].questionText = "Welke karakter hoort niet bij die van de Looney Tunes?";
        quizQuestions[11].answerAText = "Road Runner";
        quizQuestions[11].answerBText = "Pepe le pew";
        quizQuestions[11].answerCText = "Zazoe";
        quizQuestions[11].answerText = "Zazoe";
        quizQuestions[11].imageName = "";
        quizQuestions[12].questionText = "Vul het ontbrekende woord in: Whats with …";
        quizQuestions[12].answerAText = "Carolina";
        quizQuestions[12].answerBText = "Andy";
        quizQuestions[12].answerCText = "Frank";
        quizQuestions[12].answerText = "Andy";
        quizQuestions[12].imageName = "";
        quizQuestions[13].questionText = "Hoe heet de roodharige Totally Spie?";
        quizQuestions[13].answerAText = "Sam";
        quizQuestions[13].answerBText = "Alex";
        quizQuestions[13].answerCText = "Clover";
        quizQuestions[13].answerText = "Sam";
        quizQuestions[13].imageName = "";
        quizQuestions[14].questionText = "Hoe wordt dit meisje ook wel genoemd?";
        quizQuestions[14].answerAText = "Blondie";
        quizQuestions[14].answerBText = "Beugelbekkie";
        quizQuestions[14].answerCText = "Angsthaas";
        quizQuestions[14].answerText = "Beugelbekkie";
        quizQuestions[14].imageName = "imgBeugelBekkie";
        quizQuestions[15].questionText = "Hoe werd hij genoemd bij rocket power?";
        quizQuestions[15].answerAText = "Newbie";
        quizQuestions[15].answerBText = "The New Guy";
        quizQuestions[15].answerCText = "The Squit";
        quizQuestions[15].answerText = "The Squit";
        quizQuestions[15].imageName = "imgSquit";
        quizQuestions[16].questionText = "Op welke bal leek het hoofd van ‘Hey Arnold’?";
        quizQuestions[16].answerAText = "Basketbal";
        quizQuestions[16].answerBText = "Rugbybal";
        quizQuestions[16].answerCText = "Tennisbal";
        quizQuestions[16].answerText = "Rugbybal";
        quizQuestions[16].imageName = "";
        quizQuestions[17].questionText = "Op welk vervoersmiddel staan de boys van O’zone in het nummer ‘Dragoste din tei’?";
        quizQuestions[17].answerAText = "Trein";
        quizQuestions[17].answerBText = "Vliegtuig";
        quizQuestions[17].answerCText = "Speedboot";
        quizQuestions[17].answerText = "Vliegtuig";
        quizQuestions[17].imageName = "";
        quizQuestions[18].questionText = "Hoe heette de munteenheid die gebruikt werd in Nederland voor de Euro?";
        quizQuestions[18].answerAText = "Franken";
        quizQuestions[18].answerBText = "Kronen";
        quizQuestions[18].answerCText = "Gulden";
        quizQuestions[18].answerText = "Gulden";
        quizQuestions[18].imageName = "";
        quizQuestions[19].questionText = "Wie zie je op de foto?";
        quizQuestions[19].answerAText = "Cheetos";
        quizQuestions[19].answerBText = "Chester";
        quizQuestions[19].answerCText = "Cherder";
        quizQuestions[19].answerText = "Chester";
        quizQuestions[19].imageName = "imgChester";
    }
}
