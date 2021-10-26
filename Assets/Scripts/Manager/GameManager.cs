using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Diana diana;
    [SerializeField] PlayerController player;
    [SerializeField] PauseMenu pausa;
    AudioPersistence audioP;
    [HideInInspector]public AudioSource audioSource;
    [SerializeField] JavelImpulse impulse;
    private void OnEnable()
    {
        if(diana != null)
        diana.finishGame += FinishGameVictory;
        if(player != null)
        player.playerDeath += FinishGamePlayerDeath;

        pausa.pause += GamePaused;
        pausa.pauseClose += GameResume;
    }
    private void OnDisable()
    {
        if (diana != null)
            diana.finishGame -= FinishGameVictory;
        if (player != null)
            player.playerDeath -= FinishGamePlayerDeath;

        pausa.pause -= GamePaused;
        pausa.pauseClose -= GameResume;
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 80;
        audioP = GameObject.Find("AudioPersistence").GetComponent<AudioPersistence>();
        audioSource.volume = audioP.VolumenValue;
        audioSource.mute = audioP.mute;

        pausa.volumen = audioP.VolumenValue;
        pausa.mute = audioP.mute;
    }
    void Start()
    {

    }
    private void Update()
    {
        audioP.VolumenValue = pausa.volumen;
        audioP.mute = pausa.mute;
    }


    void FinishGameVictory()
    {
        this.Invoke("LoadCredits", 0.4f);
    }

    void LoadCredits()
    {
        SceneManager.LoadScene("Creditos");
    }

    void FinishGamePlayerDeath()
    {
        pausa.Invoke("OpenPauseMenu", 0.4f);
    }

    void GamePaused()
    {
        impulse.enabled = false;
    }

    void GameResume()
    {
        this.Invoke("ActivateImpulse", 0.5f);
    }

    void ActivateImpulse()
    {
        impulse.enabled = true;
    }
}
