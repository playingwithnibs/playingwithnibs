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

    private List<BrainZone> brainZones;

    private Dictionary<Button, BrainZone> buttonZoneMap;
    private Dictionary<Button, int> buttonStimulatorNameMap;

    private Dictionary<BrainZone, int> zoneStimulationTypeMap;

    private Dictionary<Button, ZoneConfig> config;

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
        dlpfcZoneButton = GameObject.Find("dlpfc-zone").GetComponent<Button>();
        oZoneButton = GameObject.Find("o-zone").GetComponent<Button>();
        m1ZoneButton = GameObject.Find("m1-zone").GetComponent<Button>();

        //medicalEquipmentRecap.sprite = Resources.Load("Sprites/medical-eq-recap-" + pm.medicalEquipment.ToString().ToLower(), typeof(Sprite)) as Sprite;

        zoneStimulationTypeMap = new Dictionary<BrainZone, int>();
        config = new Dictionary<Button, ZoneConfig>();
        initBrainAreas();
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

        brainAreas = new List<Button> { dlpfcZoneButton, oZoneButton, m1ZoneButton };
        brainAreas.ForEach((zoneButton) => {
            zoneButton.onClick.AddListener(() => {
                ZoneConfig zoneConfig = null;

                if (mode == MODE_SELECTION || mode == MODE_DIRECTION)
                {
                    Debug.Log("Clicked area: " + zoneButton.name + " | " + buttonZoneMap[zoneButton].stimulator.tapCounter);
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
                    config[zoneButton] = zoneConfig;
                    zoneStimulationTypeMap[buttonZoneMap[zoneButton]] = buttonStimulatorNameMap[selectedStimulator];
                }                
            });
        });

        forwardButton.onClick.AddListener(() => generateConfiguration());
    }

    private void initBrainAreas()
    {
        brainAreas = new List<Button> { dlpfcZoneButton, oZoneButton, m1ZoneButton };
        Stimulator dlpfcStimulator = new Stimulator();
        Stimulator oZoneStimulator = new Stimulator();
        Stimulator m1ZoneStimulator = new Stimulator();

        BrainZone dlpfcZoneLeft = new BrainZone(BrainZoneNames.DLPFC, Position.LEFT);
        BrainZone dlpfcZoneUpper = new BrainZone(BrainZoneNames.DLPFC, Position.UPPER);
        BrainZone dlpfcZoneRight = new BrainZone(BrainZoneNames.DLPFC, Position.RIGHT);

        BrainZone oZoneLeft = new BrainZone(BrainZoneNames.O, Position.LEFT);
        BrainZone oZoneUpper = new BrainZone(BrainZoneNames.O, Position.UPPER);
        BrainZone oZoneRight = new BrainZone(BrainZoneNames.O, Position.RIGHT);

        BrainZone m1ZoneLeft = new BrainZone(BrainZoneNames.M1, Position.LEFT);
        BrainZone m1ZoneUpper = new BrainZone(BrainZoneNames.M1, Position.UPPER);
        BrainZone m1ZoneRight = new BrainZone(BrainZoneNames.M1, Position.RIGHT);

        brainZones = new List<BrainZone> {
            dlpfcZoneLeft, dlpfcZoneUpper, dlpfcZoneRight,
            oZoneLeft, oZoneUpper, oZoneRight,
            m1ZoneLeft, m1ZoneUpper, m1ZoneRight
        };

        buttonZoneMap = new Dictionary<Button, BrainZone>();
        buttonZoneMap.Add(dlpfcZoneButton, dlpfcZoneUpper);
        buttonZoneMap.Add(oZoneButton, oZoneUpper);
        buttonZoneMap.Add(m1ZoneButton, m1ZoneUpper);

        // TODO: lateral positions
        // TODO: refactor button names according to their position (left, right, center)
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
                    s = positive;
                    targetImage.enabled = true;
                    break;
                case 2:
                    s = negative;
                    targetImage.enabled = true;
                    break;
                case 3:
                    s = neutral;
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
        brainZone.position = Position.UPPER; // TODO: make other cases
        brainZone.stimulator = new Stimulator((ElectrodeType) state);
        brainZone.stimulatorType = targetImage.enabled ? stimulatorType : (int) StimulationType.NO;
        return brainZone;  
    }

    private void generateConfiguration()
    {
        Debug.Log("## START CONFIGURATION ##");
        brainZones.ForEach((zone) => Debug.Log(zone));
    }
}
