using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    MainMenu,
    Game,
    Settings,
    Rules,
    GameOver
}

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneName.Game.ToString());
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Rules()
    {
        SceneManager.LoadScene(SceneName.Rules.ToString());
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneName.MainMenu.ToString());
    }

    public void Settings()
    {
        SceneManager.LoadScene(SceneName.Settings.ToString());
    }

}
