using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Trigger atteched to first aid kit object
*/
public class FirstAidTrigger : MonoBehaviour
{
    /** @brief Communicate displayed on the scrren      */
    private  Image communicate;
    /** @brief Text displayed on the communicate      */
    private Text text;
    /** @brief  Object of first aid kit which trigger is attached to      */
    public FirstAidKit firstAid;
    /** @brief Audio source with "pick up" sound file     */
    public AudioSource audioSource;
    /** @brief Healing skill attached to player     */
    public Heal heal;


    /**
    * @brief Start is called before the first frame update. Disable communicate. Attach components.
    */
    void Start()
    {
        heal=GameObject.Find("Player").GetComponent<Heal>();
        communicate=GameObject.Find("FirstAidCom").GetComponent<Image>();
        text=GameObject.Find("FirstAidComText").GetComponent<Text>();
        communicate.enabled=false;
        text.text="";     
    }

    /**
    * @brief OnTriggerEnter is called while some object enter trigger. Enable communicate.
    * @param other collider attached to some game object
    */
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && !firstAid.getIsEmpty())
        {
            text.text="Press [E] to pick up first aid kit";
            communicate.enabled=true;
        }
    }

    /**
    * @brief OnTriggerStay is calling while some object stay in trigger. If player press "E", it pick up first aid kit.
    * @param other collider attached to some game object
    */
    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) && !firstAid.getIsEmpty() && other.tag=="Player")
        {
            audioSource.Play();
            heal.incrementAids();
            communicate.enabled=false;
            firstAid.setEmpty();
            text.text="";
            firstAid.removeElements();   
        }
    }

    /**
    * @brief OnTriggerEnter is called while some object is exit trigger. Disable communicate.
    * @param other collider attached to some game object
    */
    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            communicate.enabled=false;
            text.text="";
        }
    }
}
