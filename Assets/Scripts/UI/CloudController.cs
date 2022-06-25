using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CloudController : MonoBehaviour
    {
        [SerializeField] private Image ImageComponent;
        [SerializeField] private Sprite CloudSprite;

        private float _phase;
        private Vector3 _initialPosition;

        private void OnValidate()
        {
            Start();
        }

        void Start()
        {
            _initialPosition = transform.localPosition;
            _phase = Random.Range(0f, 360f);
            ImageComponent.sprite = CloudSprite;
            ImageComponent.SetNativeSize();
        }
    
        void Update()
        {
            float usedTime = Time.time + _phase;
            var positionShift = new Vector3(Mathf.Sin(usedTime / 3) * 100, Mathf.Cos(usedTime * 3) * 2);
            transform.localPosition = _initialPosition + positionShift;
        }
    }
}