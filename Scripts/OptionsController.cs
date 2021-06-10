using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/**
*@brief Class representing object of ammobox
*/
public class OptionsController : MonoBehaviour
{
    /** @brief Canvas with options menu    */
    public Canvas optionsMenu;
    /** @brief  Canvas with main menu     */
    public Canvas mainMenu;
    /** @brief Canvas with pause menu      */
    public Canvas pauseMenu;
    /** @brief Slider setting music volume     */
    public Slider musicVolumeSlider;
    /** @brief  Slider setting effects volume     */
    public Slider fxVolumeSlider;
    /** @brief Slider setting game time      */
    public Slider timeSlider;
    /** @brief Slider setting amount of zombies  */
    public Slider zombieSlider;
    /** @brief Slider setting amount of worms      */
    public Slider wormSlider;
    /** @brief Slider setting amount of ammoboxes      */
    public Slider ammoSlider;
    /** @brief Slider setting amount of first aid kits      */
    public Slider firstAidSlider;
    /** @brief Slider in pause menu setting music volume     */
    public Slider pauseMusicVolumeSlider;
    /** @brief Slider in pause menu setting effects volume       */
    public Slider pauseFXVolumeSlider;
    /** @brief Lebel in pause menu with  music volume value     */
    public Text pauseMusicVolumeText;
    /** @brief  Lebel in pause menu with  effects volume value     */
    public Text pauseFXVolumeText;
    /** @brief Lebel with  music volume value      */
    public Text musicVolumeText;
    /** @brief Lebel with effects volume value      */
    public Text fxVolumeText;
    /** @brief Lebel with game time      */
    public Text timeText;
    /** @brief Lebel with zombies amount     */
    public Text zombieText;
    /** @brief Lebel with worms amount     */
    public Text wormText;
    /** @brief Lebel with ammoboxes amount      */
    public Text ammoText;
    /** @brief Lebel with first aid kits amount      */
    public Text firstAidText;
    /** @brief Main audio mixer     */
    public AudioMixer mainMixer;

    /**
    * @brief Update is called once per frame. Enable or disable sliders. Update labels with values.
    */
    void Update()
    {
        pauseMusicVolumeText.text=(int)(pauseMusicVolumeSlider.value+40.0f)*5/3+"%";
        pauseFXVolumeText.text=(int)(pauseFXVolumeSlider.value+40.0f)*5/3+"%";

        musicVolumeText.text=(int)(musicVolumeSlider.value+40.0f)*5/3+"%";
        fxVolumeText.text=(int)(fxVolumeSlider.value+40.0f)*5/3+"%";
        timeText.text=timeSlider.value+"";
        zombieText.text=zombieSlider.value+"";
        wormText.text=wormSlider.value+"";
        ammoText.text=ammoSlider.value+"";
        firstAidText.text=firstAidSlider.value+"";

        if(optionsMenu.enabled && !musicVolumeSlider.interactable)
        {
            musicVolumeSlider.interactable=true;
            fxVolumeSlider.interactable=true;
            timeSlider.interactable=true;
            zombieSlider.interactable=true;
            wormSlider.interactable=true;
            ammoSlider.interactable=true;
            firstAidSlider.interactable=true;
            
        }
        if(!optionsMenu.enabled && musicVolumeSlider.interactable)
        {
            musicVolumeSlider.interactable=false;
            fxVolumeSlider.interactable=false;
            timeSlider.interactable=false;
            zombieSlider.interactable=false;
            wormSlider.interactable=false;
            ammoSlider.interactable=false;
            firstAidSlider.interactable=false;

        }

        if(pauseMenu.enabled && !pauseMusicVolumeSlider.interactable)
        {
            pauseFXVolumeSlider.interactable=true;
            pauseMusicVolumeSlider.interactable=true;
        }
        if(!pauseMenu.enabled && pauseMusicVolumeSlider.interactable)
        {
            pauseFXVolumeSlider.interactable=false;
            pauseMusicVolumeSlider.interactable=false;
        }
        
    }
    /**
    * @brief Close options menu, open main menu.
    */
    public void backButton_pressed()
    {
        optionsMenu.enabled=false;
        mainMenu.enabled=true;
    }

    /**
    * @brief Set music volume in mixer.
    */
    public void setMusicVolume(float vol)
    {
        mainMixer.SetFloat("musicVol",vol);
    }

    /**
    * @brief Set effects volume in mixer.
    */
    public void setEffectsVolume(float vol)
    {
        mainMixer.SetFloat("effectsVol",vol);
    }
}
