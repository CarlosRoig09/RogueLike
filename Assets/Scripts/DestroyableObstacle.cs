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
        float minRange=0;
        var random = Random.Range(minRange, _items.Items.Length*10+_chanceOfDropNothing);
        for (var i = 0; i < _items.Items.Length; i++)
        {
            Debug.Log(random);
            if (random >= minRange && random <= _items.Items[i].RateAperance / _items.Items.Length * (i+1))
            {
                Instantiate(_items.Items[i].prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                return true;
            }
            else
                minRange = _items.Items[i].RateAperance / _items.Items.Length;
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
