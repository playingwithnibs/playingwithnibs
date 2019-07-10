using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.UI;

public class MachineConfigurationController : MonoBehaviour

{
    private PlayerManager pm;

    private Text subtitle;
    private SpriteRenderer
        medicalEquipmentRecap,
        intensityRectangle;
    private SpriteRenderer pulseRectangle;
    private Toggle
        intensityToggle,
        pulseToggle,
        singlePulseToggle,
        rtmsHfToggle,
        rtmsLfToggle,
        mtToggle,
        ampereToggle;
    private Slider intensitySlider;
    private Button forwardButton;

    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.getInstance();

        subtitle = GameObject.Find("Subtitle").GetComponent<Text>();
        medicalEquipmentRecap = GameObject.Find("medical-eq-recap").GetComponent<SpriteRenderer>();
        intensityRectangle = GameObject.Find("intensity-rectangle").GetComponent<SpriteRenderer>();
        pulseRectangle = GameObject.Find("pulse-rectangle").GetComponent<SpriteRenderer>();
        intensityToggle = GameObject.Find("intensity-toggle").GetComponent<Toggle>();
        pulseToggle = GameObject.Find("pulse-toggle").GetComponent<Toggle>();
        mtToggle = GameObject.Find("mt-toggle").GetComponent<Toggle>();
        ampereToggle = GameObject.Find("ampere-toggle").GetComponent<Toggle>();
        intensitySlider = GameObject.Find("intensity-slider").GetComponent<Slider>();
        singlePulseToggle = GameObject.Find("single-pulse-toggle").GetComponent<Toggle>();
        rtmsHfToggle = GameObject.Find("rtms-hf-toggle").GetComponent<Toggle>();
        rtmsLfToggle = GameObject.Find("rtms-lf-toggle").GetComponent<Toggle>();
        forwardButton = GameObject.Find("forward-arrow").GetComponent<Button>();

        subtitle.text = "Configure the " + pm.medicalEquipment + " you have selected";
        medicalEquipmentRecap.sprite = Resources.Load("Sprites/medical-eq-recap-" + pm.medicalEquipment.ToString().ToLower(), typeof(Sprite)) as Sprite;

        intensityToggle.onValueChanged.AddListener((isChecked) => {
            intensityRectangle.sprite = Resources.Load("Sprites/intensity-rectangle-" + (isChecked ? "on" : "off"), typeof(Sprite)) as Sprite;
            mtToggle.interactable = isChecked;
            ampereToggle.interactable = isChecked;
            intensitySlider.interactable = isChecked;
        });

        pulseToggle.onValueChanged.AddListener((isChecked) => {
            pulseRectangle.sprite = Resources.Load("Sprites/pulse-rectangle-" + (isChecked ? "on" : "off"), typeof(Sprite)) as Sprite;
            singlePulseToggle.interactable = isChecked;
            rtmsHfToggle.interactable = isChecked;
            rtmsLfToggle.interactable = isChecked;
        });
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public double getMin() {
        if (new SimulationSolution().isTdcs(pm.medicalEquipment)) {
            Tdcs tdcs = (Tdcs)pm.medicalEquipment;
            return tdcs.min;
        } else {
            Tms tms = (Tms)pm.medicalEquipment;
            return tms.min;
        }
    }

    public double getMax() {
        if (new SimulationSolution().isTdcs(pm.medicalEquipment)) {
            Tdcs tdcs = (Tdcs)pm.medicalEquipment;
            return tdcs.max;
        } else {
            Tms tms = (Tms)pm.medicalEquipment;
            return tms.max;
    }
    }
}
