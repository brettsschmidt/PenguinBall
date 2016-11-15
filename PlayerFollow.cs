using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {

    public Transform target;

    private Animator anim;
    private Vector3 targetPosition;
    public float moveSpeed;
    public float range;
    public GameObject player;
    
    bool playerInRange;
    PlayerHealth playerHealth;
    public int attackDamage = 10;
    public int startingHealth = 50;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
  
    public float timer;
    public Collider2D enemy;

    private static bool cameraExists;
    public Transform myTransform;
    public bool playerMoving;
    public float moveX;
    public float moveY;
    public float moveX1;
    public float moveY1;
    public float lastmoveX;
    public float lastmoveY;
    public float angle;

    public bool sealDead;
    private Renderer sealRenderer;

    
    



    public  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "rocket")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
    }





    void Awake()
    {
        myTransform = transform;
        currentHealth = startingHealth;
    }

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
       
            DontDestroyOnLoad(gameObject);

        anim = GetComponent<Animator>();
        sealRenderer = GetComponent<Renderer>();


    }

  

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(myTransform.position, target.position);
        if (sealDead == false)
        {
            if (range >= distance)
            {
                targetPosition = new Vector3(target.transform.position.x,
                    target.transform.position.y, transform.position.z);
                moveX = myTransform.position.x - target.transform.position.x;
                moveY = myTransform.position.y - target.transform.position.y;

                playerMoving = true;
                transform.position = Vector3.Lerp(transform.position, targetPosition,
                    moveSpeed * Time.deltaTime);
            }
        }
        
        if (moveY < 0 && angle >= 1 || angle <= -1)
        {
            moveY1 = 1;
            lastmoveY = 1;
        }
        if (moveY > 0 && angle >= 1 || angle <= -1)
        {
            moveY1 = -1;
            lastmoveY = -1;
        }
        if (angle <= 1 && angle >= -1)
        {
            moveY1 = 0;
        }

        if (range < distance)
        {
            moveX = 0;
            moveY = 0;
            moveX1 = 0;
            moveY1 = 0;
            playerMoving = false;
        }
        angle = moveY / moveX;
        if(angle < 1 && angle > -1 && moveX > 0)
        {
            moveX1 = 1;
            lastmoveX = 1;
        }
        if(angle >= 1 || angle <= -1)
        {
            moveX1 = 0;
        }
        if(angle > -1 && angle <1 && moveX < 0)
        {
            moveX1 = -1;
            lastmoveX = -1;
        }


       
        //if (moveX = 1,1)
        //{
          //  Debug.LogError("Itworks!!!");
        //}


        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= .5F)
        {
            if (playerInRange == true) //&& enemyHealth.currentHealth > 0)
            {
                // ... attack.
                TakeDamage();
                timer = 0f;
                sealRenderer.material.color = Color.red;
            }

            // If the player has zero or less health...
            if (currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                //Destroy(gameObject);
                sealDead = true;

            }
        }
        if (timer >= 1f)
        {
            sealRenderer.material.color = Color.white;
        }
            anim.SetFloat("MoveX", moveX1);
            anim.SetFloat("MoveY", moveY1);
            anim.SetFloat("LastMoveX", lastmoveX);
            anim.SetFloat("LastMoveY", lastmoveY);
            anim.SetBool("PlayerMoving", playerMoving);
            anim.SetBool("SealDead", sealDead);
           
        


        



    }
    void TakeDamage()
    {
        currentHealth -= attackDamage;
    }
    }
    


//void Attack()
    //{
      //  playerHealth.TakeDamage(attackDamage);
    //}


