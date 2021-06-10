using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Class representing player's health
*/
public class PlayerHealth : MonoBehaviour
{
	/** @brief  Max players' hp    */
	public float maxHP=200.0f;
	/** @brief  Current player's hp    */
	public float hp = 200.0f;
	/** @brief Slider showing player's hp     */
    public Slider HPBar;
	/** @brief Audio source with pain sound file     */
    public AudioSource audioPain;
	/** @brief Information of player is dead.    */
	private bool isDead=true;

    /**
    * @brief Update is called once per frame. Update value of HPBar.
    */
	public void Update()
	{
		HPBar.value=hp/maxHP;

	}

	/**
	*@brief Decrease hp
	*@param obrazenia HP to decrease
	*/
	public void damage(float obrazenia) {
		//Odięcie od zdrowia punktów zadanych obrażeń.
		hp -= obrazenia;
        audioPain.Play();
		//Jeżeli zdrowie równe zero to obiekt do usunięcia.
		if(hp <=0 && isDead){
			isDead=false;
		}
	}
	
	/**
	*@brief Check if enemy is dead.
	*@return Information if creature is dead
	*/
	public bool checkIfDead(){
		if (hp <= 0) {
			return true;
		}
		return false;
	}

	/**
	*@brief Setter of hp variable.
	*@param _hp new current hp.
	*/
	public void setHP(float _hp)
	{
		hp=_hp;
	}

	/**
	*@brief Getter of hp variable.
	*@return Current hp
	*/
	public float getHP()
	{
		return hp;
	}
}
