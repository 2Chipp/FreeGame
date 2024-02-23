using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] 
    Transform player;

    [SerializeField]
    Transform target;
    
    public Vector3 axisX;
    public Vector3 axisZ;
    Vector3 distance;
    private void Start()
    {
        distance = transform.position - player.position;
    }
    private void Update()
    {
        CamData();
    }
    private void LateUpdate()
    {
        CamRot();
    }
    void CamData ()        
    {
        axisX = transform.right;
        axisZ = transform.forward;
        axisX.y = 0;
        axisZ.y = 0;
        axisX = axisX.normalized;
        axisZ = axisZ.normalized;
    }
    void CamRot() 
    {
        distance = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 2, Vector3.up) * distance;
        transform.position = player.position + distance;
        transform.LookAt(target);
    }
}
