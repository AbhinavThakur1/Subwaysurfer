using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectDestroy : MonoBehaviour
{

    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text coincount;
    [SerializeField] TMP_Text scorepause;
    [SerializeField] TMP_Text coincountpause;
    [SerializeField] TMP_Text scorerestart;
    [SerializeField] TMP_Text coincountrestart;
    [SerializeField] AudioClip coinAudioClip;
    [SerializeField] GameObject playtime;
    [SerializeField] GameObject playtimepause;
    [SerializeField] GameObject playtimerestart;
    [SerializeField] Slider sensitivityslider;
    [SerializeField] Slider musicvolumeslider;
    [SerializeField] AudioSource musicsource;
    [SerializeField] Slider sfxvolumeslider;
    [SerializeField] AudioSource sfxsource;
    public bool pause;
    AudioSource coinAudioSource;
    float timepassed;
    public float speed;
    float timepasseduntilnow = 30f;
    public int coins = 0;

    private void Start()
    {

        coinAudioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivityslider.value = PlayerPrefs.GetFloat("Sensitivity");
        }
        else
        {
            PlayerPrefs.SetFloat("Sensitivity", 200f);
            sensitivityslider.value = 200f;
        }
        sensitivityslider.onValueChanged.AddListener(Sensitivityvaluechanged);

        if (PlayerPrefs.HasKey("Music"))
        {
            musicvolumeslider.value = PlayerPrefs.GetFloat("Music");
        }
        else
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
            musicvolumeslider.value = 0.5f;
        }
        musicvolumeslider.onValueChanged.AddListener(Musicvaluechanged);

        if (PlayerPrefs.HasKey("Sfx"))
        {
            sfxvolumeslider.value = PlayerPrefs.GetFloat("Sfx");
        }
        else
        {
            PlayerPrefs.SetFloat("Sfx", 0.5f);
            sfxvolumeslider.value = 0.5f;
        }
        sfxvolumeslider.onValueChanged.AddListener(Sfxvaluechanged);

    }

    void Update()
    {
        score.text = "Score: " + ((int)timepassed).ToString();
        coincount.text = "Coins: " + coins.ToString();
        scorepause.text = "Score: " + ((int)timepassed).ToString();
        coincountpause.text = "Coins: " + coins.ToString();
        scorerestart.text = "Score: " + ((int)timepassed).ToString();
        if (Time.timeScale > 0)
        {
            coincountrestart.text = "Coins: " + coins.ToString();
        }
        timepassed += Time.deltaTime;
        if (timepasseduntilnow < timepassed)
        {
            timepasseduntilnow += 20f;
            if (speed != -17f)
            {
                speed -= 1f;
            }
        }
        if (Time.timeScale == 0)
        {
            AudioSource [] ad = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            foreach (AudioSource i in ad)
            {
                i.Stop();
            }
        }
        else
        {
            AudioSource[] ad = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            foreach (AudioSource i in ad)
            {
                if (!i.isPlaying)
                {
                    i.Play();
                }
            }
        }
        if(Time.timeScale == 0 && !pause)
        {
            playtime.SetActive(false);
            playtimerestart.SetActive(true);
        }
    }

    void Sensitivityvaluechanged(float i)
    {
        FindFirstObjectByType<PlayerControl>().swipe = i;
        PlayerPrefs.SetFloat("Sensitivity",i);
    }

    void Musicvaluechanged(float i)
    {
        musicsource.volume = i;
        PlayerPrefs.SetFloat("Music", i);
    }

    void Sfxvaluechanged(float i)
    {
        sfxsource.volume = i;
        PlayerPrefs.SetFloat("Sfx", i);
    }

    public void Playcoinpicksound()
    {
        coinAudioSource.PlayOneShot(coinAudioClip);
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coins);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        if (PlayerPrefs.GetInt("Score") < (int)timepassed)
        {
            PlayerPrefs.SetInt("Score", (int)timepassed);
        }
        playtime.SetActive(true);
        playtimerestart.SetActive(false);
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coins);
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        playtime.SetActive(true);
        playtimepause.SetActive(false);
        pause = false;
    }

    public void Pause()
    {
        playtime.SetActive(false);
        playtimepause.SetActive(true);
        Time.timeScale = 0;
        pause = true;
    }

    public void Coinuse()
    {
        if(coins+PlayerPrefs.GetInt("Coins") >= 100)
        {
            if(coins >= 100)
            {
                coins -= 100;
                FindFirstObjectByType<PlayerControl>().transform.position = new Vector3(0f,15f,0f);
                playtime.SetActive(true);
                playtimerestart.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - (100 - coins));
                coins = 0;
                FindFirstObjectByType<PlayerControl>().transform.position = new Vector3(0f, 15f, 0f);
                playtime.SetActive(true);
                playtimerestart.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else
        {
            coincountrestart.text = "Not Enough coins";
        }
    }
}
