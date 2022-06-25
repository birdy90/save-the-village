using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SoundToggle : MonoBehaviour
    {
        [SerializeField] private Sprite OnSprite;
        [SerializeField] private Sprite OffSprite;
        
        private Button _button;

        void Start()
        {
            _button = GetComponent<Button>();
            SetSprite();
                
            _button.onClick.AddListener(() =>
            {
                AudioManager.Instance.Toggle();
                SetSprite();
            });
        }

        void SetSprite()
        {
            _button.image.sprite = AudioManager.Instance.IsMuted ? OffSprite : OnSprite;
        }
    }
}
