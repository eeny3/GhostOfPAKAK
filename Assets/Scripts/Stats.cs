using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text score;
    public Text wave;

    void Start()
    {
        score.text = "Souls: " + PlayerPrefs.GetInt("souls").ToString();
    }
}
