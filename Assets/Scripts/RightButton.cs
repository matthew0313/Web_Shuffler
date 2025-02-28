using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightButton : QuizButton
{
    [SerializeField] Text text;
    public override void Set(Quiz quiz)
    {
        text.text = quiz.answer;
        base.Set(quiz);
    }
    public override void OnClick()
    {
        if (!canClick) return;
        manager.SelectRight(this);
    }
}
