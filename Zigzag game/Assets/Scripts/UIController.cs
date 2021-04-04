using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text totalScore;
    [SerializeField] Text score;

    [Header("Difficulty Toggles")]
    [SerializeField] Toggle toggleEasy;
    [SerializeField] Toggle toggleNormal;
    [SerializeField] Toggle toggleHard;

    bool isEasy = false;
    bool isNormal = false;
    bool isHard = true;
    bool gameStarted = false;

    public enum Difficulty
    {
        easy,
        normal,
        hard
    }
    public Difficulty currentDifficulty;

    public delegate void StartGame();
    public static event StartGame onStartGame;

    static UIController instance;

    public static UIController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIController>();
            }
            return instance;
        }
    }

    void Start()
    {
        Player.onGameOver += HideGameOverScreen;
        HideGameOverScreen();
        UpdateScore();
        toggleEasy.isOn = isEasy;
        toggleNormal.isOn = isNormal;
        toggleHard.isOn = isHard;
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        totalScore.text = "Total score: " + CollectablesController.Instance.GetTotalScore().ToString();
    }

    void HideGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void UpdateScore()
    {
        score.text = CollectablesController.Instance.GetTotalScore().ToString();
    }

    public void OnStartClick() // кнопка "Старт"
    {
        SetDifficulty();
        startScreen.SetActive(false);
        gameStarted = true;
        onStartGame();
    }

    public void SelectEasy(bool newValue)
    {
        isEasy = newValue;
    }
    
    public void SelectNormal(bool newValue)
    {
        isNormal = newValue;
    }    

    public void SelectHard(bool newValue)
    {
        isHard = newValue;
    }

    public void SetDifficulty()
    {
        if (isEasy == true)
        {
            currentDifficulty = Difficulty.easy;
        }
        if (isNormal == true)
        {
            currentDifficulty = Difficulty.normal;
        }
        if (isHard == true)
        {
            currentDifficulty = Difficulty.hard;
        }
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    public bool IsEasy()
    {
        return isEasy;
    }
    public bool IsNormal()
    {
        return isNormal;
    }
}
