using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Class used for counting points
*/
public class Points : MonoBehaviour
{
    /** @brief Points gained by player     */
    public static int points;
    /** @brief Text with points displayed on HUD    */
    public Text pointsText;
    
    /**
    * @brief Start is called before the first frame update. Reset points.
    */
    void Start()
    {
        points=0;
    }

    /**
    * @brief Update is called once per frame. Update text with points.
    */
    void Update()
    {
        pointsText.text="Points:   "+points;
    }
    /**
    * @brief Getter of points variable
    * @return actual points
    */
    public int getPoints()
    {
        return points;
    }

}
