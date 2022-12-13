using UnityEngine;

public class DestroyableObstacle : MonoBehaviour, IDestroyable
{
    [SerializeField]
    private float _Maxlife;
    private float _life;
    [SerializeField]
    private DropegableItems _items;

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
        foreach (var item in _items.Items)
        {
            var random = Random.Range(1, 101);
            Debug.Log(random);
            if(random<= item.RateAperance)
            {
                Instantiate(item.prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                return true;
            }
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
