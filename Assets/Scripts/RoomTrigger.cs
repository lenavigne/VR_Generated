using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToDesable;
    [SerializeField] private GameObject[] objectsToDestroy;
    [SerializeField] private GameObject[] objectsToEnable;
    //[SerializeField] private Transform posObjectToSpawn;
    //[SerializeField] private GameObject objectToSpawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Leaving Room");
            foreach (GameObject o in objectsToDesable)
            {
                o.SetActive(false);
            }
            if (objectsToDestroy.Length > 0)
            {
                foreach (GameObject o in objectsToDestroy)
                {
                    Destroy(o);
                }
                objectsToDestroy = null;
            }
            foreach (GameObject o in objectsToEnable)
            {
                o.SetActive(true);
            }
            //SpawnBlock(posObjectToSpawn, objectToSpawn);
        }
    }

    public void SpawnBlock(Transform position, GameObject obj)
    {
        GameObject o = Instantiate(
            obj,
            position.position,
            Quaternion.identity);
        o.transform.parent = position;
    }
}
