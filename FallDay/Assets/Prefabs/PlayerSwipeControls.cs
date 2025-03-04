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
    public float JumpPower = 7f;
    public GameObject GroundCheck;
    public LayerMask Floor;
    public Collider2D playercollide;
    public LayerMask OtherFloor;
    int touchydifference;
    private float touchendydiff;
    private float touchendxdiff;

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
  

            if (touchend.y > touchstart.y && IsGrounded() && touchend.y > touchstart.y + 80f)
            {

                Jump();
            }

            if (touchend.y < touchstart.y&&IsGrounded()&&touchend.y < touchstart.y - 80f)
            {
                DropDown();
            }

            if (touchend.x > touchstart.x&& touchend.x > touchstart.x + 60f)
            {
                TurnRight();
                Debug.Log("Right");
            }

            if (touchend.x < touchstart.x && touchend.x < touchstart.x + 60f)
            {
                TurnLeft();
            }

        }
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.transform.position, 0.01f, Floor) || Physics2D.OverlapCircle(GroundCheck.transform.position, 0.2f, OtherFloor);

    }
    private void DropDown()
    {
        playercollide.excludeLayers = Floor.value;
        jumptimer = 0.4f;
    }

    private void Jump()
    {
        playerrb.linearVelocity = new Vector2(playerrb.linearVelocity.x, JumpPower);
        playercollide.excludeLayers = Floor.value;
        jumptimer = 0.5f;
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
