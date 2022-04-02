using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    public static Vector3 RoundVector3(Vector3 vector3, float roundFactor)
    {
        vector3 /= roundFactor;
        vector3 = new Vector3(Mathf.Round(vector3.x), Mathf.Round(vector3.y), Mathf.Round(vector3.z));
        vector3 *= roundFactor;
        return vector3;
    }
}
