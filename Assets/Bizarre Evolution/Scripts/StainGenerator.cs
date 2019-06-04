using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainGenerator : MonoBehaviour
{

    public Transform resetPos;
    public GameObject[] stains;
    public Transform[] spawnPoints;
    public float speed;

    void FixedUpdate()
    {
        foreach (GameObject item in stains)
        {
            if (item.transform.position.y < resetPos.position.y)
                item.transform.position = spawnPoints[(int) Random.Range(0, spawnPoints.Length)].position;

            item.GetComponent<Rigidbody2D>().velocity = (DataManager.isTimeRunnig) ? Vector2.down * speed : Vector2.zero;
        }
    }
    
}
