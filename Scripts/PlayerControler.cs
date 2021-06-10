
using UnityEngine;
using System.Collections;

/**
*@brief Class used for move player
*/
public class PlayerControler : MonoBehaviour
{
	/** @brief Character controller attached to player object     */
	public CharacterController characterControler;
	/** @brief Object with script used for control player's atmina    */
	public Stamina stamina;
	/** @brief  Script used for control shooting   */
	public Strzal strzal;
	/** @brief Object with script used for control menu     */
	public MainMenuController menuController; 

	/** @brief Script used for control healing skill   */
	public Heal heal;

	/** @brief Current player's speed     */
	public float actualSpeed = 5.0f;
	/** @brief  Walking spedd    */
	public float walkingSpeed = 5.0f;
	/** @brief  Runing speed   */
	public float runningSpeed = 12.0f;
	/** @brief  Squatting spped    */
	public float squatSpeed = 2.0f;

	/** @brief Player's height while standing    */
	public float standingHeight = 3.5f;
	/** @brief  Jummp height   */
	public float jumpHeight = 5.0f;
	/** @brief  Player's height while squatting    */
	public float squatHeight = 0.5f;
	/** @brief Current player's height      */
	public float actualHeight;
	/** @brief  Auxiliary variable  */
	public float tmpHeight =3.5f;

	/** @brief Informs if players is squatting     */
	private bool isSquatting = false;

	/** @brief Informs if players is running     */
	private bool isRunning = false; 

	/** @brief Sensivity of mouse     */
	public float mouseSensitivity = 2.0f;
	/** @brief  Auxiliary variable    */
	public float mouseUpDown = 0.0f;
	/** @brief  Range of up/down rotating    */
	public float mouseRange = 50.0f;

	/** @brief Auxiliary variable     */
	private float moveBackForward;
	/** @brief  Auxiliary variable    */
	private float moveRightLeft;

    /**
    * @brief Start is called before the first frame update. 
    */
	void Start()
	{
		characterControler = GetComponent<CharacterController>();
		actualHeight=characterControler.height;
	}

    /**
    * @brief Update is called once per frame.
    */
	void Update()
	{
		squat();
		move();
		rotate();
		crossHairSpread();
	}

    /**
    * @brief Used for move player
    */
	private void move()
	{
		
		
		moveBackForward = Input.GetAxis("Vertical") * actualSpeed;
		moveRightLeft = Input.GetAxis("Horizontal") * actualSpeed;
		
		if(heal.getIsHealing() || menuController.mainMenu.enabled || menuController.optionsMenu.enabled || menuController.gameOver.enabled)
		{
			moveBackForward=0;
			moveRightLeft=0;
		}

		if (!isSquatting )
		{
			
			if (characterControler.isGrounded && Input.GetButton("Jump") && !stamina.isResting())
			{
				actualHeight = jumpHeight;
			}
			else if (!characterControler.isGrounded)
			{
				actualHeight += Physics.gravity.y * Time.deltaTime;
			}

			if (characterControler.isGrounded )
			{
				if(Input.GetKeyDown("left shift"))
				{
					actualSpeed = runningSpeed;
					isRunning=true;
					strzal.hideGun();
				}else if(Input.GetKeyUp("left shift"))
				{
					actualSpeed = walkingSpeed;
					isRunning=false;
					strzal.showGun();
				}
				if(!Input.GetKey("left shift"))
				{
					strzal.showGun();
					isRunning=false;
					actualSpeed = walkingSpeed;
				}
			}

			if(stamina.isResting())
			{
				actualSpeed = walkingSpeed;
				isRunning=false;
			}


		}

		Vector3 move = new Vector3(moveRightLeft, actualHeight, moveBackForward);
		move = transform.rotation * move;
		characterControler.Move(move * Time.deltaTime);
		

		
	}

    /**
    * @brief Used for rotate player
    */
	private void rotate()
	{

		float mouseRightLeft = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, mouseRightLeft, 0);

		mouseUpDown -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		mouseUpDown = Mathf.Clamp(mouseUpDown, -mouseRange, mouseRange);

		Camera.main.transform.localRotation = Quaternion.Euler(mouseUpDown, 0, 0);
	}

    /**
    * @brief Used for control squatting
    */
	private void squat()
	{

		if (characterControler.isGrounded && Input.GetKeyDown("left ctrl"))
		{
			//Jeżeli kucasz, to przestań
			if (isSquatting)
			{
				isSquatting = false;
				actualSpeed = walkingSpeed;
				tmpHeight=standingHeight;
			}
			else //jeżeli nie kucasz to zacznij
			{
				isSquatting = true;
				actualSpeed = squatSpeed;
				tmpHeight=squatHeight;
			}

		}


		float ostatniaWysokosc = characterControler.height;

		characterControler.height = Mathf.Lerp (characterControler.height, tmpHeight, 4 * Time.deltaTime);

		float polozenieGraczaY = transform.position.y + (characterControler.height - ostatniaWysokosc) ;
		Vector3 pozycja = new Vector3 (transform.position.x, polozenieGraczaY, transform.position.z);
		transform.position = pozycja;
	}

    /**
    * @brief Getter of isRunning variable
	* @return Information if player is running
    */
	public bool getIsRunning()
	{
		return isRunning;
	}

    /**
    * @brief Getter of isSquatting variable
	* @return Information if player is squatting
    */
	public bool getIsSquatting()
	{
		return isSquatting;
	}

    /**
    * @brief Informs if player is moving
	* @return Information if player is moving
    */
	public bool isMoving()
	{
		if (moveBackForward != 0 || moveRightLeft != 0) {
			return true;
		}
		return false;
	}

    /**
    * @brief Used for chnage spread of crosshair while moving
    */
	private void crossHairSpread()
	{
		if(isMoving() && !isSquatting && DynamicCrosshair.spread<DynamicCrosshair.WALK_SPREAD)
			DynamicCrosshair.spread=DynamicCrosshair.WALK_SPREAD;
		

		if(!characterControler.isGrounded && DynamicCrosshair.spread<DynamicCrosshair.JUMP_SPREAD) 
			DynamicCrosshair.spread=DynamicCrosshair.JUMP_SPREAD;

	}



}



