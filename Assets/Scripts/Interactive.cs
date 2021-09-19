using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Interactive : NetworkBehaviour
{
    
    protected bool status;

    public abstract void Switch();
}
