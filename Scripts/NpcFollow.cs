using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Questa classe permette ad un gameobject specifico di seguire un altro gameobject specificato
public class NpcFollow : MonoBehaviour
{

    public GameObject player;
    public float TargetDistance;
    public float AllowedDistance = 5;
    public float FollowSpeed;
    public RaycastHit Shot;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        {
            TargetDistance = Shot.distance;

            if (TargetDistance >= AllowedDistance)
            {
               // FollowSpeed = 1.1f;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, FollowSpeed);
            }
            else
            {
                FollowSpeed = 0;
            }

        }
    }
}
