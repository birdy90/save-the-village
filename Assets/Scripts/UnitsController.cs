public class UnitsController: ResourceController
{
    /// <summary>
    /// The interval food is updated by units
    /// </summary>
    public int FoodUpdateInterval;
    
    /// <summary>
    /// Amount of food one unit preduce(+)/consumes(-) each time interval
    /// </summary>
    public int FoodUpdateAmount;
}