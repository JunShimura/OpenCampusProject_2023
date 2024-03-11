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
        // ‰•œ‚µ‚½’l‚ğŠÔ‚©‚çŒvZ
        var y = Mathf.PingPong(Time.time, 2);

        // yÀ•W‚ğ‰•œ‚³‚¹‚Äã‰º‰^“®‚³‚¹‚é
        //transform.localPosition = new Vector3(0, y, 0);

        transform.localScale
            = new Vector3(
                Mathf.Sin(Time.time)+1,
                1-Mathf.Sin(Time.time),
                1);
    }
}
