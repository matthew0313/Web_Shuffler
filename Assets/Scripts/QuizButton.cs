using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Animator))]
public abstract class QuizButton : MonoBehaviour
{
    Button button;
    protected Animator anim;
    protected Manager manager;
    protected bool canClick = true;
    private void Awake()
    {
        button = GetComponent<Button>();
        anim = GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        button.onClick.AddListener(OnClick);
    }
    public Quiz setQuiz { get; private set; }
    public virtual void Set(Quiz quiz)
    {
        Debug.Log(gameObject.name);
        setQuiz = quiz;
        anim.SetTrigger("Out");
    }
    public virtual void OnClick()
    {

    }
    public void Select()
    {
        anim.SetTrigger("Selected");
    }
    public void Correct()
    {
        anim.SetTrigger("Correct");
    }
    public void Wrong()
    {
        anim.SetTrigger("Incorrect");
    }
    public void Clickable()
    {
        canClick = true;
    }
    public void Unclickable()
    {
        canClick = false;
    }
}
