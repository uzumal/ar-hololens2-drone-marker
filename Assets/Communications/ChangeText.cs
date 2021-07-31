using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour {
    /// <summary>
    /// 文字列を反映するテキストフィールド
    /// </summary>
    [SerializeField, Tooltip("文字列を反映するテキストフィールド")]
    private Text TargetTextField;

    private string getMessage = "null";

    /// <summary>
    /// roll,pitch,yawのdata
    /// </summary>
    private string Angle_Message = "null";
    
    /// <summary>
    /// 動作のdata
    /// <summary>
    private string Move_Message = "null";

    /// <summary>
    /// byte列を文字列に変換してテキストフィールドに反映する
    /// </summary>
    /// <param name="message"></param>
    public void SetASCIIBytes(byte[] bytes)
    {
        // データを文字列に変換
        getMessage = System.Text.Encoding.ASCII.GetString(bytes);
        // mes = getMessage.split(' ');
        getMessage = getMessage.Trim('\0');
        // deb_mes = "start:" + getMessage + ":end";
        // TargetTextField.text = deb_mes;
        // Debug.Log(getMessage);
        if(getMessage.Length >= 18){
            Angle_Message = getMessage;
        }else{
            Move_Message = getMessage;
        }
        Debug.Log(Move_Message);
    }
    public string Move_Check(){
        return Move_Message;
    }

    public string Angle_Check(){
        return Angle_Message;
    }

    public void Move_Del(){
        Move_Message = "null";
    }

    public void Angle_Del(){
        Angle_Message = "null";
    }

}