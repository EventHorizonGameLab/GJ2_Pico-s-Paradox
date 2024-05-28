using System.Collections;

using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public delegate void RoomManager(float time);
    public static event RoomManager OnChangingRoom;
    Vector3 targetPos;
    GameObject player;
    [SerializeField] float transitionTime;
    public delegate void Dialogue();
    public event Dialogue OnDialogue;

    private void Awake()
    {
        targetPos = transform.GetChild(0).position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ITeleportable>(out _))
        {
            Debug.Log(targetPos);
            player = other.gameObject;
            OnChangingRoom(transitionTime);
            StartCoroutine(Delay(transitionTime/4));
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.transform.position = targetPos;
        MovePoint.OnChanginRoom(targetPos);
        OnDialogue?.Invoke();
    }
}
