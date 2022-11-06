using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameUpdate : MonoBehaviour
{
    private TextMeshProUGUI nameText;
    // Start is called before the first frame update
    void Start()
    {
        nameText = GetComponent<TextMeshProUGUI>();
        nameText.text = PlayerPrefs.GetString("username");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
