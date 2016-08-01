using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    private MoveController mc;
	// Use this for initialization
	void Start () {
        mc = GetComponent<MoveController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, 1 << LayerMask.NameToLayer("CanAttack")))
            {
                mc.MoveTo(hit.collider.transform);  
            }
        }
	}
    
}
