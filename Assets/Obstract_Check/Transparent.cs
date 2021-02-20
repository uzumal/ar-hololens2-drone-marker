using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // Except

public class Transparent : MonoBehaviour
{
    private GameObject drone;
    public GameObject _mask;
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

    /// <summary>
    /// 半透明マテリアル
    /// </summary>
    private Material mat;

    private Renderer[] rendererHitsPrevs_;
    void Start()
    {
        drone = GameObject.Find("droneModel");
        mat = Resources.Load<Material>("Custom_Cutout") as Material;
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
        RaycastHit[] _hits = Physics.RaycastAll(_ray, _difference.magnitude, layerMask_);
        // RaycastHit[] _hits = Physics.SphereCastAll(this.transform.position, 0.03f ,_direction, _difference.magnitude, layerMask_);


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
            //MaskObjectのRendererコンポーネント
            Renderer _Mask = _mask.GetComponent<Renderer>();
            if (_renderer != null)
            {   
                Debug.Log(_renderer.material.name);
                _renderer.material = mat;
                rendererHitsList_.Add(_renderer);
                _Mask.enabled = true;
            }else{
                _Mask.enabled = false;
            }
        }

        // 前回まで対象で、今回対象でなくなったものは、表示を元に戻す。
        foreach (Renderer _renderer in rendererHitsPrevs_.Except<Renderer>(rendererHitsList_))
        {
            Compare_Material = (_renderer.material.name).SequenceEqual(mat.name + " (Instance)");
            // 遮蔽物でなくなった Renderer コンポーネントを有効にする
            if (Compare_Material)
            {
                Material mat_default = Resources.Load<Material>("default") as Material;
                _renderer.material = mat_default;
            }
            Debug.Log("end:" + _renderer.material);
        }
        
        Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.red, 5);
    }
}
