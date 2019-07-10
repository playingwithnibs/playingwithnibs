using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlacementController : MonoBehaviour
{
    private PlayerManager pm;

    private Button
        backButton,
        forwardButton,
        eightCoilButton,
        circularCoilButton,
        hCoilButton,
        twoCoilButton,
        hdCoilButton,
        dlpfcZoneButton,
        oZoneButton,
        m1ZoneButton;
    private SpriteRenderer
        medicalEquipmentRecap;

    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.getInstance();

        backButton = GameObject.Find("back-button").GetComponent<Button>();
        forwardButton = GameObject.Find("forward-button").GetComponent<Button>();
        medicalEquipmentRecap = GameObject.Find("medical-eq-recap").GetComponent<SpriteRenderer>();
        eightCoilButton = GameObject.Find("eight-coil-button").GetComponent<Button>();
        circularCoilButton = GameObject.Find("circular-coil-button").GetComponent<Button>();
        hCoilButton = GameObject.Find("h-coil-button").GetComponent<Button>();
        twoCoilButton = GameObject.Find("two-coil-button").GetComponent<Button>();
        hdCoilButton = GameObject.Find("hd-coil-button").GetComponent<Button>();
        dlpfcZoneButton = GameObject.Find("dlpfc-zone").GetComponent<Button>();
        oZoneButton = GameObject.Find("o-zone").GetComponent<Button>();
        m1ZoneButton = GameObject.Find("m1-zone").GetComponent<Button>();

        //medicalEquipmentRecap.sprite = Resources.Load("Sprites/medical-eq-recap-" + pm.medicalEquipment.ToString().ToLower(), typeof(Sprite)) as Sprite;

        List<Button> toolbox = new List<Button> { eightCoilButton, circularCoilButton, hCoilButton, twoCoilButton, hdCoilButton };
        toolbox.ForEach((stimulator) => {
            stimulator.onClick.AddListener(() => {
                Debug.Log(stimulator.name);
            });
        });
    }
}
