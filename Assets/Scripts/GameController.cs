using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using static Constants;

public class GameController : MonoBehaviour
{
    private string macchinario;
    private string voltaggio;
    public GameObject macchina1;
    public GameObject macchina2;
    public GameObject parametro1;
    public GameObject parametro2;
    public GameObject sliderA;
    public GameObject sliderP;

    private void Start()
    {
        
    }

    public void onTdcsSelected()
    {
        macchinario = TDCS;
        iniziaFaseDue();
    }
    public void onTmsSelected()
    {
        macchinario = TMS;
        iniziaFaseDue();
    }

    public void iniziaFaseDue()
    {
        SceneManager.LoadScene(GAME_2, LoadSceneMode.Single);
        macchina1.SetActive(false);
        macchina2.SetActive(false);
        parametro1.SetActive(true);
        parametro2.SetActive(true);
    }

    public void scegliVoltaggioA()
    {
        voltaggio = "Ahm";
        sliderA.SetActive(true);
        sliderP.SetActive(false);     
    }

    public void scegliVoltaggioP()
    {
        voltaggio = "%";
        sliderA.SetActive(false);
        sliderP.SetActive(true);
    }


    public void exitSession()
    {
        SceneManager.LoadScene(Constants.GAME_SELECTION, LoadSceneMode.Single);
    }

}
