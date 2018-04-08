using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	// Use this for initialization
   public List<GameObject> gameobjec = new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            gameobjec.Add(col.gameObject);
        }
    }
   
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            gameobjec.Remove(col.gameObject);
        }
    }
    public float attackRatTime=0.5f;
    private float timer = 0;
    public float damageRate = 70;
    void Start()
    {
        timer = attackRatTime;//1
    }
    public GameObject bulletPrefab;
    public Transform firePosition;
    public bool useLaser = false;
    public Transform head;
    public LineRenderer laserRender;
    public GameObject laserEffect;
    void Update()
    {
        //timer += Time.deltaTime;//2
        if (gameobjec.Count > 0 && gameobjec[0] != null)
        {
            Vector3 targetPosition = gameobjec[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        if (useLaser == false)
        {
            timer += Time.deltaTime;
            if (gameobjec.Count > 0 && timer >= attackRatTime)//
            {
                timer = -1;//1
                Attack();
            }
        }
        else if (gameobjec.Count > 0)
        {
            if (laserRender.enabled == false)
            {
                laserRender.enabled = true;
            }
            if (gameobjec[0] == null)
            {
                UpdateEnemys();
            }
            if (gameobjec.Count > 0)
            {
            
                gameobjec[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
                laserRender.SetPositions(new Vector3[] { firePosition.position, gameobjec[0].transform.position });

                //laserEffect.transform.position = gameobjec[0].transform.position;
                laserEffect.transform.position = gameobjec[0].transform.position;
                laserEffect.SetActive(true);
                //laserEffect.SetActive(true);
                Vector3 pos = this.transform.position;
                pos.y =gameobjec[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        
        //else
        //{
        //    Debug.Log("还没有激光");
        //    laserRender.enabled = false;
        //}
        }
        else
        {
            //Debug.Log("还没有激光");
            laserRender.enabled = false;
            laserEffect.SetActive(false);
        }

    }
    void Attack()
    {
        if(gameobjec[0]==null)
        {
            UpdateEnemys();
        }
        if (gameobjec.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTagrget(gameobjec[0].transform);
        }
        else
        {
            timer = attackRatTime;
        }
    }
    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for(int index=0;index<gameobjec.Count;index++)
        {
            if(gameobjec[index]==null)
            {
                emptyIndex.Add(index);
            }
        }
        for(int i=0;i<emptyIndex.Count;i++)
        {
            gameobjec.RemoveAt(emptyIndex[i] - i);
        }
    }
}
