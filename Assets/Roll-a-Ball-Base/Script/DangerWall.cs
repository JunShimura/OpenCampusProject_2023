using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerWall : MonoBehaviour
{

    //オブジェクトと接触した時に呼ばれるコールバック
    private void OnCollisionEnter(Collision hit)
    {
        //接触したオブジェクトのタグが"Player"だった場合
        if (hit.gameObject.CompareTag("Player") && GameController.count > 0) {
            hit.transform.root.gameObject.GetComponent<PlayerController>().SetBroken();
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        //接触したオブジェクトのタグが"Player"だった場合
        if (hit.gameObject.CompareTag("Player") && GameController.count > 0)
        {
            hit.transform.root.gameObject.GetComponent<PlayerController>().SetBroken();
        }
    }
}
