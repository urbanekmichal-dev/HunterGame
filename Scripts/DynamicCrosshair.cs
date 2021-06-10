using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief Class representing dynamic crosshair displayed on the HUD.
*/
public class DynamicCrosshair : MonoBehaviour
{

    /** @brief Actual spread of crosshair   */
    public static float spread=0.0f;
    /** @brief Crosshair's spread during stand shooting     */
    public const int STAND_SHOOT_SPREAD=18;
    /** @brief Crosshair's spread during squat shooting     */
    public const int SQUAT_SHOOT_SPREAD=12;
    /** @brief Crosshair's spread during walk  */
    public const int WALK_SPREAD=10;
    /** @brief Crosshair's spread during jumping     */
    public const int JUMP_SPREAD=24;
    /** @brief Image representing top part of crosshair    */
    public GameObject topPart;
    /** @brief Image representing bop part of crosshair       */
    public GameObject botPart;
    /** @brief Image representing left part of crosshair       */
    public GameObject leftPart;
    /** @brief Image representing right part of crosshair       */
    public GameObject rightPart;
    /** @brief Initial position of crosshair      */
    private float initialPosition;

    /**
    * @brief Start is called before the first frame update. Set initial possition of crosshair
    */
    void Start()
    {
        initialPosition=topPart.GetComponent<RectTransform>().localPosition.y;
    }

    /**
    * @brief Update is called once per frame. Set new spread of crosshair.
    */
    void Update()
    {
        if(spread!=0)
        {
            topPart.GetComponent<RectTransform>().localPosition=new Vector3(0,initialPosition+spread,0);   
            botPart.GetComponent<RectTransform>().localPosition=new Vector3(0,-(initialPosition+spread),0);  
            leftPart.GetComponent<RectTransform>().localPosition=new Vector3(-(initialPosition+spread),0,0);  
            rightPart.GetComponent<RectTransform>().localPosition=new Vector3(initialPosition+spread,0,0);  

            spread-=1;
        }
    }
}
