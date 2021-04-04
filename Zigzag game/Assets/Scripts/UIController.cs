using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text score;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject startScreen;
//    [SerializeField] Text tapToStartText;

    [Header("Difficulty Toggles")]
    [SerializeField] Toggle toggleEasy;
    [SerializeField] Toggle toggleNormal;
    [SerializeField] Toggle toggleHard;

    public bool isEasy = false;
    public bool isNormal = false;
    public bool isHard = true;
    public bool gameStarted = false;

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
    }

    void HideGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void UpdateScore()
    {
        score.text = CollectablesController.Instance.GetTotalScore().ToString();
    }

    public void OnStartClick()
    {
        SetDifficulty();
        startScreen.SetActive(false);
 //       tapToStartText.gameObject.SetActive(true);
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

    public enum Difficulty
    {
        easy,
        normal,
        hard
    }
    public Difficulty currentDifficulty;

    public void SetDifficulty()
    {
        switch (currentDifficulty)
        {
            case Difficulty.easy:
                isEasy = true;
                break;
            case Difficulty.normal:
                isNormal = true;
                break;
            case Difficulty.hard:
                isHard = true;
                break;
        }
    }

}
