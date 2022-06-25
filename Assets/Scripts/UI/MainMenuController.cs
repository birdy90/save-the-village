using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void Start()
        {
            Time.timeScale = 1f;
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Scenes/GameScene");
        }
    }
}
