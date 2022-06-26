using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArmyController : MonoBehaviour
{
    /// <summary>
    /// Link to a DefendersController
    /// </summary>
    [SerializeField] private ResourceController DefendersController;

    /// <summary>
    /// List of available sprites for units
    /// </summary>
    public Sprite[] UnitSprites;

    /// <summary>
    /// Prefab of a unit to create instances from
    /// </summary>
    public GameObject UnitPrefab;

    /// <summary>
    /// List of current existing units
    /// </summary>
    private Stack<GameObject> _units;

    /// <summary>
    /// Previous amount of units. Used to check if number of units increased or decreased
    /// </summary>
    private float _previousAmount = 0;
    
    /// <summary>
    /// Number of units on one line
    /// </summary>
    private int _countOnOneline = 10;
    
    /// <summary>
    /// Random shift of unit from it's calculated position
    /// </summary>
    private int _randomShiftSize = 20;
    
    /// <summary>
    /// Width of one line
    /// </summary>
    private float _lineWidth = 90f;
    
    /// <summary>
    /// Height of one line
    /// </summary>
    private float _lineHeight = 500f;

    void Start()
    {
        _units = new Stack<GameObject>();
    }

    /// <summary>
    /// Check the number of units, add or delete if needed
    /// </summary>
    void Update()
    {
        var amountDiif = DefendersController.Amount - _previousAmount;
        if (amountDiif == 0) return;

        for (var i = 0; i < Mathf.Abs(amountDiif); i++)
        {
            if (amountDiif < 0)
            {
                if (_units.Count == 0) break;
                Destroy(_units.Pop());
            }
            else
            {
                var newUnit = Instantiate(UnitPrefab, gameObject.transform);
                _units.Push(newUnit);
                var line = Mathf.FloorToInt((float)_units.Count / _countOnOneline);
                var indexOnLine = _units.Count % _countOnOneline;
                var spriteIndex = Random.Range(0, UnitSprites.Length);
                newUnit.GetComponent<Unit>().Image.sprite = UnitSprites[spriteIndex];
                
                var unitX = - 0.7f * _lineWidth * (line - (float)indexOnLine / _countOnOneline);
                var unitY = _lineHeight * (1f - (float)indexOnLine / _countOnOneline);
                var shiftX = Random.Range(-_randomShiftSize, _randomShiftSize);
                var shiftY = Random.Range(-_randomShiftSize, _randomShiftSize);
                newUnit.transform.localPosition = new Vector3(unitX + shiftX, unitY + shiftY);
            }
        }
            
        _previousAmount = DefendersController.Amount;
    }
}
