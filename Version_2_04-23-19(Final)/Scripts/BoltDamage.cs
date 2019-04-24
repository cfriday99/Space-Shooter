using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltDamage : MonoBehaviour
{
    public float dmg;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.transform.SendMessage("Damage", dmg);
        }
    }
}
