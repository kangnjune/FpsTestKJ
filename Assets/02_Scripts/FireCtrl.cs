using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;
    
    public MeshRenderer muzzleFlash;
    [HideInInspector]
    public AudioSource audioSource;
    public AudioClip fireSfx;



        void Start()
    {
        audioSource = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    
    void Fire()
    {
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        StartCoroutine(SHowMuzzleFlash());
        audioSource.PlayOneShot(fireSfx, 0.8f);
    }
    IEnumerator SHowMuzzleFlash()
    {
        
        Vector2 offset = new Vector2(Random.Range(0,2),Random.Range(0,2))*0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        Quaternion rot = Quaternion.Euler(Vector3.forward*Random.Range(0 ,360 ));
        muzzleFlash.transform.localRotation = rot;

        muzzleFlash.transform.localScale = Vector3.one*Random.Range(1.0f,3.0f);
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.enabled = false;
    }
}
