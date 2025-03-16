using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed ; // Velocidade de movimento
    [SerializeField] private float jumpForce ; // Força do pulo
    [SerializeField] private float rayLength;
    //wallSlide / jumping
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private float wallJumpingDirection;
    [SerializeField] private float wallJumpingTime;
    [SerializeField] private float wallJumpingCount;
    [SerializeField] private float wallJumpingDuration;
   

    //wallJumping


    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    

    

    private Rigidbody2D rb;

    private bool isGrounded; 
    private bool isOnPlatform; 

    //wall slide / jumping
    private bool isWallSliding;
    private bool isWallJumping;

    private Vector2 startTouchPosition; // Posição inicial do toque
    private Vector2 endTouchPosition; // Posição final do toque

    [SerializeField]private float currentDirection = 1f; // Direção atual do movimento (-1: esquerda, 1: direita, 0: parado)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleTouchInput();
        HandleCollision();
       //MovePlayer(); Movimenta o personagem continuamente
    }
    private void FixedUpdate()
    {
        MovePlayer();
        WallSlide();
       // WallJump();

    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0) // Verifica se há toques na tela
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position; // Guarda a posição inicial do toque
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position; // Guarda a posição final do toque
                    DetectSwipe(); // Detecta o movimento
                    break;
            }
        }
    }

    void DetectSwipe()
    {
        Vector2 swipeDirection = endTouchPosition - startTouchPosition;



        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y)) // Movimento horizontal
        {
            if (swipeDirection.x > 0) // Arrastou para a direita
            {
                currentDirection = 1f; // Define a direção para direita
            }
            else // Arrastou para a esquerda
            {
                currentDirection = -1f; // Define a direção para esquerda
            }
        }
        else if (swipeDirection.y > 0) // Arrastou para cima (pulo)
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        // Aplica a velocidade horizontal continuamente
        rb.linearVelocity = new Vector2(currentDirection * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (isGrounded) // Só pula se estiver no chão
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void HandleCollision()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, rayLength, groundLayer);
        isOnPlatform = Physics2D.OverlapCircle(transform.position, rayLength, platformLayer);
        //wallslide
        isWallSliding = Physics2D.OverlapCircle(transform.position, rayLength, wallLayer);

    }
    private void WallSlide()
    {
        if (isWallSliding &&  !isGrounded && currentDirection != 0f) 
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed,float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    /*
    private void WallJump()
    {
        if (!isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCount = wallJumpingTime;

            CancelInvoke(nameof(StopWallJump));
        }
        else
        {
            wallJumpingCount -= Time.deltaTime;
        }

        if(wallJumpingCount > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCount = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {

            }
            Invoke(nameof(StopWallJump),wallJumpingDuration);
        }
    }

    private void StopWallJump()
    {
        isWallJumping = false;
    }

    */
    

   

}