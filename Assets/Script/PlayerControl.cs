﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    private Animation ani;
    private CombatProperty combat;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animation>();
        combat = GetComponent<CombatProperty>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!ani.IsPlaying("Skill") && Physics.Raycast(ray, out hit, 100.0f, 1 << LayerMask.NameToLayer("CanAttack")))
            {
                //ui_target.GE_target = hit.collider.GetComponent<GameEntity>();
                transform.LookAt(hit.collider.transform);
                ani.CrossFade("Skill");
                hit.collider.GetComponent<CombatProperty>().BeAttacked(combat.Attack);
            }
        }
	}
    
}