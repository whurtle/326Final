using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float lerpSpeed = 5;

    public float zOffset = -19;

    private Camera cam;

    private void Start()
    {
        this.cam = GetComponent<Camera>();
    }

    #region Monobehaviour API
    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (cam.orthographicSize + Input.mouseScrollDelta.y * -1 > 0)
            {
                cam.orthographicSize += Input.mouseScrollDelta.y * -1;
            }
        }
    }

    void LateUpdate()
    {
        var currentPosition = transform.position;

        var targetPosition = new Vector3(target.position.x, currentPosition.y, target.position.z + zOffset);

        transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * lerpSpeed);
    }

    #endregion
}
