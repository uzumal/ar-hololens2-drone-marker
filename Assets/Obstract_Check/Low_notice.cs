using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Low_notice : MonoBehaviour
{
    public GameObject _drone;
    private Vector3 _direction;
    private int count = 0;
    private int check = 1;
    private Vector3 hitPos;
    private Vector3 _difference = new Vector3(0, 0, 0);
    private float pre_distance = 0;
    private float now_distance = 0;
    private List<GameObject> ContactList = new List<GameObject>();
    // private List<GameObject> prevContactList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        // Debug.Log(other);
        if(other.gameObject.CompareTag("Building") && !ContactList.Contains(other.gameObject)){
            // Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
            // _difference = (other.transform.position - _drone.transform.position);
            // _direction = _difference.normalized;
            ContactList.Add(other.gameObject);
            // Debug.Log(other);
            check = 0;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Building") && ContactList.Contains(other.gameObject)){
            ContactList.Remove(other.gameObject);
            check = 1;
            // Debug.Log("remove");
        }
    }

    void Update(){
        float prev_distance = 10;
        foreach(GameObject hit in ContactList){
            if((hit.transform.position - _drone.transform.position).magnitude < prev_distance){
                _difference = (hit.transform.position - _drone.transform.position);
                _direction = _difference.normalized;
                prev_distance = (hit.transform.position - _drone.transform.position).magnitude;
                // Debug.Log("gameobject: " + hit + "distance: " + prev_distance);
            }
            
            // Debug.Log("gameobject: " + hit + "distance: " + hit.ClosestPointOnBounds(_drone.transform.position));
        }
        // Debug.Log(_direction);
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
        // this.gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(2.0f);
        // check = 1;
        // this.gameObject.GetComponent<SphereCollider>().enabled = true;
        // yield break;
    }

    // void OnCollisionStay(Collision other){
    //     // 建物限定
    //     // Debug.Log(other.gameObject.tag);
    //     // Debug.Log(other.gameObject.name);
    //     pre_distance = 0;
    //     now_distance = 0;
    //     if(other.gameObject.tag == "Building"){
    //         check = 0;
    //         foreach (ContactPoint point in other.contacts)
    //         {
    //             // Debug.Log(point.point);
    //             Ray ray = new Ray(_drone.transform.position, (point.point - _drone.transform.position).normalized);
    //             RaycastHit hit;
    //             if(Physics.Raycast(ray, out hit)){
                    
    //                 if(count == 0){
    //                     hitPos = hit.point;
    //                     pre_distance = (hitPos - _drone.transform.position).sqrMagnitude;
    //                 }
    //                 // 距離比較用
    //                 now_distance = (hit.point - _drone.transform.position).sqrMagnitude;
    //                 // Debug.Log("count: " + count + "今distance: " + now_distance);
    //                 // 最短の障害物決定
    //                 if(pre_distance >= now_distance){
    //                     hitPos = hit.point;
    //                     pre_distance = now_distance;
    //                     _difference = (hitPos - _drone.transform.position);
    //                 }
    //             }
    //             // Debug.Log("count: " + count + "最短distance: " + pre_distance);
    //             count++;
    //             if(count == 3){
    //                 break;
    //             }
    //         }
    //         // 障害物までの方向取得
    //         _direction = _difference.normalized;
    //         Debug.Log(_direction);
    //         // Mathf.Pow(pre_distance, 2)
    //         // Debug.Log("距離: " + pre_distance);
    //         // 距離に応じて色変化
    //         // if(pre_distance)

    //         // 時間停止、コライダーoFFコルーチンスタート
    //         StartCoroutine("times_later");
    //         // カウントリセット
    //         count = 0;



    //         // hitPos = other.ClosestPoint(_drone.transform.position);
    //         // // Debug.Log("hitPos:" + hitPos);
    //         // _difference = (hitPos - _drone.transform.position);
    //         // Debug.Log("hitPos" + hitPos);
    //         // Debug.Log("drone" + _drone.transform.position);
    //         // // Debug.Log("difference" + hitPos - _drone.transform.position);

            
    //         // Debug.Log("direction" + _direction);

    //         // 方向へのデバッグ
    //         // Debug.DrawRay(_drone.transform.position, _direction * 10, Color.green, 5, false);

            

    //         // Script ON
    //         // this.gameObject.GetComponent<Shooting>().enabled = true;

            
    //     }
    // }

    // void OnCollisionExit(Collision other){
    //     if(other.gameObject.tag == "Building"){
    //         Debug.Log("Exit");
    //         // Script OFF
    //         // this.gameObject.GetComponent<Shooting>().enabled = false;
    //         check = 1;
    //     }
        
    // }


    public Vector3 obst_direction(){
        
        return _difference;
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
