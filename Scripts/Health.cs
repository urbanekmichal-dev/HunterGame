using UnityEngine;
using System.Collections;



/**
*@brief Class representing creature health.
*/
public class Health : MonoBehaviour 
{
	/** @brief Max hp of creature.     */
	public float maxHP=100.0f;
	/** @brief Actual hp.     */
	public float hp = 100.0f;
	/** @brief Information whether creature is dead.    */
	private bool isDead=true;
	
	/**
	*@brief Decrease hp
	*@param obrazenia HP to decrease
	*/
	public void damage(float obrazenia) {
		//Odięcie od zdrowia punktów zadanych obrażeń.
		hp -= obrazenia;
		//Jeżeli zdrowie równe zero to obiekt do usunięcia.
		if(hp <=0 && isDead){
			isDead=false;
			Points.points+=50;
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

}
