using UnityEngine;

public class DefendersController : UnitsController
{
    /// <summary>
    /// Statistics counter for defenders
    /// </summary>
    [SerializeField] private StatisticsCounter StatisticsCounter;
    
    /// <summary>
    /// Modified UpdateAmount to count new defenders in statistics
    /// </summary>
    /// <param name="value">Defenders amount change</param>
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