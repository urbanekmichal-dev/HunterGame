using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
*@brief Class used for controlling  shoting
*/
public class Strzal : MonoBehaviour {

    /** @brief  Script used for control player's movement    */
	public PlayerControler playerControler;  
	/** @brief Object with script used for control menu     */
	public MainMenuController menuController; 
	/** @brief  Text on HUD displaying amount of ammunition    */
	public Text textAmmo;
	/** @brief Shooting range     */
	public float range = 100.0f;
	/** @brief Shoot frequency     */
	public float shotFrequency = 0.1f;
	/** @brief Damage deal to enemies    */
	public float damage = 50.0f;
	/** @brief Muzzle flash animated after shoot    */
	public ParticleSystem muzzleFlash;
	/** @brief Impact effect animated after hit      */
	public GameObject impactEffect;
	/** @brief Audio source with shoot sound file    */
	public AudioSource shootSound;
	/** @brief Audio source with reload sound file     */
	public AudioSource reloadSound;
	/** @brief  Force transfered to the target    */
	public float impactForce=30f;
	/** @brief Time to next shot     */
	private float countdownShot = 0f;
	/** @brief  Ammount of total ammuniton    */
	public static int totalAmmo;
	/** @brief Ammount of ammunition in magazine     */
	private int magAmmo;
	/** @brief  Informs if gun is hiden    */
	private bool isHide=false;
	/** @brief Animator attached to the gun    */
	public Animator animator;
	/** @brief Object with attached healing skill    */
	public Heal heal;
	/** @brief Informs if player is reloading    */
	private bool isReloading=false;
	/** @brief Blood effect animated after hit       */
	public GameObject bloodEfect;

    /**
    * @brief Start is called before the first frame update. Reset amount of ammunition
    */
	void Start () {
		totalAmmo=120;
		magAmmo=30;
		textAmmo.text=magAmmo + "/" + totalAmmo;
	}
	
    /**
    * @brief Update is called once per frame.
    */
	void Update () {
		if (countdownShot <shotFrequency) 
			countdownShot += Time.deltaTime;
		
		if(Input.GetMouseButton(0) && countdownShot >= shotFrequency && !menuController.isPaused() && !isHide && magAmmo>0 && !isReloading && !heal.getIsHealing() )
		{
			countdownShot = 0;

			animator.CrossFadeInFixedTime("Fire",0.1f);
			
			muzzleFlash.Play();
			shootSound.Play();
			magAmmo-=1;

			if(playerControler.getIsSquatting())
			{
				DynamicCrosshair.spread+=DynamicCrosshair.SQUAT_SHOOT_SPREAD;
				if(DynamicCrosshair.spread>40)
					DynamicCrosshair.spread=40;

			}else if(playerControler.isMoving())
			{
				DynamicCrosshair.spread+=DynamicCrosshair.STAND_SHOOT_SPREAD;
				if(DynamicCrosshair.spread>70)
					DynamicCrosshair.spread=70;
			}else
			{
				DynamicCrosshair.spread+=DynamicCrosshair.STAND_SHOOT_SPREAD;
				if(DynamicCrosshair.spread>60)
					DynamicCrosshair.spread=60;
			}
			
			float randX = Random.Range(-DynamicCrosshair.spread/800.0f,DynamicCrosshair.spread/800.0f);
			float randY = Random.Range(-DynamicCrosshair.spread/800.0f,DynamicCrosshair.spread/800.0f);
			float randZ = Random.Range(-DynamicCrosshair.spread/800.0f,DynamicCrosshair.spread/800.0f);

			Vector3 randomDestination = new Vector3(randX,randY,randZ);

			Ray ray = new Ray(Camera.main.transform.position, randomDestination + Camera.main.transform.forward);

			RaycastHit hitInfo;
			
			if(Physics.Raycast(ray, out hitInfo, range))
			{
			
				Vector3 hitPoint = hitInfo.point;

				GameObject go = hitInfo.collider.gameObject;
				
				hit(go);

				if(hitInfo.rigidbody!=null)
				{
					hitInfo.rigidbody.AddForce(-hitInfo.normal*impactForce);
				}

				GameObject impactGO = Instantiate(impactEffect,hitInfo.point,Quaternion.LookRotation(hitInfo.normal));
				Destroy(impactGO,0.1f);

				if(hitInfo.transform.tag=="Zombie" || hitInfo.transform.tag=="Creature")
                {
					GameObject bEffect=Instantiate(bloodEfect,hitInfo.point,Quaternion.identity);
					Destroy(bEffect,0.6f);
                }

				
			}
		}
		

		if((magAmmo==0 || Input.GetKeyDown(KeyCode.R)) && totalAmmo>0 && !Input.GetMouseButton(0) && !isReloading && !isHide)
		{
			reload();
		}else if(magAmmo>=0)
		{
			textAmmo.text=magAmmo + "/" + totalAmmo;
		}

	}

    /**
    * @brief Fixed update is called once per frame. Used for properly control animations.
    */
	void FixedUpdate()
	{
		AnimatorStateInfo info=animator.GetCurrentAnimatorStateInfo(0);

		if(info.IsName("Fire"))
			animator.SetBool("Fire",false);
		
		if(info.IsName("Hide"))
			animator.SetBool("Hide",false);
		
		isReloading=info.IsName("Reload");

		if(info.IsName("Hide") || info.IsName("KeepHide") || info.IsName("Show"))
			isHide=true;
		else
			isHide=false;

		
		
	}

	/**
    * @brief Used to attack enemies
    * @param go Hitted object 
    */
	void hit( GameObject go){
		Health zdrowie = go.GetComponent<Health>();
		if(zdrowie != null) {
			zdrowie.damage(damage);
		}
	}

	/**
    * @brief Set new amounts of ammuniton, and play animations.
    */
	void reload()
	{
		if(totalAmmo+magAmmo==0)
			return;
		
		animator.CrossFadeInFixedTime("Reload",0.1f);
		reloadSound.Play();

		if(totalAmmo>=30)
		{		
			totalAmmo-=30-magAmmo;
			magAmmo=30;
		}else
		{
			if(magAmmo+totalAmmo>=30)
			{
				totalAmmo-=30-magAmmo;
				magAmmo=30;	
			}
			else
			{
				magAmmo+=totalAmmo;
				totalAmmo=0;
			}
		}
	

	}

	/**
    * @brief Used for play hiding gun animation
    */
	public void hideGun()
	{	
		animator.SetBool("Hide",true);
		animator.SetBool("Show",false);
	
	}

	/**
    * @brief Used for play showing gun animation
    */
	public void showGun()
	{
		animator.SetBool("Show",true);
	}

	/**
    * @brief Set new amounts of ammunition.
	* @param mag new amount of ammunition in magazine
	* @param total new amount of total ammunition
    */
	public void setAmmo(int mag,int total)
	{
		magAmmo=mag;
		totalAmmo=total;
	}






}
