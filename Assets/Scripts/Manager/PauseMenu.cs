using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Image pauseMenu;

    [HideInInspector]public float volumen;
    [HideInInspector] public bool mute;

    [SerializeField] Slider volumenBar;
    [SerializeField] Image muteButton;
    [SerializeField]GameManager gManager;
    [SerializeField] AudioClip buttonPress;
    [SerializeField] GameObject instrucciones;

    public delegate void OnPause();
    public event OnPause pause;
    bool activate;

    public delegate void OnClosePause();
    public event OnClosePause pauseClose;
    // Start is called before the first frame update
    void Start()
    {
        volumenBar.value = Mathf.Sqrt(volumen);
        if (mute)
        {
            muteButton.color = Color.red;
        }
        else
        {
            muteButton.color = Color.white;
        }
        pauseMenu.gameObject.SetActive(false);
        instrucciones.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!activate)
            {
                OpenPauseMenu();
            }
            else
            {
                CerrarPausa();
            }

        }

        gManager.audioSource.volume = (volumenBar.value * volumenBar.value);
    }

    public void OpenPauseMenu()
    {
        pause.Invoke();
        activate = true;
        pauseMenu.gameObject.SetActive(true);
        instrucciones.SetActive(true);
        Time.timeScale = 0;
    }

    public void Reiniciar()
    {
        BotonPresionado();
        Time.timeScale = 1;
        SceneManager.LoadScene("Nivel");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Mute()
    {
        if (mute)
        {
            muteButton.color = Color.white;
            mute = false;
        }
        else
        {

            muteButton.color = Color.red;
            mute = true;
        }
        gManager.audioSource.mute = mute;
        BotonPresionado();
    }

    public void CerrarPausa()
    {
        pauseClose();
        activate = false;
        Time.timeScale = 1;
        BotonPresionado();
        pauseMenu.gameObject.SetActive(false);
        instrucciones.SetActive(false);
    }

    void BotonPresionado()
    {
        AudioSource.PlayClipAtPoint(buttonPress, transform.position);
    }
}
