using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] Slider audioBar;
    [SerializeField] Image muteButton;
    [SerializeField] AudioSource audioVFX;
    bool mute;
    AudioPersistence persistence;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        persistence = GameObject.Find("AudioPersistence").GetComponent<AudioPersistence>();
    }
    public void IniciarJuego()
    {
        SceneManager.LoadScene("PantallaCarga");
        BotonPresionado();
    }

    public void MuteMusic()
    {
        if (mute)
        {
            mute = false;
            muteButton.color = Color.white;
        }
        else
        {
            mute = true;
            muteButton.color = Color.red;
        }
        audioSource.mute = mute;
        persistence.mute = mute;
        BotonPresionado();
    }

    public void Salir()
    {
        Application.Quit();
    }
    private void Update()
    {
        audioSource.volume = (audioBar.value * audioBar.value);
        persistence.VolumenValue = audioSource.volume;
    }

    void BotonPresionado()
    {
        audioVFX.Play();
    }


}
