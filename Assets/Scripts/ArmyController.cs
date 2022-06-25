using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArmyController : MonoBehaviour
{
    [SerializeField] private ResourceController DefendersController;

    public Sprite[] UnitSprites;

    public GameObject UnitPrefab;

    private Stack<GameObject> _units;

    private float _previousAmount = 0;
    
    private int _countOnOneline = 10;
    private int _randomShiftSize = 20;
    private float _lineWidth = 90f;
    private float _lineHeight = 500f;

    void Start()
    {
        _units = new Stack<GameObject>();
    }

    // Update is called once per frame
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
