using UnityEngine;
using System.Collections;

public class HurtItem : MonoBehaviour
{
    private Color[] colors = new Color[] { Color.red, Color.yellow, Color.blue, Color.green, Color.white };

    public UILabel txtHurt;
    private Camera mainCamera;
    private Vector3 sourcePosition;
    private Vector3 targetPosition;
    private float destoryTime;

    public void ChangeData(int hurt, Vector3 position, Camera mainCamera)
    {
        this.mainCamera = mainCamera;
        this.txtHurt.text = "- " + hurt.ToString();
        this.txtHurt.color = this.colors[Random.Range(0, this.colors.Length)];

        this.sourcePosition = position + new Vector3(0, 1.5f, 0f);
        this.targetPosition = position + new Vector3(0, Random.Range(1.8f, 2f), 0f);
        this.transform.position = this.sourcePosition;
        this.destoryTime = Time.time + 2;
    }

    void Update()
    {
        if (this.destoryTime == 0) return;

        Vector3 directionVector = this.sourcePosition - this.mainCamera.transform.position;
        // 计算血条与摄像机的夹角
        float angleValue = Vector3.Angle(directionVector, this.mainCamera.transform.forward);

        // 如果在摄像机范围内，显示，否则隐藏
        if (angleValue < this.mainCamera.fieldOfView)
        {
            if (Time.time < this.destoryTime)
            {
                this.sourcePosition = Vector3.Lerp(this.sourcePosition, this.targetPosition, Time.deltaTime);
                this.transform.position = this.sourcePosition;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


}