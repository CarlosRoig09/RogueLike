using UnityEngine;

public class DoorBehaivours : MonoBehaviour
{
    private GameObject _parent;
    [SerializeField]
    private int _doorId;
    private bool _openDoor;
    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent.parent.gameObject;
        _parent.GetComponent<LoadScenari>().OnOpenDoor += Open;
        _openDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void Open()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        _openDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_openDoor&&collision.gameObject.CompareTag("Player"))
            GameObject.Find("Spawner").GetComponent<ControlScenari>().LoadScenari(_doorId);
    }
}
