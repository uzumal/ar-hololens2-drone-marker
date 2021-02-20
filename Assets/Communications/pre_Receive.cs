using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pre_Receive : MonoBehaviour
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
        string action = udp.GetComponent<ChangeText>().Move_Check();
        udp.GetComponent<ChangeText>().Move_Del();
        switch(action)
        {
            case "takeoff":
                pos = this.transform.position;
                pos.y += 0.9f;
                speed = 1.0f;
                Quaternion q= Quaternion.Euler(0f, 2.0f, 0f);
                StartCoroutine(AnimateCoroutine(this.transform, speed, pos, this.transform.rotation * q));
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
                break;
        }
    }

    // 移動
    IEnumerator AnimateCoroutine(Transform transform, float time, Vector3? position, Quaternion? rotation)
    {
        // 現在のposition, rotation
        var currentPosition = transform.position;
        var currentRotation = transform.rotation;

        // 目標のposition, rotation
        var targetPosition = position ?? currentPosition;
        var targetRotation = rotation ?? currentRotation;

        var sumTime = 0f;
        while (true)
        {
            // Coroutine開始フレームから何秒経過したか
            sumTime += Time.deltaTime;
            // 指定された時間に対して経過した時間の割合
            var ratio = sumTime / time;

            transform.SetPositionAndRotation(
                Vector3.Lerp(currentPosition, targetPosition, ratio),
                Quaternion.Lerp(currentRotation, targetRotation, ratio)
            );

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
