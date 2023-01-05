using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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
    private Sprite forwardSprite;

    private AudioSource audioSource;
    private float audioTimer;

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
        forwardSprite = GameObject.Find("forward-button").GetComponent<Sprite>();
        backButton = GameObject.Find("back-button").GetComponent<Button>();
        minText = GameObject.Find("min-text").GetComponent<Text>();
        maxText = GameObject.Find("max-text").GetComponent<Text>();
        currentText = GameObject.Find("current-text").GetComponent<Text>();
        audioSource = GameObject.Find("sound-effects").GetComponent<AudioSource>();
        audioTimer = audioSource.clip.length;

        subtitle.text = "Configure the " + pm.medicalEquipment.name + " you have selected";
        medicalEquipmentRecap.sprite = Resources.Load("Sprites/medical-eq-recap-" + pm.medicalEquipment.name, typeof(Sprite)) as Sprite;

        if(pm.unitMeasure != UnitMeasure.NO) {
            intensityToggle.isOn = true;
            intensityToggle.interactable = true;
            intensitySlider.interactable = true;
            ampereToggle.interactable = true;
            mtToggle.interactable = true;

            switch(pm.unitMeasure){
                case UnitMeasure.MILLIAMPERE:
                    ampereToggle.isOn = true;
                    intensitySlider.minValue = Tdcs.min;
                    intensitySlider.maxValue = Tdcs.max;
                    intensitySlider.value = (float)Math.Round(pm.intensity, 1);
                    break;
                case UnitMeasure.PERCENTAGE_OF_MT:
                    mtToggle.isOn = true;
                    intensitySlider.minValue = Tms.min;
                    intensitySlider.maxValue = Tms.max;
                    intensitySlider.value = (float)Math.Round(pm.intensity);
                    break;
            }
            
            minText.text = intensitySlider.minValue.ToString();
            maxText.text = intensitySlider.maxValue.ToString();
            currentText.text = intensitySlider.value.ToString();
            intensityRectangle.sprite = Resources.Load("Sprites/intensity-rectangle-on", typeof(Sprite)) as Sprite;
        }

        if(pm.pulse != Pulse.NO) {
            pulseToggle.isOn = true;
            pulseToggle.interactable = true;
            switch (pm.pulse)
            {
                case Pulse.LOW:
                    rtmsLfToggle.isOn = true;
                    break;

                case Pulse.HIGH:
                    rtmsHfToggle.isOn = true;
                    break;

                case Pulse.SINGLE:
                    singlePulseToggle.isOn = true;
                    break;
 
            }
            singlePulseToggle.interactable = true;
            rtmsHfToggle.interactable = true;
            rtmsLfToggle.interactable = true;

            pulseRectangle.sprite = Resources.Load("Sprites/pulse-rectangle-on", typeof(Sprite)) as Sprite;
        }


        intensityToggle.onValueChanged.AddListener((isChecked) => {
            audioSource.Play();
            forwardButton.enabled = isChecked;
            forwardButton.GetComponent<Image>().sprite = Resources.Load("Sprites/play-button-" + (isChecked ? "on" : "off"), typeof(Sprite)) as Sprite;
            Debug.Log("intensityToggle" + isChecked.ToString());
            intensityRectangle.sprite = Resources.Load("Sprites/intensity-rectangle-" + (isChecked ? "on" : "off"), typeof(Sprite)) as Sprite;
            if(!isChecked){
                mtToggle.isOn = false;
                ampereToggle.isOn = false;
            }
            mtToggle.interactable = isChecked;
            ampereToggle.interactable = isChecked;
            
            // Debug.Log(intensitySlider.minValue);
        });

        pulseToggle.onValueChanged.AddListener((isChecked) => {
            audioSource.Play();
            forwardButton.enabled = isChecked;
            forwardButton.GetComponent<Image>().sprite = Resources.Load("Sprites/play-button-" + (isChecked ? "on" : "off"), typeof(Sprite)) as Sprite;
            Debug.Log("pulseToggle" + isChecked.ToString());
            pulseRectangle.sprite = Resources.Load("Sprites/pulse-rectangle-" + (isChecked ? "on" : "off"), typeof(Sprite)) as Sprite;
            singlePulseToggle.isOn = false;
            rtmsHfToggle.isOn = false;
            rtmsLfToggle.isOn = false;
            singlePulseToggle.interactable = isChecked;
            rtmsHfToggle.interactable = isChecked;
            rtmsLfToggle.interactable = isChecked;
        });

        mtToggle.onValueChanged.AddListener((isChecked) => {           
            audioSource.Play();
            Debug.Log("mtToggle" + isChecked.ToString());
            intensitySlider.interactable = isChecked;
            intensitySlider.minValue = isChecked ? Tms.min : Tdcs.min;
            intensitySlider.maxValue = isChecked ? Tms.max : Tdcs.max;
            intensitySlider.value = isChecked ? Tms.min : Tdcs.min;

            if (isChecked)
            {
                minText.text = intensitySlider.minValue.ToString();
                maxText.text = intensitySlider.maxValue.ToString();
                currentText.text = intensitySlider.value.ToString();
            }
            else
            {
                minText.text = "";
                maxText.text = "";
                currentText.text = "";
            }
        });

        ampereToggle.onValueChanged.AddListener((isChecked) => {
            audioSource.Play();
            intensitySlider.interactable = isChecked;
            intensitySlider.minValue = isChecked ? Tdcs.min : Tms.min;
            intensitySlider.maxValue = isChecked ? Tdcs.max : Tms.max;
            intensitySlider.value = isChecked ? Tdcs.min : Tms.min;
             if (isChecked)
            {
                minText.text = intensitySlider.minValue.ToString();
                maxText.text = intensitySlider.maxValue.ToString();
                currentText.text = intensitySlider.value.ToString();
            }
            else
            {
                minText.text = "";
                maxText.text = "";
                currentText.text = "";
            }
        });

        intensitySlider.onValueChanged.AddListener((value) => {
            currentText.text = value.ToString(ampereToggle.isOn ? "f1" : "f0");
        });

        rtmsHfToggle.onValueChanged.AddListener((value) => {
            audioSource.Play();
        });

        rtmsLfToggle.onValueChanged.AddListener((value) => {
            audioSource.Play();
        });

        singlePulseToggle.onValueChanged.AddListener((value) => {
            audioSource.Play();
        });

        forwardButton.onClick.AddListener(() => {
            audioSource.Play();
            saveConfig();
            StartCoroutine(LoadLevelTimer(audioTimer, Constants.GAME_3));
        });

        backButton.onClick.AddListener(() => {
            pm.unitMeasure = UnitMeasure.NO;
            pm.pulse = Pulse.NO;
            pm.intensity = 0;
            SceneManager.LoadScene(Constants.GAME_1, LoadSceneMode.Single);
        });

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

    // Update is called once per frame
    void Update()
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

    IEnumerator LoadLevelTimer(float audioTimer, string name)
    {
        yield return new WaitForSeconds(audioTimer);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
