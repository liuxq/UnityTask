using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    private Animation ani;
    private SmoothFollow sf;
    private CharacterController cc;
    private Vector2 pos;
    private bool isPressDown;
    private CombatProperty cp;
    private bool isAutoMove = false;
    private Transform moveDes;
    private CombatProperty combat;
    private Object m_skill;

    void OnEnable()
    {
        Debug.Log("Registering finger gesture events from C# script");

        // register input events
        FingerGestures.OnFingerDown += OnFingerDown;
        FingerGestures.OnFingerUp += OnFingerUp;
    }

    void OnDisable()
    {
        // unregister finger gesture events
        FingerGestures.OnFingerDown -= OnFingerDown;
        FingerGestures.OnFingerUp -= OnFingerUp;
    }

    void Start()
    {
        isAutoMove = false;
        m_skill = Resources.Load("Skill1");
        combat = GetComponent<CombatProperty>();
        ani = GetComponent<Animation>();
        sf = Camera.main.GetComponent<SmoothFollow>();
        sf.target = transform;
        cc = GetComponent<CharacterController>();
        cp = GetComponent<CombatProperty>();
        sf.ResetView();
    }
    void OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        isAutoMove = false;
        isPressDown = true;
        pos = fingerPos;
    }
    void OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        isPressDown = false;
        if (!ani.IsPlaying("Skill") && !cp.IsDead())
            ani.CrossFade("Idle");
    }
    void Update()
    {
        if(!cc.isGrounded)
        {
            cc.Move(Vector3.down);
        }
        if (isPressDown && Input.mousePosition.x != pos.x && Input.mousePosition.y != pos.y && !ani.IsPlaying("Skill") && !cp.IsDead())
        {
            //设置角色的朝向（朝向当前坐标+摇杆偏移量）
            Quaternion r = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

            Vector3 dir = r * new Vector3(Input.mousePosition.x - pos.x, 0, Input.mousePosition.y - pos.y);
            transform.LookAt(new Vector3(transform.position.x + dir.x, transform.position.y, transform.position.z + dir.z));

            //移动玩家的位置（按朝向位置移动）
            //transform.Translate(Vector3.forward * Time.deltaTime * 2);
            dir.Normalize();
            cc.Move(dir * Time.deltaTime * 3);
            //移动摄像机
            sf.FollowUpdate();
            //播放奔跑动画
            //ani.wrapMode = WrapMode.Loop;
            ani.CrossFade("Walk");
        }

        if (isAutoMove)
        {
            if (Vector3.Distance(transform.position, moveDes.position) < 6)
            {
                isAutoMove = false;
                if (!ani.IsPlaying("Skill") && !combat.IsDead())
                {

                    if (moveDes.GetComponent<CombatProperty>().BeAttacked(combat.Attack))
                    {
                        transform.LookAt(moveDes);
                        ani.CrossFade("Skill");
                        Instantiate(m_skill, transform.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                transform.LookAt(moveDes);
                cc.Move(transform.localToWorldMatrix * Vector3.forward * Time.deltaTime * 3);
                //移动摄像机
                sf.FollowUpdate();
                //播放奔跑动画
                ani.CrossFade("Walk");
            }
        }
        
    }

    //移动，直到操作或者到达指定距离，然后释放技能
    public void MoveTo(Transform des)
    {
        isAutoMove = true;
        moveDes = des;
    }
   

}
