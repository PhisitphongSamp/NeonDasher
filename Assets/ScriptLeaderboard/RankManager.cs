using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class RankManager : MonoBehaviour
{
    public GameObject rankDataPrototype;
    public Transform rankPanel;

    public List<PlayerData> playerDatas;
    public List<GameObject> playerDataObjects;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    
    // Start is called before the first frame update
    void Start()
    {
        playerDatas = new List<PlayerData>();
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateRankData()
    {
        playerDataObjects = new List<GameObject>();
        for (int i = 0; i < playerDatas.Count; i++)
        {
            GameObject rankObj = Instantiate(rankDataPrototype, rankPanel) as GameObject;
            playerDataObjects.Add(rankObj);
            RankData rankData = rankObj.GetComponent<RankData>();
            rankData.playerData = new PlayerData(playerDatas[i].rankNumber, playerDatas[i].playerName, playerDatas[i].playerScore);
            rankData.UpdateData();
        }
    }

    public void AssignRankData(int rank, string name, int score)
    {
        playerDatas.Add(new PlayerData(rank, name, score));
    }

    public void ClearRankData()
    {
        playerDatas = new List<PlayerData>();
        foreach (GameObject playerData in playerDataObjects)
        {
            Destroy(playerData);
        }
        playerDataObjects = new List<GameObject>();
    }
}

