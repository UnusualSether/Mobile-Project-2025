using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
   public Transform player;
    private float targetpos;


        private void Update()
    {
        Vector3 targetposition = new Vector3(transform.position.x, player.position.y, -10f);
        transform.position = targetposition;


    }
}
