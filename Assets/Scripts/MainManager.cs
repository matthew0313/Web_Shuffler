using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] Text hiText;
    private void Start()
    {
        hiText.text = "High score: " + PlayerPrefs.GetInt("HighScore", 0);
    }
    public void StartGame()
    {
        SceneSwitcher.Instance.SwitchScene("Game");
    }
}
