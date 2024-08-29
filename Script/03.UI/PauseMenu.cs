using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // 플레이어 esc 눌렀을 때 발동시킬지, 그냥 버튼만들어서 할지, 아니면 둘다할지 나중에 생각해봄 코드만 만들어둠.
    public GameObject pauseMenuUI;



    public void GameResume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

    }

    public void GamePause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

    }

    public void ReturnMain()
    {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
