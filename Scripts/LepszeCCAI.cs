using UnityEngine;
using System.Collections;

/**
*@brief AI for zombie
*/
public class LepszeCCAI : MonoBehaviour
{
	/** @brief Character controler of object.   */
	public CharacterController characterControler;
	/** @brief Transform information of player.     */
	private Transform player;
	/** @brief Transform information of zombie.     */
	private Transform enemy;
	/** @brief Speed of rotation     */
	public float enemyRotate = 4.0f;
	/** @brief Speed of walking     */
	public float enemySpeed = 5.0f;
	/** @brief Speed of idle annimation   */
	public float enemySpeedIdle=1.0f;
	/** @brief Field of zombie's view     */
	public float fieldOfView = 50.0f;
	/** @brief Current relative y.position of zombie    */
	private float currentJumpHight = 0f;
	/** @brief Distance from player     */
	public float distanceFromPlayer = 4f;
	/** @brief Information whether zombie is a ghost     */
	public bool isGhost;
	/** @brief Speed of rotation      */
	public float rotSpeed=100f;
	/** @brief  Information if zombie is wandering    */
	private bool isWandering=false;
	/** @brief Information if zombie is rotating left     */
	private bool isRotatingLeft=false;
	/** @brief Information if zombie is rotating right     */
	private bool isRotatingRight=false;
	/** @brief Information if zombie is walking     */
	private bool isWalking=false;
	/** @brief zombie's animator     */
	private Animator animator;
	/** @brief      */
	private HashIDs hash;
	/** @brief Rigidbody attached to object     */
	private Rigidbody rb;
	/** @brief Speed of dumb time     */
	public float speedDumbTime=0.5f;
	/** @brief Object with all zombie's sounds     */
	private ZombieAudio zombieAudio;
	/** @brief Time to next attack    */
	private float countDownAttack=0f;
	/** @brief Pause between attacks    */
	public float attackFrequency=2f;
	/** @brief  Pause between steps     */
	public float stepFrequency=2f;
	/** @brief  Time to next step    */
	private float countDownSteps=0f;
	/** @brief Pause between steps  while running     */
	private float runStepFrequency=0.5f;
	/** @brief  Damage dealed by zombie    */
	public float damage = 20.0f;
	/** @brief  Time between breaths    */
	public float breathingFrequency;
	/** @brief Time to next breath    */
	private float countDownBreathing;
	/** @brief  Information if zombie is dead    */
	private bool dead=true;
	/** @brief Object of spawn      */
	private SpawnObject spawn;
	/** @brief Variable used to delete the object */
	public static bool order66=false;

	/**
    * @brief Use this for initialization.
    */
	void Awake()
	{
		animator=GetComponent<Animator>();
		rb=GetComponent<Rigidbody>();
		zombieAudio=GetComponent<ZombieAudio>();
		hash=GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
		spawn=GameObject.FindGameObjectWithTag("SpawnController").GetComponent<SpawnObject>();
	}

	/**
    * @brief Start is called before the first frame update.
    */
	void Start () {
		//Podłączenie fizyki ciała i zablokowanie jej
		if (GetComponent<Rigidbody> ()) GetComponent<Rigidbody> ().freezeRotation = true;
		//Podłączenie animatora i włączenie animacji
		if(GetComponent<Animator>()) {
			animator=GetComponent<Animator>();
			//animator.SetBool("IsAnimating",true);
			//animator.SetBool("IsWalk",false);
		}
		characterControler = GetComponent<CharacterController> ();
		enemy = transform;
		GameObject go = GameObject.FindWithTag ("Player");
		player = go.transform;
	}
	
	/**
    * @brief Update is called once per frame. Controls zombie.
    */
	void Update (){

		if(order66)
        {
            Destroy(gameObject);
        }

		//Pobranie dystansu dzielącego wroga od obiektu gracza.
		float distance = Vector3.Distance (enemy.position, player.position);

		/*Jeżeli dystans jaki dzieli obiekt wroga od obiektu gracza mieści się w zakresie widzenia wroga to 
		obiekt wroga zaczyna poruszać się w kierunku gracza.
		Obiekt wroga zatrzyma się przed graczem w zadanym odstępie.*/
		

		if (distance < fieldOfView && !isDead()) {

			Vector3 graczPoz = new Vector3(player.position.x, enemy.position.y, player.position.z);

			/*Funkcja Quaternion.Slerp (spherical linear interpolation)
			pozwala obracać obiekt w zadanym kierunku z zadaną prędkością.
			Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji.*/
			enemy.rotation = Quaternion.Slerp (enemy.rotation, Quaternion.LookRotation (graczPoz - enemy.position), enemySpeed * Time.deltaTime);

			//Aby uniknąć latania przeciwnika wymuszamy pozostanie na ziemi.
			if (!characterControler.isGrounded ){//Jezeli jestesmy w powietrzu(skok)
				currentJumpHight += Physics.gravity.y * Time.deltaTime;
			}

				//Tworzymy wektor odpowiedzialny za ruch.
				//x - odpowiada za ruch lewo/prawo,
				//y - odpowiada za ruch góra/dół,
				//z - odpowiada za ruch przód/tył.
				Vector3 ruch = new Vector3(enemy.forward.x, currentJumpHight, enemy.forward.z);
            //Ruch wroga.

            animator.SetFloat(hash.speed, enemySpeed, speedDumbTime, Time.deltaTime);

			if (countDownAttack < attackFrequency) countDownAttack += Time.deltaTime;
            if (distance < distanceFromPlayer)
            {
				animator.SetBool(hash.isAttack, true);
				characterControler.Move(ruch * enemySpeed * Time.deltaTime);
                if (countDownAttack >= attackFrequency)
                {
                    zombieAudio.roarSound.Play();
					player.GetComponent<PlayerHealth>();
					GameObject go = GameObject.FindWithTag ("Player");
					hit(go);
					
                    countDownAttack = 0f;

                }


            }
            else
                animator.SetBool(hash.isAttack, false);

			if (countDownSteps < runStepFrequency) countDownSteps += Time.deltaTime;
			if(countDownSteps >= runStepFrequency)
			{ 
				zombieAudio.footstepSound.volume=Random.Range(0.8f,1);
				zombieAudio.footstepSound.pitch=Random.Range(0.8f,1.1f);
				zombieAudio.footstepSound.Play();
				countDownSteps=0f;
			}

			

        }
		else if(!isDead())
		{
			freeMovement();
			if (countDownSteps < stepFrequency) countDownSteps += Time.deltaTime;
			if(countDownSteps >= stepFrequency)
			{ 
				zombieAudio.footstepSound.volume=Random.Range(0.6f,1);
				zombieAudio.footstepSound.pitch=Random.Range(0.6f,1.3f);
				zombieAudio.footstepSound.Play();
				countDownSteps=0f;
			}
			
			if (countDownBreathing < breathingFrequency) countDownBreathing += Time.deltaTime;
			if(countDownBreathing >= breathingFrequency)
			{ 
				
				
				zombieAudio.breathingSound.volume=Random.Range(0.8f,1);
				zombieAudio.breathingSound.pitch=Random.Range(0.8f,1.1f);
				zombieAudio.breathingSound.PlayOneShot(RandomClip());
				countDownBreathing=0f;
				breathingFrequency=Random.Range(3,15);
			}

		}
		else if(isDead())
		{
			if (GetComponent<Rigidbody> ()) {
				GetComponent<Rigidbody> ().freezeRotation = false;
				StartCoroutine( zombieDead());
				
			}

		}

	}

	/**
    * @brief Used to attack player
    * @param go Hitted object 
    */
	void hit( GameObject go){
		PlayerHealth zdrowie = go.GetComponent<PlayerHealth>();
		if(zdrowie != null) {
			zdrowie.damage(damage);
		}
	}

	/**
    * @brief Used for controling zombie while walking free
    */
	private void freeMovement()
	{
		if(isWandering==false)
			{
				StartCoroutine(Wander());
			}
			if(isRotatingRight==true)
			{
				enemy.Rotate(transform.up*Time.deltaTime*rotSpeed);
			}
			if(isRotatingLeft==true)
			{
				enemy.Rotate(transform.up*Time.deltaTime*-rotSpeed);
			}
			if(isWalking==true)
			{
				if (!characterControler.isGrounded ){//Jezeli jestesmy w powietrzu(skok)
				currentJumpHight += Physics.gravity.y * Time.deltaTime;
				
			}
				
				Vector3 ruch = new Vector3(enemy.forward.x, currentJumpHight, enemy.forward.z);
				//Ruch wroga.
				characterControler.Move(ruch * enemySpeedIdle * Time.deltaTime);
				
			}
			//animator.SetBool("IsWalk",false);
			animator.SetFloat(hash.speed,enemySpeedIdle,speedDumbTime,Time.deltaTime);
			animator.SetBool(hash.isAttack,false);
	}

	/**
    * @brief Coroutine. Draws parameters for moving.
    */
	IEnumerator Wander()
	{
		int rotTime = Random.Range(1,3);
		int rotateWait=Random.Range(1,4);
		int rotateLorR=Random.Range(1,2);
		int walkWait=Random.Range(1,7);
		int walkTime=Random.Range(1,3);

		isWandering=true;

		yield return new WaitForSeconds(walkWait);
		isWalking=true;
		yield return new WaitForSeconds(walkTime);
		isWalking=false;
		yield return new WaitForSeconds(rotateWait);
		if(rotateLorR==1)
		{
			isRotatingRight=true;
			yield return new WaitForSeconds(rotTime);
			isRotatingRight=false;
		}
		if(rotateLorR==2)
		{
			isRotatingLeft=true;
			yield return new WaitForSeconds(rotTime);
			isRotatingLeft=false;
		}
		isWandering = false;

		}

	/**
    * @brief Check if zombie is dead
    * @return Information if zombie is dead
    */
	bool isDead(){
		Health h = gameObject.GetComponent<Health>();
		if(h != null) {
			return h.checkIfDead();
		}
		return false;
	}

	/**
    * @brief Draws a breath clip
    * @return audio clip to play
    */
	AudioClip RandomClip(){
    return zombieAudio.breathingSounds[Random.Range(0, zombieAudio.breathingSounds.Length)];
	}

	/**
    * @brief Coroutine. Animate zombie's death
    */
	IEnumerator zombieDead()
	{
		if(dead)
		{
		switch(Random.Range(0,2))
			{
				case 0: animator.SetTrigger(hash.isDeadBack);
				break;
				case 1: animator.SetTrigger(hash.iSDead);
				break;
			}
			zombieAudio.deadSound.Play();
			spawn.zombieCount++;
			dead=false;
			
		}
		yield return new WaitForSeconds(4);
		Destroy(gameObject);
		dead=true;
			
			
			
	}
}
