using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizMenu : Openable
{
    private int _rightAnswer;
    [SerializeField] private Text _equationHolder;
    [SerializeField] private Text[] _answerHolders;
    private System.Action _onSucceeded;
    private void GenerateNewEquation()
    {
        int firstTerm = Random.Range(0, 10);
        int secondTerm = Random.Range(0, 10);
        _equationHolder.text = $"{firstTerm} + {secondTerm} = ?";
        _rightAnswer = firstTerm + secondTerm;

        int rightAnswerIndex = Random.Range(0, _answerHolders.Length);
        var answers = new int[_answerHolders.Length];
        for (int i = 0; i < _answerHolders.Length; i++)
        {
            if (rightAnswerIndex == i)
                _answerHolders[i].text = _rightAnswer.ToString();
            else
            {
                int answer;
                do
                {
                    answer = Random.Range(0, 19);
                }
                while (answers.Contains(answer) || answer == firstTerm + secondTerm);
                answers[i] = answer;
                _answerHolders[i].text = answer.ToString();
            }
        }
    }
    public void SetSucceededCallback(System.Action onSucceeded)
    {
        _onSucceeded = onSucceeded;
        _onSucceeded += Close;
    }
    public override void Open()
    {
        base.Open();
        GenerateNewEquation();
    }
    public override void Close()
    {
        base.Close();
    }
    public void HandleAnswer(Text text)
    {
        if (text.text == _rightAnswer.ToString())
            _onSucceeded();
        else
            Close();
    }
}
