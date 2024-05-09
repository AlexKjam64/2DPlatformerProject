using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounter;
    public static ScoreManager scoreMan;
    public int playerScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreMan = this;
    }

    public void IncreaseScore(int increase)
    {
        playerScore += increase;
        coinCounter.text = "Coins: " + playerScore.ToString();
    }
}
