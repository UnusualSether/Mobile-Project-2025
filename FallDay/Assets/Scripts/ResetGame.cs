using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{

    [SerializeField] private string NextLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(NextLevel);
    }
}
