using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    float time = 10;
    float timer;
    Slider bar;
    [SerializeField] Button playButton;
    private void Awake()
    {
        bar = GetComponent<Slider>();
        playButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        bar.value = timer / time;

        if(bar.value == 1)
        {
            playButton.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Nivel");
    }
}
