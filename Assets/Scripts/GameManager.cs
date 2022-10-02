using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float fadeTime;

    [SerializeField] NumberOne numberOnePrefab;

    public static GameManager Instance { get; private set; }

    public static GameObject UIMiniGame { get; private set; }
    public static NumberOne NumberOnePrefab { get => Instance.numberOnePrefab; }

    IEnumerator nextLevelRoutine = null;
    int score = 0;
    double timer = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            StartCoroutine(StartLevel());
        }
    }

    public void Quit()
    {
        print("Exiting game");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    IEnumerator StartLevel()
    {
        UIMiniGame = GameObject.FindWithTag("MainCanvas");
        var levelOverlay = UIMiniGame.transform.Find("NextLevel").GetComponent<Image>();
        levelOverlay.CrossFadeAlpha(1f, 0f, false);

        var levelNumber = UIMiniGame.transform.Find("LevelNumber").GetComponent<Text>();
        levelNumber.text = SceneManager.GetActiveScene().buildIndex > 0 ? (SceneManager.GetActiveScene().buildIndex).ToString() : "";
        if (levelNumber.text == "10")
        {
            levelNumber.text = "  0";
        }
        else if (levelNumber.text != "1")
        {
            levelNumber.text = levelNumber.text.Replace('1', Convert.ToChar(65533));
        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            levelOverlay.CrossFadeAlpha(0f, 0f, false);
            levelNumber.text = "";
            yield break;
        }

        yield return new WaitForSecondsRealtime(fadeTime);

        levelOverlay.CrossFadeAlpha(0f, fadeTime, false);

        yield return new WaitForSecondsRealtime(fadeTime);
    }

    private IEnumerator NextLevelRoutine()
    {
        print("LEVEL COMPLETE");
        var levelOverlay = UIMiniGame.transform.Find("NextLevel").GetComponent<Image>();
        
        levelOverlay.CrossFadeAlpha(1f, fadeTime, true);
        yield return new WaitForSecondsRealtime(fadeTime);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }

        yield return null;

        yield return StartLevel();

        nextLevelRoutine = null;
    }

    public void NextLevel()
    {
        if (nextLevelRoutine == null)
        {
            GetComponent<AudioSource>().Play();
            nextLevelRoutine = NextLevelRoutine();
            StartCoroutine(nextLevelRoutine);
        }
    }

    void Pause()
    {

    }

    void Play()
    {

    }
}
