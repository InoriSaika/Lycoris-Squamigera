using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask; //Este parámetro se toma para la detección de las plataformas, si está en contacto con el suelo para que salte o no
    [SerializeField] private GameObject graphic; //Parámetro necesario para gráfico (transformación) que si no ponemos, vamos a tener bugs  gordos


    public bool movingForward;
    public float movementSpeed;
    public float JumpForce;
    public bool airControl;
    bool justGrounded;
    public float airControlForce;
    public Rigidbody2D rb2D;
    private BoxCollider2D capCollider2D;
    string animationState = "AnimationState";
    private Vector3 origLocalScale;
    Animator animator;



    enum CharStates //los estados se numeran y no se atribuyen desde el script, se hace desde la máquina en la pestaña de animación
    {

        idle = 0,
        walkLeft = 1,
        walkRight = 2,
        jumping = 3,
        falling = 4,
        transf = 5,
        transfinverse = 6,
        liana = 7,
        death = 8,

    }

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        capCollider2D = transform.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        origLocalScale = transform.localScale;
        movingForward = true;
    }


    void Start()
    {
        
    }

    private void Move()
    {
        //    if(Input.GetKeyUp("d") || Input.GetKeyUp("a")) animator.SetInteger(animationState, (int)CharStates.idle);
        float inp = Input.GetAxis("Horizontal"); 
        float Velocidad = Mathf.Abs(inp);


        if (inp != 0.0f) //Si el jugador está pulsando algo, el input
        {
            // walking();
            Vector2 move = new Vector2(inp * movementSpeed, rb2D.velocity.y);
            rb2D.velocity = move;

            if (move.x > 0.01f)  /* Si se mueve un poquito porque está en positivo en el eje x, se mueve a la derecha */
            {
                if (graphic.transform.localScale.x < 0) /* Si el clip está hacia la izquierda... */
                {
                    graphic.transform.localScale = new Vector3(origLocalScale.x, transform.localScale.y, transform.localScale.z); // ...Pon el sprite con la origLocalScale del eje x en positivo

                    if (movingForward == false) movingForward = true;
                }
            }
            else if (move.x < -0.01f) /* Si se mueve a la izquierda porque el movimiento es negativo en el eje x*/
            {
                if (graphic.transform.localScale.x > 0)  /* Y el clip está a la derecha... */
                {
                    graphic.transform.localScale = new Vector3(-origLocalScale.x, transform.localScale.y, transform.localScale.z); // ...Invierte el sprite con la origLocalScale del eje x en negatico (-origLocalScale.x)

                    if (movingForward == true) movingForward = false;
                }
            }

            if (!IsGrounded())
            {
                move.x = move.x / airControlForce;
                move.x = Mathf.Clamp(move.x, -movementSpeed, +movementSpeed);
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(capCollider2D.bounds.center, capCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        //  Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    void FixedUpdate() //Esto se ejecuta en cada frame. Podemos usar un update a secas, pero es menos preciso, FixedUpdate hace comprobaciones en cada frame
    {
        
        Move(); //En todos los frames te puedes mover
       
        if (IsGrounded() && Input.GetKeyDown("space")) Jump();

        if (IsGrounded() == false) justGrounded = false;

        if (IsGrounded() && justGrounded == false )
        {
            justGrounded = true;
         
            animator.SetInteger(animationState, (int)CharStates.idle);
        }

        UpdateState();
    }

    private void Jump()  //Permite que cambie la trayectoria y se mueva en el aire, toda la fórmula
    {

        animator.SetInteger(animationState, (int)CharStates.jumping);
        animator.SetBool("Jumped", true);
        justGrounded = false;
        if (airControl == true)
        {
            rb2D.velocity = Vector2.up * JumpForce; 
        }
        else
        {
            if (movingForward == false) rb2D.velocity = Vector2.up * JumpForce + Vector2.left * JumpForce / 5;
            else rb2D.velocity = Vector2.up * JumpForce + Vector2.left * -JumpForce / 5;
        }
    }

    private void UpdateState()
    {
        /* se ejecuta al final del voidUpdate para comprobar en que estado se 
           encuentra el player y modifica el animationState aplicando un parámetro
           de tipo Int
         */
        if (rb2D.velocity.x > 0 && rb2D.velocity.y == 0) //Mayor de cero, ergo se mueve hacia la derecha
        {
            animator.SetInteger(animationState, (int)CharStates.walkRight);    
        }
        else if (rb2D.velocity.x < 0 && rb2D.velocity.y == 0) //Menor de cero, ergo se mueve a la izquierda
        {
            animator.SetInteger(animationState, (int)CharStates.walkLeft);
        }
        else if (rb2D.velocity.y == 0 && rb2D.velocity.x == 0) //Si no se está pulsando nada, no se mueve, ergo cambiar de sprite
        {
            animator.SetInteger(animationState, (int)CharStates.idle);
        }
    }

    private void Death ()
    {
        if (Input.GetKeyDown("E"))
        {
            animator.SetInteger(animationState, (int)CharStates.death);
        } 
        else if (Input.GetKeyDown("E") /* && el estado actual es death, porque no sé ponerlo */) {
            animator.SetInteger(animationState, (int)CharStates.idle);
        }
    }





}
