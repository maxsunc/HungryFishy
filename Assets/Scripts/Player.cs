using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    public float size;
    public Movement movement;
    private Camera cam;
    public Transform spawnMidpoint;
    public AudioSource munch;
    public AudioSource hurt;
    public bool isHurt;


    void Start()
    {
        movement = GetComponent<Movement>();
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, 1, 1) * size;
        size = Mathf.Clamp(size, 2f, 10000f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "Enemy")
        {
            // check size and die or 
            Size enemySize = obj.GetComponent<Size>();

            if(enemySize.size > size && !isHurt)
            {
                // Damage and Lower in size
                isHurt = true;
                size -= enemySize.size / 30f;
                AddProperties((-enemySize.size / 30f));
                hurt.Play();
                StartCoroutine(ResetHurtCooldown());
            }

        }
        else if (obj.tag == "Trash" && !isHurt)
        {
            isHurt = true;
            size -= size / 40;
            size--;
            AddProperties((-1 + (-size / 40)));
            Destroy(obj);
            hurt.Play();
            StartCoroutine(ResetHurtCooldown());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "Enemy")
        {
            // check size and die or 
            Size enemySize = obj.GetComponent<Size>();

            if (enemySize.size <= size)
            {
                munch.Play();
                // EAT
                size += enemySize.size / 16f;
                AddProperties(enemySize.size);
                Destroy(obj, 0.02f);
               
            }

        }
    }

    public void AddProperties(float otherSize)
    {
        movement.maxSpeed += otherSize / 30f;
        movement.moveRate += otherSize / 40f;
        movement.ps.startSize += otherSize / 20f;
        ResizeCam(otherSize);
    }

    IEnumerator ResetHurtCooldown()
    {
        yield return new WaitForSeconds(1f);
        isHurt = false;
    }

    void ResizeCam(float amount)
    {
        // every 1 size is 0.5 plus cam size
        cam.orthographicSize += (amount /20f);
        spawnMidpoint.localScale += new Vector3(amount / 60f, amount / 60f, amount / 60f);
    }

}
