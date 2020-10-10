using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    public GameObject bullet;
    public int maxBullets;
    private Queue<GameObject> m_bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        _BuildBulletPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void _BuildBulletPool()
    {
        m_bulletPool = new Queue<GameObject>();
        for(int i = 0; i < maxBullets; i++)
        {
            var tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            tempBullet.transform.parent = transform;
            m_bulletPool.Enqueue(tempBullet);
        }
    }

    public GameObject GetBullet(Vector3 position)
    {
        var newBullet = m_bulletPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;        
        return newBullet;
    }

    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        m_bulletPool.Enqueue(returnedBullet);
    }
}
