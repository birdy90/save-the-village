using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    /// <summary>
    /// Initial amount of resource
    /// </summary>
    public int InitialAmount;
    
    /// <summary>
    /// Link to a game controller
    /// </summary>
    [SerializeField] protected GameController GameController;
    
    /// <summary>
    /// Sprite for a resource panel
    /// </summary>
    [SerializeField] protected Sprite ResourceIcon;
    
    /// <summary>
    /// Inage on a resource panel
    /// </summary>
    [SerializeField] protected Image ResourceImage;
    
    /// <summary>
    /// Text under resource panel
    /// </summary>
    [SerializeField] protected TMP_Text Text;

    /// <summary>
    /// Current amount of a resource
    /// </summary>
    public float Amount { get; private set; }

    // Start is called before the first frame update
    void OnValidate()
    {
        Start();
    }

    void Start()
    {
        Amount = InitialAmount;
        ResourceImage.sprite = ResourceIcon;
        UpdateUI();
    }

    /// <summary>
    /// Set new amount for a resource
    /// </summary>
    /// <param name="value">Change value</param>
    public virtual void UpdateAmount(float value)
    {
        Amount = Mathf.Max(0, Amount + value);
        GameController.RecalculateFoodConsumption();
        UpdateUI();
    }
    
    /// <summary>
    /// Update UI of a resource panel
    /// </summary>
    public virtual void UpdateUI()
    {
        Text.text = Amount.ToString("");
    }
}
