using UnityEngine;

public class AreaTransitions : MonoBehaviour {
    private CameraController cam;
    public Vector2 newMinPos;
    public Vector2 newMaxPos;
    public Vector3 movePlayer;

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
