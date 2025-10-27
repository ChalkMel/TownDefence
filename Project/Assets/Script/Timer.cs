using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Timer : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private TextMeshProUGUI resourseText;
    [SerializeField] private Image resTimer;
    private int resourses;
    private float _currentTime;
    [SerializeField] private float maxTime;
    [SerializeField] private GameObject pauseMenu;


    [SerializeField] private Button peasantButton;
    [SerializeField] private int neededForHirePeasant;
    [SerializeField] private float needPeasantCoef;
    [SerializeField] private TextMeshProUGUI neededForHirePeasantText;
    private int peasantCount = 1;
    [SerializeField] private TextMeshProUGUI peasantStat;
    [SerializeField] private Image peasantTimer;
    [SerializeField] private bool isOnPeasantTimer;
    private float peasantTimerStartTime;
    private float peasantTimerDuration;

    [SerializeField] private Button warriorButton;
    [SerializeField] private int neededForHireWarrior;
    [SerializeField] private float needWarriorCoef;
    [SerializeField] private TextMeshProUGUI neededForHireWarriorText;
    private int warriorCount;
    [SerializeField] private TextMeshProUGUI warriorStat;
    [SerializeField] private Image warriorTimer;
    [SerializeField] private bool isOnWarriorTimer;
    private float warriorTimerStartTime;
    private float warriorTimerDuration;

    [SerializeField] private Button managerButton;
    [SerializeField] private int neededForHireManager;
    [SerializeField] private float needHireManagerCoef;
    [SerializeField] private TextMeshProUGUI neededForHireManagerText;
    private int managerCount = 1;
    [SerializeField] private TextMeshProUGUI managerStat;
    [SerializeField] private Image managerTimer;
    [SerializeField] private bool isOnManagerTimer;
    private float managerTimerStartTime;
    private float managerTimerDuration;

    [SerializeField] private int enemyWillArrive;
    [SerializeField] private int enemyWillArriveOne;
    [SerializeField] private int waveCount = 10;
    [SerializeField] private float enemyRemainTime;
    [SerializeField] private float enemySpawnCoef;
    [SerializeField] private float enemyTimeCoef;
    private float enemyTimerStartTime;
    [SerializeField] private float enemyTimerDuration = 20f;
    [SerializeField] private TextMeshProUGUI enemyWillArriveText;
    [SerializeField] private TextMeshProUGUI waveCountText;
    [SerializeField] private Image enemyTimer;

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private Button playButton;
    void Start()
    {
        playButton.onClick.AddListener(StartGame);
        Time.timeScale = 0;
    }
    void StartGame()
    {
        Time.timeScale = 1;
        StartScreen.SetActive(false);
        _currentTime = maxTime;
        UpdateUI();
        peasantButton.onClick.AddListener(AddPeasant);
        managerButton.onClick.AddListener(AddManager);
        warriorButton.onClick.AddListener(AddWarrior);
        peasantTimer.color = new Color(1, 1, 1, 0);
        warriorTimer.color = new Color(1, 1, 1, 0);
        managerTimer.color = new Color(1, 1, 1, 0);
        enemyTimerStartTime = Time.time;
        enemyWillArriveOne = enemyWillArrive;
    }
    private void ResTimeUpdate()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            resTimer.fillAmount = _currentTime / maxTime;
        }
        else
        {
            resTimer.fillAmount = 1;
            _currentTime = maxTime;
            if (managerCount != 0) 
            {
                resourses += peasantCount * managerCount;
            }
            else { 
                resourses += peasantCount; 
            }
            WarriorEat();
        }
        }
    
    private void AddPeasant()
    {
        if (resourses >= neededForHirePeasant)
        {
            peasantCount++;
            peasantTimer.fillAmount = 1;
            resourses -= neededForHirePeasant;
            neededForHirePeasant = Convert.ToInt32(Math.Ceiling(neededForHirePeasant * needPeasantCoef));
            UpdateUI();
            peasantTimer.color = new Color(1, 1, 1, 1);
            isOnPeasantTimer = true;
            peasantTimerStartTime = Time.time;
            peasantTimerDuration = 2f; 
        }
    }
    private void AddWarrior()
    {
        if(resourses >= neededForHireWarrior)
        {
            warriorCount++;
            warriorTimer.fillAmount = 1;
            resourses -= neededForHireWarrior;
            neededForHireWarrior = Convert.ToInt32(Math.Ceiling(neededForHireWarrior * needWarriorCoef));
            UpdateUI();
            warriorTimer.color = new Color(1, 1, 1, 1);
            isOnWarriorTimer = true;
            warriorTimerStartTime = Time.time;
            warriorTimerDuration = 2f;
        }
    }
    private void AddManager()
    {
        if (resourses >= neededForHireManager)
        {
            managerCount++;
            managerTimer.fillAmount = 1;
            resourses -= neededForHireManager;
            neededForHireManager = Convert.ToInt32(Math.Ceiling(neededForHireManager * needHireManagerCoef));
            UpdateUI();
            managerTimer.color = new Color(1, 1, 1, 1);
            isOnManagerTimer = true;
            managerTimerStartTime = Time.time;
            managerTimerDuration = 2f;
        }
    }
    private void ButtonUpdate()
    {
        if (resourses < neededForHirePeasant+warriorCount|| isOnPeasantTimer)
        {
            peasantButton.enabled = false;
            peasantButton.image.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            peasantButton.enabled = true;
            peasantButton.image.color = new Color(1, 1, 1, 1);
        }
        if (resourses < neededForHireWarrior+warriorCount || isOnWarriorTimer)
        {
            warriorButton.enabled = false;
            warriorButton.image.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            warriorButton.enabled = true;
            warriorButton.image.color = new Color(1, 1, 1, 1);
        }
        if (resourses < neededForHireManager + warriorCount || isOnManagerTimer)
        {
            managerButton.enabled = false;
            managerButton.image.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            managerButton.enabled = true;
           managerButton.image.color = new Color(1, 1, 1, 1);
        }
    }
    void UpdateUI()
    {
        warriorStat.text = warriorCount.ToString();
        neededForHireWarriorText.text = neededForHireWarrior.ToString();
        peasantStat.text = peasantCount.ToString();
        neededForHirePeasantText.text = neededForHirePeasant.ToString();
        managerStat.text = managerCount.ToString();
        neededForHireManagerText.text = neededForHireManager.ToString();
        resourseText.text = $"Resourses: {resourses}\n (-{warriorCount})";
        enemyWillArriveText.text = $"Fans will arrive:{enemyWillArrive}";
        waveCountText.text = $"Waves remain:{waveCount-1}";
    }
    void WarriorEat()
    {
        resourses -= warriorCount;
        UpdateUI();
    }
    void CheckForLose()
    {
        if (peasantCount < 0)
        {
            GameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    void IsPeasantTimerOn()
    {
        if (isOnPeasantTimer)
        {
            float elapsedTime = Time.time - peasantTimerStartTime; 
            float fillAmount = 1 - (elapsedTime / peasantTimerDuration);

            peasantTimer.fillAmount = fillAmount;

            if (elapsedTime >= peasantTimerDuration)
            {
                peasantTimer.fillAmount = 0;
                peasantTimer.color = new Color(1, 1, 1, 0);
                UpdateUI();
                isOnPeasantTimer = false;
            }
        }
    }
    void IsWarriorTimerOn()
    {
        if (isOnWarriorTimer)
        {
            float elapsedTime = Time.time - warriorTimerStartTime;
            float fillAmount = 1 - (elapsedTime / warriorTimerDuration);

            warriorTimer.fillAmount = fillAmount;

            if (elapsedTime >= warriorTimerDuration)
            {
                warriorTimer.fillAmount = 0;
                warriorTimer.color = new Color(1, 1, 1, 0);
                UpdateUI();
                isOnWarriorTimer = false;
            }
        }
    }
    void IsManagerTimerOn()
    {
        if (isOnManagerTimer)
        {
            float elapsedTime = Time.time - managerTimerStartTime;
            float fillAmount = 1 - (elapsedTime / managerTimerDuration);

           managerTimer.fillAmount = fillAmount;

            if (elapsedTime >= managerTimerDuration)
            {
                managerTimer.fillAmount = 0;
                managerTimer.color = new Color(1, 1, 1, 0);
                UpdateUI();
                isOnManagerTimer = false;
            }
        }
    }
    void Wave()
    {
        Debug.Log("Work");
        if (warriorCount >= enemyWillArrive && enemyWillArrive != 0)
        {
            warriorCount -= enemyWillArrive;
        }
        else
        {
            if (warriorCount < enemyWillArrive && enemyWillArrive != 0)
            {
                while (warriorCount > 0 && enemyWillArrive != 0)
                {
                    warriorCount--;
                    enemyWillArrive--;
                }
                if (warriorCount == 0 && enemyWillArrive != 0)
                {
                    while (managerCount > 0 && enemyWillArrive != 0)
                    {
                        managerCount -= 1;
                        enemyWillArrive -= 1;
                    }
                    if (warriorCount == 0 & managerCount == 0 & enemyWillArrive != 0)
                    {
                        while (peasantCount > 0 & enemyWillArrive != 0)
                        {
                            peasantCount -= 3;
                            enemyWillArrive--;
                            CheckForLose();
                        }
                    }
                }
                
            }
        }
        waveCount--;
        enemyWillArriveOne += 1;
        enemyWillArrive = enemyWillArriveOne;
        enemyTimerDuration += 5f + ((10-waveCount)*2);
        UpdateUI();
        CheckForWin();

    }
    void CheckForWin()
    {
        if (waveCount == 0)
        {
            WinScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    void EnemyTimer()
    {
        float elapsedTime = Time.time - enemyTimerStartTime;
        float fillAmount = 1 - (elapsedTime / enemyTimerDuration);

        enemyTimer.fillAmount = fillAmount;

        if (elapsedTime >= enemyTimerDuration)
        {
            enemyTimer.fillAmount = 1;
            Wave();
            UpdateUI();
            enemyTimerStartTime = Time.time;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    void Update()
    {
        ResTimeUpdate();
        ButtonUpdate();
        CheckForLose();
        IsPeasantTimerOn();
        IsWarriorTimerOn();
        IsManagerTimerOn();
        EnemyTimer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
     public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
