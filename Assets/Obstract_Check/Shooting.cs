using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    // bullet prefab
    public GameObject bullet;

    // 弾丸発射点
    public Transform muzzle;

    // 弾丸の速度
    private float speed = 30f;

    // コルーチン定義
    private IEnumerator coroutine;

    private int check_num = 0;

    //インスペクターから変更可能（もちろんスクリプトからも）
    public float INTERVAL_SECONDS = 1.0f;

    private Color startColor;
    private Color endColor;

    private float t;

    MeshRenderer bulletMeshRenderer;

	// Use this for initialization
	void Start () {
		// coroutine = TempoShot();
        // StartCoroutine(coroutine);
        InvokeRepeating("Shot", 0.7f, 0.7f); // 1秒ごとにShotを繰り返す
        startColor = Color.red;
        endColor = Color.yellow;
        bulletMeshRenderer = bullet.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        bool change = this.gameObject.GetComponent<Low_notice>().stop();
        if(change){
            CancelInvoke();
            // StopCoroutine(coroutine);
            // Debug.Log("coroutineが止まる");
            // check_num = 1;
            // this.gameObject.GetComponent<Shooting>().enabled = false;
            
        }else{
            // Debug.Log("再開");
            // StartCoroutine(coroutine);
                // check_num = 0;
            if(IsInvoking("Shot") == false){
                InvokeRepeating("Shot",0.7f,0.7f);
            }
        }
	}

    // IEnumerator TempoShot()//一定間隔で警告する
    // {
    //     while (true) {
    //         yield return new WaitForSecondsRealtime(INTERVAL_SECONDS);
    //         Shot();
    //     }
    // }

    void Shot(){
        // Debug.Log("再開");
        Vector3 difference = this.gameObject.GetComponent<Low_notice>().obst_direction();
        Vector3 direction = difference.normalized;
        // Debug.Log("direction:" + direction);

        float distance = difference.magnitude;
        // Debug.Log("distance:" + distance);
        if(distance <= 0.3f){
            t = 0f;
        }else{
            t = 1f;
        }
        bulletMeshRenderer.sharedMaterial.SetColor("_Color", Color.Lerp(startColor, endColor, t));

        // 弾丸の複製
        GameObject bullets = Instantiate(bullet) as GameObject;

        Vector3 force;

        

        // force = this.gameObject.transform.forward * speed;
        force = direction * speed;

        
        // Vector3 pos = bullets.transform.position;
        // pos += force;

        // StartCoroutine(AnimateCoroutine(bullets.transform, speed, pos));

        // Rigidbodyに力を加えて発射
        bullets.GetComponent<Rigidbody>().AddForce(force);
        // 弾丸の位置を調整
        bullets.transform.position = muzzle.position;
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
}

