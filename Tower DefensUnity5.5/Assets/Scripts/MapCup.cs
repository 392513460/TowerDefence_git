using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapCup : MonoBehaviour {
    [HideInInspector]
    public GameObject turretGo;
    public GameObject buildEffect;
    private Renderer render;
   public TurretDate turretData;
    [HideInInspector]
    public bool isUpgrade = false;
    public void UpgradeTurret()
    {
        if(isUpgrade==true)
        {
            return;

        }
        Destroy(turretGo);
        isUpgrade = true;
        turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgrade = false;
        turretGo = null;
        turretData = null;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
    public void BuildTurret(TurretDate turretData)
    {
        this.turretData = turretData;
        isUpgrade = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
    }
	// Use this for initialization
	void Start () {
        render=GetComponent<Renderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseEnter()
    {
        //Debug.Log("dd");
        if(turretGo==null&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            render.material.color = Color.red;
        }
    }
    void OnMouseExit()
    {
        render.material.color = Color.white;
    }
}
