using UnityEngine;
using System.Collections;
using System;

public class UI_SelectItem : MonoBehaviour {

    public Transform player;
    public void SelectMonster()
    {
        int index = Int32.Parse(gameObject.name);
        if (GameObject.Find("Monster" + index))
            player.GetComponent<MoveController>().MoveTo(GameObject.Find("Monster" + index).transform);
    }
}
