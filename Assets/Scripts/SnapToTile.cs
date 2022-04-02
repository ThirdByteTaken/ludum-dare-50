using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToTile : MonoBehaviour
{
    void Update()
    {
        if (transform.hasChanged)
        {
            var pos = transform.position;
            pos = DevTools.RoundVector3(pos, 32);
            transform.position = pos;
        }
    }
}
