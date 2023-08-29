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
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button restartButton;
    public GameObject HeadAnim;

    List<QuestionList> qList;
    QuestionList crntQ;
    int RandQ;

    void Start()
    {
        qList = new List<QuestionList>(questions);
        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    public void OnClickPlay()
    {
        questionGenerate();
        if (!HeadAnim.GetComponent<Animator>().enabled) HeadAnim.GetComponent<Animator>().enabled = true;
        else HeadAnim.GetComponent<Animator>().SetTrigger("InTrigger");
    }

    void questionGenerate()
    {
        if (qList.Count > 0)
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
                answersText[i].gameObject.SetActive(true); // Показываем кнопки ответов
            }
        }
        else
        {
            ShowResults();
        }
    }

    void ShowResults()
    {
        qTextTMP.gameObject.SetActive(false); // Скрываем текст вопроса
        foreach (TextMeshProUGUI answerText in answersText)
        {
            answerText.gameObject.SetActive(false); // Скрываем текст кнопок ответов
        }

        resultPanel.SetActive(true);
        int correctAnswers = questions.Length - qList.Count;
        int totalQuestions = questions.Length;
        resultText.text = "Результат: " + correctAnswers + " из " + totalQuestions;

        restartButton.gameObject.SetActive(true);
    }

    public void OnClickRestart()
    {
        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        qList = new List<QuestionList>(questions);
        qTextTMP.gameObject.SetActive(true);
        questionGenerate();
    }

    public void AnswerBttns(int index)
    {
        if (answersText[index].text == crntQ.answer[0])
        {
            print("Правильный ответ");
        }
        else
        {
            print("Неправильный ответ");
        }
        qList.RemoveAt(RandQ);
        foreach (TextMeshProUGUI answerText in answersText)
        {
            answerText.gameObject.SetActive(false); // Скрываем кнопки ответов
        }
        questionGenerate();
    }
}

[System.Serializable]
public class QuestionList
{
    public string Question;
    public string[] answer = new string[3];
}
