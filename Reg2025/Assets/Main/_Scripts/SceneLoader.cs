using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadLevelEditorScene()
    {
        SceneManager.LoadScene("LevelEditor");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Reg2025");
    }
}
