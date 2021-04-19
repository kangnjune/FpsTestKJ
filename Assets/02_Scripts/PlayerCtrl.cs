using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h;
    private float v;
    private float r;


    [Header("이동속도 및 회전속도")]
    [Range(3.0f, 10.0f)]

    public float moveSpeed = 8.0f;
    [Range(30.0f , 200.0f)]
    public float turnSpeedvalue = 150.0f;
    private float turnSpeed;
    private Rigidbody rigidBody;

    private Transform tr;

    private Animator anim;

    public float maxHealth = 100.0f;
    public float currHealth;

    public Hpbar healthBar;

    public delegate void PlayerDieHandler();

    public static event PlayerDieHandler OnplayerDie;

    IEnumerator Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        turnSpeed = 0.0f;
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        anim.Play("Idle_Shoot_Ar");

        yield return new WaitForSeconds(0.3f);
        turnSpeed = turnSpeedvalue;

    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        tr.Rotate(Vector3.up * Time.deltaTime * turnSpeed * r);
        PlayAnimation();
        
    }

    void PlayAnimation()
    {
        anim.SetFloat("SpeedH",0.1f);
        anim.SetFloat("SpeedV",0.1f);
    }


    void OnTriggerEnter(Collider coll)
    {
        if (currHealth > 0.0f && coll.CompareTag("PUNCH"))
        {
            currHealth -= 10.0f;
            healthBar.setHealth(currHealth);
            if (currHealth <= 0.0f)
            {
                PlayerDie();
            }
        }
    }
    
    void PlayerDie()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        foreach(GameObject monster in monsters)
        {
            monster.SendMessage("WinMon" , SendMessageOptions.DontRequireReceiver);
        }
        GameManager.instance.IsGameOver = true;
    }

}
