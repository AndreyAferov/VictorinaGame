using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public QuestionList[] questions;
    public QuestionList[] questionsNormal;
    public QuestionList[] questionsHard;
    public QuestionList[] questionsUnpossible;
    public TextMeshProUGUI qNumberTMP;
    public TextMeshProUGUI qTextTMP;

    public TextMeshProUGUI[] answersTextHard;
    public TextMeshProUGUI[] answersTextUn;
    public TextMeshProUGUI[] answersTextNormal;
    public TextMeshProUGUI[] answersText;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button restartButton;
    public GameObject HeadAnim;
    public GameObject HeadAnim1;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public GameObject panel;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    List<QuestionList> qList;
    QuestionList crntQ;

    List<QuestionList> qListNormal;
    QuestionList crntQNormal;

    List<QuestionList> qListHard;
    QuestionList crntQHard;

    List<QuestionList> qListUnpossible;
    QuestionList crntQUnpossible;

    int correctAnswers;
    int correctAnswersNormal;
    int correctAnswersHard;
    int correctAnswersUnpossible;

    private int currentQuestionIndex = 0;
    private int currentQuestionIndexNormal = 0;
    private int currentQuestionIndexHard = 0;
    private int currentQuestionIndexUnpossible = 0;
    private int totalQuestions;
    private int totalQuestionsNormal;
    private int totalQuestionsHard;
    private int totalQuestionsUnpossible;

    void Start()
    {
        qList = new List<QuestionList>(questions);
        qListNormal = new List<QuestionList>(questionsNormal);
        qListHard = new List<QuestionList>(questionsHard);
        qListUnpossible = new List<QuestionList>(questionsUnpossible);

        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        correctAnswers = 0;
        correctAnswersNormal = 0;
        correctAnswersHard = 0;
        correctAnswersUnpossible = 0;

        HideAnswerButtons();

        totalQuestions = questions.Length;
        totalQuestionsNormal = questionsNormal.Length;
        totalQuestionsHard = questionsHard.Length;
        totalQuestionsUnpossible = questionsUnpossible.Length;
    }

    public void ResetLiteLevelQuestions()
    {
        qList = new List<QuestionList>(questions);
    }

    public void ResetNormalLevelQuestions()
    {
        qListNormal = new List<QuestionList>(questionsNormal);
    }

    public void ResetHardLevelQuestions()
    {
        qListHard = new List<QuestionList>(questionsHard);
    }

    public void ResetUnpossibleLevelQuestions()
    {
        qListUnpossible = new List<QuestionList>(questionsUnpossible);
    }

    public void OnClickPlay()
    {
        HideResult();

        if (!HeadAnim.GetComponent<Animator>().enabled)
            HeadAnim.GetComponent<Animator>().enabled = true;
        else
            HeadAnim.GetComponent<Animator>().SetTrigger("InTrigger");
    }

    public void OnClickNormal()
    {
        HidePanel2();
        HidePanel3();
        panel1.SetActive(true);
        ShowPanel();
        HideAnswerButtons();

        if (!HeadAnim1.GetComponent<Animator>().enabled)
            HeadAnim1.GetComponent<Animator>().enabled = true;
        else
            HeadAnim1.GetComponent<Animator>().SetTrigger("InTrigger1");

        currentQuestionIndexNormal = 0;
        correctAnswersNormal = 0;
        ResetNormalLevelQuestions();

        questionGenerateNormal();
    }

    void questionGenerateNormal()
    {

        if (qListNormal.Count > 0)
        {
            int RandQ1 = Random.Range(0, qListNormal.Count);
            crntQNormal = qListNormal[RandQ1];

            qNumberTMP.text = (currentQuestionIndexNormal + 1) + "/" + totalQuestionsNormal;
            qTextTMP.text = crntQNormal.Question;

            List<string> answers1 = new List<string>(crntQNormal.answer);

            for (int i = 0; i < crntQNormal.answer.Length; i++)
            {
                int Rand = Random.Range(0, answers1.Count);
                answersTextNormal[i].text = answers1[Rand];
                answers1.RemoveAt(Rand);
                answersTextNormal[i].gameObject.SetActive(true);
            }

            currentQuestionIndexNormal++;
        }
        else
        {
            ShowResultsNormal();
        }
    }

    public void OnClickLite()
    {
        HidePanel2();
        HidePanel3();
        panel1.SetActive(true);
        HidePanel();
        ShowAnswerButtons();

        if (!HeadAnim1.GetComponent<Animator>().enabled)
            HeadAnim1.GetComponent<Animator>().enabled = true;
        else
            HeadAnim1.GetComponent<Animator>().SetTrigger("InTrigger1");

        ResetLiteLevelQuestions(); // Восстанавливаем вопросы для уровня "легкий"
        questionGenerate();
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

            qList.RemoveAt(RandQ);
        }
        else
        {
            ShowResults();
        }
    }

    public void OnClickHard()
    {
        ShowPanel2();
        HidePanel3();
        panel1.SetActive(true);
        ShowPanel();
        HideAnswerButtons();

        if (!HeadAnim1.GetComponent<Animator>().enabled)
            HeadAnim1.GetComponent<Animator>().enabled = true;
        else
            HeadAnim1.GetComponent<Animator>().SetTrigger("InTrigger1");

        currentQuestionIndexHard = 0;
        correctAnswersHard = 0;
        ResetHardLevelQuestions();

        questionGenerateHard();
    }

    void questionGenerateHard()
    {
        if (qListHard.Count > 0)
        {
            int RandQ1 = Random.Range(0, qListHard.Count);
            crntQHard = qListHard[RandQ1];

            qNumberTMP.text = (currentQuestionIndexHard + 1) + "/" + totalQuestionsHard;
            qTextTMP.text = crntQHard.Question;

            List<string> answers1 = new List<string>(crntQHard.answer);

            for (int i = 0; i < crntQHard.answer.Length; i++)
            {
                int Rand = Random.Range(0, answers1.Count);
                answersTextHard[i].text = answers1[Rand];
                answers1.RemoveAt(Rand);
                answersTextHard[i].gameObject.SetActive(true);
            }

        }
        else
        {
            ShowResultsHard();
        }
    }

    public void OnClickUnpossible()
    {
        HidePanel();
        HidePanel2();
        panel1.SetActive(true);
        ShowPanel3();
        HideAnswerButtons();

        if (!HeadAnim1.GetComponent<Animator>().enabled)
            HeadAnim1.GetComponent<Animator>().enabled = true;
        else
            HeadAnim1.GetComponent<Animator>().SetTrigger("InTrigger1");

        currentQuestionIndexUnpossible = 0;
        correctAnswersUnpossible = 0;
        ResetUnpossibleLevelQuestions();

        questionGenerateUnpossible();
    }

    void questionGenerateUnpossible()
    {
        if (qListUnpossible.Count > 0)
        {
            int RandQ1 = Random.Range(0, qListUnpossible.Count);
            crntQUnpossible = qListUnpossible[RandQ1];

            qNumberTMP.text = (currentQuestionIndexUnpossible + 1) + "/" + totalQuestionsUnpossible;
            qTextTMP.text = crntQUnpossible.Question;

            List<string> answers1 = new List<string>(crntQUnpossible.answer);

            for (int i = 0; i < crntQUnpossible.answer.Length; i++)
            {
                int Rand = Random.Range(0, answers1.Count);
                answersTextUn[i].text = answers1[Rand];
                answers1.RemoveAt(Rand);
                answersTextUn[i].gameObject.SetActive(true);
            }

            currentQuestionIndexUnpossible++;
        }
        else
        {
            ShowResultsUnpossible();
        }
    }

    public void AnswerBttnsNormal(int index1)
    {
        if (answersTextNormal[index1].text == crntQNormal.answer[0])
        {
            print("Правильный ответ");
            correctAnswersNormal++;
        }
        else
        {
            print("Неправильный ответ");
        }

        if (currentQuestionIndexNormal < totalQuestionsNormal)
        {
            questionGenerateNormal();
        }
        else
        {
            ShowResultsNormal();
        }
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

    public void AnswerBttnsHard(int index)
    {
        if (answersTextHard[index].text == crntQHard.answer[0]) 
        {
            print("Правильный ответ");
            correctAnswersHard++;
        }
        else
        {
            print("Неправильный ответ");
        }

        qListHard.Remove(crntQHard);
        currentQuestionIndexHard++;
        questionGenerateHard();
    }


    public void AnswerBttnsUnpossible(int index)
    {
        if (answersTextUn[index].text == crntQUnpossible.answer[0])
        {
            print("Правильный ответ");
            correctAnswersUnpossible++;
        }
        else
        {
            print("Неправильный ответ");
        }

        qListUnpossible.Remove(crntQUnpossible);

        if (currentQuestionIndexUnpossible < totalQuestionsUnpossible)
        {
            questionGenerateUnpossible();
        }
        else
        {
            ShowResultsUnpossible();
        }
    }



    void ShowResults()
    {
        HidePanel();
        qNumberTMP.gameObject.SetActive(false);
        qTextTMP.gameObject.SetActive(false);

        foreach (TextMeshProUGUI answerText in answersText)
        {
            answerText.gameObject.SetActive(false);
        }

        resultPanel.SetActive(true);
        resultText.text = "Результат: " + correctAnswers + " из " + totalQuestions + " вопросов";

        restartButton.gameObject.SetActive(true);
        currentQuestionIndex = 0;
        correctAnswers = 0;

        HideAnswerButtons();
    }

    void ShowResultsNormal()
    {
        HidePanel();
        qNumberTMP.gameObject.SetActive(false);
        qTextTMP.gameObject.SetActive(false);

        foreach (TextMeshProUGUI answerText in answersText)
        {
            answerText.gameObject.SetActive(false);
        }

        resultPanel.SetActive(true);
        resultText.text = "Результат: " + correctAnswersNormal + " из " + totalQuestionsNormal + " вопросов";

        restartButton.gameObject.SetActive(true);
        currentQuestionIndexNormal = 0;
        correctAnswersNormal = 0;

        HideAnswerButtons();
    }

    void ShowResultsHard()
    {
        HidePanel();
        HidePanel2();
        qNumberTMP.gameObject.SetActive(false);
        qTextTMP.gameObject.SetActive(false);

        foreach (TextMeshProUGUI answerText in answersText)
        {
            answerText.gameObject.SetActive(false);
        }

        resultPanel.SetActive(true);
        resultText.text = "Результат: " + correctAnswersHard + " из " + totalQuestionsHard + " вопросов";

        restartButton.gameObject.SetActive(true);
        currentQuestionIndexHard = 0;
        correctAnswersHard = 0;

        HideAnswerButtons();
    }

    void ShowResultsUnpossible()
    {
        HidePanel();
        HidePanel3();
        qNumberTMP.gameObject.SetActive(false);
        qTextTMP.gameObject.SetActive(false);

        foreach (TextMeshProUGUI answerText in answersText)
        {
            answerText.gameObject.SetActive(false);
        }

        resultPanel.SetActive(true);
        resultText.text = "Результат: " + correctAnswersUnpossible + " из " + totalQuestionsUnpossible + " вопросов";

        restartButton.gameObject.SetActive(true);
        currentQuestionIndexUnpossible = 0;
        correctAnswersUnpossible = 0;

        HideAnswerButtons();
    }

    public void OnClickRestart()
    {
        panel1.SetActive(false);
        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        qList = new List<QuestionList>(questions);
        qListNormal = new List<QuestionList>(questionsNormal);
        qListHard = new List<QuestionList>(questionsHard);
        qListUnpossible = new List<QuestionList>(questionsUnpossible);

        qNumberTMP.gameObject.SetActive(true);
        qTextTMP.gameObject.SetActive(true);

        currentQuestionIndex = 0;
        correctAnswers = 0;

        currentQuestionIndexNormal = 0;
        correctAnswersNormal = 0;

        currentQuestionIndexHard = 0;
        correctAnswersHard = 0;

        currentQuestionIndexUnpossible = 0;
        correctAnswersUnpossible = 0;

        ShowAnswerButtons();
        questionGenerate();
    }

    void HideResult()
    {
        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    void HidePanel()
    {
        panel.SetActive(false);
    }

    void ShowPanel()
    {
        panel.SetActive(true);
    }
    void HidePanel2()
    {
        panel2.SetActive(false);
    }

    void ShowPanel2()
    {
        panel2.SetActive(true);
    }
    void HidePanel3()
    {
        panel3.SetActive(false);
    }

    void ShowPanel3()
    {
        panel3.SetActive(true);
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
