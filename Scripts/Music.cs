using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief Class used for controlling  music in game.
*/
public class Music : MonoBehaviour
{
    /** @brief  Audio source with game music file   */
    public AudioSource gameMusic;
    /** @brief Audio source with menu music file     */
    public AudioSource menuMusic;
    /** @brief  Obejct with menu controller    */
    public MainMenuController menuController; 

    /** @brief Information if game music is playing     */
    private bool gameMusicPlaying;
    /** @brief Information if menu music is playing      */
    private bool menuMusicPlaying=false;

   /**
    * @brief Start is called before the first frame update.
    */
    void Start()
    {
        menuMusic.Play();
    }

   /**
    * @brief Update is called once per frame. Control play of the music. 
    */
    void Update()
    {
        if(menuController.isPaused())
        {
            gameMusic.Pause();
            gameMusicPlaying=false;
        }else if(!gameMusicPlaying)
        {
            gameMusic.Play();
            gameMusicPlaying=true;

        }

        if(!menuController.mainMenu.enabled && !menuController.optionsMenu.enabled && !menuController.pauseMenu.enabled ! && !menuController.gameOver.enabled)
        {
            menuMusic.Pause();
            menuMusicPlaying=false;
        }else if(!menuMusicPlaying)
        {
            menuMusic.Play();
            menuMusicPlaying=true;

        }

        
    }
}
