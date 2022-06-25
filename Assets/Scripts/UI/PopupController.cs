using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopupController : MonoBehaviour
    {
        public float PopupWidth;
        public float PopupHeight;
        
        [SerializeField] private GameObject PopupWrapper;
        [SerializeField] private Image Border;
        [SerializeField] private Color BorderColor;

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

        public void Show()
        {
            if (!PopupWrapper) return;
        
            Time.timeScale = Single.Epsilon;
            PopupWrapper.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Time.timeScale = 1f;
            PopupWrapper.gameObject.SetActive(false);
        }
    }
}