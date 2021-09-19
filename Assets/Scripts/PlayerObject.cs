using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerObject : NetworkBehaviour
{
    public GameObject human;
    public GameObject ghost;
    public GameObject spotLight;
    

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer) return;

        Debug.Log("Spawning player");
        CmdSpawn(!isServer);

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    void CmdSpawn(bool isHuman)
    {
        GameObject obj = isHuman ? human : ghost;
        GameObject player = Instantiate(obj, 
            transform.position, Quaternion.identity);
        player.transform.position += obj.transform.position;
        if (!isHuman)
        {
            GameObject light = Instantiate(spotLight, player.transform);
            //NetworkServer.Spawn(light);
        }

        NetworkServer.Spawn(player, connectionToClient);
    }

}
