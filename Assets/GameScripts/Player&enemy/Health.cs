using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int OriginalHealth = 10;
    private int health;
    private bool isdead = false;
    public DeathEffect Parent = null;
    private PhotonView PV;
    // private Explodable exp;
    public GameObject effect;

    public GameObject BloodEffect;

    public UIBehaviour UI;
    public ArmController Arm;
    private CameraController cam;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        cam.targets.Add(transform);
        if (PV.IsMine)
        {
            health = OriginalHealth;
            UI.OnChange(health, "Health");
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("BulletActive"))
        {
            health -= other.gameObject.GetComponent<Damage>().damage;
            //PhotonNetwork.Instantiate(BloodEffect.name, other.GetContact(0).point,Quaternion.LookRotation(other.GetContact(0).point));
            Destroy(other.gameObject);
            UI.OnChange(health, "Health");
            Debug.Log("Health: " + health);
        }

        if (health <= 0 && !isdead)
        {
            //for quick respawn
            isdead = true;
            Parent.OnDie();
            cam.targets.Remove(transform);
            PhotonNetwork.Instantiate(effect.name, transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death") && other.gameObject.layer != 14)
        {
            isdead = true;
            Parent.OnDie();
            cam.targets.Remove(transform);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
