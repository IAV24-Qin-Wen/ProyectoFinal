using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSShower : MonoBehaviour
{
    TextMeshProUGUI FPSText;
    int avg;
    int qty;
    void Start()
    {
        FPSText = GetComponent<TextMeshProUGUI>();
        avg = ((int)(1f / Time.unscaledDeltaTime));
        qty = 0;
    }

    void Update()
    {
        avg += ((int)(1f / Time.unscaledDeltaTime) - avg) / ++qty;
        FPSText.text = avg.ToString();
    }
}
