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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartLevel()
    {
        UIMiniGame = GameObject.FindWithTag("MainCanvas");
        var levelOverlay = UIMiniGame.transform.Find("NextLevel").GetComponent<Image>();
        levelOverlay.CrossFadeAlpha(1f, 0f, false);

        var levelNumber = UIMiniGame.transform.Find("LevelNumber").GetComponent<Text>();
        levelNumber.text = SceneManager.GetActiveScene().buildIndex > 1 ? (SceneManager.GetActiveScene().buildIndex - 1).ToString() : "";

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

        yield return null;

        yield return StartLevel();        
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelRoutine());
    }

    void Pause()
    {

    }

    void Play()
    {

    }
}
