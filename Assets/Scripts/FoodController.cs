using UnityEngine;

public class FoodController : ResourceController
{
    private float _foodConsumption;

    private float _lastUpdateTime = 0f;

    [SerializeField] private StatisticsCounter StatisticsCounter;
    [SerializeField] private UnitsController WorkersController;

    public void SetConsumtion(float consumtion)
    {
        _lastUpdateTime = Time.time;
        _foodConsumption = consumtion;
        UpdateUI();
    }

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

    public override void UpdateUI()
    {
        var consumption = _foodConsumption.ToString("F1").Replace(".0", "");
        Text.text = $"{Mathf.FloorToInt(Amount)} ({consumption})";
    }
}
