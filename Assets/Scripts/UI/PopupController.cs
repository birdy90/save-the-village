using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopupController : MonoBehaviour
    {
        /// <summary>
        /// Width of a popup
        /// </summary>
        public float PopupWidth;
        
        /// <summary>
        /// Height of a popup
        /// </summary>
        public float PopupHeight;
        
        /// <summary>
        /// Link to a popup wrapper
        /// </summary>
        [SerializeField] private GameObject PopupWrapper;
        
        /// <summary>
        /// Image of a border to set its' color
        /// </summary>
        [SerializeField] private Image Border;
        
        /// <summary>
        /// Color of a border
        /// </summary>
        [SerializeField] private Color BorderColor;

        /// <summary>
        /// Check whether popup is opened or not 
        /// </summary>
        public bool IsActive => PopupWrapper.activeSelf;

        private void OnValidate()
        {
            Start();
        }

        void Start()
        {
            Border.GetComponent<RectTransform>().sizeDelta = new Vector2(PopupWidth, PopupHeight);
            Border.color = BorderColor;
        }

        /// <summary>
        /// Show popup and set pause
        /// </summary>
        public void Show()
        {
            if (!PopupWrapper) return;
        
            Time.timeScale = Single.Epsilon;
            PopupWrapper.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide popup and continue game
        /// </summary>
        public void Hide()
        {
            Time.timeScale = 1f;
            PopupWrapper.gameObject.SetActive(false);
        }
    }
}