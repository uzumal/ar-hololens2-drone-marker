using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : MonoBehaviour
{
    public float speed = 0.1f;

    /// <summary>
    /// 移動した際の変更位置
    /// </summary>
    private Vector3 pos;
    void Start(){
        pos = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			// transform.position += transform.forward * speed * Time.deltaTime;
            pos = this.transform.position;
            pos.x += 3.0f;
            speed = 1f;
            // Debug.Log("Loading");
            this.transform.Translate(Vector3.up * 0.1f);
		}else if (Input.GetKey ("down")) {
			// transform.position -= transform.forward * speed * Time.deltaTime;
            this.transform.Translate(Vector3.back * 0.1f);
		}else if (Input.GetKey("right")) {
			// transform.position += transform.right * speed * Time.deltaTime;
            this.transform.Translate(Vector3.right * 0.1f);
		// }else if (Input.GetKey ("left")) {
		// 	// transform.position -= transform.right * speed * Time.deltaTime;
        //     this.transform.Translate(Vector3.left * 0.1f);
		}else if (Input.GetKey("left")) {
			// transform.position += transform.forward * speed * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(0f, -10f, 0f);
            // this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}
        // this.transform.position = Vector3.MoveTowards(this.transform.position, pos, speed * Time.deltaTime);
        // StartCoroutine("times_later");
	}

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
    // public float speed = 2.0f;
    // public Rigidbody rb;

    // void Start() 
    // {
    //     rb = GetComponent<Rigidbody>();
    // }

    // void FixedUpdate() 
    // {
    //     float x =  Input.GetAxis("Horizontal") * speed;
    //     float z = Input.GetAxis("Vertical") * speed;
    //     rb.AddForce(x , 0 , z );
    // }
}
