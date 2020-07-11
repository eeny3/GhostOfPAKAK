using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour
{
    float timer;
    float lifeSpan = 1.4f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeSpan)
        {
            Destroy(gameObject);
        }
    }
}
