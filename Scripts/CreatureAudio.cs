using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief Class cointaining audio variables for worm
*/
public class CreatureAudio : MonoBehaviour
{
    /** @brief Steps sound file    */
    public AudioClip stepsSound;
    /** @brief Audio source with dead sound file    */
    public AudioSource deadSound;
    /** @brief Array of breathing sounds files    */
    public AudioClip [] breathingSounds;
    /** @brief Audio source with steps sound file      */
    public AudioSource sounds;
    /** @brief Audio source with hurt sound file      */
    public AudioSource hurtSound;
    
    /**
    * @brief Start is called before the first frame update.
    */
    void Start()
    {
        
    }

    /**
    * @brief Update is called once per frame.
    */
    void Update()
    {
        
    }
}
