using UnityEngine;

public class DefendersController : UnitsController
{
    [SerializeField] private StatisticsCounter StatisticsCounter;
    
    public override void UpdateAmount(float value)
    {
        base.UpdateAmount(value);
        if (value > 0)
        {
            // only if we are talking about creating units
            StatisticsCounter.DefendersCreated += value;
        }
    }
}