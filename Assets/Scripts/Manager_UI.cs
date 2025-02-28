using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Manager_UI : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Text time, score, hiScore;
    [SerializeField] Text gameEndScore;
    Manager origin;
    private void Start()
    {
        origin = GetComponent<Manager>();
        hiScore.text = "High score: " + PlayerPrefs.GetInt("HighScore", 0);
        ScoreUI();
    }
    public void OnUpdate()
    {
        time.text = "Time left: " + (Mathf.FloorToInt(origin.counter) / 60) + ":" + ((Mathf.FloorToInt(origin.counter)%60 >= 10) ? "" : "0") + (Mathf.FloorToInt(origin.counter) % 60);
    }
    public void ScoreUI()
    {
        score.text = "Score: " + origin.score;
    }
    public void GameEnd(bool highScore)
    {
        anim.SetTrigger("GameOver");
        gameEndScore.text = "Your score: " + origin.score + (highScore ? "(New record!)" : "");
    }
}
