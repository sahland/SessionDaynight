using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public string[] texts;
    public float duration;
    public int messagesShown = 0;

    private GameObject _messageSign;
    private Text _messageText;
    private bool _messageShown = false;

    private void Awake()
    {
        GameObject hud = GameObject.Find("HUD");
        if (hud == null)
        {
            Debug.LogError("HUD not found");
            return;
        }

        _messageSign = hud.transform.Find("MessageBox").gameObject;
        if (_messageSign == null)
        {
            Debug.LogError("MessageBox not found under HUD");
            return;
        }

        _messageText = _messageSign.transform.Find("MessageText").GetComponent<Text>();
        if (_messageText == null)
        {
            Debug.LogError("MessageText not found or missing Text component");
            return;
        }
    }

    private void Start()
    {
        if (_messageSign != null)
        {
            _messageSign.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShowMessageBox();
        }
    }

    public void ShowMessageBox()
    {
        if (_messageSign != null && _messageText != null)
        {
            _messageSign.SetActive(true);
            _messageText.text = texts[messagesShown];
            _messageText.enabled = true;
            _messageShown = true;
            StartCoroutine(HideMessageAfterDuration());
        }
    }

    public void Accept()
    {
        if (_messageShown)
        {
            HideMessageBox();
            messagesShown++;
            if (messagesShown < texts.Length)
            {
                ShowMessageBox();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void HideMessageBox()
    {
        if (_messageSign != null && _messageText != null)
        {
            _messageSign.SetActive(false);
            _messageText.enabled = false;
            _messageShown = false;
        }
    }

    private IEnumerator HideMessageAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        HideMessageBox();
    }
}
