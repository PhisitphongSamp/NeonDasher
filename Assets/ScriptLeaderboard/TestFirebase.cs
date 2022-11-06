using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using SimpleJSON;
using System.Linq;
using UnityEngine.SceneManagement;

[System.Serializable]
public class User
{
    
        public string name;
        public int score;

        public User(string name, int score = 0)
        {
            this.name = name;
            this.score = score;

        }
    
   
}
public class TestFirebase : MonoBehaviour
{
    public string url = "https://fir-8eddb-default-rtdb.asia-southeast1.firebasedatabase.app/";
    public string secret = "l1jeKIe692sqph00MebwrLbIksbzWENwuIea5HUC";

    private RankManager rankManager;
    public List<User> user;
    public User currentUser;
    // Start is called before the first frame update
    void Start()
    {
        SetData();
        rankManager = FindObjectOfType<RankManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetData()
    {
        string urlData = $"{url}/User/userData.json?auth={secret}";

        user = new List<User>();

        RestClient.Get(urlData).Then(response =>
        {
            Debug.Log(response.Text);
            JSONNode jsonNode = JSONNode.Parse(response.Text);

            for (int i = 0; i < jsonNode.Count; i++)
            {
                user.Add(new User(jsonNode[i]["name"], jsonNode[i]["score"]));
            }

            //user = new User(jsonNode["name"], jsonNode["gender"], jsonNode["age"]); 
            user = user.OrderByDescending(score => score.score).ToList();
            rankManager.ClearRankData();
            string currentName = PlayerPrefs.GetString("username");
            for (int i = 0; i < user.Count; i++)
            {
                rankManager.AssignRankData(i + 1, user[i].name, user[i].score);
                if(user[i].name == currentName)
                {
                    rankManager.rankText.text = (i + 1).ToString();
                    rankManager.nameText.text = user[i].name;
                    rankManager.scoreText.text = user[i].score.ToString();
                }
            }
            rankManager.CreateRankData();

        }).Catch(error =>
        {
            Debug.Log("error");
        });
    }

    public void SetData()
    {
        string userId = PlayerPrefs.GetString("userId");
        string urlData = $"{url}/User/userData/{userId}.json?auth={secret}";



        RestClient.Get(urlData).Then(response =>
        {
            Debug.Log(response.Text);
            JSONNode jsonNode = SimpleJSON.JSON.Parse(response.Text);
            int currentScore = PlayerPrefs.GetInt("score");
            
            if (currentScore > jsonNode["score"])
            {
                string currentName = PlayerPrefs.GetString("username");
                currentUser = new User(currentName, currentScore);
                
                RestClient.Put<User>(urlData, currentUser).Then(response =>
                {
                    Debug.Log("Upload Data Complete");
                    GetData();
                    
                }).Catch(error =>
                {
                    Debug.Log("error on set to server");
                });
            }
            else
            {
                GetData();
            }
            //user = new User(jsonNode["name"], jsonNode["gender"], jsonNode["age"]); 

        }).Catch(error =>
        {
            Debug.Log("error");
        });

    }

    public void BackToMain()
    {
        SceneManager.LoadScene(1);
    }
}