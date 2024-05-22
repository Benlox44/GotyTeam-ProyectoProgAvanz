using UnityEngine;

public class HurtEnemy : MonoBehaviour {



    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") Destroy(other.gameObject);
    }
}
