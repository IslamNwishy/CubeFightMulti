using UnityEngine;

public class UICharacterController : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        //Orthographic Camera
        //Getting the difference between the mouse position and the game object and setting the angle accordingly
        Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float midpoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;
            Vector3 difference = (MouseRay.origin + MouseRay.direction * midpoint) - transform.position;
            difference.Normalize();
            float RotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, RotZ);

        if (RotZ > 90 || RotZ < -90)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, -RotZ + 180);
            }
    }
}
