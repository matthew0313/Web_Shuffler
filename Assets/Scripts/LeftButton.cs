using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftButton : QuizButton
{
    [SerializeField] Image image;
    public override void Set(Quiz quiz)
    {
        image.sprite = quiz.image;
        base.Set(quiz);
    }
    public override void OnClick()
    {
        if (!canClick) return;
        manager.SelectLeft(this);
    }
}
