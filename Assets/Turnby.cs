using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 往復した値を時間から計算
        var y = Mathf.PingPong(Time.time, 2);

        // y座標を往復させて上下運動させる
        //transform.localPosition = new Vector3(0, y, 0);

        transform.localScale
            = new Vector3(
                Mathf.Sin(Time.time)+1,
                1-Mathf.Sin(Time.time),
                1);
    }
}
