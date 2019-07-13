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
        hdCoilButton,
        dlpfcUpperZoneButton,
        dlpfcLeftZoneButton,
        dlpfcRightZoneButton,
        oUpperZoneButton,
        oLeftZoneButton,
        oRightZoneButton,
        soUpperZoneButton,
        soLeftZoneButton,
        soRightZoneButton,
        m1UpperZoneButton,
        m1LeftZoneButton,
        m1RightZoneButton;
    private SpriteRenderer
        medicalEquipmentRecap;

    private static int MODE_NOTHING = 0;
    private static int MODE_SELECTION = 1;
    private static int MODE_DIRECTION = 2;

    private int mode;
    private Button selectedStimulator;

    private List<Button> toolbox;
    private List<Button> brainZoneButtons;

    private List<BrainZone> brainZones;

    private Dictionary<Button, BrainZone> buttonZoneMap;
    private Dictionary<Button, int> buttonStimulatorNameMap;

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
        hdCoilButton = GameObject.Find("hd-coil-button").GetComponent<Button>();

        dlpfcUpperZoneButton = GameObject.Find("Upper/dlpfc-zone").GetComponent<Button>();
        dlpfcLeftZoneButton = GameObject.Find("Left/dlpfc-zone").GetComponent<Button>();
        dlpfcRightZoneButton = GameObject.Find("Right/dlpfc-zone").GetComponent<Button>();

        oUpperZoneButton = GameObject.Find("Upper/o-zone").GetComponent<Button>();
        oLeftZoneButton = GameObject.Find("Left/o-zone").GetComponent<Button>();
        oRightZoneButton = GameObject.Find("Right/o-zone").GetComponent<Button>();

        soUpperZoneButton = GameObject.Find("Upper/so-zone").GetComponent<Button>();
        soLeftZoneButton = GameObject.Find("Left/so-zone").GetComponent<Button>();
        soRightZoneButton = GameObject.Find("Right/so-zone").GetComponent<Button>();

        m1UpperZoneButton = GameObject.Find("Upper/m1-zone").GetComponent<Button>();
        m1LeftZoneButton = GameObject.Find("Left/m1-zone").GetComponent<Button>();
        m1RightZoneButton = GameObject.Find("Right/m1-zone").GetComponent<Button>();

        //medicalEquipmentRecap.sprite = Resources.Load("Sprites/medical-eq-recap-" + pm.medicalEquipment.ToString().ToLower(), typeof(Sprite)) as Sprite;

        initStimulatorNames();

        toolbox = new List<Button> { eightCoilButton, circularCoilButton, hCoilButton, hdCoilButton };
        toolbox.ForEach((stimulator) => {
            stimulator.onClick.AddListener(() => {
                if (mode == MODE_DIRECTION)
                {
                    toolbox.ForEach(button => changeButtonColor(button, Color.white));
                    mode = MODE_NOTHING;
                    return;
                }
                if (mode == MODE_SELECTION)
                {
                    toolbox.ForEach(button => changeButtonColor(button, Color.white));
                    mode = MODE_NOTHING;
                    return;
                }
                Debug.Log("Selection mode: " + stimulator.name);
                selectedStimulator = stimulator;
                mode = MODE_SELECTION;
                changeButtonColor(stimulator, Color.black);
            });
        });

        initBrainZones();
        brainZoneButtons.ForEach((zoneButton) => {
            zoneButton.onClick.AddListener(() => {
                if (mode == MODE_SELECTION || mode == MODE_DIRECTION)
                {
                    mode = MODE_DIRECTION;

                    if (selectedStimulator.name == "circular-coil-button")
                    {
                        buttonZoneMap[zoneButton].stimulator.tap(4);
                        handleBrainZoneClick(zoneButton, buttonZoneMap[zoneButton].stimulator.tapCounter, (int) TmsStimulator.CIRCULAR);
                    } else if (selectedStimulator.name == "eight-coil-button")
                    {
                        buttonZoneMap[zoneButton].stimulator.tap(2);
                        handleBrainZoneClick(zoneButton, buttonZoneMap[zoneButton].stimulator.tapCounter, (int) TmsStimulator.EIGHT);
                    } else if (selectedStimulator.name == "hd-coil-button")
                    {
                        buttonZoneMap[zoneButton].stimulator.tap(2);
                        handleBrainZoneClick(zoneButton, buttonZoneMap[zoneButton].stimulator.tapCounter, (int) TdcsStimulator.HD);
                    }
                }                
            });
        });

        forwardButton.onClick.AddListener(() => generateConfiguration());
        backButton.onClick.AddListener(() => {
            SceneManager.LoadScene(Constants.GAME_2, LoadSceneMode.Single);
        });
    }

    private void initBrainZones()
    {
        brainZoneButtons = new List<Button> {
            dlpfcUpperZoneButton, oUpperZoneButton, m1UpperZoneButton, soUpperZoneButton,
            dlpfcLeftZoneButton, oLeftZoneButton, m1LeftZoneButton, soLeftZoneButton,
            dlpfcRightZoneButton, oRightZoneButton, m1RightZoneButton, soRightZoneButton
        };

        BrainZone dlpfcZoneLeft = new BrainZone(BrainZoneNames.DLPFC, Position.RIGHT);
        BrainZone dlpfcZoneUpper = new BrainZone(BrainZoneNames.DLPFC, Position.UPPER);
        BrainZone dlpfcZoneRight = new BrainZone(BrainZoneNames.DLPFC, Position.LEFT);

        BrainZone oZoneLeft = new BrainZone(BrainZoneNames.O, Position.RIGHT);
        BrainZone oZoneUpper = new BrainZone(BrainZoneNames.O, Position.UPPER);
        BrainZone oZoneRight = new BrainZone(BrainZoneNames.O, Position.LEFT);

        BrainZone soZoneLeft = new BrainZone(BrainZoneNames.SO, Position.RIGHT);
        BrainZone soZoneUpper = new BrainZone(BrainZoneNames.SO, Position.UPPER);
        BrainZone soZoneRight = new BrainZone(BrainZoneNames.SO, Position.LEFT);

        BrainZone m1ZoneLeft = new BrainZone(BrainZoneNames.M1, Position.RIGHT);
        BrainZone m1ZoneUpper = new BrainZone(BrainZoneNames.M1, Position.UPPER);
        BrainZone m1ZoneRight = new BrainZone(BrainZoneNames.M1, Position.LEFT);

        brainZones = new List<BrainZone> {
            dlpfcZoneLeft, dlpfcZoneUpper, dlpfcZoneRight,
            oZoneLeft, oZoneUpper, oZoneRight,
            soZoneLeft, soZoneUpper, soZoneRight,
            m1ZoneLeft, m1ZoneUpper, m1ZoneRight
        };

        buttonZoneMap = new Dictionary<Button, BrainZone>();
        buttonZoneMap.Add(dlpfcUpperZoneButton, dlpfcZoneUpper);
        buttonZoneMap.Add(dlpfcLeftZoneButton, dlpfcZoneLeft);
        buttonZoneMap.Add(dlpfcRightZoneButton, dlpfcZoneRight);

        buttonZoneMap.Add(oUpperZoneButton, oZoneUpper);
        buttonZoneMap.Add(oLeftZoneButton, oZoneLeft);
        buttonZoneMap.Add(oRightZoneButton, oZoneRight);

        buttonZoneMap.Add(soUpperZoneButton, soZoneUpper);
        buttonZoneMap.Add(soLeftZoneButton, soZoneLeft);
        buttonZoneMap.Add(soRightZoneButton, soZoneRight);

        buttonZoneMap.Add(m1UpperZoneButton, m1ZoneUpper);
        buttonZoneMap.Add(m1LeftZoneButton, m1ZoneLeft);
        buttonZoneMap.Add(m1RightZoneButton, m1ZoneRight);
    }

    private void initStimulatorNames()
    {
        buttonStimulatorNameMap = new Dictionary<Button, int>();
        buttonStimulatorNameMap.Add(eightCoilButton, (int) TmsStimulator.EIGHT);
        buttonStimulatorNameMap.Add(circularCoilButton, (int) TmsStimulator.CIRCULAR);
        buttonStimulatorNameMap.Add(hCoilButton, (int) TmsStimulator.H);
        buttonStimulatorNameMap.Add(hdCoilButton, (int) TdcsStimulator.HD);
    }

    private void changeButtonColor(Button button, Color color)
    {
       button.GetComponent<Image>().color = color;
    }

    private BrainZone handleBrainZoneClick(Button zoneButton, int state, int stimulatorType)
    {
        BrainZone brainZone = buttonZoneMap[zoneButton];
        Image targetImage = zoneButton.GetComponentInChildren<Button>().transform.GetChild(0).GetComponent<Image>();
        Sprite s = Resources.Load<Sprite>("none");

        if (stimulatorType == (int) TmsStimulator.CIRCULAR)
        {
            Sprite neutral = Resources.Load<Sprite>("Sprites/electrode-neutral");
            Sprite negative = Resources.Load<Sprite>("Sprites/electrode-negative");
            Sprite positive = Resources.Load<Sprite>("Sprites/electrode-positive");

            switch (state)
            {
                case 0:
                    targetImage.enabled = false;
                    break;
                case 1:
                    s = neutral;
                    targetImage.enabled = true;
                    break;
                case 2:
                    s = positive;
                    stimulatorType = (int)ElectrodeName.DEFAULT;
                    targetImage.enabled = true;
                    break;
                case 3:
                    s = negative;
                    stimulatorType = (int)ElectrodeName.DEFAULT;
                    targetImage.enabled = true;
                    break;
            }
            
        }
        else if (stimulatorType == (int) TmsStimulator.EIGHT)
        {
            if (state == 1)
            {
                targetImage.enabled = true;
                s = Resources.Load<Sprite>("Sprites/eight-coil");
            }
            else {
                targetImage.enabled = false;
            }
        }
        else if (stimulatorType == (int) TdcsStimulator.HD)
        {
            if (state == 1)
            {
                targetImage.enabled = true;
                s = Resources.Load<Sprite>("Sprites/hd-coil");
            }
            else {
                targetImage.enabled = false;
            }
        }
        targetImage.sprite = s;
        brainZone.stimulator = 
            new Stimulator((ElectrodeType)state, 
                targetImage.enabled 
                    ? (ElectrodeName)stimulatorType : ElectrodeName.NO);
        //brainZone.stimulatorType = targetImage.enabled ? stimulatorType : (int) StimulationType.NO;
        
        Debug.Log("INSIDE METHOD: " + brainZone.stimulator);

        return brainZone;  
    }

    private void generateConfiguration()
    {
        Debug.Log("## START CONFIGURATION ##");
    brainZones.ForEach((zone) => { if (zone.isActive()) Debug.Log(zone); }
    );

    //brainZones.ForEach((zone) => { Debug.Log(zone); }
    //     );

        pm.outcome = 
            new SimulationSolution()
            .getOutcome(
                pm.buildMedicalEquipment(),
                pm.medicalReport,
                new BrainZonesArray(brainZones));

        // Debug.Log(pm.medicalReport + "\n" + pm.medicalEquipment +
        //     "\n" + pm.brainZones);
        Debug.Log(pm.outcome);
    }
}
