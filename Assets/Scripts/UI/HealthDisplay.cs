using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Text _health;
    [SerializeField] private Animation _animation;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.OnHealtChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        _player.OnHealtChanged -= UpdateHealth;
    }
    private void UpdateHealth(int health)
    {
        _health.text = health.ToString();
        _animation.Play();
    }
}
