using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyArmyController : MonoBehaviour
{
    [SerializeField] private int EnemiesWaveInterval = 30;
    [SerializeField] private int NumberOfEmptyWaves = 2;

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject EnemiesPanel;
    [SerializeField] private ResourceController DefendersController;

    [SerializeField] private GameController GameController;
    [SerializeField] private StatisticsCounter StatisticsCounter;
    [SerializeField] private TMP_Text NextWaveIndicator;

    [SerializeField] private AudioClip ZombieClip;
    
    private List<GameObject> _enemies;
    private float _lastWaveTime;
    private int _waveSize;
    private int _waveNumber;
    
    private int _countOnOneline = 10;
    private int _randomShiftSize = 20;
    private float _lineWidth = 100f;
    private float _lineHeight = 450f;

    void Start()
    {
        _waveNumber = 0;
        _lastWaveTime = Time.time + NumberOfEmptyWaves * EnemiesWaveInterval;
        _enemies = new List<GameObject>();
    }

    void Update()
    {
        var timePassed = Time.time - _lastWaveTime;

        if (timePassed > 0)
        {
            string text = "";
            
            if (timePassed < EnemiesWaveInterval / 3f && _waveNumber > 0)
            {
                text = $"Волна {_waveNumber}";
            }
            else
            {
                text = $"В волне {_waveNumber + 1} ожидается {Mathf.FloorToInt((_waveNumber + 1)* 1.5f)} зомби";
            }
            
            // check less than 0 to avoid information during empty waves
            if (timePassed > EnemiesWaveInterval * 2f / 3f)
            {
                text += $"<br>До следующей волны  осталось {Mathf.Ceil(EnemiesWaveInterval - timePassed)} сек.";
            }

            NextWaveIndicator.text = text;
        }

        if (Time.time - _lastWaveTime >= EnemiesWaveInterval)
        {
            _lastWaveTime = Time.time;
            RunEnemiesWave();
        } 
        
        MovePanel();
    }

    void RunEnemiesWave()
    {
        EnemiesPanel.transform.position = new Vector2(0, 160f);
        EnemiesPanel.SetActive(true);
        CalculateWaveParams();
        GenerateEnemies();  
        AudioManager.Instance.PlaySound(ZombieClip);
    }

    void MovePanel()
    {
        if (!EnemiesPanel.activeSelf) return;

        var moveX = (Time.time - _lastWaveTime) * 90 + Mathf.Abs(Mathf.Sin(Time.time * 5) * 5);
        var moveY = 160 + Mathf.Abs(Mathf.Sin(Time.time * 5) * 5);
        EnemiesPanel.transform.position = new Vector3(moveX, moveY, 0f);

        if (moveX > 650)
        {
            EndEnemiesWave();
        }
    }

    void EndEnemiesWave()
    {
        if (DefendersController.Amount < _waveSize)
        {
            DefendersController.UpdateAmount(-DefendersController.Amount);
            StatisticsCounter.EnemiesDied += DefendersController.Amount;
            GameController.GameOver();
        }
        else
        {
            StatisticsCounter.EnemiesDied += _waveSize;
            DefendersController.UpdateAmount(-_waveSize);
        }
        
        EnemiesPanel.SetActive(false);
    }

    void CalculateWaveParams()
    {
        _waveNumber += 1;
        _waveSize = Mathf.FloorToInt(_waveNumber * 1.5f);
    }

    void GenerateEnemies()
    {
        _enemies.Clear();

        for (var i = 0; i < _waveSize; i++)
        {
            var newUnit = Instantiate(EnemyPrefab, EnemiesPanel.transform);
            _enemies.Add(newUnit);
            var line = Mathf.FloorToInt((float)_enemies.Count / _countOnOneline);
            var indexOnLine = _enemies.Count % _countOnOneline;

            var unitX = 0.7f * _lineWidth * (-line - (float)indexOnLine / _countOnOneline);
            var unitY = _lineHeight * (1f - (float)indexOnLine / _countOnOneline);
            var shiftX = Random.Range(-_randomShiftSize, _randomShiftSize);
            var shiftY = Random.Range(-_randomShiftSize, _randomShiftSize);
            newUnit.transform.localPosition = new Vector3(unitX + shiftX, unitY + shiftY);
        }
    }
}
