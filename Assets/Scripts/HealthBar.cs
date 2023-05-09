using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color damaged;
    private SpriteRenderer _spriteRenderer;
    private RectTransform _sliderTransform;
    private Camera _cam;

    private void Awake()
    {
        _spriteRenderer = transform.parent.gameObject.GetComponentInParent<SpriteRenderer>();
        _sliderTransform = slider.GetComponent<RectTransform>();
        _cam = Camera.main;
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
        
        slider.fillRect.GetComponentInChildren<Image>().color = damaged;
    }

    private void Update()
    {
        slider.transform.position = _cam!.WorldToScreenPoint(transform.parent.position + new Vector3(0, _spriteRenderer.size.y / 2 + .2f));
    }
}
