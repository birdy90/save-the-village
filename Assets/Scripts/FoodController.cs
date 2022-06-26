using UnityEngine;

public class FoodController : ResourceController
{
    /// <summary>
    /// Current food consumption value (food per second)
    /// </summary>
    private float _foodConsumption;

    /// <summary>
    /// Last time food amount was updated
    /// </summary>
    private float _lastUpdateTime = 0f;

    /// <summary>
    /// Link to a statistics counter
    /// </summary>
    [SerializeField] private StatisticsCounter StatisticsCounter;
    
    /// <summary>
    /// Link to a workers controller
    /// </summary>
    [SerializeField] private UnitsController WorkersController;

    /// <summary>
    /// Set consumption value
    /// </summary>
    /// <param name="consumtion">Consumtion value</param>
    public void SetConsumtion(float consumtion)
    {
        _lastUpdateTime = Time.time;
        _foodConsumption = consumtion;
        UpdateUI();
    }

    /// <summary>
    /// Update food amount and check if the game is won
    /// </summary>
    private void Update()
    {
        if (Time.time - _lastUpdateTime > 1)
        {
            _lastUpdateTime = Mathf.Floor(Time.time);
            UpdateAmount(_foodConsumption);
            StatisticsCounter.FoodProduced += WorkersController.Amount * WorkersController.FoodUpdateAmount /
                                              WorkersController.FoodUpdateInterval;

            if (StatisticsCounter.FoodProduced >= 777)
            {
                GameController.GameWon();
            }
        }
    }

    /// <summary>
    /// Update UI for food
    /// </summary>
    public override void UpdateUI()
    {
        var consumption = _foodConsumption.ToString("F1").Replace(".0", "");
        Text.text = $"{Mathf.FloorToInt(Amount)} ({consumption})";
    }
}
