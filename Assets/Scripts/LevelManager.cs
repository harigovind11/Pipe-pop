using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
 
    [SerializeField] GameObject gameOverCanvas;

    [SerializeField] private float timeLimit = 10;
    [SerializeField] private TextMeshProUGUI countdownText;

    private float currentTime;
    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f; // Ensure time scale is reset when the game starts
    }
    private void Start()
    {
        currentTime = timeLimit;
    }


    private void Update()
    {
        currentTime -= Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0f, timeLimit);

        // Format time as MM:SS
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        countdownText.text = $"{minutes:00}:{seconds:00}";

        if (currentTime <= 0f)
        {
           GameOver();
        }
        else if (currentTime <= 10f)
        {
            countdownText.color = Color.red; // Change text color to red when time is low
        }

    }
    public void GameOver()
    {
   
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;

    }

    public void ReloadGame()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


    public void QuitGame()
    {
        Application.Quit();
    }


}