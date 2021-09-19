using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurvival : MonoBehaviour
{
    public int maxHealth = 100;

    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Kill();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "SmokeCloud")
        {
            health = 0;
        }
    }

    void Kill()
    {
        Debug.Log("Player is killed");
        FindObjectOfType<PlayerControl>().enabled = false;
    }
}
