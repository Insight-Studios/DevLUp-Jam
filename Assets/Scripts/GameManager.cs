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
            DontDestroyOnLoad(this);
            UIMiniGame = GameObject.FindWithTag("MainCanvas");
            UIMiniGame.transform.Find("NextLevel").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
            Instance = this;
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

        yield return new WaitForSecondsRealtime(fadeTime);

        UIMiniGame = GameObject.FindWithTag("MainCanvas");
        levelOverlay = UIMiniGame.transform.Find("NextLevel").GetComponent<Image>();

        levelOverlay.CrossFadeAlpha(0f, fadeTime, false);
        yield return new WaitForSecondsRealtime(fadeTime);
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
