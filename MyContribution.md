# My Contribution
As a programmer, I created script for core framework which established the game loop and logic, also enabling UI control. I also implemented specific game mechanics such as needle attachment and eyeball animation.

The game jam required us not to use old source from previous projects. Therefore I had to adjust my workflow and code design accordingly with situation to succesfully complete the working build in time. Following lists are overviews about the approach I took to accomplish my task.

* Handling Game Loop and Logic:
  * [GameManager](#gamemanager)
  * [ScoreManager](#scoremanager)
  * [UI Control](#ui-control)
* Gameplay Mechanics:
  * [Eyeball Movement](#eyeball-movement)
  * [Needle](#needle)
  * [Obstacles](#obstacles)
  * [Spawner](#Spawner)

-----------------------------------------------------
## Handling Game Loop and Logic
  ### GameManager([Assets/Scripts/GameManager.cs](Assets/Scripts/GameManager.cs))
  Unity's beloved standard GameManager class was utilized to handle game loop, scene transition, and the three game state (Title, Playing, GameOver). GameManager also derives [singleton](Assets/Scripts/Interface/Singleton.cs) class to ensure that there are only one instance of it at all time. The Boot Scene contains this class and starts the game by loading the title scene and the game scene according to the State of the game.

  The Singleton setup also allows fast scaling iteration of the code since it provided unified accessor for underlying game logic without hindering the encapsulations, revealing other classes to each others. Also, since our game loop was only governed by one player variable, the lives of the player, it made more sense to keep the code more light-weight by having the GameState and this variable within the same class.

  Tha GameManager class acted as a connection between the UI and ScoreManager, notifying the UI Control of the changes within the ScoreManager to update the display accordingly. This creates abstraction of data between the UI and the ScoreManager to allow easier initializations of these objects through the one and only GameManager.

  ### ScoreManager([Assets/Scripts/ScoreManager.cs](Assets/Scripts/ScoreManager.cs))
  ScoreManager simply does what its name implies, it handles score and multiplier gained by the player. Since score is only relevant during the playing state, this class only exists within the PinballBoardMain Scence, which was our playing scene. Then on its Awake() it sets itself as a ScoreManager of the Current GameManager instance. It is then initialized within OnSceneLoaded of GameManager class to handle update on ScoreManager. Since OnSceneLoaded() is always called after Awake(), this ensures ScoreManager to be always valid when playing scene is loaded.

  ```C#

  public class GameManager : Singleton<GameManager>
  {
    // Initializes ScoreManager
    void OnSceneLoaded(Scene newScene, LoadSceneMode mode)
    {
      currentScene = newScene;
      if (newScene.name == "PinballBoardMain")
      {
        ScoreManager.OnScoreUpdate += HandleScoreUpdate;
        ScoreManager.OnMultiplierUpdate += HandleMultiUpdate;
        OnGameOver += ((BoardUI)CurrentUI).UpdateGameOverText;
      }
    }
  }
  ```

  ### UI Control([Assets/Scripts/UI](Assets/Scripts/UI))
  There are two classes that handle UI control, [TitleUI](Assets/Scripts/UI/TitleUI.cs) and [BoardUI](Assets/Scripts/UI/BoardUI.cs). Both of these classes derive from the abstract [UIController](Assets/Scripts/UI/UIController.cs) class, which all it does is sets itself as a CurrentUI of the GameManager in its Awake()

  ```C#
  protected virtual void Awake()
  {
    GameManager.Instance.CurrentUI = this;
  }
  ```

  For TitleUI, no control was needed since everything was animated by our designer and all we needed was an input to transition into the playable scene. Since our designer was responsible for the input and he chose not to create any Input Handling classes, I used the TitleUI as a simple hack to process the input from the player.

  BoardUI needed to display the logic handled in ScoreManager as well as GameManager. To handle changes in both classes, the BoardUI has a set of function which is called by the GameManager class when specific update from ScoreManager is being handled, as well as one fuction subscribed to the event of the GameManager.

  ```C#
  public void UpdateScoreText(int newScore)
  {
    scoreText.text = "Tears: " + newScore.ToString();
  }
  public void UpdateMultiplierText(float newMult)
  {
    multiplierText.text = "Multiplier: x" + newMult.ToString("0.0");
  }
  public void UpdateLivesText(int newLives)
  {
    livesText.text = "x" + newLives.ToString();
  }

  // subscribed to GameManager.OnGameOver when scene is loaded
  public void UpdateGameOverText()
  {
    gameOverText.gameObject.SetActive(true);
  }
  ```

-----------------------------------------------------
## Gameplay Mechanics
  ### Eyeball Movement([Assets/Scripts/Eyelook.cs](Assets/Scripts/Eyelook.cs))
  Our artist Nicolas wanted an eye that followed the doll around which I helped implement. Since the art asset was in 2D, a simple rotation towards the reference object could not be used. A simple 2D vector math algorithm was devised instead, which is called on fixed update of the Eyelook.cs script to update the movement every frame.

  ```C#
  void FixedUpdate()
  {
    // point to move towards during this frame
    Vector2 targetLoc;
    // make sure theres a doll to follow
    if (doll != null)
    {
      // gets the direction and distance between the doll and the eye
      Vector2 direction = (Vector2)doll.transform.position - origin;
      // if doll is inside the radius of an eye, put the eyeball directly below the doll
      if (direction.magnitude < eyeRadius)
      {
          targetLoc = doll.transform.position;
      }
      // if doll is outside of the boundary of an eye
      else
      {
        // clamp the displacement to the edge of the eye radius
        Vector2 displacement = Vector2.ClampMagnitude(direction, eyeRadius);
        // transform the target location
        targetLoc = origin + displacement;
      }
      // start moving the eye towards the target location
      this.transform.position = Vector2.MoveTowards(this.transform.position, targetLoc, speed * Time.deltaTime);
    }
  }
  ```

  ### Needle([Assets/Scripts/Needle.cs](Assets/Scripts/Needle.cs))
  Writing the code for the needle was the least of worry. In fact, the solution was already devised as soon as we've thought of the idea: to simply make the needle the child of the doll.

  ```C#
  private void attachNeedle(Transform bodyPart)
  {
    // make needle follow player
    this.transform.parent.parent = bodyPart.transform;
    bodyPart.GetComponent<Rigidbody2D>().mass += mass;
    this.GetComponent<Collider2D>().enabled = false;
  }
  ```

  The harder part was dealing with the edge case where needle sticks to the doll when colliding at the edge of the needle, which seemed physically unintuitive. To handle this case, I used a trigger-box half the size of the needle so that it would seem like the needle only sticks to the doll when it penetrates half way through.

  ### Obstacles([Assets/Scripts/Obstacles/ObstacleBase.cs](Assets/Scripts/Obstacles/ObstacleBase.cs))
  This is a script attached to the grave or spinning blades to add score and play sound effect when colliding with the doll. Both score and audio clip fields were serialized for the designer to implement. When a game object with this script collided with the doll, the doll called the ObstacleBase's Interact() method to play the effect and add score.

  ```C#
  public virtual void Interact(float velocity)
  {
    // add the score
    GameManager.Instance.ScoreManager.UpdateScore(scoreModifier);
    //play the clip
    if (clip)
      SFXPlayer.Instance.PlayClip(clip, velocity);
  }
  ```

  The PlayClip method of the SFXPlayer takes in the velocity as float as well. This was set initially to adjust the clip's volume accordingly to the colliding velocity. However, due to the volume scailing unintuitively, this mechanic was removed from the final build.

  ### Spawner([Assets/Scripts/Spawner.cs](Assets/Scripts/Spawner.cs))
  Spawner, unlike its name implies, handles the spawning after death of the VooDooDoll by holding a reference to the doll prefab, the transform location for the starting position, and the trigger box responsible for the doll's death. It is attached to the Trigger Box object which determines the death boundary, which is the fire burning at the bottom of the screen.

  ```C#
  private void OnTriggerEnter2D(Collider2D collision)
  {
    VooDooDoll doll = collision.GetComponent<VooDooDoll>();
    if (!doll)
      return;
    if (isDestryoing)
      return;
    GetComponent<AudioSource>().Play();
    StartCoroutine(DestroyDoll());
    // if the game didn't end from previous line, respawn the doll
    if (GameManager.Instance.CurrentState == GameState.Playing)
    {
      Transform newDoll = Instantiate(dollPrefab, spawnPoint.position, Quaternion.identity).transform.GetChild(0);
      mainCamera.target = newDoll;
      eye.Doll = newDoll;
    }
  }
  ```

  This is because the playable scene starts off with the doll already loaded, and Spawner only needs to respawn the doll after destruction while handling all the dependency on the VooDooDoll object from other classes.
