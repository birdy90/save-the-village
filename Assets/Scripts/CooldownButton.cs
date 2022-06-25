using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownButton : MonoBehaviour
{
    public int CooldownDuration;
    public bool IsLocked;
    public Sprite IconSprite;

    [SerializeField] private GameObject CooldownPanel;
    [SerializeField] private GameObject LockPanel;
    [SerializeField] private Image ButtonImage;
    [SerializeField] private Image CooldownIndicator;
    [SerializeField] private TMP_Text CooldownText;
    [SerializeField] private Button Button;
    [SerializeField] private FoodController FoodController;
    [SerializeField] private UnitsController UnitsController;

    private float _lastClickTime;

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

    void UpdateCooldown()
    {
        var timeLeft = CooldownDuration - (Time.time - _lastClickTime);
        CooldownIndicator.fillAmount = timeLeft / CooldownDuration;
        CooldownText.text = Mathf.CeilToInt(timeLeft).ToString(CultureInfo.InvariantCulture);
    }

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
