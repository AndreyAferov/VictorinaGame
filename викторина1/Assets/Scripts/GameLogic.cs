using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public QuestionList[] questions;
    public TextMeshProUGUI qNumberTMP;
    public TextMeshProUGUI qTextTMP;  
    public TextMeshProUGUI[] answersText;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button restartButton;
    public GameObject HeadAnim;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    List<QuestionList> qList;
    QuestionList crntQ;
    int correctAnswers;

    private int currentQuestionIndex = 0;
    private int totalQuestions;

    void Start()
    {
        qList = new List<QuestionList>(questions);
        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);
        correctAnswers = 0;
        HideAnswerButtons();
        totalQuestions = questions.Length;
    }

    public void OnClickPlay()
    {
        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        if (!HeadAnim.GetComponent<Animator>().enabled)
            HeadAnim.GetComponent<Animator>().enabled = true;
        else
            HeadAnim.GetComponent<Animator>().SetTrigger("InTrigger");

        ShowAnswerButtons();
        questionGenerate();
        currentQuestionIndex = 0;
    }

    void questionGenerate()
    {
        if (qList.Count > 0)
        {
            int RandQ = Random.Range(0, qList.Count);
            crntQ = qList[RandQ];

            qNumberTMP.text = (currentQuestionIndex + 1) + "/" + totalQuestions;
            qTextTMP.text = crntQ.Question;

            List<string> answers = new List<string>(crntQ.answer);

            for (int i = 0; i < crntQ.answer.Length; i++)
            {
                int Rand = Random.Range(0, answers.Count);
                answersText[i].text = answers[Rand];
                answers.RemoveAt(Rand);
                answersText[i].gameObject.SetActive(true);
            }
        }
        else
        {
            ShowResults();
        }
    }

    void ShowResults()
    {
        qNumberTMP.gameObject.SetActive(false); 
        qTextTMP.gameObject.SetActive(false);   

        foreach (TextMeshProUGUI answerText in answersText)
        {
            answerText.gameObject.SetActive(false);
        }

        resultPanel.SetActive(true);
        resultText.text = "Результат: " + correctAnswers + " из " + totalQuestions + " вопросов";

        restartButton.gameObject.SetActive(true);

        HideAnswerButtons();
    }

    public void OnClickRestart()
    {
        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        qList = new List<QuestionList>(questions);
        qNumberTMP.gameObject.SetActive(true); 
        qTextTMP.gameObject.SetActive(true);

        currentQuestionIndex = 0;

        correctAnswers = 0;
        ShowAnswerButtons();
        questionGenerate();
    }

    public void AnswerBttns(int index)
    {
        if (answersText[index].text == crntQ.answer[0])
        {
            print("Правильный ответ");
            correctAnswers++;
        }
        else
        {
            print("Неправильный ответ");
        }

        qList.Remove(crntQ);
        currentQuestionIndex++;
        questionGenerate();
    }

    void ShowAnswerButtons()
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        button4.gameObject.SetActive(true);
    }

    void HideAnswerButtons()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class QuestionList
{
    public string Question;
    public string[] answer = new string[3];
}
