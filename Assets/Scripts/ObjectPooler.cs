using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        //최대 몇개까지 게임에 enable할 수 있는가.
        public int size;
    }

    #region Singleton
    public static ObjectPooler instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("there are more than one" + name + ". This is should not be happened!");
        }
        instance = this;
    }

    #endregion

    public List<Pool> pools;

    //큐를 쓰는 이유는 꺼낼때 빠르기 떄문이다.(스택과는 다르게 인간이 볼때 합리적인 순서로 저장되는 자료구조이기도하고) List는 인덱스로 꺼내고, 큐는 그냥 꺼내기 떄문에 더 빠름.
    Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            //각각의 pool을 pools에서 옮길 큐. 오브젝트 풀 하나는 Queue<GameObject>로 표현된다. 
            Queue<GameObject> objectPool = new Queue<GameObject>();


            GameObject parent = new GameObject(pool.prefab.name + "Pool");

            for (int i = 0; i < pool.size; i++)
            {
                //풀 사이즈만큼 오브젝트를 미리 만들어 놓음
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                //인스펙터창 정리하기 위해 묶음
                obj.transform.SetParent(parent.transform);
                //생성한 인스턴스에 대한 레퍼런스를 pool에 넣는다.
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("The tag" + tag + "is not exist in poolDictionary");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        //재사용하기 위해 다시 레퍼런스를 해당 풀에 넣는다.
        poolDictionary[tag].Enqueue(objectToSpawn);


        return objectToSpawn;
    }
}
