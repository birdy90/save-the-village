using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownButton : MonoBehaviour
{
    /// <summary>
    /// Time interval button will be unavailable after click 
    /// </summary>
    public int CooldownDuration;
    
    /// <summary>
    /// If the button is locked, no matter if it is in cooldown or not
    /// </summary>
    public bool IsLocked;
    
    /// <summary>
    /// Sprite to draw on a button
    /// </summary>
    public Sprite IconSprite;

    /// <summary>
    /// Link to a panel with cooldown interface
    /// </summary>
    [SerializeField] private GameObject CooldownPanel;
    
    /// <summary>
    /// Link to a panel with a lock interface
    /// </summary>
    [SerializeField] private GameObject LockPanel;
    
    /// <summary>
    /// Link to image on the button component
    /// </summary>
    [SerializeField] private Image ButtonImage;
    
    /// <summary>
    /// Image on a cooldown panel to make radial fill
    /// </summary>
    [SerializeField] private Image CooldownIndicator;
    
    /// <summary>
    /// Text of cooldown to show remaining time
    /// </summary>
    [SerializeField] private TMP_Text CooldownText;
    
    /// <summary>
    /// Link to the button component
    /// </summary>
    [SerializeField] private Button Button;
    
    /// <summary>
    /// Link to a food controller (to check if button is locked or not)
    /// </summary>
    [SerializeField] private FoodController FoodController;
    
    /// <summary>
    /// Link to a unit controller to manupulate particlar type of units
    /// </summary>
    [SerializeField] private UnitsController UnitsController;

    /// <summary>
    /// Last time the button was clicked
    /// </summary>
    private float _lastClickTime;

    /// <summary>
    /// If button can perform action on a click event
    /// </summary>
    private bool IsClickable => !IsLocked && Time.time - _lastClickTime >= CooldownDuration;

    private void OnValidate()
    {
        Start();
    }

    void Start()
    {
        _lastClickTime = Time.time - CooldownDuration;
        ButtonImage.sprite = IconSprite;
    }

    /// <summary>
    /// Click event handler
    /// </summary>
    public void OnClick()
    {
        if (!IsClickable) return;

        FoodController.UpdateAmount(-2);
        UnitsController.UpdateAmount(1);

        Button.interactable = false;
        _lastClickTime = Time.time;
        CooldownPanel.SetActive(true);
        
        UpdateCooldown();
    }

    /// <summary>
    /// Update UI due to cooldown state
    /// </summary>
    void UpdateCooldown()
    {
        var timeLeft = CooldownDuration - (Time.time - _lastClickTime);
        CooldownIndicator.fillAmount = timeLeft / CooldownDuration;
        CooldownText.text = Mathf.CeilToInt(timeLeft).ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Check the button state
    /// </summary>
    void Update()
    {
        if (FoodController.Amount < 2)
        {
            if (!IsLocked)
            {
                IsLocked = true;
                LockPanel.SetActive(true);
            }
        }
        else
        {
            if (IsLocked)
            {
                IsLocked = false;
                LockPanel.SetActive(false);
            }
        }

        if (Time.time - _lastClickTime >= CooldownDuration)
        {
            CooldownPanel.SetActive(false);
            Button.interactable = true;
        }

        UpdateCooldown();
    }
}
