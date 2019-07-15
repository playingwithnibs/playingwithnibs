using System.Collections;
using System;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlacementController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public GameObject cam;
    public VideoClip videoClip;
    private PlayerManager pm;

    Font regularFont;
    Font boldFont;

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
    private Text
        dlpfcZoneText,
        oZoneText,
        soZoneText,
        m1ZoneText;

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
    private Dictionary<BrainZone, Text> zoneTextMap;

    private HashSet<BrainZone> activeZones;
    private AudioSource audioSource;

    private AudioSource audioSourceElect;

    private AudioSource audioSourceMagn;

    // Start is called before the first frame update
    void Start()
    {
        // Will attach a VideoPlayer to the main camera.
        cam = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        videoPlayer = cam.AddComponent<VideoPlayer>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        //videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;

        // This will cause our Scene to be visible through the video being played.
        videoPlayer.targetCameraAlpha = 0.5F;

        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        videoPlayer.url = "/Video/explosionTrim2.mp4";

        // Skip the first 100 frames.
        //videoPlayer.frame = 100;

        // Restart from beginning when done.
        videoPlayer.isLooping = false;

        // Each time we reach the end, we slow down the playback by a factor of 10.
        //videoPlayer.loopPointReached += EndReached;

        pm = PlayerManager.getInstance();

        pm.time -= Time.deltaTime;

        if (pm.time <= 0)
        {
        GameObject
            .Find("BonusText")
            .GetComponent<Text>()
            .text
                = "Malus:\n" +
                    ((int)pm.time).ToString() +
                    " pts.";

        GameObject
            .Find("BonusText")
            .GetComponent<Text>()
            .color = new Color(1f, 0.13f, 0f, 1f);
        }
        else
        {
        GameObject
            .Find("BonusText")
            .GetComponent<Text>()
            .text
                = "Bonus\n" +
                    ((int)pm.time).ToString() +
                    " pts. ";

        GameObject
            .Find("BonusText")
            .GetComponent<Text>()
            .color = new Color(0.5647059f, 1f, 0.48f, 1f);
        }


        regularFont = Resources.Load<Font>("Fonts/TitilliumWeb-Regular") as Font;
        boldFont = Resources.Load<Font>("Fonts/TitilliumWeb-Bold") as Font;

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

        dlpfcZoneText = GameObject.Find("Brain zone names/dlpfc").GetComponent<Text>();
        m1ZoneText= GameObject.Find("Brain zone names/m1").GetComponent<Text>();
        soZoneText = GameObject.Find("Brain zone names/so").GetComponent<Text>();
        oZoneText = GameObject.Find("Brain zone names/o").GetComponent<Text>();
        

        audioSource = GameObject.Find("sound-effects").GetComponent<AudioSource>();

        audioSourceElect = 
            GameObject.Find("sound-effect-elec").GetComponent<AudioSource>();

        audioSourceMagn = 
            GameObject.Find("sound-effect-magn").GetComponent<AudioSource>();
        
        medicalEquipmentRecap.sprite = Resources.Load("Sprites/medical-eq-recap-" + pm.medicalEquipment.name, typeof(Sprite)) as Sprite;


        initStimulatorNames();

        toolbox = new List<Button> { eightCoilButton, circularCoilButton, hCoilButton, hdCoilButton };
        toolbox.ForEach((stimulator) => {
            stimulator.onClick.AddListener(() => {
                audioSource.Play();
                if (mode == MODE_DIRECTION)
                {
                    toolbox.ForEach(button => changeButtonColor(button, Color.white));
                    mode = MODE_NOTHING;
                    
                }
                if (mode == MODE_SELECTION)
                {
                    toolbox.ForEach(button => changeButtonColor(button, Color.white));
                    mode = MODE_NOTHING;
                    
                }
                //Debug.Log("Selection mode: " + stimulator.name);
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

                // activate forward button only if there is at least one active zone
                bool atLeastOneActive = false;
                brainZones.ForEach(zone =>
                {
                    if (zone.isActive())
                    {
                        atLeastOneActive = true;
                        return;
                    }
                });
                forwardButton.interactable = atLeastOneActive;
            });
        });

        forwardButton.onClick.AddListener(() => generateConfiguration());
        backButton.onClick.AddListener(() => {
            SceneManager.LoadScene(Constants.GAME_2, LoadSceneMode.Single);
        });
    }

    private void Update()
    {
        pm.time -= Time.deltaTime;

        if (pm.time <= 0)
        {
        GameObject
            .Find("BonusText")
            .GetComponent<Text>()
            .text
                = "Malus:\n" +
                    ((int)pm.time).ToString() +
                    " pts.";

        GameObject
            .Find("BonusText")
            .GetComponent<Text>()
            .color = new Color(1f, 0.13f, 0f, 1f);
        }
        else
        {
        GameObject
            .Find("BonusText")
            .GetComponent<Text>()
            .text
                = "Bonus\n" +
                    ((int)pm.time).ToString() +
                    " pts. ";

        GameObject
            .Find("BonusText")
            .GetComponent<Text>()
            .color = new Color(0.5647059f, 1f, 0.48f, 1f);
        }

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

        zoneTextMap = new Dictionary<BrainZone, Text>();
        zoneTextMap.Add(dlpfcZoneUpper, dlpfcZoneText);
        zoneTextMap.Add(dlpfcZoneLeft, dlpfcZoneText);
        zoneTextMap.Add(dlpfcZoneRight, dlpfcZoneText);

        zoneTextMap.Add(oZoneUpper, oZoneText);
        zoneTextMap.Add(oZoneLeft, oZoneText);
        zoneTextMap.Add(oZoneRight, oZoneText);

        zoneTextMap.Add(soZoneUpper, soZoneText);
        zoneTextMap.Add(soZoneLeft, soZoneText);
        zoneTextMap.Add(soZoneRight, soZoneText);

        zoneTextMap.Add(m1ZoneUpper, m1ZoneText);
        zoneTextMap.Add(m1ZoneLeft, m1ZoneText);
        zoneTextMap.Add(m1ZoneRight, m1ZoneText);
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
                    audioSourceMagn.Play();
                    break;
                case 2:
                    s = positive;
                    stimulatorType = (int)ElectrodeName.DEFAULT;
                    targetImage.enabled = true;
                    audioSourceElect.Play();
                    break;
                case 3:
                    s = negative;
                    stimulatorType = (int)ElectrodeName.DEFAULT;
                    targetImage.enabled = true;
                    audioSourceElect.Play();
                    break;
            }
            
        }
        else if (stimulatorType == (int) TmsStimulator.EIGHT)
        {
            if (state == 1)
            {
                audioSourceMagn.Play();
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
                audioSourceMagn.Play();
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

        //Debug.Log("INSIDE METHOD: " + brainZone.stimulator);

 
        // highlight brain area text of type T, only if there is at least one active zone of type T.
        bool atLeastOneActive = false;
        brainZones.ForEach(z =>
        {
            if (z.brainZoneName == brainZone.brainZoneName && z.isActive())
            {
                atLeastOneActive = true;
                return;
            }
        });
        zoneTextMap[brainZone].font = atLeastOneActive ? boldFont : regularFont;        
        return brainZone;  
    }

    private void generateConfiguration()
    {
        //Debug.Log("## START CONFIGURATION ##");
        //brainZones.ForEach((zone) => { if (zone.isActive()) Debug.Log(zone); }
        //);

        //brainZones.ForEach((zone) => { Debug.Log(zone); }
        //     );

        pm.endTime = pm.getCurrentTimestampInSeconds();

        pm.outcome = 
            new SimulationSolution()
            .getOutcome(
                pm.buildMedicalEquipment(),
                pm.medicalReport,
                new BrainZonesArray(brainZones));

        if (pm.outcome == Outcome.EXPLOSION) {
            // Start playback. This means the VideoPlayer may have to prepare (reserve
            // resources, pre-load a few frames, etc.). To better control the delays
            // associated with this preparation one can use videoPlayer.Prepare() along with
            // its prepareCompleted event.
            videoPlayer.Play();
        }

        // Debug.Log(pm.medicalReport + "\n" + pm.medicalEquipment +
        //     "\n" + pm.brainZones);
        //Debug.Log(pm.outcome);

        SceneManager.LoadScene(Constants.RESULT, LoadSceneMode.Single);
    }
}
