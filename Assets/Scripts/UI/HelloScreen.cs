using UnityEngine;

namespace UI
{
    public class HelloScreen : MonoBehaviour
    {
        /// <summary>
        /// Key to store first run value. This popup only being showed to the player at his first play
        /// </summary>
        private string _key = "first_run";
    
        void Start()
        {
            var firstRun = PlayerPrefs.GetInt(_key, 0) == 0;
            if (!firstRun) return;
        
            PlayerPrefs.SetInt(_key, 1);
            GetComponent<PopupController>().Show();
        }
    }
}
