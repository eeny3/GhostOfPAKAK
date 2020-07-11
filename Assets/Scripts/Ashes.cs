using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashes : MonoBehaviour
{
    float timer;
    float lifeSpan = 1.2f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeSpan)
        {
            Destroy(gameObject);
        }
    }
}
