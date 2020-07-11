using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVoice : MonoBehaviour
{
    AudioSource audioSource;
    float timer = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            audioSource.PlayOneShot(audioSource.clip);
            timer = 5.0f;
        }
    }
}
