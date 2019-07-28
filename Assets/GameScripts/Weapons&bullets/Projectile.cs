using UnityEngine;

public class Projectile : MonoBehaviour//, IPooledObject
{

    [SerializeField]
    private float speed;




    [SerializeField]
    Transform ForcePosition;

    public bool UsePhysics = true;
    public bool UseForceAtAPosition = false;
    private Rigidbody2D rb;
    // Use this for initialization

    public void Start()
    {
        if (UsePhysics)
        {
            rb = GetComponent<Rigidbody2D>();
            // if(rb.velocity!=new Vector2(0,0))

            if (UseForceAtAPosition)
            {
                rb.AddForceAtPosition(transform.right * speed, ForcePosition.position);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(transform.right * speed);
            }
        }
    }

    private void Update()
    {
        if (UsePhysics)
            return;

            transform.Translate(Vector2.right * speed * Time.deltaTime);
        
    }

}
