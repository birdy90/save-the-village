using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ClickSound : MonoBehaviour
    {
        /// <summary>
        /// Click audio clip to be played
        /// </summary>
        public AudioClip Clip;
    
        void Start()
        {
            // add listener to button to play sound on click
            var button = GetComponent<Button>();
            if (!button) return;
            
            button.onClick.AddListener(() => AudioManager.Instance.PlaySound(Clip));   
        }
    }
}
