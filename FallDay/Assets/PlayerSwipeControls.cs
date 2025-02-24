using UnityEngine;

public class PlayerSwipeControls : MonoBehaviour
{
    Vector2 touchstart;
    Vector2 touchend;
    //0 = left , 1 = right
    public int facingside = -1;
    Vector3 JumpedFrom;
    public Rigidbody2D playerrb;
    public float speed;
    private float JumpPower = 8f;
    public GameObject GroundCheck;
    public LayerMask Floor;
    public Collider2D playercollide;
    public LayerMask OtherFloor;

    public float jumptimer = -0f;
    private void Start()
    {
      facingside = -1;
       jumptimer = -0.1f;
    }

    private void Update()
    {
        #region jumptimer
        if (jumptimer >= 0.0f)
        {
            jumptimer -= Time.deltaTime;
        }

        if (jumptimer <= 0.0f)
        {
            playercollide.excludeLayers = 0;
        }

     #endregion

        if (facingside == -1)
        {
            playerrb.linearVelocity = new Vector2(facingside * speed, playerrb.linearVelocity.y);
        }

        if (facingside == 1)
        {
            playerrb.linearVelocity = new Vector2(facingside * speed, playerrb.linearVelocity.y);
        }





        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchstart = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchend = Input.GetTouch(0).position;

            if (touchend.y > touchstart.y&&IsGrounded())
            {
                Jump();
            }

            if (touchend.y < touchstart.y&&IsGrounded())
            {
                DropDown();
            }

            if (touchend.x > touchstart.x)
            {
                TurnRight();
            }

            if (touchend.x < touchstart.x)
            {
                TurnLeft();
            }

        }
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.transform.position, 0.2f, Floor) || Physics2D.OverlapCircle(GroundCheck.transform.position, 0.2f, OtherFloor);

    }
    private void DropDown()
    {
        playercollide.excludeLayers = Floor.value;
        jumptimer = 0.5f;
    }

    private void Jump()
    {
        playerrb.linearVelocity = new Vector2(playerrb.linearVelocity.x, JumpPower);
        playercollide.excludeLayers = Floor.value;
        jumptimer = 1f;
    }

    private void TurnLeft()
    {
        facingside = -1;
    }

    private void TurnRight()
    {
        facingside = 1;
    }
}
