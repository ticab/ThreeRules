using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    MainMenu,
    Game,
    Settings,
    Rules
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

    public void RulesBack()
    {
        SceneManager.LoadScene(SceneName.MainMenu.ToString());
    }

}
