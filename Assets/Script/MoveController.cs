using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    private Animation ani;
    private SmoothFollow sf;
    private CharacterController cc;
    private Vector2 pos;
    private bool isPressDown;

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
        ani = GetComponent<Animation>();
        sf = Camera.main.GetComponent<SmoothFollow>();
        cc = GetComponent<CharacterController>();
        sf.ResetView();
    }
    void OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        isPressDown = true;
        pos = fingerPos;
    }
    void OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        isPressDown = false;
        ani.CrossFade("Idle");
    }
    void Update()
    {
        if(isPressDown)
        {
            //设置角色的朝向（朝向当前坐标+摇杆偏移量）
            Quaternion r = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

            Vector3 dir = r * new Vector3(Input.mousePosition.x - pos.x, 0, Input.mousePosition.y - pos.y);
            transform.LookAt(new Vector3(transform.position.x + dir.x, transform.position.y, transform.position.z + dir.z));

            //移动玩家的位置（按朝向位置移动）
            //transform.Translate(Vector3.forward * Time.deltaTime * 2);
            dir.Normalize();
            cc.Move(dir * Time.deltaTime * 2);
            //移动摄像机
            sf.FollowUpdate();
            //播放奔跑动画
            //ani.wrapMode = WrapMode.Loop;
            ani.CrossFade("Walk");
        }
        
    }
   

}
