using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // Except

public class Visualize : MonoBehaviour {

    private GameObject drone;
    /// <summary>
    /// 今回の Update で検出された遮蔽物の Renderer コンポーネント。
    /// </summary>
    private List<Renderer> rendererHitsList_ = new List<Renderer>();

    /// <summary>
    /// 前回の Update で検出された遮蔽物の Renderer コンポーネント。
    /// 今回の Update で該当しない場合は、遮蔽物ではなくなったので Renderer コンポーネントを有効にする。
    /// </summary>

    /// <summary>
    /// 遮蔽物とするレイヤーマスク。
    /// </summary>
    private int layerMask_;

    /// <summary>
    /// マテリアル比較用
    /// </summary>
    private bool Compare_Material;

    private Renderer[] rendererHitsPrevs_;

    private Vector3 Obst_pos;
    void Start()
    {
        drone = GameObject.Find("droneModel");
        if(drone == null){
            Debug.Log("null");
        }
        layerMask_ = 1;
    }

    void Update()
    {   
        // カメラと被写体を結ぶ ray を作成
        Vector3 _difference = (drone.transform.position - this.transform.position);
        Vector3 _direction = _difference.normalized;
        Ray _ray = new Ray(this.transform.position, _direction);
        // 前回の結果を退避してから、Raycast して今回の遮蔽物のリストを取得する
        // RaycastHit[] _hits = Physics.RaycastAll(_ray, _difference.magnitude, layerMask_);
        Quaternion angle = new Quaternion(0f,0f,0f,0f);

        RaycastHit[] _hits = Physics.BoxCastAll(transform.position, Vector3.one, _direction, angle,  10.0f, layerMask_);
        RaycastHit[] collider_hits = Physics.BoxCastAll(this.transform.position, Vector3.one, _direction, angle,  _difference.magnitude, layerMask_);


        rendererHitsPrevs_ = rendererHitsList_.ToArray();
        rendererHitsList_.Clear();
        // 遮蔽物は一時的にすべて描画機能を無効にする。
        foreach (RaycastHit collider_hit in collider_hits)
        {
            foreach (RaycastHit _hit in _hits)
            {
                if (collider_hit.collider != null && (collider_hit.collider.gameObject != drone))
                { 
                    // 遮蔽物が被写体の場合は例外とする
                    if (_hit.collider.gameObject == drone)
                    {
                        continue;
                    }
                    float alter_diff = (_hit.point - this.transform.position).sqrMagnitude;
                    float now_diff = (drone.transform.position - this.transform.position).sqrMagnitude;
                    Obst_pos = collider_hit.point;
                    // 遮蔽物の Renderer コンポーネントを無効にする
                    Renderer _renderer = _hit.collider.gameObject.GetComponent<Renderer>();
                    // Renderer collider_renderer = collider_hit.collider.gameObject.GetComponent<Renderer>();
                    if (_renderer != null && (alter_diff >= now_diff))
                    {   
                        // Debug.Log(_renderer.material);
                        // Material mat = Resources.Load<Material>("Assets/Custom_Cutout.mat");
                        Debug.Log(_renderer);
                        _renderer.enabled = true;
                        rendererHitsList_.Add(_renderer);
                        //Test
                        // Material mat = _renderer.material;
                        // mat = Resources.Load<Material>("Custom_Cutout") as Material;
                        // _renderer.material = mat;
                        
                        // Debug.Log(mats[1]);
                        // Debug.Log(_renderer.materials[1]);
                        // Color color = _hit.collider.gameObject.setComponent<Renderer>().materials[0].color;
                        // Color sec_color = _hit.collider.gameObject.GetComponent<Renderer>().materials[1].color;
                        // Debug.Log(color);
                        // Debug.Log(sec_color);
                        // color.a = 0.2f;
                        // sec_color.a = 0.2f;
                        // _hit.collider.gameObject.GetComponent<Renderer>().materials[0].color = color;
                        // _hit.collider.gameObject.GetComponent<Renderer>().materials[1].color = sec_color;

                        // rendererHitsList_.Add(_renderer);
                        // _renderer.enabled = false;
                    }
                }
            }
        }
            // 前回まで対象で、今回対象でなくなったものは、表示を元に戻す。
        foreach (Renderer _renderer in rendererHitsPrevs_.Except<Renderer>(rendererHitsList_))
        {
            Material mat_check = Resources.Load<Material>("Custom_Cutout") as Material;
            // Debug.Log(mat_check.name+"aaa");
            // Debug.Log(_renderer.material.name+"aaa");
            // Compare_Material = (_renderer.material.name).SequenceEqual(mat_check.name + " (Instance)");
            // Compare_Material = (_renderer).SequenceEqual(mat_check.name + " (UnityEngine.MeshRenderer)");
            // 遮蔽物でなくなった Renderer コンポーネントを有効にする
            // if (Compare_Material)
            // {
                // Material mat = _renderer.material;
                // mat = Resources.Load<Material>("Default_OBJ") as Material;
                // _renderer.material = mat;
            _renderer.enabled = false;
            // }
            // Debug.Log(_renderer.material);
        }
        Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.red, 5);
    }


    public Vector3 GetPosition(){
            return Obst_pos;
    }
    // void OnDrawGizmos() {
    //     //　Cubeのレイを疑似的に視覚化
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireCube(transform.position + transform.forward * 10.0f, Vector3.one);
    // }
}

