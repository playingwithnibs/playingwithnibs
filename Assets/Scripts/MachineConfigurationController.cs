using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineConfigurationController : MonoBehaviour

{
    private Text subtitle;
    private SpriteRenderer medicalEquipmentRecap;
    private PlayerManager pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.getInstance();
        subtitle = GameObject.Find("Canvas/Subtitle").GetComponent<Text>();
        medicalEquipmentRecap = GameObject.Find("Canvas/medical-eq-recap").GetComponent<SpriteRenderer>();

        subtitle.text = "Configure the " + pm.medicalEquipment.name + " you have selected";

        Debug.Log(medicalEquipmentRecap.sprite.name);
        medicalEquipmentRecap.sprite = Resources.Load("Sprites/medical-eq-recap-tdcs", typeof(Sprite)) as Sprite;
        Debug.Log(medicalEquipmentRecap.sprite.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
