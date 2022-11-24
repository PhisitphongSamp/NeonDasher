using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private SpriteRenderer[] sprites;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        //find the lowest y value in world space
        float minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 19)).x;

        foreach (SpriteRenderer sr in sprites)
        {
            sr.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

            if (sr.bounds.max.x < minX)
            {
                Vector3 position = sr.transform.position;

                position.x = position.x + (sprites.Length * sr.bounds.size.x) - 0.1f;
                sr.transform.position = position;

            }
        }
    }
}
