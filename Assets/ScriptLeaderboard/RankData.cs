using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int rankNumber;
    

    public int playerScore;

    

    public PlayerData(int rankNumber, string playerName, int playerScore)
    {
        this.rankNumber = rankNumber;
        this.playerName = playerName;
        this.playerScore = playerScore;
    }
}

public class RankData : MonoBehaviour
{
    //[Header("Data")]


    public PlayerData playerData;

    [Space]
    [Header("UI")]
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateData()
    {
        rankText.text = playerData.rankNumber.ToString();
        playerNameText.text = playerData.playerName;
        scoreText.text = playerData.playerScore.ToString();
    }
}
