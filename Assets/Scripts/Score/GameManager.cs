using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score Settings")]
    public TextMeshProUGUI scoreText;
    public int score = 0;

    [Header("Timer Settings")]
    public TextMeshProUGUI timerText;
    private float timeRemaining;
    private bool timerIsRunning = false;

    public HashSet<string> answeredChests = new HashSet<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartTimer(8 * 60); // 8 menit
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void MarkChestAsAnswered(string chestID)
    {
        if (!answeredChests.Contains(chestID))
        {
            answeredChests.Add(chestID);
        }
    }

    public bool IsChestAnswered(string chestID)
    {
        return answeredChests.Contains(chestID);
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindUIElements();
        UpdateScoreText();
        if (timerIsRunning)
        {
            DisplayTime(timeRemaining);
        }
    }

    public void StartTimer(float durationInSeconds)
    {
        timeRemaining = durationInSeconds;
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                // === PERUBAHAN PENTING ADA DI SINI ===
                Debug.Log("Waktu Habis! Pindah ke scene Exit.");
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining);

                // Langsung pindah ke scene "Exit" tanpa menambah poin
                SceneManager.LoadScene("Exit");
                // =====================================
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timerText == null) return;
        if (timeToDisplay < 0) { timeToDisplay = 0; }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 100;

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
    }

    void FindUIElements()
    {
        GameObject scoreTextObject = GameObject.Find("ScoreText");
        if (scoreTextObject != null) { scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>(); }

        GameObject timerTextObject = GameObject.Find("TimerText");
        if (timerTextObject != null) { timerText = timerTextObject.GetComponent<TextMeshProUGUI>(); }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null) { scoreText.text = "SCORE : " + score; }
    }
}