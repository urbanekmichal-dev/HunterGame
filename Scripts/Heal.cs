using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Class representing healing skill.
*/
public class Heal : MonoBehaviour
{
    /** @brief Object with player health.    */
    public PlayerHealth playerHP;
    /** @brief Amount of available first aid kits.     */
	public int firstAidCounter=2;
    /** @brief Audio source with healing sound file.    */
    public AudioSource healingSound;
    /** @brief Information whether the player is healing.     */
    public bool isHealing;
    /** @brief HUD text with amount of available first aid kits     */
    public Text firstAidText;

    /**
    * @brief Start is called before the first frame update.
    */
    void Start()
    {
        firstAidText.text="2";
        isHealing=false;
    }

    /**
    * @brief Update is called once per frame. Checks if player want to heal itself.
    */
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && firstAidCounter>0)
        {
            StartCoroutine(useFirstAid());
        }
        firstAidText.text=""+firstAidCounter;
    }

    /**
    * @brief Increment amount of first aid kits.
    */
    public void incrementAids()
	{
		firstAidCounter++;
	}

    /**
    * @brief Coroutine. Disable moving and shooting. Play heal sound and increade player's hp.
    */
	IEnumerator  useFirstAid()
	{
        isHealing=true;
		firstAidCounter--;
        healingSound.Play();
        yield return new WaitForSeconds(4);
        if(playerHP.getHP()<=100)
		    playerHP.setHP(playerHP.getHP()+100.0f);
        else
        {
            playerHP.setHP(200.0f);
        }
        
         isHealing=false;
	}

    /**
    * @brief Getter of isHealing variable
    * @return information if player is healing
    */
    public bool getIsHealing()
    {
        return isHealing;
    }
}
