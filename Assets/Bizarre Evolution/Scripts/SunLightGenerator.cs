using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLightGenerator : MonoBehaviour
{

    public GameObject prefab;
    public int lightLimit, xPosMin, xPosMax, yPosMin, yPosMax;

    private Vector2[] positions;

    void Start()
    {
        positions = new Vector2[lightLimit];

        for (int i = 0; i < lightLimit; i++)
        {
            float x = Random.Range(xPosMin, xPosMax);
            float y = Random.Range(yPosMin, yPosMax);

            if (x > -5 && x <= 0)
                x -= 5;
            else if (x > 0 && x < 5)
                x += 5;
            if (y > -5 && y <= 0)
                y -= 5;
            else if (y > 0 && y < 5)
                y += 5;

            positions[i] = new Vector2(x, y);

            GameObject clone = Instantiate(prefab, positions[i], Quaternion.identity);
            clone.transform.parent = transform;
        }
    }
    
}
