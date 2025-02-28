using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance { get; private set; }
    [SerializeField] Animator anim;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    bool switching = false;
    string loadingScene;
    public void SwitchScene(string sceneName)
    {
        if (switching) return;
        anim.SetTrigger("SwitchScene");
        switching = true;
        loadingScene = sceneName;
    }
    public void Switch()
    {
        SceneManager.LoadScene(loadingScene);
        anim.SetTrigger("EndSwitching");
        switching = false;
    }
}
