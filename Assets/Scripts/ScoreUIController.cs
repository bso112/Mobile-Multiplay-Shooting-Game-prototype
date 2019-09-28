using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour
{
    private GameManager gm;
    private Text homeScoreText; 
    private Text awayScoreText; 

    private void Start()
    {
        gm = GameManager.Instance;
        gm.OnScoreUpdated += UpdateUI; 
    }

    void UpdateUI()
    {
        homeScoreText.text = gm.HomeScore.ToString();
        awayScoreText.text = gm.AwayScore.ToString();
    }
}
