using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage = 20;
    public int speed = 20;
    public GameObject explosionEffectPrefab;
    private Transform target;
    public void SetTagrget(Transform _target)
    {
        this.target = _target;
    }

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed*Time.deltaTime);

		
	}
    void OnTriggerEnter(Collider col)
    {
        
        if(col.tag=="Enemy")
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(effect, 1);
    }
    
}
