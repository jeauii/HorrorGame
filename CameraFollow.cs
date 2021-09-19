using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Range(0.01f,1.0f)]
    public float smoothSpeed = 0.5f;
    public Vector3 offset;

    void Start()
    {
      transform.position = target.position + offset;
      transform.rotation = target.rotation;
    }
      void LateUpdate()
    {
      //Vector3 newPos = target.position + offset;
      //transform.position = Vector3.Slerp(transform.position, newPos,smoothSpeed);
      //transform.rotation = target.rotation;
    }
}
