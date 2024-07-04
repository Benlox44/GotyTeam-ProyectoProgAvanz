using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyToMenu : MonoBehaviour {
    [SerializeField] private string sceneName;
    [SerializeField] private float delay;

    private void OnDestroy() {
        StartCoroutine(ChangeSceneAfterDelay());
    }

    private IEnumerator ChangeSceneAfterDelay() {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}