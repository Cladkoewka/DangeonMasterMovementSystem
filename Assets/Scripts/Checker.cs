using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public bool CheckWallCollisions()
    {
        Collider[] collision = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider collider in collision)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Walls"))
                return true;
        }
        return false;
    }
}
