using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBTN : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private string startScene;

    // Update is called once per frame
    private void Awake()
    {
        buttonPlay.onClick.AddListener(Play);
    }

    public void Play()
    {
        SceneManager.LoadScene(startScene);
    }
}
