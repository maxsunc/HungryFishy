using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    [HideInInspector]
    public float size;
    public float reductionFactor = 1f;
    public float range1, range2;
    public float[] speedRange = new float[2];
    private float speed;
    public bool isPlastic;

    void Awake()
    {
        size += Random.Range(range1, range2);
        transform.localScale = new Vector3(1, 1, 1) * size / reductionFactor;
        speed = Random.Range(speedRange[0], speedRange[1]);
        Destroy(this.gameObject, 20f);
    }
    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            transform.Translate(transform.right * Time.deltaTime * speed, Space.World);
        }

        if (isPlastic)
        {

        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isPlastic)
        {
            if (col.gameObject.tag == "Enemy")
            {
                
                // compare size
                Destroy(col.gameObject,0.1f);
                if (col.gameObject.transform.localScale.magnitude >= transform.localScale.magnitude * 3f)
                {
                    Destroy(this.gameObject, 0.1f);
                }
            }
        }
    }

}
