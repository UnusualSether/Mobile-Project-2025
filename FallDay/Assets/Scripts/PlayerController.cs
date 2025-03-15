using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Velocidade de movimento
    [SerializeField] private float jumpForce = 10f; // Força do pulo

    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;

    private Rigidbody2D rb;

    private bool isGrounded; 
    private bool isOnPlatform; 

    private Vector2 startTouchPosition; // Posição inicial do toque
    private Vector2 endTouchPosition; // Posição final do toque

    private float currentDirection = 0f; // Direção atual do movimento (-1: esquerda, 1: direita, 0: parado)

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

    }

}