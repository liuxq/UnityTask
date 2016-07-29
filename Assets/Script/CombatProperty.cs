using UnityEngine;
using System.Collections;

public class CombatProperty : MonoBehaviour {

    public int HPMax = 100;
    public int HP = 100;
    public int Attack = 5;

    public Transform m_headUI;
    public UISlider m_xue;

    private Object m_goDamage;

	// Use this for initialization
	void Start () {
        m_goDamage = Resources.Load("Damage");
	}
    void LateUpdate()
    {
        m_headUI.rotation = Camera.main.transform.rotation;
    }

    public void BeAttacked(int attack)
    {
        HP -= attack;
        if (HP < 0)
            HP = 0;

        m_xue.value = (float)HP/HPMax;

        GameObject da = Instantiate(m_goDamage, Vector3.zero, Quaternion.identity) as GameObject;
        da.GetComponent<HurtItem>().ChangeData(attack, transform.position, Camera.main);
    }
}
