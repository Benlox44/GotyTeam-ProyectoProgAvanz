using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private Animator animator;
    [SerializeField] private float speed;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

        animator.SetFloat("moveX", rigidBody.velocity.x);
        animator.SetFloat("moveY", rigidBody.velocity.y);
        
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
}