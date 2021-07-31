using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger_notice : MonoBehaviour
{
    public GameObject _drone;
    private Vector3 _direction;
    private int count = 0;
    private int check = 1;
    private Vector3 hitPos;
    private Vector3 _difference = new Vector3(0, 0, 0);
    private float pre_distance = 0;
    private float now_distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // void OnCollisionEnter(Collision other){
    //     // 建物限定
    //     if(other.gameObject.tag == "building"){
            
    //         check = 0;
    //         foreach (ContactPoint point in other.contacts)
    //         {
    //             // 1つだけcollider取得の場合
    //             if(count == 0){
    //                 hitPos = point.point;
    //                 pre_distance = (hitPos - _drone.transform.position).sqrMagnitude;
    //             }
    //             // 距離比較用
    //             now_distance = (point.point - _drone.transform.position).sqrMagnitude;
    //             // 最短の障害物決定
    //             if(pre_distance >= now_distance){
    //                 hitPos = point.point;
    //                 pre_distance = (hitPos - _drone.transform.position).sqrMagnitude;
    //                 _difference = (hitPos - _drone.transform.position);
    //             }
    //             count++;
    //         }
            

    //         // 障害物までの方向取得
            
    //         _direction = _difference.normalized;

    //         // Script ON
    //         // this.gameObject.GetComponent<Shooting>().enabled = true;

    //         // カウントリセット
    //         count = 0;
    //     }
    // }

    // void OnCollisionExit(){
    //     Debug.Log("Exit");
    //     // Script OFF
    //     // this.gameObject.GetComponent<Shooting>().enabled = false;
    //     check = 1;
    // }
    
    IEnumerator times_later()//一定間隔で警告する
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(2.0f);
        check = 1;
        this.gameObject.GetComponent<SphereCollider>().enabled = true;
        yield break;
    }

    void OnCollisionStay(Collision other){
        // 建物限定
        // Debug.Log(other.gameObject.tag);
        // Debug.Log(other.gameObject.name);
        pre_distance = 0;
        now_distance = 0;
        if(other.gameObject.tag == "Building"){
            check = 0;
            foreach (ContactPoint point in other.contacts)
            {
                // Debug.Log(point.point);
                Ray ray = new Ray(_drone.transform.position, (point.point - _drone.transform.position).normalized);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)){
                    
                    if(count == 0){
                        hitPos = hit.point;
                        pre_distance = (hitPos - _drone.transform.position).sqrMagnitude;
                    }
                    // 距離比較用
                    now_distance = (hit.point - _drone.transform.position).sqrMagnitude;
                    // Debug.Log("count: " + count + "今distance: " + now_distance);
                    // 最短の障害物決定
                    if(pre_distance >= now_distance){
                        hitPos = hit.point;
                        pre_distance = now_distance;
                        _difference = (hitPos - _drone.transform.position);
                    }
                }
                // Debug.Log("count: " + count + "最短distance: " + pre_distance);
                count++;
                if(count == 3){
                    break;
                }
            }
            // 障害物までの方向取得
            _direction = _difference.normalized;
            Debug.Log(_direction);
            // Mathf.Pow(pre_distance, 2)
            // Debug.Log("距離: " + pre_distance);
            // 距離に応じて色変化
            // if(pre_distance)

            // 時間停止、コライダーoFFコルーチンスタート
            StartCoroutine("times_later");
            // カウントリセット
            count = 0;



            // hitPos = other.ClosestPoint(_drone.transform.position);
            // // Debug.Log("hitPos:" + hitPos);
            // _difference = (hitPos - _drone.transform.position);
            // Debug.Log("hitPos" + hitPos);
            // Debug.Log("drone" + _drone.transform.position);
            // // Debug.Log("difference" + hitPos - _drone.transform.position);

            
            // Debug.Log("direction" + _direction);

            // 方向へのデバッグ
            // Debug.DrawRay(_drone.transform.position, _direction * 10, Color.green, 5, false);

            

            // Script ON
            // this.gameObject.GetComponent<Shooting>().enabled = true;

            
        }
    }

    // void OnCollisionExit(Collision other){
    //     if(other.gameObject.tag == "Building"){
    //         Debug.Log("Exit");
    //         // Script OFF
    //         // this.gameObject.GetComponent<Shooting>().enabled = false;
    //         check = 1;
    //     }
        
    // }


    public Vector3 obst_direction(){
        
        return _direction;
    }

    public bool stop(){
        // Debug.Log(check);
        // Invoke停止
        if(check == 1){
            return true;
        }else{
            return false;
        }
        
    }
}
