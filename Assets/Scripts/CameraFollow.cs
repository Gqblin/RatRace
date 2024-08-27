using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera mainCamera;
    private PlayerBehavior player;
    [SerializeField] private Vector3 camOffset;
    [SerializeField] Vector4 camBorders;
    [SerializeField] float flySpeed;
    [SerializeField] bool flying = false;

    void Start()
    {
        player = FindObjectOfType<PlayerBehavior>();
        mainCamera = gameObject.GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector3 pViewportPosition = mainCamera.WorldToViewportPoint(player.transform.position);

        if (pViewportPosition.x <= camBorders.x || pViewportPosition.x >= camBorders.z || pViewportPosition.y <= camBorders.y || pViewportPosition.y >= camBorders.w)
        {
            flying = true;
        }

        if (flying)
        {
            Vector3 positionToLerpTo = player.transform.position + camOffset;
            Vector3 currentPosition = Vector3.Lerp(transform.position, positionToLerpTo, flySpeed * Time.deltaTime);
            transform.position = currentPosition;

            Vector2 roundedCurrentPos = new(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y));
            Vector2 roundedPlayerPos = new(Mathf.RoundToInt(positionToLerpTo.x), Mathf.RoundToInt(positionToLerpTo.y));
            if (roundedCurrentPos == roundedPlayerPos) { flying = false; }
        }
    }
}
