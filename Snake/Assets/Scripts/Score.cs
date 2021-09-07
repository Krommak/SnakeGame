using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int humansScore = 0;
    private int gemsScore = 0;
    [SerializeField]
    private int gemsCountForFever = 3;

    [SerializeField]
    private Text humansScoreText;
    [SerializeField]
    private Text gemsScoreText;

    private void Start()
    {
        CheckScore();
    }

    private void CheckScore()
    {
        humansScoreText.text = humansScore.ToString();
        gemsScoreText.text = gemsScore.ToString();
    }

    public void AddHumanScore()
    {
        humansScore++;
        if (gemsScore > 0)
        {
            ResetGemsScore();
        }
        CheckScore();
    }

    public bool AddGemsScore()
    {
        gemsScore++;
        CheckScore();
        if (gemsScore == gemsCountForFever)
        {
            gemsScore = 0;
            CheckScore();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetGemsScore()
    {
        gemsScore = 0;
    }

    public int GetScore()
    {
        return humansScore;
    }

}
