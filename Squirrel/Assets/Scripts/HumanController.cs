using UnityEngine;

public class HumanController : MonoBehaviour
{
    private Animator myAnim;
    private Transform target;
    private HumanHealth enemyHealth;
    [SerializeField] private Transform homePos;
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        enemyHealth = GetComponent<HumanHealth>();
    }

    void Update()
    {
        if (enemyHealth != null && (enemyHealth.isDead || enemyHealth.isTransitioningToRage)) return;

        // Si homePos es null, solo seguir al jugador sin volver a ninguna posición
        if (homePos == null)
        {
            FollowPlayer();
        }
        else
        {
            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
            {
                FollowPlayer();
            }
            else if (Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                GoHome();
            }
        }
    }

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void GoHome()
    {
        if (homePos != null)
        {
            myAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
            myAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));
            transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, homePos.position) == 0)
            {
                myAnim.SetBool("isMoving", false);
            }
        }
        else
        {
            // Si homePos es null, no hacer nada (no ir a ninguna posición de inicio)
        }
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }
}
