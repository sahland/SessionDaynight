using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;

    private float _visibleTime;
    private float _lastModeVisibleTime;
    private Transform _ui;
    private Image _healthSlider;
    private Transform _cam;

    private void Start()
    {
        _visibleTime = 5f;
        _cam = Camera.main.transform;

        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                _ui = Instantiate(uiPrefab, c.transform).transform;
                _healthSlider = _ui.GetChild(0).GetComponent<Image>();
                _ui.gameObject.SetActive(false);
                break;
            }
        }

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (_ui != null)
        {
            _ui.gameObject.SetActive(true);
            _lastModeVisibleTime = Time.time;

            float healthPercent = currentHealth / (float)maxHealth;
            _healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(_ui.gameObject);
            }
        }
    }

    private void LateUpdate()
    {
        if (_ui != null)
        {
            _ui.position = target.position;
            _ui.forward = -_cam.forward;

            if (Time.time - _lastModeVisibleTime > _visibleTime)
            {
                _ui.gameObject.SetActive(false);
            }
        }
    }
}
