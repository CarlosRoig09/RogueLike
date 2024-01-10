using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorBehaivours : MonoBehaviour
{
    private GameObject _parent;
    [SerializeField]
    private int _doorId;
    private bool _openDoor;
    private TilemapRenderer _renderer;
    private Collider2D _collider;
    // Start is called before the first frame update
    void Awake()
    {
        _parent = transform.parent.parent.gameObject;
        _parent.GetComponent<LoadScenari>().OnOpenDoor += Open;
        _openDoor = false;
        _renderer= GetComponent<TilemapRenderer>();
        _collider= GetComponent<Collider2D>();
        _collider.enabled = false;
        _renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void Open(int id)
    {

        _openDoor = true;
        _collider.enabled = true;
        _renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        // _parent.GetComponent<LoadScenari>().OnOpenDoor -= Open;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_openDoor && collision.gameObject.CompareTag("Player")) {
            Debug.Log(transform.parent.parent.name);
        GameObject.Find("Spawner").GetComponent<ControlScenari>().LoadScenari(_doorId);
    }
   }
    private void OnDestroy()
    {
        _parent.GetComponent<LoadScenari>().OnOpenDoor -= Open;
        Destroy(transform.parent.gameObject);
    }
}
