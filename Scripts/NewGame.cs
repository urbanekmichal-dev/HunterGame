using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Class used for starting new game
*/
public class NewGame : MonoBehaviour
{
    /** @brief Object of player     */
    public GameObject player;
    /** @brief Stamina slider     */
    public Slider staminaBar;
    /** @brief Script with game time     */
    public GameTime gameTime;
    /** @brief Script to control options     */
    public OptionsController options;
    /** @brief  Object of spawn    */
    public SpawnObject spawn;

   /**
    * @brief Destroy all enemies, ammoboxes and first aid kits, get new game parameters, start spawning new objects.
    * Reset player's position and stats, time and points.
    */
    public void newGame ()
    {
        THC6_ctrl.order66=false;
		LepszeCCAI.order66=false;
		AmmoBox.order66=false;
		FirstAidKit.order66=false;


        //pobranie wartości z opcji
        gameTime.setSecondsLeft(options.timeSlider.value); 
        spawn.setZombieCount(options.zombieSlider.value);
        spawn.setWormCount(options.wormSlider.value);
        spawn.setAmmoCount(options.ammoSlider.value);
        spawn.setFirstAidCount(options.firstAidSlider.value);

        spawn.stop=false;


        PlayerHealth zdrowie = player.GetComponent<PlayerHealth>();
        PlayerControler controller = player.GetComponent<PlayerControler>();
        Strzal strzal = player.GetComponent<Strzal>();
        CharacterController characterControler = player.GetComponent<CharacterController>();

        zdrowie.setHP(200.0f);
        staminaBar.value=1.0f;
        strzal.setAmmo(30,120);
        Points.points=0;

        Vector3 move = new Vector3(1647.81f-player.transform.position.x, 155.63f-player.transform.position.y, 255.49f-player.transform.position.z);
		characterControler.Move(move);
  
        
    }
}
