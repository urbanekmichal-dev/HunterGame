using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Trigger atteched to ammobox object
*/
public class AmmoTrigger : MonoBehaviour
{
    /** @brief Communicate displayed on the scrren    */
    private  Image communicate;
    /** @brief Text displayed on the communicate    */
    private Text text;
    /** @brief  Object of ammobox which trigger is attached to    */
    public AmmoBox ammoBox;
    /** @brief Audio source with "pick up" sound file */
    public AudioSource audioSource;


    /**
    * @brief Start is called before the first frame update. Disable communicate. Attach components.
    */
    void Start()
    {
        communicate=GameObject.Find("AmmoCom").GetComponent<Image>();
        text=GameObject.Find("AmmoComText").GetComponent<Text>();
        communicate.enabled=false;
        text.text="";     
    }

    /**
    * @brief OnTriggerEnter is called while some object enter trigger. Enable communicate.
    * @param other collider attached to some game object
    */
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && ammoBox.getAmmo()!=0)
        {
            text.text="Press [E] to pick up "+ammoBox.getAmmo()+" ammo";
            communicate.enabled=true;
        }
    }

    /**
    * @brief OnTriggerStay is calling while some object stay in trigger. If player press "E", it pick up ammo.
    * @param other collider attached to some game object
    */
    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) && ammoBox.getAmmo()!=0 && other.tag=="Player")
        {
            Strzal.totalAmmo+=ammoBox.getAmmo();
            ammoBox.removeElements();
            audioSource.Play();
            communicate.enabled=false;
            text.text="";   
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
