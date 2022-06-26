using UnityEngine;

public class StatisticsCounter : MonoBehaviour
{
    /// <summary>
    /// Statistics of overall food produced 
    /// </summary>
    public float FoodProduced { get; set; } 
    
    /// <summary>
    /// Statistics of all defenders created
    /// </summary>
    public float DefendersCreated { get; set; }
    
    /// <summary>
    /// Statistics of all enemies being killed
    /// </summary>
    public float EnemiesDied { get; set; }
}