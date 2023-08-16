using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    public QuestionList[] questions;

}

[System.Serializable]
public class QuestionList
{
    public string Question;
    public string[] answer = new string[3];
}
