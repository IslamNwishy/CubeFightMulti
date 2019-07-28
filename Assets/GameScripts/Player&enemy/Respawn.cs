using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform RespawnPoint;
    private Rigidbody2D rb;
    private Vector2 zeroPos = new Vector2(0f, 0f);
    private Vector3 zeroPos2 = new Vector3(0f, 0f, 0f);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death") && other.gameObject.layer != 14)
        {
            rb.velocity = zeroPos;
            int spawnPicker = Random.Range(0, GameSetup.GS.SpawnPoints.Length);
            transform.position = GameSetup.GS.SpawnPoints[spawnPicker].position;
        }
    }
}
