using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Ins;
    
   public bool dontDestroyOnload = false;
    [SerializeField] Button pauseBTN;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameOverUI;   
    [SerializeField] GameObject gameWinUI;
    public event EventHandler OnReplay;
    private void Awake()
    {
        if(Ins != null && Ins != this)
        {
            Destroy(gameObject);
            return;
        }
        Ins = this;
        if(dontDestroyOnload)
        {
            DontDestroyOnLoad(this);
        }
       
    }
    
    public void PauseGame(){
        Time.timeScale = 0f;
    }
    public void TriggerGameWin()
    {
        Time.timeScale = 0f;
        gameWinUI.SetActive(true);
    }
    public void TriggerGameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }


    public void Quit()
    {
        Application.Quit();
    }
    public void Replay()
    {
        Reset();
        OnReplay?.Invoke(this, EventArgs.Empty);
        SceneManager.LoadScene(1);
        
    }
    public void Continue()
    {
        Time.timeScale = 1f;
    }
    public void Mene()
    {
        Destroy(Player.Ins.gameObject);
        Destroy(AudioController.Ins.gameObject);
        Destroy(UIManager.Ins.gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
      
    }
    public void LoadActiveScene()
    {
        Reset();
        OnReplay?.Invoke(this, EventArgs.Empty);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Reset()
    {
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        pauseUI.SetActive(false);
    }
}
