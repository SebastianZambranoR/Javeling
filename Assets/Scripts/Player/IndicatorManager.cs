using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorManager : MonoBehaviour
{
    [HideInInspector] public float holdTime;
    [HideInInspector] public float holdMaxTime;
    [SerializeField] Image indicador;

    // Update is called once per frame
    void Update()
    {
        if(holdTime < holdMaxTime)
        {
            indicador.fillAmount = holdTime / (holdMaxTime - 1);
        }
        else
        {
            indicador.fillAmount = 0;
        }

    }
}
