using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour {
    [SerializeField] private float waitToLoad;
    private bool reloading;

    void Start() {
        
    }

    void Update() {
        if (reloading) {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Player") {
            other.gameObject.SetActive(false);
            reloading = true;
        }
    }
}
