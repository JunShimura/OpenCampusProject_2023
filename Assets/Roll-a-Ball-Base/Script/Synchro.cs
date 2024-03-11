using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Synchro : MonoBehaviour
{
    [SerializeField] ThirdPersonController thirdPersonController;
    private Transform movingFloor;
    private Vector3 oldPos, currentPos;

    private void FixedUpdate()
    {
        if (movingFloor != null)
        {
            currentPos = movingFloor.transform.position;
            currentPos = new Vector3(currentPos.x, 0, currentPos.z);
            if (currentPos != oldPos)
            {
                thirdPersonController.floorDirection = (currentPos - oldPos).normalized;
                thirdPersonController.floorSpeed = ((currentPos - oldPos) * 50).magnitude;
                oldPos = currentPos;
            }
        }

        if (movingFloor == null)
        {
            if (thirdPersonController.floorSpeed > 0.01f)
            {
                thirdPersonController.floorSpeed *= 0.95f;
            }
            else
            {
                thirdPersonController.floorSpeed = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            movingFloor = other.transform;
            thirdPersonController.floorDirection = Vector3.zero;
            oldPos = movingFloor.position;
            oldPos = new Vector3(oldPos.x, 0, oldPos.z);
            currentPos = movingFloor.position;
            currentPos = new Vector3(currentPos.x, 0, currentPos.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (movingFloor != null)
            if (other.transform == movingFloor.transform)
            {
                //thirdPersonController.floorSpeed = 0;
                movingFloor = null;
            }
    }
}
