using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gracz_strza³ : MonoBehaviour
{
	RaycastHit target;
	[SerializeField] Transform gracz;
	[SerializeField] Transform PrefabPortalBlueOpen;
	[SerializeField] Transform PrefabPortalBlueClosed;
	[SerializeField] Transform PrefabPortalRedOpen;
	[SerializeField] Transform PrefabPortalRedClosed;
	Transform PortalPrefab;
	float odstêpStrza³u = 0;
	float nastepnyStrzal;



	[SerializeField] Animator trelo;

	[SerializeField] Transform lufa;
	[SerializeField] Transform prefabBlu;
	[SerializeField] Transform prefabRed;

	public mui mui;

	private Transform _AktualnyBlue;

	public Transform AktualnyBlue
	{
		get { return _AktualnyBlue; }
		set 
		{
			mui.upb(true);
			_AktualnyBlue = value;
		}

	}

	private Transform _AktualnyRed;

	public Transform AktualnyRed
	{
		get { return _AktualnyRed;  } 
		set
		{
			mui.upo(true);
			_AktualnyRed = value;
		}
	}


    // Start is called before the first frame update
    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && Time.time >= nastepnyStrzal)
		{

			nastepnyStrzal = Time.time + odstêpStrza³u;
			Shoot(PortalColor.BLUE);
		}
		if (Input.GetMouseButtonDown(1) && Time.time >= nastepnyStrzal)
		{

			nastepnyStrzal = Time.time + odstêpStrza³u;
			Shoot(PortalColor.RED);
		}
	}
	private void Shoot(PortalColor pc)
	{
		if (Physics.Raycast(gracz.position, gracz.forward, out target))
		{
			if (!CanPlace(target)) return;
            trelo.SetTrigger("triggi");
            if (pc == PortalColor.BLUE)
			{
                if (AktualnyRed != null)
				{
					//1) po³o¿enie i rotacja zamkniêtego czerwonego -- Transform AktualnyRed
					//2) usuwamy zamkniêty czerwony --Destroy(akt...red)
					//3)  tworzymy otwarty czerwony w tym samym miejscu -- Instantiate(PrefabPortalRedOpen, pozycja, rotacja)
					// A jednoczeœnie zmieniamy ju¿ istniej¹cy ORANGE na *OPEN*
					Transform tempTransform = AktualnyRed;
					Destroy(AktualnyRed.gameObject);
					AktualnyRed = Instantiate(PrefabPortalRedOpen, tempTransform.position, tempTransform.rotation);

					PortalPrefab = PrefabPortalBlueOpen;
				}
				else
				{
					PortalPrefab = PrefabPortalBlueClosed;
				}
				// Zamknij stary, jeœli istnieje
				if (AktualnyBlue != null)
				{
					Destroy(AktualnyBlue.gameObject);
				}
			}
			else if (pc == PortalColor.RED)
			{
				if (AktualnyBlue != null)
				{
					Transform tempTransform = AktualnyBlue;
					Destroy(AktualnyBlue.gameObject);
					AktualnyBlue = Instantiate(PrefabPortalBlueOpen, tempTransform.position, tempTransform.rotation);

					PortalPrefab = PrefabPortalRedOpen;
				}
				else
				{
					PortalPrefab = PrefabPortalRedClosed;
				}
				if (AktualnyRed != null)
				{
					Destroy(AktualnyRed.gameObject);
				}

			}
            KulaOgnia(pc,target);
            Debug.DrawRay(gracz.position, gracz.forward * 50f, Color.blue, 3f);
			var portal = Instantiate(PortalPrefab, target.point, Quaternion.identity);
			portal.rotation = Quaternion.LookRotation(target.normal);
			portal.transform.Rotate(90, 0, 0);
            

            if (pc == PortalColor.BLUE)
			{
				AktualnyBlue = portal;
			}
			else
			{
				AktualnyRed = portal;
			}

		}
	}
	private bool CanPlace(RaycastHit hit)
	{
		if (hit.transform.CompareTag("NO SHOOTING"))
		{
			return false;
		}
		return true;
	}
    private void KulaOgnia(PortalColor colorPortal, RaycastHit target) 
    {
        Transform prefabPocisku;
        if(colorPortal == PortalColor.BLUE)
        {
            prefabPocisku = prefabBlu;
        }
		else
		{
            prefabPocisku = prefabRed;
        }
        var kulaognia = Instantiate(prefabPocisku, lufa.position, Quaternion.identity);
        Vector3 kierunek = target.point - lufa.position;
        kulaognia.GetComponent<Rigidbody>().AddForce(kierunek, ForceMode.VelocityChange);

    }
}




