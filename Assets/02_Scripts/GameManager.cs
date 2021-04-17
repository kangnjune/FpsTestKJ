using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public List<Transform> points = new List<Transform>();
    public GameObject monsterPrefab;
    public float createTime = 3.0f;
    public bool IsGameOver = false;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != null)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        GameObject.Find("_SpawnPoints").GetComponentsInChildren<Transform>(points);
        monsterPrefab = Resources.Load<GameObject>("Zombie3");
        StartCoroutine(CreateMonster());

    }


    IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(2.0f);


        while(!IsGameOver)
        {
            int idx = Random.Range(1,points.Count);

            GameObject monster = Instantiate<GameObject>(monsterPrefab);
            monster.name = "Zombie3";
            monster.transform.position = points[idx].position;

            Vector3 dir = points[0].position - points[idx].position;
            Quaternion rot = Quaternion.LookRotation(dir);
            monster.transform.rotation = rot;

            yield return new WaitForSeconds(createTime);
        }
    }
}
