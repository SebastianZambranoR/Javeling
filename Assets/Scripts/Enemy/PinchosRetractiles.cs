using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchosRetractiles : MonoBehaviour
{
    [SerializeField] GameObject pincho;
    [SerializeField] float time;
    float timer;
    bool active;
    Signifier sig;

    private void Awake()
    {
        sig = GetComponentInChildren<Signifier>();
    }
    void Start()
    {
        pincho.SetActive(false);
    }

    private void OnEnable()
    {
        sig.finishAnim += ActivarDesactivarPincho;
    }

    private void OnDisable()
    {
        sig.finishAnim -= ActivarDesactivarPincho;
    }


    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= time)
        {
            if (!active)
            {
                sig.PlayAnimation();
            }
            else
            {
                ActivarDesactivarPincho();
            }

            timer = 0;
        }
    }

    void ActivarDesactivarPincho()
    {
        if (active)
        {
            pincho.SetActive(false);
            active = false;
        }
        else
        {
            pincho.SetActive(true);
            active = true;

        }
    }


}
