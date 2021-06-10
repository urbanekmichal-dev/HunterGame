using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief Class representing object of ammobox
*/
public class HashIDs : MonoBehaviour
{
    [HideInInspector]
    /** @brief      */
    public int speed;
    [HideInInspector]
    /** @brief      */
    public int iSDead;
    /** @brief      */
    public int isAttack;
    /** @brief      */
    public int isDeadBack;
    
	/**
    * @brief Use this for initialization.
    */
    void Awake()
    {
        speed = Animator.StringToHash("Speed");
        iSDead = Animator.StringToHash("IsDead");
        isAttack=Animator.StringToHash("IsAttack");
        isDeadBack=Animator.StringToHash("IsDeadBack");
    }
}
