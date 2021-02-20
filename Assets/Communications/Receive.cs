using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receive : MonoBehaviour
{
    /// <summary>
    /// udpで通信しているオブジェクト
    /// </summary>
    private GameObject udp;

    /// <summary>
    /// telloの移動速度
    /// </summary>
    private float speed;

    /// <summary>
    /// telloの方向転換角度
    /// </summary>
    private float minAngle;
    private float maxAngle;
    private float angle;

    /// <summary>
    /// 移動した際の変更位置
    /// </summary>
    private Vector3 pos;

    /// <summary>
    /// ロールピッチヨーの文字列分裂指定
    /// </summary>
    private char[] del;

    /// <summary>
    /// ロールピッチヨーを受け取ったことがあるか否か
    /// </summary>
    private bool flag;

    /// <summary>
    /// ベースのヨー
    /// </summary>
    private float based_yaw;

    /// <summary>
    /// yawの変化量
    /// </summary>
    private float change_yaw = 0;

    /// <summary>
    /// ベースのピッチ
    /// </summary>
    private float based_pitch = 0;

    /// <summary>
    /// pitchの変化量
    /// </summary>
    private float change_pitch = 0;

    /// <summary>
    /// ベースのロール
    /// </summary>
    private float based_roll = 0;
    
    /// <summary>
    /// rollの変化量
    /// </summary>
    private float change_roll = 0;


    // 外れ値
    private float Outliers = 0f;

    // count
    private int Out_num = 0;

    private int count = 0;

    private string pre_action = "null";

    // Start is called before the first frame update
    void Start()
    {
        udp = GameObject.Find("Text");
        speed = 0.17f;
        minAngle = 0.0F;
        maxAngle = 20.0F;
        pos = this.transform.position;
        del = new char[] {':',';'};
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        string action = udp.GetComponent<ChangeText>().Move_Check();
        udp.GetComponent<ChangeText>().Move_Del();
        // string new = udp.GetComponent<ChangeText>().check();
        // Debug.Log("action: " + action + "new: " + new);
        switch(action)
        {
            case "takeoff":
                break;
                // obj_distance = (this.transform.position - pos).sqrMagnitude;
                // if(obj_distance == 0){
                //     break;
                // }
            case "land":
                // Debug.Log("land");
                Ray ray = new Ray(this.transform.position, Vector3.down);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)){
                    pos = hit.point;
                    speed = 0.5f;
                    StartCoroutine(AnimateCoroutine(this.transform, speed, pos));
                }
                break;
            case "forward":
                Debug.Log("forward");
                pos = this.transform.position;
                pos += transform.forward * 0.323f;
                pos += transform.right * 0.02f;
                speed = 1.2f;
                StartCoroutine(AnimateCoroutine(this.transform, speed, pos));
                Outliers++;
                break;
            case "backward":
                // Debug.Log("backward");
                pos = this.transform.position;
                pos -= transform.forward * 0.335f;
                speed = 1.2f;
                StartCoroutine(AnimateCoroutine(this.transform, speed, pos));
                Outliers++;
                break;
            case "left":
                // Debug.Log("left");
                pos = this.transform.position;
                pos -= transform.right * 0.235f;
                pos -= transform.forward * 0.02f;
                speed = 1.2f;
                StartCoroutine(AnimateCoroutine(this.transform, speed, pos));
                Outliers++;
                break;
            case "right":
                // Debug.Log("right");
                pos = this.transform.position;
                pos += transform.right * 0.235f;
                pos -= transform.forward * 0.02f;
                speed = 1.2f;
                StartCoroutine(AnimateCoroutine(this.transform, speed, pos));
                Outliers++;
                break;
            case "rotate_left":
                break;
            case "rotate_right":
                break;
            case "up":
                // Debug.Log("up");
                pos = this.transform.position;
                pos += transform.up * 0.2f;
                speed = 1.2f;
                StartCoroutine(AnimateCoroutine(this.transform, speed, pos));
                break;
            case "down":
                // Debug.Log("down");
                pos = this.transform.position;
                pos -= transform.up * 0.2f;
                speed = 1.2f;
                StartCoroutine(AnimateCoroutine(this.transform, speed, pos));
                break;
            case "null":
                // Debug.Log("null");
                // this.transform.position = Vector3.MoveTowards(this.transform.position, pos, speed * Time.deltaTime);
                // this.transform.eulerAngles = new Vector3(0, angle, 0) * Time.deltaTime;
                break;
            default:
                // // Debug.Log("default");
                // string[] arr = action.Split(del);
                // float yaw = float.Parse(arr[5]) + Outliers;
                // float pitch = float.Parse(arr[1]);
                // float roll = float.Parse(arr[3]);

                // if(Out_num == 150){
                //     Outliers += 1f;
                //     Out_num = 0;
                // }

                // // 初めに自分のヨーを固定
                // if(flag){
                //     Debug.Log("flag = true");
                //     flag = false;
                //     based_yaw = float.Parse(arr[5]);
                //     based_pitch = float.Parse(arr[1]);
                //     based_roll = float.Parse(arr[3]);
                // }else{
                //     if(yaw - based_yaw > 180){
                //         change_yaw = yaw - based_yaw - 360f;
                //     }else if(yaw - based_yaw < -180) {
                //         change_yaw = yaw - based_yaw + 360f;
                //     }else{
                //         change_yaw = yaw - based_yaw;
                //     }

                //     // pitch変更
                //     change_pitch = pitch - based_pitch;
                //     based_pitch = pitch;
                //     // roll変更
                //     change_roll = roll - based_roll;
                //     based_roll = roll;
                //     // ベースのyawの更新
                //     Debug.Log("pre: " + based_yaw + "yaw: " + yaw);
                //     based_yaw = yaw;
                // }
                
                // pos = this.transform.position;
                // pos -= transform.right * 0.01f * change_roll;
                // pos += transform.forward * 0.01f * change_pitch;
                // speed = 0.17f;
                // Debug.Log("change_yaw: " + change_yaw);
                // if(change_yaw != 0){
                //     Quaternion q= Quaternion.Euler(0f, change_yaw, 0f);
                //     StartCoroutine(AnimateCoroutine(this.transform, speed, null, this.transform.rotation * q));
                // }
                
                // Out_num++;
                // // this.transform.rotation = Quaternion.Euler(roll, yaw, pitch);
                // // StartCoroutine("times_later");
                
                // // for(int i = 0; i < arr.Length; i++){
                // //     Debug.Log("number: " + i + " str: " + arr[i]);
                // // }
                break;
        }
    }

    // 移動
    IEnumerator AnimateCoroutine(Transform transform, float time, Vector3? position)
    {
        // 現在のposition, rotation
        var currentPosition = transform.position;

        // 目標のposition, rotation
        var targetPosition = position ?? currentPosition;

        var sumTime = 0f;
        while (true)
        {
            // Coroutine開始フレームから何秒経過したか
            sumTime += Time.deltaTime;
            // 指定された時間に対して経過した時間の割合
            var ratio = sumTime / time;

            this.transform.position = Vector3.Lerp(currentPosition, targetPosition, ratio);

            if (ratio > 1.0f)
            {
                // 目標の値に到達したらこのCoroutineを終了する
                // ~.Lerpは割合を示す引数は0 ~ 1の間にClampされるので1より大きくても問題なし
                break;
            }

            yield return null;
        }
    }

    IEnumerator times_later()//一定間隔で警告する
    {
        yield return new WaitForSeconds(1.5f);
    }
}
