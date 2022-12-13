using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnControler : MonoBehaviour
{
    private List<GameObject> _enemySpawned;

    private void Start()
    {
        _enemySpawned = new List<GameObject>();
    }
    public void EnemySpawn(GameObject enemy, float number)
    {
        for (int i = 0; i < number; i++)
            _enemySpawned.Add(ControlInstancePosition(enemy));
    }
    public void CurrentEnemy(ref float currentEnemies)
    {
        foreach (var enemy in _enemySpawned)
        {
            if (enemy.GetComponent<Enemy>().State == Life.Death)
            {
                currentEnemies -= 1;
                _enemySpawned.Remove(enemy);
            }
        }
    }

    private GameObject ControlInstancePosition(GameObject enemy)
    {
        Vector3 position;
        do
        {
            position = new Vector3(Random.Range(-32.4f, 12.62f),Random.Range(-8.75f,10.42f));
        } while (GameObjectInThatPosition(position));
        return Instantiate(enemy);
    }
    private bool GameObjectInThatPosition(Vector3 position)
    {
        var hitColliders = Physics.OverlapSphere(position, 2);
        if (hitColliders.Length > 0)
            return true;
        return false;
    }
}
