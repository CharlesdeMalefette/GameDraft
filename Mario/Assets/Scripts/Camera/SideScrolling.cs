using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrolling : MonoBehaviour
{
    private new Camera camera;
    private Transform player;

    public float height = 6.5f;
    public float undergroundHeight = -9.5f;
    public float undergroundThreshold = 0f;
    public bool absoluteFollow = false;


    private float initialPositionX;
    private float cameraHeight;
    public float epsilon;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        player = GameObject.FindWithTag("Player").transform;
        initialPositionX = transform.position.x;
        cameraHeight = camera.orthographicSize;
    }

    private void LateUpdate()
    {
        // Track the player
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        //DEBUG double assignation?
        Vector3 cameraPosition = transform.position;
        if (absoluteFollow)
        {
            cameraPosition.x = player.position.x;
            cameraPosition.y = player.position.y;
        }
        else
        {
            cameraPosition.x = math.max(player.position.x, initialPositionX);
            if (player.position.y > cameraPosition.y + cameraHeight - epsilon)
            {
                cameraPosition.y = player.position.y - cameraHeight + epsilon;
            }
            else if (player.position.y < cameraPosition.y - cameraHeight + epsilon)
            {
                cameraPosition.y = player.position.y + cameraHeight - epsilon;
            }
        }
        transform.position = cameraPosition;
    }

    public void SetUnderground(bool underground)
    {
        // set underground height offset
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }

}
