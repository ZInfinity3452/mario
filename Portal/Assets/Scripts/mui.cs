using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mui : MonoBehaviour
{
    public Sprite bos;
    public Sprite oos;
    public Sprite bcs;
    public Sprite ocs;

    public Image bip;
    public Image rip;

    public void upb(bool ZmianaBlue)
    {
        if (ZmianaBlue)
        {
            bip.sprite = bos;
        }
        else
        {
            bip.sprite = bcs;
        }
    }

    public void upo(bool ZmianaRed)
    {
        if (ZmianaRed)
        {
            rip.sprite = oos;
        }
        else
        {
            rip.sprite = ocs;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
