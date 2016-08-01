using UnityEngine;
using System.Collections;
using System;

public class UI_MonsterSelect : MonoBehaviour {
    public GameObject MonsterSelectPanel;
    public void CloseOrOpenMonsterSelect()
    {
        MonsterSelectPanel.SetActive(!MonsterSelectPanel.activeSelf);
    }
}
