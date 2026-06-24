using UnityEngine;

public class FollowingPlayer : MonoBehaviour
{
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public float cameraSpeed { get; private set; } = 5f;

    private void Update()
    {
        Vector3 dir = Player.position - transform.position;
        Vector3 moveVector = new Vector3(dir.x, dir.y, 0.0f) * Time.deltaTime * cameraSpeed;
        transform.Translate(moveVector);
    }
}
