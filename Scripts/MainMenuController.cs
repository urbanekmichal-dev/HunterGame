using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief Script used for controling interface
*/
public class MainMenuController : MonoBehaviour
{
	/** @brief Script responsible for makig new game     */
	public NewGame newGame;
	/** @brief Script responsible for counting time in single game     */
	public GameTime gameTime;
	/** @brief Script resonsible for counting points in game     */
	public Points points;
	/** @brief Object with player health.    */
	public PlayerHealth playerHP;

	/** @brief Canvas with main menu    */
	public Canvas mainMenu;
	/** @brief Canvas with quit menu      */
	public Canvas quitMenu;
	/** @brief Canvas with pause menu      */
	public Canvas pauseMenu;
	/** @brief Canvas with HUD      */
	public Canvas HUD;
	/** @brief Canvas with game over menu      */
	public Canvas gameOver;
	/** @brief Canvas with options menu     */
	public Canvas optionsMenu;

	/** @brief Image used for fading screen     */
	public Image fadeScreen;
	/** @brief Button used for starting new game     */
	public Button newGameButton;
	/** @brief Button used for enter to the options menu      */
	public Button optionsButton;
	/** @brief Button used for exit the game      */
	public Button exitButton;

	/** @brief Text with question displaying in quit menu    */
    public Text text;
	/** @brief Text with current points gained by player     */
	public Text pointsText;
	/** @brief Object of spawn     */
	public SpawnObject spawn;
	/** @brief Information about to which menu go after clicking "YES" in quit menu. true-mainMenu, false- exit game   */
	private bool whereToGo;

    /**
    * @brief Start is called before the first frame update. Set enable only main menu canvas.
    */	
	void Start (){
		fadeScreen.enabled=false;
        mainMenu.enabled=true;
		optionsMenu.enabled=false;
		pauseMenu.enabled=false;
		quitMenu.enabled=false;
		HUD.enabled=false;
		gameOver.enabled=false;
		Time.timeScale = 1;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;//Odblokowanie kursora myszy.

	}

    /**
    * @brief Update is called once per frame. Checks if player want to open pause menu or if game is over.
    */
	void Update () {

		if(!mainMenu.enabled && Input.GetKeyUp(KeyCode.Escape) && !gameOver.enabled && !optionsMenu.enabled)
		{
			pauseMenu.enabled = !pauseMenu.enabled;
			Cursor.visible = !Cursor.visible;
				
			if(pauseMenu.enabled) {
				Cursor.lockState = CursorLockMode.Confined;
				Cursor.visible = true;
				Time.timeScale = 0;
						
			} else {
				Cursor.lockState = CursorLockMode.Locked; 
				Cursor.visible = false;
				Time.timeScale = 1;
			}
			quitMenu.enabled = false; 
					
		}

		if((gameTime.gettimeOver() || playerHP.checkIfDead()) && !mainMenu.enabled && !optionsMenu.enabled)
		{
			HUD.enabled=false;
			pointsText.text="Your points: " +points.getPoints();
			gameOver.enabled=true;

			THC6_ctrl.order66=true;
			LepszeCCAI.order66=true;
			AmmoBox.order66=true;
			FirstAidKit.order66=true;
			spawn.stop=true;

			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
		}

	}

	/**
    * @brief Start new game
    */
	public void newGameButton_pressed (){

		StartCoroutine(startGame());

	}

	/**
    * @brief Coroutine. Disable all menus, enabled HUD, start time, call newGame() function.
    */
	IEnumerator startGame()
	{
		newGameButton.enabled=false;
		optionsButton.enabled=false;
		exitButton.enabled=false;
		Time.timeScale = 1;
		fadeScreen.canvasRenderer.SetAlpha(0);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		fadeScreen.enabled=true;
		fadeScreen.CrossFadeAlpha(1,1,false);
		yield return new WaitForSeconds(2);
		newGame.newGame();
		mainMenu.enabled = false; 
		optionsMenu.enabled=false;
		pauseMenu.enabled=false;
		HUD.enabled=true; 
		gameOver.enabled=false;
		fadeScreen.CrossFadeAlpha(0,2,false);
		yield return new WaitForSeconds(2);
		fadeScreen.enabled=false;
	}

	/**
    * @brief Enter options menu
    */
	public void optionsButton_pressed (){

		mainMenu.enabled = false;
		optionsMenu.enabled = true; 

	}
	/**
    * @brief Close application
    */
	public void exitGameFromMainButton_pressed() {
		Application.Quit(); 
		
	}

	/**
    * @brief Resume game from pause
    */
	public void resumeButton_pressed() {
		quitMenu.enabled = false; 
        pauseMenu.enabled = false;
        Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked; 
		Cursor.visible = false;
	}

	/**
    * @brief Exit from the game
    */
	public void exitGameButton_pressed() {

		quitMenu.enabled = true; 

        text.text="Do you want to exit the game?";
        text.enabled=true;
        whereToGo=false;
	}

	/**
    * @brief Exit to main menu from pause menu
    */
    public void exitToMainButton_pressed() {
		quitMenu.enabled = true; 
        text.text="Do you want to exit to main menu?";
        text.enabled=true;
        whereToGo=true;
	}

	/**
    * @brief Close quit menu
    */
    public void noButton_pressed()
    {
        quitMenu.enabled=false;
        pauseMenu.enabled=true;
    }

	/**
    * @brief Close application or go to main menu.
    */
	public void yesButton_pressed() {
        if(whereToGo)
        {
            quitMenu.enabled=false;
			pauseMenu.enabled=false;
			HUD.enabled=false;
			pointsText.text="Your points: " +points.getPoints();
			gameOver.enabled=true;
			Time.timeScale = 0;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
        }	    
        else
        {
            Application.Quit();
        }
		
	}

	/**
    * @brief Close game over menu. Enter to main menu.
    */
	public void exitFromGameOverButton_pressed()
	{
		gameOver.enabled=false;
		mainMenu.enabled=true;
		newGameButton.enabled=true;
		optionsButton.enabled=true;
		exitButton.enabled=true;
		Time.timeScale = 1;
	
	}

	/**
    * @brief Check if game is paused.
	* @return Information if game is paused.
    */
	public bool isPaused()
	{
		if(mainMenu.enabled || pauseMenu.enabled || quitMenu.enabled || optionsMenu.enabled || gameOver.enabled)
			return true;
		else
			return false;
	}


}
