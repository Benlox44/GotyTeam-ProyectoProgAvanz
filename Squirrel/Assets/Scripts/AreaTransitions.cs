using UnityEngine;

public class AreaTransitions : MonoBehaviour {
    private CameraController cam;
    [SerializeField] private Vector2 newMinPos;
    [SerializeField] private Vector2 newMaxPos;
    [SerializeField] private Vector3 movePlayer;

    void Start() {
        cam =  Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            cam.minPosition = newMinPos;
            cam.maxPosition = newMaxPos;
            other.transform.position += movePlayer;
        }   
    }
}