using UnityEngine;

public class SpawnNextFloor : MonoBehaviour
{

    public GameObject floor;
    public GameObject wall;
    public float floordistance;
    public float walldistance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(floor,new Vector3(transform.position.x, transform.position.y + floordistance,transform.position.z),Quaternion.identity);
            Instantiate(wall, new Vector3(transform.position.x, transform.position.y + walldistance, transform.position.z), Quaternion.identity);
        }
    }
}
