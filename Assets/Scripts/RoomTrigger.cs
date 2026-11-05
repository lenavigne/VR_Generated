using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsCurrentRoom;
    [SerializeField] private GameObject[] objectsNextRoom;
    [SerializeField] private GameObject micellium;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Leaving Room 1 ");
            foreach (GameObject o in objectsCurrentRoom)
            {
                o.SetActive(false);
            }
            foreach (GameObject o in objectsNextRoom)
            {
                o.SetActive(true);
            }
        }
    }

    public void SpawnBlock(Vector3 position)
    {
        Instantiate(
        micellium,
        position,
        Quaternion.identity);
    }
}
