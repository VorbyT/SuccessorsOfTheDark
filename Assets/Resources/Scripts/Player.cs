using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Photon.MonoBehaviour {

    public LayerMask Clickable;

    private NavMeshAgent Agent;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!photonView.isMine) return;
        if (Input.GetMouseButton(0))
        {
            var camToCursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(camToCursorRay,out hitInfo, 100f, Clickable))
            {
                Agent.SetDestination(hitInfo.point);
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log(Agent.transform.position);
        var currentHeroPosition = Agent.transform.position;
        var currentHeroRotation = Agent.transform.rotation;
        stream.Serialize(ref currentHeroPosition);
        stream.Serialize(ref currentHeroRotation);
        if (stream.isReading)
        {
            Agent.transform.position = currentHeroPosition;
            Agent.transform.rotation = currentHeroRotation;
        }
        Debug.Log(currentHeroPosition);
    }
}
