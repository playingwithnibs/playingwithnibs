using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Application;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public Application.Pathology pathology;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
