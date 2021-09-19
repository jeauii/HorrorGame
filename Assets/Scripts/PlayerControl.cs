using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    private CharacterController controller;

    private Transform origin;
    private Text toggleText;

    public float speed;
    public float tolerance;
    public float actionRange;

    public LayerMask interactive;

    void Start()
    {
        if (!hasAuthority) return;

        controller = GetComponent<CharacterController>();
       
        Camera camera = FindObjectOfType<Camera>();
        Canvas canvas = FindObjectOfType<Canvas>();
        origin = camera.transform;
        toggleText = canvas.GetComponentsInChildren<Text>()[1];

        controller.enabled = true;

    }

    void Update()
    {
        if (!hasAuthority) return;

        speed = Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift) ?
            4.0f : 2.0f;

        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        Vector3 displacement = transform.right * xAxis +
            transform.forward * zAxis;
        controller.Move(displacement * speed * Time.deltaTime);
        
        Vector3 halfExtents = new Vector3(tolerance, tolerance, 0);
        if (Physics.BoxCast(origin.position, halfExtents, origin.forward,
            out RaycastHit hitInfo, origin.rotation, actionRange, interactive))
        {
            toggleText.enabled = true;
            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("Interactive detected: " + hitInfo.collider.name);
                CmdSwitch(hitInfo.transform.gameObject);
            }
        }
        else
        {
            toggleText.enabled = false;
        }
    }

    [Command]
    void CmdSwitch(GameObject obj)
    {
        Interactive interactive = 
            obj.GetComponent<Interactive>();
        interactive.Switch();

        RpcSwitch(obj);
    }

    [ClientRpc]
    void RpcSwitch(GameObject obj)
    {
        if (isServer) return;

        Interactive interactive = 
            obj.GetComponent<Interactive>();
        interactive.Switch();
        
    }
}