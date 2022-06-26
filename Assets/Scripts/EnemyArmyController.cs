using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyArmyController : MonoBehaviour
{
    /// <summary>
    /// Interval of enemies spawning
    /// </summary>
    [SerializeField] private int EnemiesWaveInterval = 30;
    
    /// <summary>
    /// Number of waves to skip after ame has started
    /// </summary>
    [SerializeField] private int NumberOfEmptyWaves = 2;

    /// <summary>
    /// Prefab of an enemy to instantiate from
    /// </summary>
    [SerializeField] private GameObject EnemyPrefab;
    
    /// <summary>
    /// Panel used to spawn and move enemies
    /// </summary>
    [SerializeField] private GameObject EnemiesPanel;
    
    /// <summary>
    /// Link to DefenderController to manupulate the number of defender units
    /// </summary>
    [SerializeField] private ResourceController DefendersController;

    /// <summary>
    /// Link to GameController to trigger game over
    /// </summary>
    [SerializeField] private GameController GameController;
    
    /// <summary>
    /// Link to a statistics counter
    /// </summary>
    [SerializeField] private StatisticsCounter StatisticsCounter;
    
    /// <summary>
    /// Text used to set info about enemy waves
    /// </summary>
    [SerializeField] private TMP_Text NextWaveIndicator;

    /// <summary>
    /// Audio clip to play on each wave start
    /// </summary>
    [SerializeField] private AudioClip ZombieClip;
    
    /// <summary>
    /// List of enemy objects
    /// </summary>
    private Stack<GameObject> _enemies;
    
    /// <summary>
    /// Time last wave started
    /// </summary>
    private float _lastWaveTime;
    
    /// <summary>
    /// Size of wave
    /// </summary>
    private int _waveSize;
    
    /// <summary>
    /// Size of a next wave(initial value is the size of the first wave)
    /// </summary>
    private int _nextWaveSize = 2;
    
    /// <summary>
    /// The index number of wave
    /// </summary>
    private int _waveNumber;
    
    /// <summary>
    /// Number of uits on one line
    /// </summary>
    private int _countOnOneline = 10;
    
    /// <summary>
    /// Random shift of unit from it's original position
    /// </summary>
    private int _randomShiftSize = 20;
    
    /// <summary>
    /// Width of a line
    /// </summary>
    private float _lineWidth = 100f;
    
    /// <summary>
    /// Height of a line
    /// </summary>
    private float _lineHeight = 450f;

    void Start()
    {
        _waveNumber = 0;
        _lastWaveTime = Time.time + NumberOfEmptyWaves * EnemiesWaveInterval;
        _enemies = new Stack<GameObject>();
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
                text = $"В волне {_waveNumber + 1} ожидается {_nextWaveSize} зомби";
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

    /// <summary>
    /// Start next enemies wave
    /// </summary>
    void RunEnemiesWave()
    {
        EnemiesPanel.transform.position = new Vector2(0, 160f);
        EnemiesPanel.SetActive(true);
        CalculateWaveParams();
        GenerateEnemies();  
        AudioManager.Instance.PlaySound(ZombieClip);
    }

    /// <summary>
    /// Calculate new position of panel with enemies
    /// </summary>
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

    /// <summary>
    /// Finish the wave, check if it is a game over condition
    /// </summary>
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
            while (_enemies.Count > 0)
            {
                Destroy(_enemies.Pop());
            }
        }
        
        EnemiesPanel.SetActive(false);
    }

    /// <summary>
    /// Calculate new params for a wave - its' size and index number
    /// </summary>
    void CalculateWaveParams()
    {
        _waveNumber += 1;
        _waveSize = _nextWaveSize;
        _nextWaveSize = Mathf.FloorToInt((_waveNumber + 1) * 1.5f);
    }

    /// <summary>
    /// Instantiate enemies on a panel
    /// </summary>
    void GenerateEnemies()
    {
        for (var i = 0; i < _waveSize; i++)
        {
            var newUnit = Instantiate(EnemyPrefab, EnemiesPanel.transform);
            _enemies.Push(newUnit);
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
