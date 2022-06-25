using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    [SerializeField] private FoodController FoodController;
    [SerializeField] private UnitsController WorkersController;
    [SerializeField] private UnitsController DefendersController;

    [SerializeField] private PopupController GameOverPanel;
    [SerializeField] private TMP_Text GameOverText;

    [SerializeField] private PopupController WinPanel;
    [SerializeField] private TMP_Text WinText;

    [SerializeField] private PopupController PausePanel;
    [SerializeField] private TMP_Text PauseText;

    private float _foodConsumtion;
    private StatisticsCounter _statisticsCounter;

    void OnValidate()
    {
        Start();
    }

    void Start()
    {
        Time.timeScale = 1f;
        RecalculateFoodConsumption();
        _statisticsCounter = gameObject.GetComponent<StatisticsCounter>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    
    public void RecalculateFoodConsumption()
    {
        var workers = WorkersController.Amount * WorkersController.FoodUpdateAmount /
                      WorkersController.FoodUpdateInterval;
        
        var defenders = DefendersController.Amount * DefendersController.FoodUpdateAmount /
                        DefendersController.FoodUpdateInterval;
        
        FoodController.SetConsumtion(workers + defenders);
    }

    public void GameWon()
    {
        WinPanel.Show();
        WinText.text = $"<b>Вы собрали 777 еды!<br>Теперь и всех зомби можно будет прокормить :)</b><br><br>" +
                            $"<b>Защитников нанято:</b> {_statisticsCounter.DefendersCreated}<br>" +
                            $"<b>Врагов убито:</b> {_statisticsCounter.EnemiesDied}";
    }

    public void GameOver()
    {
        GameOverPanel.Show();
        GameOverText.text = $"<b>Еды собрано:</b> {_statisticsCounter.FoodProduced:F1}<br>" +
                            $"<b>Защитников нанято:</b> {_statisticsCounter.DefendersCreated}<br>" +
                            $"<b>Врагов убито:</b> {_statisticsCounter.EnemiesDied}";
    }

    public void Pause()
    {
        if (PausePanel.IsActive)
        {
            PausePanel.Hide();
        }
        else
        {
            PausePanel.Show();
            PauseText.text = $"<b>Еды собрано:</b> {_statisticsCounter.FoodProduced:F1}<br>" +
                             $"<b>Защитников нанято:</b> {_statisticsCounter.DefendersCreated}<br>" +
                             $"<b>Врагов убито:</b> {_statisticsCounter.EnemiesDied}";
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
