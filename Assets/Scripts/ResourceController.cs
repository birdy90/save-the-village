using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public int InitialAmount;
    
    [SerializeField] protected GameController GameController;
    [SerializeField] protected Sprite ResourceIcon;
    [SerializeField] protected Image ResourceImage;
    [SerializeField] protected TMP_Text Text;

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

    public virtual void UpdateAmount(float value)
    {
        Amount = Mathf.Max(0, Amount + value);
        GameController.RecalculateFoodConsumption();
        UpdateUI();
    }

    public virtual void UpdateUI()
    {
        Text.text = Amount.ToString("");
    }
}
