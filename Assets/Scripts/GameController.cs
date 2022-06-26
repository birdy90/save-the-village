using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    /// <summary>
    /// Link to a food controller
    /// </summary>
    [SerializeField] private FoodController FoodController;
    
    /// <summary>
    /// Link to a workers controller
    /// </summary>
    [SerializeField] private UnitsController WorkersController;
    
    /// <summary>
    /// Link to a defenders controller 
    /// </summary>
    [SerializeField] private UnitsController DefendersController;

    /// <summary>
    /// Link to a game over panel
    /// </summary>
    [SerializeField] private PopupController GameOverPanel;
    
    /// <summary>
    /// Link to game over info text
    /// </summary>
    [SerializeField] private TMP_Text GameOverText;

    /// <summary>
    /// Link to a win panel
    /// </summary>
    [SerializeField] private PopupController WinPanel;
    
    /// <summary>
    /// Link to a win panel
    /// </summary>
    [SerializeField] private TMP_Text WinText;

    /// <summary>
    /// Link to a pause panel
    /// </summary>
    [SerializeField] private PopupController PausePanel;
    
    /// <summary>
    /// Link to a pause panel
    /// </summary>
    [SerializeField] private TMP_Text PauseText;

    /// <summary>
    /// Link to a statistics counter
    /// </summary>
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
    
    /// <summary>
    /// Recalculate food consumtion from current number of workers and defenders
    /// </summary>
    public void RecalculateFoodConsumption()
    {
        var workers = WorkersController.Amount * WorkersController.FoodUpdateAmount /
                      WorkersController.FoodUpdateInterval;
        
        var defenders = DefendersController.Amount * DefendersController.FoodUpdateAmount /
                        DefendersController.FoodUpdateInterval;
        
        FoodController.SetConsumtion(workers + defenders);
    }

    /// <summary>
    /// Show win panel
    /// </summary>
    public void GameWon()
    {
        WinPanel.Show();
        WinText.text = $"<b>Вы собрали 777 еды!<br>Теперь и всех зомби можно будет прокормить :)</b><br><br>" +
                            $"<b>Защитников нанято:</b> {_statisticsCounter.DefendersCreated}<br>" +
                            $"<b>Врагов убито:</b> {_statisticsCounter.EnemiesDied}";
    }

    /// <summary>
    /// Show game over panel
    /// </summary>
    public void GameOver()
    {
        GameOverPanel.Show();
        GameOverText.text = $"<b>Еды собрано:</b> {_statisticsCounter.FoodProduced:F1}<br>" +
                            $"<b>Защитников нанято:</b> {_statisticsCounter.DefendersCreated}<br>" +
                            $"<b>Врагов убито:</b> {_statisticsCounter.EnemiesDied}";
    }

    /// <summary>
    /// Show pause panel
    /// </summary>
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

    /// <summary>
    /// Restart the game
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Go to main menu
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
