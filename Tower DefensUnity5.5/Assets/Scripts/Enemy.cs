using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour {
    public Transform[] positions;
    public int index = 0;//初始索引值,使敌人依照索引值进行移动
    public float speed = 10;
    public float  hp;

    public GameObject explosionEffect;
    public Slider hpSlider;
    public float totalHp;
	// Use this for initialization
    void Start()
   {
        totalHp = hp;
        positions = WayPoints.positions;
        hpSlider = GetComponentInChildren<Slider>();
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
		
	}
    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        //如果敌人超出索引界限则返回
       
            
        //使用索引位置-当前位置并取得单位向量*速度
        if((Vector3.Distance(positions[index].position,transform.position)<0.2f))
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
 
    }
    void ReachDestination()
    {
        GameManager.instance.Failed();
        GameObject.Destroy(this.gameObject);
        //OnDestroy();
    }
    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
      
        
        
        //GameObject.Destroy(this.gameObject);
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpSlider.value =(float)hp / totalHp;
        if(hp<=0)
        {
            Die();
        }
    } 
   void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }
    }

    

