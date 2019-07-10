using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineConfigurationController : MonoBehaviour

{
    private Text subtitle;
    private PlayerManager pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.getInstance();
        subtitle = GameObject.Find("Canvas/Subtitle").GetComponent<Text>();

        subtitle.text = "Configure the " + pm.medicalEquipment.name + " you have selected";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
