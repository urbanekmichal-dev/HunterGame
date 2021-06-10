using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief Class used for controlling player's sounds
*/
public class PlayerSounds : MonoBehaviour
{
	/** @brief Character controller attached to player object     */
	public CharacterController characterControler;
	/** @brief Object with script used for control player's atmina    */
    public Stamina stamina;

    /** @brief Audio source used for playing moving sounds    */
	public AudioSource audioSourceMoving;
    /** @brief Audio source used for playing breathing sounds    */
    public AudioSource audioSourceBreathing;
    /** @brief  Array of walking sounds files    */
    public AudioClip [] stepSounds=new AudioClip [10];
    /** @brief  Array of running sounds files    */
    public AudioClip [] runSounds=new AudioClip [10];
    /** @brief  Array of squat steps sounds files    */
    public AudioClip [] squatSounds=new AudioClip [7];
    /** @brief  Land sound file    */
	public AudioClip landingSound;
    /** @brief  Time to next step    */
	public float timeToStep = 0f;
    /** @brief  Duration time of one step    */
	public float stepDuration = 0.4f;

    /** @brief Time to next breath     */
	public float timeToBreath = 0f;
    /** @brief  Duration time of one breath    */
	public float breathDuration = 41.0f;
    /** @brief Informs if player is on the ground     */
	public bool playerOnGround;
	/** @brief Object with script used for control menu     */
    public MainMenuController menuController; 
    /** @brief  Script used for control player's movement    */
	private PlayerControler playerControler;
    /** @brief  Number of taken steps    */
    private int stepCounter=0;
    /** @brief  Number of taken steps while running    */
    private int runCounter=0;
    /** @brief  Number of taken steps while squatting    */
    private int squatCounter=0;

    /**
    * @brief Start is called before the first frame update.
    */
	void Start () {
		playerControler = GetComponent<PlayerControler> ();
	}
	
    /**
    * @brief Update is called once per frame. Play sounds depending on the movement.
    */
	void Update () {

		if (audioSourceMoving != null && audioSourceBreathing != null) 
        {//Jeżeli źródło dzwięku nie zostało podpięte to i tak nie ma co odgrywać.
			
            if (timeToStep > 0)
            {
                //Sprawdzenie, jeżeli gracz biegnie to dźwięk kroku będzie odtwarzany szybciej.
                if (playerControler.getIsRunning() && playerControler.isMoving()) 
                {
                    timeToStep -= Time.deltaTime * 1.3f;
                }else if (playerControler.getIsSquatting() && playerControler.isMoving()) 
                {
                    timeToStep -= Time.deltaTime * 0.7f;
                }
                 else {
                    timeToStep -= Time.deltaTime;
                }
            }


            //Chodzenie
            if(playerControler.isMoving() && characterControler.isGrounded && timeToStep <= 0)
            {
                timeToStep = stepDuration;
                

                if (!playerControler.getIsRunning() && !playerControler.getIsSquatting()) 
                {
                    audioSourceMoving.PlayOneShot(stepSounds[stepCounter]);
                    stepCounter++;
                    if(stepCounter==9)
                        stepCounter=0;                
                }

                if(playerControler.getIsRunning())
                { 
                    audioSourceMoving.PlayOneShot (runSounds[runCounter]);
                    runCounter++;
                    if(runCounter==9)
                        runCounter=0;  

                }

                if(playerControler.getIsSquatting())
                { 
                    audioSourceMoving.PlayOneShot (squatSounds[squatCounter]);
                    squatCounter++;
                    if(squatCounter==6)
                        squatCounter=0;  

                }

            }


            //Ladowanie
            if(!playerOnGround && characterControler.isGrounded) {				
                audioSourceMoving.PlayOneShot (landingSound);				
            }

            playerOnGround = characterControler.isGrounded;

            if(timeToBreath>0)
            {
                timeToBreath-=Time.deltaTime;
            }


            if(stamina.staminaBar.value<1 && timeToBreath<=0)
            {
                timeToBreath=breathDuration;
                audioSourceBreathing.Play();
            }

            if(stamina.staminaBar.value>=1)
            {
                audioSourceBreathing.Pause();
                timeToBreath=0;
            }

        }

        if(menuController.isPaused())
        {
            audioSourceBreathing.Pause();
            timeToBreath=0;
        }

	}

}
