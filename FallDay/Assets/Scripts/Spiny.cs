using UnityEngine;

public class Spiny : MonoBehaviour
{
    public Rigidbody2D spinyrb;
    private float facingside;
    public float speed;


    private void Start()
    {
        int randomside = Random.Range( 1 , 2);
        if (randomside == 1)
        {
            facingside = -1;
        }

        if (randomside == 2)
        {
            facingside = 1;
        }
    }
    void Update()
    {
        if (facingside == -1)
        {
            spinyrb.linearVelocity = new Vector2(facingside * speed, spinyrb.linearVelocity.y);
        }

        if (facingside == 1)
        {
            spinyrb.linearVelocity = new Vector2(facingside * speed, spinyrb.linearVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            if (facingside == -1)
            {
                facingside = 1;
            }

            if (facingside == 1)
            {
                facingside = -1;
            }
        
    }

}
