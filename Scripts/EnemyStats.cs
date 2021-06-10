using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Class representing element of HUD showing enemy's statistics
*/
public class EnemyStats : MonoBehaviour
{
    /** @brief Sprite of zombie     */
    public Sprite zombieImage;
    /** @brief Sprite of worm     */
    public Sprite creatureImage;
    /** @brief Image of enemy. Place to put sprites of specific creature.   */
    public Image enemyImage;
    /** @brief Slider representing enemy's health.     */
    public Slider enemyHPSlider;
    /** @brief Range of scanning enemies. */
    private float range =100.0f;

    /**
    * @brief Start is called before the first frame update. Disable display of stats. Attach components.
    */
    void Start()
    {
        enemyImage=enemyImage.GetComponent<Image>();
        enemyHPSlider=enemyHPSlider.GetComponent<Slider>();

        enemyImage.enabled=false;
        enemyHPSlider.gameObject.SetActive(false);
    }

    /**
    * @brief Update is called once per frame.
    */
    void Update()
    {
        show();
    }

    /**
    * @brief If player is looking on the enemy, show its stats.
    */
    private void show()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

		RaycastHit hitInfo;
			
		if(Physics.Raycast(ray, out hitInfo, range))
		{

			GameObject enemy = hitInfo.collider.gameObject;

			setHealth(enemy);
            setImage(enemy);
			
		}

    }

    /**
    * @brief Set value of hp slider equal to enemy health.
    * @param enemy enemy which player is looking at.
    */
    private void setHealth(GameObject enemy)
    {
        Health health = enemy.GetComponent<Health>();

        if(health!=null)
        {
        enemyHPSlider.value=health.hp/health.maxHP;

        enemyHPSlider.gameObject.SetActive(true);
        }else
        {
            enemyHPSlider.gameObject.SetActive(false);
        }       
    }

    /**
    * @brief Set enemy's image dependings on kind of enemy.
    * @param enemy enemy which player is looking at.
    */
    private void setImage(GameObject enemy)
    {
        ZombieAudio zombie = enemy.GetComponent<ZombieAudio>();

        CreatureAudio worm = enemy.GetComponent<CreatureAudio>();

        if(zombie!=null)
        {
        
            enemyImage.overrideSprite = zombieImage;
            enemyImage.enabled=true;
        }else if(worm!=null)
        {
            enemyImage.overrideSprite = creatureImage;
            enemyImage.enabled=true;
        }else
        {
            enemyImage.enabled=false;
        }       
    }
}
