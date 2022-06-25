using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ClickSound : MonoBehaviour
    {
        public AudioClip Clip;
    
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlaySound(Clip));   
        }
    }
}
