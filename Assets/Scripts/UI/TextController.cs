using TMPro;
using UnityEngine;

namespace UI
{
    public class TextController : MonoBehaviour
    {
        [SerializeField] private float DisappearingTimeout;
        [SerializeField] private TMP_Text Text;

        private float _lastChangeTime;

        void Start()
        {
            _lastChangeTime = Time.time - DisappearingTimeout;
        }

        void Update()
        {
            if (DisappearingTimeout < float.Epsilon || !Text)
            {
                return;
            }

            if (Time.time - _lastChangeTime >= DisappearingTimeout)
            {
                Text.gameObject.SetActive(false); 
            }
        }

        void SetText(string value)
        {
            _lastChangeTime = Time.time;
            Text.text = value;
        }
    }
}