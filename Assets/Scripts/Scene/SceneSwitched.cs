using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitched : MonoBehaviour
{
    public string sceneName;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        if (_button != null)
        {
            _button.onClick.AddListener(SwitchScene);
        }
    }

    public void SwitchScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Scene name is empty");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AudioClick()
    {
        FindObjectOfType<AudioManager>().Play("click");
    }
}
