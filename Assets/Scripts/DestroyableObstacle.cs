using UnityEngine;

public class DestroyableObstacle : MonoBehaviour, IDestroyable
{
    [SerializeField]
    private float _Maxlife;
    private float _life;
    [SerializeField]
    private DropegableItems _items;
    [SerializeField]
    private float _chanceOfDropNothing;

    // Start is called before the first frame update
    void Start()
    {
        _life = _Maxlife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroyed()
    {
        DropAnObject();
        Destroy(gameObject);
    }

    public bool DropAnObject()
    {
        var item = RandomMethods.ReturnARandomObject(_items.Items, _chanceOfDropNothing, _items.Items.Length, 0);
        if (item > -1)
        {
            Instantiate(_items.Items[item].prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            return true;
        }
        return false;
    }

    public void GetHitByPlayer(float damage)
    {
        _life -= damage;
        if (_life <= 0)
            Destroyed();
    }
}
