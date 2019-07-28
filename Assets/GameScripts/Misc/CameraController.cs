using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 10f;
    public Vector3 Velocity;
    private Camera cam;



    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref Velocity, smoothTime);
    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    //for now
    float GetGreatestDistance()
    {

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            //for now
            if (targets[i] == null)
            {
                Destroy(targets[i]);
            }
            else
                bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.y;
    }

    //for now
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
            return targets[0].position;
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
            //for now
            if (targets[i] == null)
            {
                Destroy(targets[i]);

            }
            else
                bounds.Encapsulate(targets[i].position);

        return bounds.center;
    }
}
