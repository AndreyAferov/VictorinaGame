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

    List<QuestionList> qList;
    int RandQ;

    void Start()
    {
        qList = new List<QuestionList>(questions);
    }

    public void OnClickPlay()
    {
        questionGenerate();
    }

    void questionGenerate()
    {
        RandQ = Random.Range(0, qList.Count);
        QuestionList crntQ = qList[RandQ];
        qTextTMP.text = crntQ.Question;

        for (int i = 0; i < crntQ.answer.Length; i++)
        {
            answersText[i].text = crntQ.answer[i];
        }
    }

    public void AnswerBttns()
    {
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
