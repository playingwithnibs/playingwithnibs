using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MachineConfigurationController : MonoBehaviour
{
    private PlayerManager pm;

    private Text
        subtitle,
        minText,
        maxText,
        currentText;
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
    private Button
        forwardButton,
        backButton;

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
        forwardButton = GameObject.Find("forward-button").GetComponent<Button>();
        backButton = GameObject.Find("back-button").GetComponent<Button>();
        minText = GameObject.Find("min-text").GetComponent<Text>();
        maxText = GameObject.Find("max-text").GetComponent<Text>();
        currentText = GameObject.Find("current-text").GetComponent<Text>();

        subtitle.text = "Configure the " + pm.medicalEquipment + " you have selected";
        medicalEquipmentRecap.sprite = Resources.Load("Sprites/medical-eq-recap-" + pm.medicalEquipment.ToString().ToLower(), typeof(Sprite)) as Sprite;

        intensityToggle.onValueChanged.AddListener((isChecked) => {
            intensityRectangle.sprite = Resources.Load("Sprites/intensity-rectangle-" + (isChecked ? "on" : "off"), typeof(Sprite)) as Sprite;
            mtToggle.isOn = false;
            ampereToggle.isOn = false;
            mtToggle.interactable = isChecked;
            ampereToggle.interactable = isChecked;
            intensitySlider.interactable = isChecked;
            Debug.Log("clicca sta merda");
            Debug.Log(intensitySlider.minValue);
        });

        pulseToggle.onValueChanged.AddListener((isChecked) => {
            pulseRectangle.sprite = Resources.Load("Sprites/pulse-rectangle-" + (isChecked ? "on" : "off"), typeof(Sprite)) as Sprite;
            singlePulseToggle.isOn = false;
            rtmsHfToggle.isOn = false;
            rtmsLfToggle.isOn = false;
            singlePulseToggle.interactable = isChecked;
            rtmsHfToggle.interactable = isChecked;
            rtmsLfToggle.interactable = isChecked;
        });

        mtToggle.onValueChanged.AddListener((isChecked) => {

            intensitySlider.minValue = isChecked ? Tms.min : Tdcs.min;
            intensitySlider.maxValue = isChecked ? Tms.max : Tdcs.max;
            intensitySlider.value = isChecked ? Tms.min : Tdcs.min;

            if (intensityToggle.isOn)
            {
                minText.text = intensitySlider.minValue.ToString();
                maxText.text = intensitySlider.maxValue.ToString();
            }
            else
            {
                minText.text = "";
                maxText.text = "";
                currentText.text = "";
            }

            intensitySlider.interactable = isChecked;
        });

        ampereToggle.onValueChanged.AddListener((isChecked) => {
            intensitySlider.minValue = isChecked ? Tdcs.min : Tms.min;
            intensitySlider.maxValue = isChecked ? Tdcs.max : Tms.max;
            minText.text = intensitySlider.minValue.ToString();
            maxText.text = intensitySlider.maxValue.ToString();
            Debug.Log("clicca sta ampere");
            Debug.Log(isChecked);
            intensitySlider.interactable = isChecked;
        });

        intensitySlider.onValueChanged.AddListener((value) => {
            currentText.text = value.ToString(ampereToggle.isOn ? "f1" : "f0");
        });

        forwardButton.onClick.AddListener(() => {
            saveConfig();
            SceneManager.LoadScene(Constants.GAME_3, LoadSceneMode.Single);
        });

        backButton.onClick.AddListener(() => {
            SceneManager.LoadScene(Constants.GAME_1, LoadSceneMode.Single);
        });
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void saveConfig()
    {
        // unit measure
        if (mtToggle.isOn) pm.unitMeasure = UnitMeasure.PERCENTAGE_OF_MT;
        else if (ampereToggle.isOn) pm.unitMeasure = UnitMeasure.MILLIAMPERE;
        else pm.unitMeasure = UnitMeasure.NO;

        // intensity
        pm.intensity = intensitySlider.value;
        if (singlePulseToggle.isOn) pm.pulse = Pulse.SINGLE;
        else if (rtmsHfToggle.isOn) pm.pulse = Pulse.HIGH;
        else if (rtmsLfToggle.isOn) pm.pulse = Pulse.LOW;
        else pm.pulse = Pulse.NO;
    }
}
