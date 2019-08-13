﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;

    public static CoinManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("CoinManager").GetComponent<CoinManager>();
            }
            return _instance;
        }
    }
    private static CoinManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCoinAt(Vector3 position)
    {
        GameObject coin = Instantiate(coinPrefab, position, Quaternion.identity, transform);
        RezTween.ScaleFromTo(coin, 0.5f, 0, 1, RezTweenEase.SPRING);
    }
}
