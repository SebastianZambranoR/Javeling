using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPersistence : MonoBehaviour
{
    [HideInInspector] public float VolumenValue;
    [HideInInspector] public bool mute;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
