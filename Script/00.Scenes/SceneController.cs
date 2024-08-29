using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public bool reverseAt;

    [SerializeField]
    private string targetScene;

    [SerializeField]
    private string myScene;

    private IEnumerator SceneLoad() {
        var sceneName = SceneManager.GetSceneByName(targetScene);

        if(!sceneName.isLoaded) {
            var addScene = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);

            while (!addScene.isDone) {
                yield return null;
            }
        }
    }

    private IEnumerator SceneUnLoad() {
        var sceneName = SceneManager.GetSceneByName(targetScene);

        if (sceneName.isLoaded) {
            var currentScene = SceneManager.GetSceneByName(myScene);
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("GameController"), currentScene);

            var unloadScene = SceneManager.UnloadSceneAsync(targetScene);

            while (!unloadScene.isDone) {
                yield return null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            var dir = other.transform.position - transform.position;
            Debug.Log("dir " + dir);

            if (dir.x > 0) {
                if (!reverseAt)
                    StartCoroutine(SceneLoad());
                else
                    StartCoroutine(SceneUnLoad());
            } else {
                if(!reverseAt)
                    StartCoroutine(SceneUnLoad());
                else
                    StartCoroutine(SceneLoad());
            }
        }
    }
}
