using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // Except

public class Visualize2 : MonoBehaviour {

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
        Quaternion angle = new Quaternion(0f,0f,0f,0f);

        // 前回の結果を退避してから、Raycast して今回の遮蔽物のリストを取得する
        RaycastHit[] _hits = Physics.SphereCastAll(this.transform.position, 0.05f, _direction,  _difference.magnitude, layerMask_);


        rendererHitsPrevs_ = rendererHitsList_.ToArray();
        rendererHitsList_.Clear();
        // 遮蔽物は一時的にすべて描画機能を無効にする。
        foreach (RaycastHit _hit in _hits)
        {
            // 遮蔽物が被写体の場合は例外とする
            if (_hit.collider.gameObject == drone)
            {
                continue;
            }

            // 遮蔽物の Renderer コンポーネントを無効にする
            Renderer _renderer = _hit.collider.gameObject.GetComponent<Renderer>();
            float alter_diff = (_hit.point - this.transform.position).sqrMagnitude;
            float now_diff = (drone.transform.position - this.transform.position).sqrMagnitude;
            if (_renderer != null && (now_diff + 1 >= alter_diff))
            {   
                // Debug.Log(_renderer.material);
                // Material mat = Resources.Load<Material>("Assets/Custom_Cutout.mat");
                // Material[] mats = _renderer.materials;
                // mats[0] = Resources.Load<Material>("Custom_Cutout") as Material;
                // mats[1] = Resources.Load<Material>("Custom_Cutout") as Material;
                // _renderer.materials = mats;
                rendererHitsList_.Add(_renderer);
                _renderer.enabled = false;
                // Debug.Log(mats[1]);
                // Debug.Log(_renderer.materials[1]);
                // Color color = _hit.collider.gameObject.GetComponent<Renderer>().materials[0].color;
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

        // 前回まで対象で、今回対象でなくなったものは、表示を元に戻す。
        foreach (Renderer _renderer in rendererHitsPrevs_.Except<Renderer>(rendererHitsList_))
        {
            // Material mat_check = Resources.Load<Material>("Custom_Cutout") as Material;
            // Debug.Log(mat_check.name+"aaa");
            // Debug.Log(_renderer.material.name+"aaa");
            // Compare_Material = (_renderer.material.name).SequenceEqual(mat_check.name + " (Instance)");
            // 遮蔽物でなくなった Renderer コンポーネントを有効にする
            if (_renderer != null)
            {
                _renderer.enabled = true;
            }
            // if (Compare_Material)
            // {
            //     Material[] mats = _renderer.materials;
            //     mats[0] = Resources.Load<Material>("RealisticTopMaterial") as Material;
            //     mats[1] = Resources.Load<Material>("RealisticSideMaterial") as Material;
            //     _renderer.materials = mats;
            // }
            // Debug.Log(_renderer.materials[0]);
        }
        
        Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.red, 5);
    }
}