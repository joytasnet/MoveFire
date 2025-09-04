using UnityEditor.Callbacks;
using UnityEngine;

public class MoveAndFire : MonoBehaviour
{
    [Header("Refs")]
    public GameObject bullet;

    [Header("Params")]
    public float moveSpeed = 1f;
    public float fireInterval = 0.12f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool isFiring = false;
    float fireTimer = 0f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(hor, ver, 0);
        if (dir.sqrMagnitude > 1f) dir.Normalize();

        Vector3 v = rb.linearVelocity;
        v.x = dir.x * moveSpeed;
        v.y = dir.y * moveSpeed;
        rb.linearVelocity = v;

        if (Input.GetKeyDown(KeyCode.G))
        {
            isFiring = true;
            fireTimer = fireInterval;
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            isFiring = false;
            fireTimer = 0f;
        }

        if (isFiring)
        {
            fireTimer += Time.deltaTime;
            while (fireTimer > fireInterval)
            {
                Fire();
                fireTimer -= fireInterval;
            }
        }

    }
    void Fire()
    {
        Instantiate(bullet, rb.position, Quaternion.Euler(0, 0, 90));
    }
}
