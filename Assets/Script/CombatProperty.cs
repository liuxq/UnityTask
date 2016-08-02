using UnityEngine;
using System.Collections;

public class CombatProperty : MonoBehaviour {

    public int HPMax = 100;
    public int HP = 100;
    public int Attack = 5;

    public Transform m_headUI;
    public UISlider m_xue;

    public bool isPlayer;

    private Object m_goDamage;
    private Animation m_animation;
    private AI m_ai;

	// Use this for initialization
	void Start () {
        m_goDamage = Resources.Load("Damage");
        if (!isPlayer)
        {
            m_ai = GetComponent<AI>();
        }
        else
            m_animation = GetComponent<Animation>();
	}
    void LateUpdate()
    {
        m_headUI.rotation = Camera.main.transform.rotation;
    }

    public bool IsDead()
    {
        return HP == 0;
    }
    public bool BeAttacked(int attack)
    {
        if (HP == 0)
            return false;

        HP -= attack;
        if (HP < 0)
        {
            HP = 0;
        }
            
        m_xue.value = (float)HP/HPMax;

        GameObject da = Instantiate(m_goDamage, Vector3.zero, Quaternion.identity) as GameObject;
        da.GetComponent<HurtItem>().ChangeData(attack, transform.position, Camera.main);

        if(HP == 0)
        {
            if (!isPlayer)
            {
                m_ai.SetDead();
            }
            else
                m_animation.CrossFade("Death");
        }
        return true;
    }
}
