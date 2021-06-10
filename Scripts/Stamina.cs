using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Class used for controlling  player' stamina
*/
public class Stamina : MonoBehaviour
{
    /** @brief Stamina slider displayed on HUD     */
    public Slider staminaBar;
    /** @brief  Script used for control player's movement    */
    public PlayerControler playerControler;
    /** @brief Informs if payer is exhausted      */
    private bool exhausted;
    /** @brief Informs if payer is resting      */
    private bool resting;

    /**
    * @brief Start is called before the first frame update.
    */
    void Start()
    {
        staminaBar.value=1.0f;
        exhausted=false;        
    }

    /**
    * @brief Update is called once per frame. Update stamina level
    */
    void Update()
    {
        if(playerControler.getIsRunning() && playerControler.isMoving())
        {
            staminaBar.value-=Time.deltaTime/10;
        }else
        {
            staminaBar.value+=Time.deltaTime/10;
        }

        if(playerControler.characterControler.isGrounded && Input.GetButton("Jump") && !resting)
        {
            if(staminaBar.value>=0.1f)
                staminaBar.value-=0.15f;
            else
                staminaBar.value=0.0f;
        }

        if(staminaBar.value<0.01f || resting)
            exhausted=true;
        else
            exhausted=false;
    
        resting=isResting();
    }

    /**
    * @brief Check if player is resting
    * @return Information if player is resting
    */
    public bool isResting()
    {
        if(exhausted && staminaBar.value<0.3)
            return true;
        else if(exhausted && staminaBar.value>=0.3)
        {
            exhausted = false;
            return false;
        }

        return false;
    }
}
