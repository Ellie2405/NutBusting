using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Vector3 posToLookAt;
    // Start is called before the first frame update
    void Start()
    {
        AdjustCamera();
    }

    [ContextMenu("AdjustCamera")]
    void AdjustCamera()
    {
        transform.LookAt(posToLookAt);
    }
}
