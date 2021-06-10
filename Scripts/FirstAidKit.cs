using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Class representing object of first aid kit
*/
public class FirstAidKit : MonoBehaviour
{
    /** @brief Game object represeting real object of first aid.   */
    public GameObject firstAidKit;
    /** @brief Game object representing first aid's icon on minimap     */
    public GameObject icon;
    /** @brief Information if kit is empty   */
    public bool isEmpty=false;
    /** @brief Variable used to delete the object     */
    public static bool order66=false;

    /**
    * @brief Update is called once per frame. Checks if object should be destroyed .
    */
    void Update()
    {
        if(order66)
        {
            Destroy(gameObject);
        }
    }

    /**
    * @brief Removes kit from map
    */
    public void removeElements()
    {
        Destroy(gameObject);
    }

    /**
    * @brief Removes kit from map
    */
    public bool getIsEmpty()
    {
        return isEmpty;
    }

    /**
    * @brief Empty the kit.
    */
    public void setEmpty()
    {
        isEmpty=true;
    }
}
