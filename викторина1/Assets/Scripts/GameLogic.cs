using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public QuestionList[] questions;
    public TextMeshProUGUI qTextTMP; // Компонент TextMeshProUGUI для вывода вопросов

    public void OnClickPlay()
    {
        int randomIndex = Random.Range(0, questions.Length);
        qTextTMP.text = questions[randomIndex].Question;
    }
}

[System.Serializable]
public class QuestionList
{
    public string Question;
    public string[] answer = new string[3];
}
