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

    private static int MODE_NOTHING = 0;
    private static int MODE_SELECTION = 1;
    private static int MODE_DIRECTION = 2;

    private int mode;
    private Button selectedStimulator;

    private List<Button> toolbox;
    private List<Button> brainAreas;

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

        toolbox = new List<Button> { eightCoilButton, circularCoilButton, hCoilButton, twoCoilButton, hdCoilButton };
        toolbox.ForEach((stimulator) => {
            stimulator.onClick.AddListener(() => {
                Debug.Log("Selection mode: " + stimulator.name);
                selectedStimulator = stimulator;
                mode = MODE_SELECTION;
            });
        });

        

        brainAreas = new List<Button> { dlpfcZoneButton, oZoneButton, m1ZoneButton };
        brainAreas.ForEach((area) => {
            area.onClick.AddListener(() => {
                if (mode == MODE_SELECTION)
                {
                    Debug.Log("Clicked area: " + area.name);
                    mode = MODE_DIRECTION;
                    Debug.Log(area.transform.GetChild(0).name);
                    Stimulator stimulator = new Stimulator();
                  
                }                
            });
        });
    }

    private void initBrainAreas()
    {
        brainAreas = new List<Button> { dlpfcZoneButton, oZoneButton, m1ZoneButton };
        Stimulator dlpfcStimulator = new Stimulator();
        Stimulator oZoneStimulator = new Stimulator();
        Stimulator m1ZoneStimulator = new Stimulator();

        BrainZone dlpfcZoneLeft = new BrainZone(BrainZoneNames.DLPFC, Position.LEFT, dlpfcStimulator);
        BrainZone dlpfcZoneUpper = new BrainZone(BrainZoneNames.DLPFC, Position.UPPER, dlpfcStimulator);
        BrainZone dlpfcZoneRight = new BrainZone(BrainZoneNames.DLPFC, Position.RIGHT, dlpfcStimulator);

        BrainZone oZoneLeft = new BrainZone(BrainZoneNames.O, Position.LEFT, oZoneStimulator);
        BrainZone oZoneUpper = new BrainZone(BrainZoneNames.O, Position.UPPER, oZoneStimulator);
        BrainZone oZoneRight = new BrainZone(BrainZoneNames.O, Position.RIGHT, oZoneStimulator);

        BrainZone m1ZoneLeft = new BrainZone(BrainZoneNames.M1, Position.LEFT, m1ZoneStimulator);
        BrainZone m1ZoneUpper = new BrainZone(BrainZoneNames.M1, Position.UPPER, m1ZoneStimulator);
        BrainZone m1ZoneRight = new BrainZone(BrainZoneNames.M1, Position.RIGHT, m1ZoneStimulator);
    }
}
