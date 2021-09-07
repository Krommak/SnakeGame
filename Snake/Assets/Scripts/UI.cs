using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    SnakeController snakeController;
    [SerializeField]
    Score scoreComponent;

    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    GameObject endGamePanel;

    [SerializeField]
    string textForDeathByBomb, textForDeathByHumans, textforEndGameScore, textForGreatScore;
    [SerializeField]
    Text deathByText, endGameScore, greatScore;

    private void Start()
    {
        snakeController.StopGame();
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        snakeController.StartGame();
    }

    public void ToMenu()
    {
        snakeController.StopGame();
        endGamePanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EndGame(bool deathByBomb)
    {
        snakeController.StopGame();
        int nowScore = scoreComponent.GetScore();
        int greatScoreInt = PlayerPrefs.GetInt("GreatScore"); 
        if(nowScore > greatScoreInt)
        {
            PlayerPrefs.SetInt("GreatScore", nowScore);
        }
        greatScore.text = textForGreatScore + greatScoreInt;
        endGameScore.text = textforEndGameScore + scoreComponent.GetScore();
        if (deathByBomb)
        {
            deathByText.text = textForDeathByBomb;
            StartCoroutine(EndGameCoroutine());
        }
        else
        {
            deathByText.text = textForDeathByHumans;
            endGamePanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        endGamePanel.SetActive(true);
    }
}
