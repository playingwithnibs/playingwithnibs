using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager istanza;
     public PlayerManager getPlayerManager()
    {
        if (istanza == null)
            istanza= new PlayerManager();

        return istanza;
    }

    private PlayerManager()
    {

    }


    void Awake()
    {
        DontDestroyOnLoad(this);
        getPlayerManager();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
