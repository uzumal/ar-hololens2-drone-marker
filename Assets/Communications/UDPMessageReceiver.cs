using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UnityEvent を利用するため Events を追加
using UnityEngine.Events;
// usingでStreamを開くため System.IO を追加
using System.IO;

// 引数にByte列を受け取る UnityEvent<T0> の継承クラスを作成する
// UDP受信したバイト列を引数として渡す
// Inspector ビューに表示させるため、Serializable を設定する
[System.Serializable]
public class MyIntEvent : UnityEvent<byte[]>
{
}

public class UDPMessageReceiver : MonoBehaviour {
    /// <summary>
    /// UDPメッセージ受信時実行処理
    /// </summary>
    [SerializeField, Tooltip("UDPメッセージ受信時実行処理")]
    private MyIntEvent UDPReceiveEventUnityEvent;

    /// <summary>
    /// UDP受信ポート
    /// </summary>
    [SerializeField, Tooltip("UDP受信ポート")]
    private int UDPReceivePort = 4602;

    /// <summary>
    /// UDP受信データ
    /// </summary>
    private byte[] p_UDPReceivedData;

    /// <summary>
    /// UDP受信イベント検出フラグ
    /// </summary>
    private bool p_UDPReceivedFlg;

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Start()
    {
        // 検出フラグOFF
        p_UDPReceivedFlg = false;

        // 初期化処理
        UDPClientReceiver_Init();
    }
    
    /// <summary>
    /// 定期実行
    /// </summary>
    void Update()
    {
        if (p_UDPReceivedFlg)
        {
            // UDP受信を検出すればUnityEvent実行
            // 受信データを引数として渡す
            UDPReceiveEventUnityEvent.Invoke(p_UDPReceivedData);
            // 検出フラグをOFF
            p_UDPReceivedFlg = false;
        }
    }
    /// <summary>
    /// UDP受信時処理
    /// </summary>
    private void UDPReceiveEvent(byte[] receiveData)
    {
        // 検出フラグONに変更する
        // UnityEventの実行はMainThreadで行う
        p_UDPReceivedFlg = true;

        // 受信データを記録する
        p_UDPReceivedData = receiveData;
    }


#if WINDOWS_UWP
    /// <summary>
    /// UDP通信サポート
    /// </summary>
    Windows.Networking.Sockets.DatagramSocket p_Socket;
    
    /// <summary>
    /// ロック用オブジェクト
    /// </summary>
    object p_LockObject = new object();
    
    /// <summary>
    /// バッファサイズ
    /// </summary>
    const int MAX_BUFFER_SIZE = 1024;
    
    /// <summary>
    /// UDP受信初期化
    /// </summary>
    private async void UDPClientReceiver_Init()
    {
        try {
            // UDP通信インスタンスの初期化
            p_Socket = new Windows.Networking.Sockets.DatagramSocket();
            // 受信時のコールバック関数を登録する
            p_Socket.MessageReceived += OnMessage;
            // 指定のポートで受信を開始する
            p_Socket.BindServiceNameAsync(UDPReceivePort.ToString());
        } catch (System.Exception e) {
            Debug.LogError(e.ToString());
        }
    }
    
    /// <summary>
    /// UDP受信時コールバック関数
    /// </summary>
    async void OnMessage
    (Windows.Networking.Sockets.DatagramSocket sender,
    Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
    {
        using (System.IO.Stream stream = args.GetDataStream().AsStreamForRead()) {
            // 受信データを取得
            byte[] receiveBytes = new byte[MAX_BUFFER_SIZE];
            await stream.ReadAsync(receiveBytes, 0, MAX_BUFFER_SIZE);
            lock (p_LockObject) {
                // 受信データを処理に引き渡す
                UDPReceiveEvent(receiveBytes);
            }
        }
    }
#else
    /// <summary>
    /// UDP受信初期化
    /// </summary>
    private void UDPClientReceiver_Init()
    {
        // UDP受信ポートに受信する全てのメッセージを取得する
        System.Net.IPEndPoint endPoint =
            new System.Net.IPEndPoint(System.Net.IPAddress.Any, UDPReceivePort);

        // UDPクライアントインスタンスを初期化
        System.Net.Sockets.UdpClient udpClient =
            new System.Net.Sockets.UdpClient(endPoint);

        // 非同期のデータ受信を開始する
        udpClient.BeginReceive(OnReceived, udpClient);
    }

    /// <summary>
    /// UDP受信時コールバック関数
    /// </summary>
    private void OnReceived(System.IAsyncResult a_result)
    {
        // ステータスからUdpClientのインスタンスを取得する
        System.Net.Sockets.UdpClient udpClient =
        (System.Net.Sockets.UdpClient)a_result.AsyncState;
        
        // 受信データをバイト列として取得する
        System.Net.IPEndPoint endPoint = null;
        byte[] receiveBytes = udpClient.EndReceive(a_result, ref endPoint);

        // 受信データを受信時処理に引き渡す
        UDPReceiveEvent(receiveBytes);

        // 非同期受信を再開する
        udpClient.BeginReceive(OnReceived, udpClient);
    }
#endif
}