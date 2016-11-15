using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;
    public bool isRunning;

    private Animator anim;
    private Rigidbody2D myRigidbody;
    private Renderer myRenderer;

    public bool playerExists;

    private bool playerMoving;
    public Vector2 lastMove;

    public Rigidbody2D rocket;
    public float speed = 10f;

    public int startingHealth = 50;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.
    bool playerInRange;
    public float timer;
    public Collider2D enemy;

    public bool playerDead;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag  == "enemy") {
            playerInRange = true;
            
            
        }


    }

    void OnTriggerExit2D (Collider2D other)
    {
        playerInRange = false;
    }



    // Use this for initialization
    void Start()
    {
        
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        rocket = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<Renderer>();

        if (playerExists == true)
        {
           playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        currentHealth = startingHealth;


    }

    // Update is called once per frame
    void Update()
    {

        playerMoving = false;
        if (playerDead == false)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                //transform.Translate (new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                myRigidbody.velocity =
                    new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                //transform.Translate (new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                myRigidbody.velocity =
                    new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
            }
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));

        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        anim.SetBool("PlayerDead", playerDead);

        //if (Input.GetMouseButtonDown(0))
        {
            //FireRocket();
            //Rigidbody2D rocketClone = (Rigidbody2D)Instantiate(rocket);
            //rocketClone.velocity = new Vector2(Input.mousePosition.x, Input.mousePosition.y) * speed;
        }

        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= .5F)
        {
            if (playerInRange == true) //&& enemyHealth.currentHealth > 0)
            {
                // ... attack.
                TakeDamage();
                timer = 0f;
                myRenderer.material.color = Color.red;

            }

            // If the player has zero or less health...
            if (currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                //Destroy(gameObject);
                playerDead = true;
                
            }
        }
        if (timer >= 1f)
        {
            myRenderer.material.color = Color.white;
        }

    }
    
   

    public void TakeDamage()
    {
        currentHealth -= attackDamage; 
    }


    //void OnTriggerEnter2D(Collider2D other)
    //{
    // Destroy(gameObject);
    //}

    //void FireRocket()
    //{
    //Rigidbody2D rocketClone = (Rigidbody2D)Instantiate(rocket);
    //Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    //Vector2 myPos = new Vector2(transform.position.x, transform.position.y + 1);
    //Vector2 direction = target - myPos;
    //direction.Normalize();
    //Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
    //rocketClone.velocity = direction * speed;
    //rocketClone.velocity = new Vector2(Input.mousePosition.x, Input.mousePosition.y) * speed;
    // }

   

} 

