using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle_Change : MonoBehaviour
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

    //count
    private int Out_num = 0;

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
        string action = udp.GetComponent<ChangeText>().Angle_Check();
        udp.GetComponent<ChangeText>().Angle_Del();
        switch(action)
        {
            case "takeoff":
                break;
            case "land":
                break;
            case "forward":
                break;
            case "backward":
                break;
            case "left":
                break;
            case "right":
                break;
            case "rotate_left":
                break;
            case "rotate_right":
                break;
            case "up":
                break;
            case "down":
                break;
            case "null":
                break;
            default:
                // Debug.Log("default");
                string[] arr = action.Split(del);
                // for(int i = 0; i < arr.Length; i++){
                //     Debug.Log("number: " + i + " str: " + arr[i]);
                // }

                float yaw = float.Parse(arr[5]);
                float pitch = float.Parse(arr[1]);
                float roll = float.Parse(arr[3]);

                // 初めに自分のヨーを固定
                if(flag){
                    Debug.Log("flag = true");
                    flag = false;
                    based_yaw = float.Parse(arr[5]);
                    based_pitch = float.Parse(arr[1]);
                    based_roll = float.Parse(arr[3]);
                }else{
                    if(yaw - based_yaw > 180){
                        change_yaw = yaw - based_yaw - 360f;
                    }else if(yaw - based_yaw < -180) {
                        change_yaw = yaw - based_yaw + 360f;
                    }else{
                        change_yaw = yaw - based_yaw;
                    }

                    // pitch変更
                    change_pitch = pitch - based_pitch;
                    based_pitch = pitch;
                    // roll変更
                    change_roll = roll - based_roll;
                    based_roll = roll;
                    // ベースのyawの更新
                    Debug.Log("pre: " + based_yaw + "yaw: " + yaw);
                    based_yaw = yaw;
                }
                // 位置誤差推定
                pos = this.transform.position;
                if((roll != 0) || (pitch != 0)){
                    pos -= transform.right * 0.003f * change_roll;
                    pos += transform.forward * 0.003f * change_pitch;
                }
                
                // 初めに自分のヨーを固定
                // if(flag){
                //     Debug.Log("flag = true");
                //     flag = false;
                //     based_yaw = float.Parse(arr[5]);
                // }else{
                //     if(yaw - based_yaw > 180){
                //         change_yaw = yaw - based_yaw - 360f;
                //     }else if(yaw - based_yaw < -180) {
                //         change_yaw = yaw - based_yaw + 360f;
                //     }else{
                //         change_yaw = yaw - based_yaw;
                //     }
                //     // ベースのyawの更新
                //     // Debug.Log("pre: " + based_yaw + "yaw: " + yaw);
                //     based_yaw = yaw;
                // }
                
                speed = 0.05f;
                Debug.Log("change_yaw: " + change_yaw);
                if(change_yaw != 0){
                    // if(change_yaw > 0){
                    //     change_yaw += 0.7f;
                    // }else{
                    //     change_yaw -= 0.7f;
                    // }
                    Quaternion q= Quaternion.Euler(0f, change_yaw, 0f);
                    StartCoroutine(AnimateCoroutine(this.transform, speed, this.transform.rotation * q));
                }
                
                this.transform.position = pos;
                // for(int i = 0; i < arr.Length; i++){
                //     Debug.Log("number: " + i + " str: " + arr[i]);
                // }
                if(Outliers >= 50){
                    change_yaw += 0.5f;
                    Outliers = 0;
                }
                break;
        }
        
        Outliers++;
    }

    // 移動
    IEnumerator AnimateCoroutine(Transform transform, float time, Quaternion? rotation)
    {
        // 現在のposition, rotation
        var currentRotation = transform.rotation;

        // 目標のposition, rotation
        var targetRotation = rotation ?? currentRotation;

        var sumTime = 0f;
        while (true)
        {
            // Coroutine開始フレームから何秒経過したか
            sumTime += Time.deltaTime;
            // 指定された時間に対して経過した時間の割合
            var ratio = sumTime / time;

            this.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, ratio);

            if (ratio > 1.0f)
            {
                // 目標の値に到達したらこのCoroutineを終了する
                // ~.Lerpは割合を示す引数は0 ~ 1の間にClampされるので1より大きくても問題なし
                break;
            }

            yield return null;
        }
    }

    // IEnumerator times_later()//一定間隔で警告する
    // {
    //     yield return new WaitForSeconds(1.0f);
    //     yield break;
    // }
}
