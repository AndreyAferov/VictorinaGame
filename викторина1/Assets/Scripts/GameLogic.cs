using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public QuestionList[] questions;
    public TextMeshProUGUI qTextTMP;
    public TextMeshProUGUI[] answersText;
    public GameObject HeadAnim;

    List<QuestionList> qList;
    QuestionList crntQ;
    int RandQ;

    void Start()
    {
        qList = new List<QuestionList>(questions);
    }

    public void OnClickPlay()
    {
        questionGenerate();
        if (!HeadAnim.GetComponent<Animator>().enabled) HeadAnim.GetComponent<Animator>().enabled = true;
        else HeadAnim.GetComponent<Animator>().SetTrigger("InTrigger");

    }

    void questionGenerate()
    {
        if (qList.Count > 0) // Исправлено
        {
            RandQ = Random.Range(0, qList.Count);
            crntQ = qList[RandQ];
            qTextTMP.text = crntQ.Question;

            List<string> answers = new List<string>(crntQ.answer);

            for (int i = 0; i < crntQ.answer.Length; i++)
            {
                int Rand = Random.Range(0, answers.Count);
                answersText[i].text = answers[Rand];
                answers.RemoveAt(Rand);
            }
        }
        else
        {
            print("Вы прошли игру"); // Добавлено ;
        }
    }

    public void AnswerBttns(int index)
    {
        if (answersText[index].text.ToString() == crntQ.answer[0])
        {
            print("правильный ответ");
        }
        else
        {
            print("неправильный ответ");
        }
        qList.RemoveAt(RandQ);
        questionGenerate();
    }
}

[System.Serializable]
public class QuestionList
{
    public string Question;
    public string[] answer = new string[3];
}
