using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseUI;

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().Play("click");
            pauseUI.SetActive(!pauseUI.activeSelf);
        }
    }
}
