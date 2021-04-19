using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterCtrl : MonoBehaviour
{
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;

    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashDie = Animator.StringToHash("Die");
    public GameObject sparkEffect;
    private float hp = 100.0f;

    public enum STATE {IDLE, ATTACK, TRACE, DIE}
    public STATE state = STATE.IDLE;

    [Range(10,50)]
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;

    public bool isDie = false;

    void OnEnable()
    {


        StartCoroutine(CheckState());
        StartCoroutine(MonsterAction());

    }

    void OnDisable()
    {

    }
    void Awake()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        
    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            if(state == STATE.DIE)
            {
                yield break; // 해당 코루틴을 정지시킴.
            }

            float distance = Vector3.Distance(monsterTr.position, playerTr.position);

            if (distance <= attackDist)
            {
                state = STATE.ATTACK;
            }
            else if (distance <= traceDist)
            {
                state = STATE.TRACE;
            }
            else
            {
                state = STATE.IDLE;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    
    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch (state)
            {
                case STATE.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;
                
                case STATE.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped=false;
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack,false);
                    break;

                case STATE.ATTACK:
                    anim.SetBool(hashAttack , true);
                    break;

                case STATE.DIE:
                    anim.SetTrigger(hashDie);
                    GetComponent<CapsuleCollider>().enabled = false;
                    agent.isStopped = true;
                    isDie = true;
                    yield return new WaitForSeconds(2.0f);
                    this.gameObject.SetActive(false);
                    break;
            }
            
            yield return new WaitForSeconds(0.3f);
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        ContactPoint cont = coll.GetContact(0);
        Vector3 normal = cont.normal;
        Quaternion rot = Quaternion.LookRotation(-normal);

        if (coll.collider.CompareTag("BULLET"))
        {
            anim.SetTrigger(hashHit);
            GameObject spark = Instantiate(sparkEffect, cont.point, rot);
            Destroy(spark ,0.8f);
            Destroy(coll.gameObject);
            hp -=20.0f;
            if (hp<=0.0f)
            {
                state = STATE.DIE;
            }

        }
    }
    public void WinMon()
    {
        StopAllCoroutines();
        agent.isStopped = true;

        anim.SetTrigger("PlayerDie");
    }
}

