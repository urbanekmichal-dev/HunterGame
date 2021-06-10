using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief Class used to control player's stamina
*/
public class AmmoBox : MonoBehaviour
{
    /** @brief Ammount of ammuniton in one box */
    private int ammo;     
    /** @brief Game object representing bullets on the box */              
    public GameObject bullets;   
    /** @brief Game object representing ammo box's icon on minimap */       
    public GameObject icon;             
    /** @brief Variable used to delete the object */
    public static bool order66=false;    

    /**
    * @brief Start is called before the first frame update. Draws amount of ammunition in ammobox.
    */
    void Start()
    {
         ammo=Random.Range(25,70);
    }

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
    * @brief getter of variable ammo
    * @return ammount of ammo in box. 
    */
    public int getAmmo()
    {
        return ammo;
    }

   /**
    * @brief removing bullets from the box and icon from minimap. Set ammout of ammo to 0.
    */
    public void removeElements()
    {
        bullets.gameObject.SetActive(false);
        ammo=0;
        icon.gameObject.SetActive(false);
    }
}
