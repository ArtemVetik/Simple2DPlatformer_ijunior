using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private CoinCollector _collector;
    [SerializeField] private Text _coins;

    private void OnEnable()
    {
        _collector.OnCoinCollected += UpdateCoins;
    }

    private void OnDisable()
    {
        _collector.OnCoinCollected -= UpdateCoins;
    }

    private void UpdateCoins(int value)
    {
        _coins.text = value.ToString();
    }
}
