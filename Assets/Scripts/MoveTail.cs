using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTail : MonoBehaviour
{
    int count = 0;
    bool inv = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inv)
        {
            transform.Rotate(0.5f, 0, 0);
            count++;
            if (count >= 12)
            {
                inv = true;
            }
        }
        else
        {
            transform.Rotate(-0.5f, 0, 0);
            count--;
            if (count <= -12)
            {
                inv = false;
            }
        }
    }
}
