using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platformPrefab;
    private float spawnx = 20f;
    private float platformLength = 50f;
    public int platformCount = 2;
    private Transform playerTransform;
    private int safeZone = 50;
    private List<GameObject> activePlatform;
    void Start()
    {
        activePlatform = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 0; i < platformCount; i++)
        {
            spawnPlatform();
        }
    }

    void Update()
    {
        if (playerTransform.position.x - safeZone > (spawnx - platformCount * platformLength))
        {
            spawnPlatform();
            deletePlatform();
        }
    }

    void spawnPlatform(int prefabIndex= -1)
    {
        GameObject go;
        int randomPlatform = Random.Range(0, 10); 
        go = Instantiate(platformPrefab[randomPlatform]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector2.right * spawnx;
        activePlatform.Add(go);
        spawnx += platformLength;
    }

    void deletePlatform()
    {
        Destroy(activePlatform[0]);
        activePlatform.RemoveAt(0);
    }
}
