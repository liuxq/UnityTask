using UnityEngine;
using System.Collections;
using System;

public class UI_MonsterSelect : MonoBehaviour {
    public void CloseMonsterSelect()
    {
        this.gameObject.SetActive(false);
    }
}
