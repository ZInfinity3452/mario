using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class strzalgracza : MonoBehaviour
{
    [SerializeField] Transform graczpozycja;
    RaycastHit target;
    Transform PortalPrefab;
    [SerializeField] Transform Prefabblz;
    [SerializeField] Transform Prefabblo;
    [SerializeField] Transform Prefabrdz;
    [SerializeField] Transform Prefabrdo;
    [SerializeField] Animator trigger;
    [SerializeField] Animator onetrigger;
    [SerializeField] Transform lufa1;
    [SerializeField] Transform pociskzielony;
    [SerializeField] Transform pociskpomaracnczowy;
    private Transform AktualnyBlue;
    private Transform AktualnyRed;
    public mui mui;
    public Transform _AktualnyBlue
    { 
    get{ return AktualnyBlue; }
    set
    { 
            mui.upb(true);
            AktualnyBlue = value;}
    }
    public Transform _AktualnyRed
    {
        get { return AktualnyRed; }
        set
        {
            mui.upo(true);
            AktualnyRed = value;
        }
    }



    float maxodstepstrzalu;
    float odstepstrzalu = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }
    private bool canPlace(RaycastHit target)
    {
        if (target.transform.CompareTag("niestrzelac"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            trigger.SetTrigger("przycisk");
            Shoot(PortalColor.BLUE);
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            onetrigger.SetTrigger("przycisk");
            Shoot(PortalColor.RED);
        }
    }
    private void Shoot(PortalColor pc)
    {
        if (Physics.Raycast(graczpozycja.position, graczpozycja.forward, out target))
        {
            shootball(pc, target);
        
            if (canPlace(target)) 
            {
                if (pc == PortalColor.BLUE)
                {
                    if (AktualnyRed != null)
                    {
                        Transform tempTransform = AktualnyRed;
                        Destroy(AktualnyRed.gameObject);
                        AktualnyRed = Instantiate(Prefabrdo, tempTransform.position, tempTransform.rotation);
                        PortalPrefab = Prefabblo;
                    }
                    else
                    {
                        PortalPrefab = Prefabblz;
                    }
                    if (AktualnyBlue != null)
                    {
                        Destroy(AktualnyBlue.gameObject);
                    }
                }

                if (pc == PortalColor.RED)
                {
                    if (AktualnyBlue != null)
                    {
                        Transform tempTransform = AktualnyBlue;
                        Destroy(AktualnyBlue.gameObject);
                        AktualnyBlue = Instantiate(Prefabblo, tempTransform.position, tempTransform.rotation);
                        PortalPrefab = Prefabrdo;
                    }

                    else
                    {
                        PortalPrefab = Prefabrdz;
                    }
                    if (AktualnyRed != null)
                    {
                        Destroy(AktualnyRed.gameObject);
                    }
                }

                
            }
            print("strzxal");
        }
    }

    private void shootball(PortalColor colorPortal, RaycastHit target)
    {
        Transform prefabPocisku;
        if (colorPortal == PortalColor.RED)
        {
            prefabPocisku = pociskzielony;
        }
        else
        {
            prefabPocisku = pociskpomaracnczowy;
        }
        var ball = Instantiate(prefabPocisku, lufa1.position, Quaternion.identity);
        Vector3 kierunek = (target.point - lufa1.position)*2;
        ball.GetComponent<Rigidbody>().AddForce(kierunek, ForceMode.VelocityChange);
    }
    public void WystrzalPortalu (PortalColor pc, RaycastHit target)
    {

        if (Time.time >= maxodstepstrzalu)
        {
            Debug.DrawRay(graczpozycja.position, graczpozycja.forward * 50f, Color.magenta, 5f);
            var portalvar = Instantiate(PortalPrefab, target.point, Quaternion.identity);
            portalvar.rotation = Quaternion.LookRotation(target.normal);
            portalvar.transform.Rotate(0, 90, 0);
            maxodstepstrzalu = Time.time + odstepstrzalu;
            if (pc == PortalColor.BLUE)
            {
                AktualnyBlue = portalvar;
                print(AktualnyBlue);
            }
            if (pc == PortalColor.RED)
            {
                AktualnyRed = portalvar;
            }
        }
    }

}
