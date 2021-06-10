using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief Class cointaining audio variables for zombie
*/
public class ZombieAudio : MonoBehaviour
{
    /** @brief Audio source with roar sound file      */
    public AudioSource roarSound;
    /** @brief Steps sound file    */
    public AudioSource footstepSound;
    /** @brief Audio source with dead sound file    */
    public AudioSource deadSound;
    /** @brief Array of breathing sounds files    */
    public AudioClip [] breathingSounds;
    /** @brief  Audio source with breath sound file      */
    public AudioSource breathingSound;

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
