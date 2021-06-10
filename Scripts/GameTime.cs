using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/**
*@brief Class responsible for counting time in single game.
*/
public class GameTime : MonoBehaviour
{
    /** @brief Seconds left to end of game.      */
    public float secondsLeft;
    /** @brief Audio source with clock ticking sound file.     */
    public AudioSource audio;
    /** @brief Text displaying left seconds.     */
    public Text timeText;
    /** @brief Gradnient used for paint the text.       */
    public Gradient gradient;
    /** @brief Format used for displaying time.     */
    string timeFormat = "{0:00}:{1:00}";
    /** @brief Variable used for control gradient.     */
    private float gradientValue=1;
    /** @brief Change of gradient.     */
    private float change=-0.05f;
    /** @brief Informs if game time is over.      */
    private bool timeOver=false;

    /**
    * @brief Update is called once per frame. Reduce left seconds. Update displaying time. Finish game if time is over.
    */
    void Update()
    {
        secondsLeft-=Time.deltaTime;
        int minutes = TimeSpan.FromSeconds(secondsLeft).Minutes;
        int seconds = TimeSpan.FromSeconds(secondsLeft).Seconds;
        timeText.text="Time  "+string.Format(timeFormat, minutes, seconds);

        if(secondsLeft<=60.0f)
        {
            timeText.color=gradient.Evaluate(gradientValue);

            gradientValue+=change;
            if(gradientValue<=0 || gradientValue>=1)
                change*=-1;
 
        }else{
            timeText.color=gradient.Evaluate(1);
        }
        if(secondsLeft<=10 && !audio.isPlaying)
        {
            audio.Play();
        }
        if(secondsLeft<=0)
            finish();

    }

    /**
    * @brief Pause time and inform that time is over.
    */
    void finish()
    {   
        audio.Pause();
        timeOver=true;
    }

    /**
    * @brief Getter of timeOver variable.
    * @return information if time is over
    */
    public bool gettimeOver()
    {
        return timeOver;
    }

    /**
    * @brief Setter of secondsLeft variable.
    * @param seconds new amount of left seconds
    */
    public void setSecondsLeft(float seconds)
    {
        secondsLeft=seconds;
        timeOver=false;
    }




}
