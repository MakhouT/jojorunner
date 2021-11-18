using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControllerTitle : MonoBehaviour
{
    public GameObject mutedob;
    public GameObject[] images;

    public int pointer;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene("DesertMainScene");
    }
    public void mute()
    {
        FindObjectOfType<AudioManager>().Mute();
        if(FindObjectOfType<AudioManager>().muted == true)
        {
            mutedob.SetActive(true);
        }
        else
        {
            mutedob.SetActive(false);
        }
    }
    public void exit()
    {
        Application.Quit();
    }

    public void Next()
    {
        if(pointer < images.Length-1)
        {
            pointer += 1;
            changeImages();
        }
        else
        {
            pointer = 0;
            changeImages();
        }
    }
    public void Previous()
    {
        if (pointer >= 1)
        {
            pointer -= 1;
            changeImages();
        }
        else
        {
            pointer = images.Length-1;
            changeImages();
        }
    }

    public void changeImages()
    {
        for(int i = 0; i < images.Length; i++)
        {
            if(pointer == i)
            {
                images[i].SetActive(true);
            }
            else
            {
                images[i].SetActive(false);
            }
        }
    }
    
}
