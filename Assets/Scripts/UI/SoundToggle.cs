using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Sound toggle button controller
    /// </summary>
    public class SoundToggle : MonoBehaviour
    {
        /// <summary>
        /// Sprite shown when sound is on
        /// </summary>
        [SerializeField] private Sprite OnSprite;
        
        /// <summary>
        /// Sprite shown when sound is off
        /// </summary>
        [SerializeField] private Sprite OffSprite;
        
        /// <summary>
        /// Button component of the toggle
        /// </summary>
        private Button _button;

        void Start()
        {
            // set sprite
            _button = GetComponent<Button>();
            SetSprite();
                
            // add event listener to toggle sound
            _button.onClick.AddListener(() =>
            {
                AudioManager.Instance.Toggle();
                SetSprite();
            });
        }

        /// <summary>
        /// Set sprite according to the AudioManager state
        /// </summary>
        void SetSprite()
        {
            _button.image.sprite = AudioManager.Instance.IsMuted ? OffSprite : OnSprite;
        }
    }
}
