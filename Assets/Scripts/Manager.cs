using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Manager_UI), typeof(AudioSource))]
public class Manager : MonoBehaviour
{
    [SerializeField] float timeLimit;
    List<Quiz> quiz = new List<Quiz>();
    [SerializeField] LeftButton[] left;
    [SerializeField] RightButton[] right;
    [SerializeField] AudioClip correctSound, wrongSound, shuffleSound;
    AudioSource audioSource;
    public float counter { get; private set; }
    Manager_UI UIs;

    LeftButton selectedLeft;
    RightButton selectedRight;
    private void Awake()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("");
        foreach(Sprite i in sprites)
        {
            quiz.Add(new Quiz(i, i.name));
        }
        audioSource = GetComponent<AudioSource>();
    }
    void AudioPlay(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    private void Start()
    {
        counter = timeLimit;
        UIs = GetComponent<Manager_UI>(); 
        Randomize();
    }
    void Update()
    {
        if (counter > 0.0f) counter = Mathf.Max(counter - Time.deltaTime, 0.0f);
        else
        {
            GameEnd();
        }
        UIs.OnUpdate();
    }
    void GameEnd()
    {
        foreach (var i in left) i.gameObject.SetActive(false);
        foreach (var i in right) i.gameObject.SetActive(false);
        bool hi = false;
        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {
            hi = true;
            PlayerPrefs.SetInt("HighScore", score);
        }
        UIs.GameEnd(hi);
        this.enabled = false;
    }
    public void SelectLeft(LeftButton button)
    {
        if (selectedLeft != null) return;
        selectedLeft = button;
        selectedLeft.Select();
        if (selectedRight != null) Compare();
    }
    public void SelectRight(RightButton button)
    {
        if (selectedRight != null) return;
        selectedRight = button;
        selectedRight.Select();
        if (selectedLeft != null) Compare();
    }
    public int score { get; private set; }
    const int correctScore = 100;
    const int wrongScore = 50;
    int solved = 0;
    void Compare()
    {
        if(selectedRight.setQuiz == selectedLeft.setQuiz)
        {
            selectedLeft.Correct(); selectedRight.Correct();
            solved++;
            score += correctScore;
            AudioPlay(correctSound);
            if (solved == 4)
            {
                Invoke("Randomize", 1.0f);
                solved = 0;
            }
        }
        else
        {
            selectedLeft.Wrong(); selectedRight.Wrong();
            score -= wrongScore;
            AudioPlay(wrongSound);
        }
        UIs.ScoreUI();
        selectedRight = null;
        selectedLeft = null;
    }
    void Randomize()
    {
        List<Quiz> quizzes = new List<Quiz>();
        foreach(Quiz i in quiz)
        {
            quizzes.Add(i);
        }
        Quiz[] usedQuiz = new Quiz[4];
        for(int i = 0; i < 4; i++)
        {
            usedQuiz[i] = quizzes[Random.Range(0, quizzes.Count)];
            quizzes.Remove(usedQuiz[i]);
        }
        List<LeftButton> leftButtons = new List<LeftButton>();
        List<RightButton> rightButtons = new List<RightButton>();
        for(int i = 0; i < 4; i++)
        {
            leftButtons.Add(left[i]);
            rightButtons.Add(right[i]);
        }
        LeftButton[] randomizedLeft = new LeftButton[4];
        RightButton[] randomizedRight = new RightButton[4];
        for(int i = 0; i < 4; i++)
        {
            int num = Random.Range(0, leftButtons.Count);
            leftButtons[num].Set(usedQuiz[i]);
            leftButtons.RemoveAt(num);
            num = Random.Range(0, rightButtons.Count);
            rightButtons[num].Set(usedQuiz[i]);
            rightButtons.RemoveAt(num);
        }
        AudioPlay(shuffleSound);
    }
    public void ReturnToTitle()
    {
        SceneSwitcher.Instance.SwitchScene("Main");
    }
}
[System.Serializable]
public class Quiz
{
    public Quiz(Sprite image, string answer)
    {
        this.image = image;
        this.answer = answer;
    }
    public Sprite image;
    public string answer;
}
