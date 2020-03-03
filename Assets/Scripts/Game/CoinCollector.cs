using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public event Action<int> OnCoinCollected;

    public int Coins { get; private set; }

    private void Start()
    {
        Coins = 0;
        OnCoinCollected?.Invoke(Coins);
    }

    public void AddCoins(int value)
    {
        Coins += value;
        OnCoinCollected?.Invoke(Coins);
    }
}
