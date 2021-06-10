using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief Class representing spawner
*/
public class SpawnObject : MonoBehaviour
{
    /** @brief Spawning range    */
    public int range=80;
    /** @brief Zombie object to spawn     */
    public GameObject zombie;
    /** @brief Worm object to spawn     */
    public GameObject worm;
    /** @brief Ammobox object to spawn     */
    public GameObject ammo;
    /** @brief First aid kit object to spawn     */
    public GameObject firstAid;
    /** @brief Auxiliary object    */
    private GameObject gameObject;
    /** @brief Position of first spawner   */
    private Transform spawner1;
    /** @brief Position of second spawner   */
    private Transform spawner2;
    /** @brief Amount zombies to spawn     */
    public int zombieCount=20;
    /** @brief Amount worms to spawn     */
    public int wormCount=20;
    /** @brief Amount ammoboxes to spawn     */
    public int ammoCount=5;
    /** @brief Amount first aid kits to spawn     */
    public int firstAidCount=5;
    /** @brief Informs if spawners should work    */
    public bool stop=true;
    /** @brief Auxiliary object to set spawn point     */
    private RaycastHit hit;

  

    /**
    * @brief Start is called before the first frame update.Set spawners positions.
    */
    void Start()
    {
        GameObject go = GameObject.FindWithTag ("Spawner1");
		spawner1 = go.transform;
        go=GameObject.FindWithTag ("Spawner2");
        spawner2= go.transform;
    
    }
    /**
    * @brief Update is called once per frame. 
    */
    void Update()
    {
        if(!stop)
        {
            if(zombieCount>0)
            {
                zombieCount--;
                gameObject=zombie;
                Spawn();
            }
            if(wormCount>0)
            {
                wormCount--;
                gameObject=worm;
                Spawn();
            }

            if(ammoCount>0)
            {
                ammoCount--;
                gameObject=ammo;
                Spawn();
            }

            if(firstAidCount>0)
            {
                firstAidCount--;
                gameObject=firstAid;
                Spawn();
            }
        }


    }

    /**
    * @brief Used for spawning objext. Set spawn point and instantiate object
    */
    void Spawn()
    {
        if (Random.Range(0, 2) == 1)
        {
            Vector3 pos1 = new Vector3((Random.Range(spawner1.position.x - range, spawner1.position.x + range)), spawner1.position.y, (Random.Range(spawner1.position.z - range, spawner1.position.z + range)));
            Physics.Raycast (pos1 + new Vector3(0, 100.0f, 0), Vector3.down, out hit, 200.0f);
            Instantiate(gameObject, hit.point, Quaternion.identity);
        }
        else
        {
            Vector3 pos2 = new Vector3((Random.Range(spawner2.position.x - range, spawner2.position.x + range)), spawner2.position.y, (Random.Range(spawner2.position.z - range, spawner2.position.z + range)));
            Physics.Raycast (pos2 + new Vector3(0, 100.0f, 0), Vector3.down, out hit, 200.0f);
            Instantiate(gameObject, hit.point, Quaternion.identity);
        }
    }

    /**
    * @brief Setter of zombieCount variable
    * @param cnt New amount of zombies to spawn
    */
    public void setZombieCount(float cnt)
    {
        zombieCount=(int)cnt;
    }

    /**
    * @brief Setter of wormCount variable
    * @param cnt New amount of worms to spawn
    */
    public void setWormCount(float cnt)
    {
        wormCount=(int)cnt;
    }

    /**
    * @brief Setter of ammoCount variable
    * @param cnt New amount of ammoboxes to spawn
    */
    public void setAmmoCount(float cnt)
    {
        ammoCount=(int)cnt;
    }

    /**
    * @brief Setter of firstAidCount variable
    * @param cnt New amount of dirst aid kits to spawn
    */
    public void setFirstAidCount(float cnt)
    {
        firstAidCount=(int)cnt;
    }

    
}
