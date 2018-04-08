using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuildManager : MonoBehaviour
{
    public TurretDate laserTurretData;
    public TurretDate missileTurretData;
    public TurretDate standardTurretData;
    public TurretDate selectedTurretData;
    public GameObject upgradeCanvas;
    public Button buttonUpgade;
    public int money = 1000;
    private MapCup selectedMapCub;
    public Text moneyText;
    private Animator upgradeCanvasAniamtion;
    void ChangeMoney(int change )
    {
        money += change;
        moneyText.text = "$" + money;

    }
    void Start()
    {
        upgradeCanvasAniamtion = upgradeCanvas.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray,out hit,1000, LayerMask.GetMask("MapCup"));
                if (isCollider)
                {
                    MapCup mapCup = hit.collider.GetComponent<MapCup>();
                    if (selectedTurretData!=null&&mapCup.turretGo == null)
                    {
                        if (money > selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapCup.BuildTurret(selectedTurretData);
                        }
                        else 
                        {

                        }
                    }
                    else if(mapCup.turretGo!=null)
                    {
                        if(mapCup.isUpgrade)
                        {
                            ShowUpgardeBtn(mapCup.transform.position, true);
                        }
                        else
                        {
                            //Debug.Log("Ui出现");
                            ShowUpgardeBtn(mapCup.transform.position, false);
                        }
                        if(mapCup==selectedMapCub&&upgradeCanvas.activeInHierarchy)
                        {
                          
                            StartCoroutine( HideUpgadeBtn());
                        }
                        selectedMapCub = mapCup;
                       
                    }
                }
            }
        }
    }
    void ShowUpgardeBtn(Vector3 pos1,bool isDisable=false)
  
    {
        StopCoroutine("HideUpgadeBtn");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        buttonUpgade.interactable = !isDisable;
        upgradeCanvas.transform.position = pos1;
        
    }
    IEnumerator HideUpgadeBtn()
    {
        upgradeCanvasAniamtion.SetTrigger("Hide");
     
        yield return new WaitForSeconds(0.8f);
        upgradeCanvas.SetActive(false);
    }
    public void OnUpgradeButtonDown()
    {
        if(money>selectedMapCub.turretData.costUpgraded)
        {
            ChangeMoney(-selectedMapCub.turretData.costUpgraded);
            selectedMapCub.UpgradeTurret();
            StartCoroutine(HideUpgadeBtn());
        }
        else
        {
            
        }
    
    }
    public void OnDestroyButtonDown()
    {
        selectedMapCub.DestroyTurret();
        StartCoroutine(HideUpgadeBtn());
    }
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardTurret(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }

    }
}
