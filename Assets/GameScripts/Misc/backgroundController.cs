using UnityEngine;

public class backgroundController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float StretchSpeed = 1f;
    public float EndThreshhold = 10f;
    void Start()
    {
        transform.localScale = Vector3.one;
        rb = GetComponent<Rigidbody2D>();
        rb.rotation = Random.Range(0, 360);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * StretchSpeed * Time.deltaTime;

        if (transform.localScale.x >= EndThreshhold)
        {
            transform.localScale = Vector3.one;
            rb.rotation = Random.Range(0, 360);
        }
    }




}
