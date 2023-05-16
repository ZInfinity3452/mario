using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ball : MonoBehaviour
{
    public strzalgracza s;
    [SerializeField] PortalColor pc;
    RaycastHit target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 kierunek = collision.contacts[0].point - transform.position;
        if (Physics.Raycast(transform.position, kierunek, out target))
        {
            s.WystrzalPortalu(pc, target);
        }
    }
    private void OnEnable()
    {
        s = GameObject.FindObjectOfType<strzalgracza>();    
    }
}
