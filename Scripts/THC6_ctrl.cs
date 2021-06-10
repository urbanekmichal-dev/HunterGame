using UnityEngine;
using System.Collections;

/**
*@brief AI for worm
*/
public class THC6_ctrl : MonoBehaviour
{
    /** @brief Worm animator     */
    private Animator anim;
    /** @brief Character controler of object.   */
    private CharacterController controller;
    /** @brief Walking speed     */
    public float speed = 6.0f;
    /** @brief Running speed    */
    public float runSpeed = 12.0f;
    /** @brief Transform information of player.     */
    private Transform player;
    /** @brief Transform information of worm.     */
    private Transform enemy;
    /** @brief Field of worm's view     */
    public float fieldOfView = 50.0f;
    /** @brief Current relative y.position of worm    */
    private float currentJumpHight = 0f;
    /** @brief Speed of rotation      */
    public float rotSpeed = 10f;
    /** @brief  Information if worm is wandering    */
    private bool isWandering = false;
    /** @brief Information if worm is rotating left     */
    private bool isRotatingLeft = false;
    /** @brief Information if worm is rotating right     */
    private bool isRotatingRight = false;
    /** @brief Information if worm is walking     */
    private bool isWalking = false;
    /** @brief Speed of idle annimation   */
    public float enemySpeedIdle = 6.0f;
    /** @brief Object with all worm's sounds     */
    private CreatureAudio creatureAudio;
    /** @brief  Time to next step    */
    private float countDownSteps = 0f;
    /** @brief Pause between steps       */
    public float stepFrequency = 2f;
    /** @brief  Time between breaths    */
    private float breathingFrequency;
    /** @brief Time to next breath    */
    private float countDownBreathing;
    /** @brief  Informs if dead animation is playing   */
    private bool deadAnimation = true;
    /** @brief Object of spawn      */
    private SpawnObject spawn;
    /** @brief Pause between steps  while running     */
    public float stepFrequencyRun = 0.5f;
    /** @brief Informs if worm is moving      */
    private bool isMove = false;
    /** @brief Informs if delay should occure    */
    private bool isDelay = true;
    /** @brief  Object with playerControler script    */
    private PlayerControler playerControler;
    /** @brief Informs if player is detected    */
    private bool isDetected = false;
    /** @brief Time  between detections   */
    private float timeToDetected = 1f;
    /** @brief Time to next detection     */
    private float countDownTimeToDetected = 0f;
    /** @brief  Distance from player is always detected    */
    public float criticalDistance = 10f;
    /** @brief Time to next no detection     */
    private float countDownTimeToNoDetected = 0f;
    /** @brief time between no detections     */
    public float timeToNoDetected = 0f;
	/** @brief Variable used to delete the object */
    public static bool order66=false;


	/**
    * @brief Start is called before the first frame update. Attach all components.
    */
    void Start()
    {
        creatureAudio = GetComponent<CreatureAudio>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        enemy = transform;
        GameObject go = GameObject.FindWithTag("Player");
        player = go.transform;
        playerControler = go.GetComponent<PlayerControler>();
        spawn = GameObject.FindGameObjectWithTag("SpawnController").GetComponent<SpawnObject>();
    }



	/**
    * @brief Update is called once per frame. Controls worm.
    */
    void Update()
    {
        if(order66)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(enemy.position, player.position);
        if (distance < fieldOfView && !isDead())
        {
            if (playerControler.getIsSquatting() && distance > criticalDistance)
            {
                if (countDownTimeToNoDetected < timeToNoDetected) countDownTimeToNoDetected += Time.deltaTime;
                if (countDownTimeToNoDetected >= timeToNoDetected)
                {
                    isDetected = false;
                }
                countDownTimeToDetected = 0f;
            }
            else
            {
                if (countDownTimeToDetected < timeToDetected) countDownTimeToDetected += Time.deltaTime;
                if (countDownTimeToDetected >= timeToDetected)
                {
                    isDetected = true;

                }
                countDownTimeToNoDetected = 0f;

            }
            if (isDetected)
            {
                if (isDelay)
                {
                    StartCoroutine(Delay());

                    isDelay = false;
                }
                if (isMove)
                {
                    EscapeFromThePlayer();
                }
            }
        }
        else if (!isDead())
        {
            freeMovement();
            PlayBreathing();
        }
        else if (isDead())
        {
            StartCoroutine(EnemyDestroy());

        }

    }

	/**
    * @brief Check if worm is dead
    * @return Information if worm is dead
    */
    bool isDead()
    {
        Health h = gameObject.GetComponent<Health>();
        if (h != null)
        {
            return h.checkIfDead();
        }
        return false;
    }

	/**
    * @brief Used for controling worm while walking free
    */
    private void freeMovement()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            enemy.Rotate(transform.up * Time.deltaTime * rotSpeed);
            Vector3 ruch = new Vector3(enemy.forward.x, currentJumpHight, enemy.forward.z);
            controller.Move(ruch * enemySpeedIdle * Time.deltaTime);
            PlaySteps(stepFrequency);
        }
        if (isRotatingLeft == true)
        {
            enemy.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            Vector3 ruch = new Vector3(enemy.forward.x, currentJumpHight, enemy.forward.z);
            controller.Move(ruch * enemySpeedIdle * Time.deltaTime);
            PlaySteps(stepFrequency);
        }
        if (isWalking == true)
        {
            if (!controller.isGrounded)
            {
                currentJumpHight += Physics.gravity.y * Time.deltaTime;

            }
            Vector3 ruch = new Vector3(enemy.forward.x, currentJumpHight, enemy.forward.z);
            controller.Move(ruch * enemySpeedIdle * Time.deltaTime);
            PlaySteps(stepFrequency);

        }

    }

	/**
    * @brief Coroutine. Draws parameters for moving.
    */
    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 7);
        int walkTime = Random.Range(1, 3);

        isWandering = true;
        anim.SetFloat("move", 0);
        yield return new WaitForSeconds(walkWait);

        isWalking = true;
        anim.SetFloat("move", enemySpeedIdle);
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        anim.SetFloat("move", 0f);
        yield return new WaitForSeconds(rotateWait);

        if (rotateLorR == 1)
        {

            anim.SetFloat("move", enemySpeedIdle);
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            anim.SetFloat("move", 0);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;

            anim.SetFloat("move", enemySpeedIdle);
            yield return new WaitForSeconds(rotTime);
            anim.SetFloat("move", 0);
            isRotatingLeft = false;
        }
        isWandering = false;
        anim.SetFloat("move", 0);
    }

	/**
    * @brief Draws a breath clip
    * @return audio clip to play
    */
    AudioClip RandomClip()
    {
        return creatureAudio.breathingSounds[Random.Range(0, creatureAudio.breathingSounds.Length)];
    }

	/**
    * @brief Draw and play step sounds
    * @param frequency Step frequency
    */
    void PlaySteps(float frequency)
    {
        if (countDownSteps < frequency) countDownSteps += Time.deltaTime;
        if (countDownSteps >= frequency)
        {
            creatureAudio.sounds.volume = Random.Range(0.8f, 1);
            creatureAudio.sounds.pitch = Random.Range(0.8f, 1.1f);
            creatureAudio.sounds.Play();
            countDownSteps = 0f;
        }
    }

	/**
    * @brief Draw and play breathing sounds
    */
    void PlayBreathing()
    {
        if (countDownBreathing < breathingFrequency) countDownBreathing += Time.deltaTime;
        if (countDownBreathing >= breathingFrequency)
        {

            creatureAudio.sounds.spatialBlend = 1;
            creatureAudio.sounds.maxDistance = 20;
            creatureAudio.sounds.volume = Random.Range(0.2f, 0.7f);
            creatureAudio.sounds.pitch = Random.Range(0.8f, 1.1f);
            creatureAudio.sounds.PlayOneShot(RandomClip());

            breathingFrequency = Random.Range(4, 8);
            countDownBreathing = 0f;
        }
    }


	/**
    * @brief Used for controlling worm while running away from player
    */
    void EscapeFromThePlayer()
    {
        Vector3 graczPoz = new Vector3(player.position.x, enemy.position.y, player.position.z);
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(-(graczPoz - enemy.position)), speed * Time.deltaTime);

        if (!controller.isGrounded)
        {
            currentJumpHight += Physics.gravity.y * Time.deltaTime;
        }
        Vector3 ruch = new Vector3(enemy.forward.x, currentJumpHight, enemy.forward.z);



        controller.Move(ruch * runSpeed * Time.deltaTime);

        anim.SetFloat("move", runSpeed);



        PlaySteps(stepFrequencyRun);
    }

	/**
    * @brief Coroutine. Set delay
    */
    IEnumerator Delay()
    {
        isMove = true;
        yield return new WaitForSeconds(2);
        isMove = false;
        isDelay = true;
    }

	/**
    * @brief Draw and play dead sounds
    */
    void PlayDeadSound()
    {
        creatureAudio.deadSound.volume = Random.Range(0.8f, 1);
        creatureAudio.deadSound.pitch = Random.Range(0.8f, 1.1f);
        creatureAudio.deadSound.Play();
    }

	/**
    * @brief Coroutine. Animate worm's death
    */
    IEnumerator EnemyDestroy()
    {
        anim.SetFloat("move", 0f);
        if (deadAnimation)
        {
            if (Random.Range(0, 2) == 0)
                anim.SetTrigger("dead");
            else
                anim.SetTrigger("dead2");

            PlayDeadSound();
            spawn.wormCount++;
            deadAnimation = false;
        }
        yield return new WaitForSeconds(4);

        Destroy(gameObject);
    }

}



