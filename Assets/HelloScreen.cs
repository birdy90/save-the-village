using UI;
using UnityEngine;

public class HelloScreen : MonoBehaviour
{
    private string _key = "first_run";
    
    void Start()
    {
        var firstRun = PlayerPrefs.GetInt(_key, 0) == 0;
        if (!firstRun) return;
        
        PlayerPrefs.SetInt(_key, 1);
        GetComponent<PopupController>().Show();
    }
}
