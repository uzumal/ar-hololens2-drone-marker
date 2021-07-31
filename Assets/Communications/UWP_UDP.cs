using UnityEngine;
using UnityEngine.Assertions;
using System.Text;

#if UNITY_EDITOR
using System.Net;
using System.Net.Sockets;
#else
using System;
using System.IO;
using Windows.Networking.Sockets;
#endif

public class UWP_UDP : MonoBehaviour
{
    public string ref_text;
    [SerializeField]
    int listenPort = 3333;

    void OnMessage(string msg)
    {
        //ここでメッセージの処理
        Debug.Log("I Receive " + msg);
        ref_text = msg;

    }

    public string check(){
        return ref_text;
    }
    
#if UNITY_EDITOR
    UdpClient udpClient;
    IPEndPoint endPoint;

    void Start()
    {
        
        Debug.Log("UnityStart");
        endPoint = new IPEndPoint(IPAddress.Any, listenPort);
        udpClient = new UdpClient(endPoint);
    }

    void Update()
    {
        while (udpClient.Available > 0) {
            byte[] data = udpClient.Receive(ref endPoint);
            string text = Encoding.UTF8.GetString(data);
            OnMessage(text);
        }
    }
#else
    DatagramSocket socket;
    object lockObject = new object();

    const int MAX_BUFFER_SIZE = 1024;

    async void Start()
    {
        Debug.Log("Start");
        try {
            socket = new DatagramSocket();
            socket.MessageReceived += OnMessage;
            await socket.BindServiceNameAsync(listenPort.ToString());
        } catch (System.Exception e) {
            Debug.LogError(e.ToString());
        }
    }

    void Update()
    {
    }

    async void OnMessage(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
    {
        // using (var stream = args.GetDataStream().AsStreamForRead()) {
        //     byte[] buffer = new byte[MAX_BUFFER_SIZE];
        //     await stream.ReadAsync(buffer, 0, MAX_BUFFER_SIZE);
        //     lock (lockObject) {
        //         OnMessage(Encoding.UTF8.GetString(buffer));
        //     }
        // }
        StreamReader reader = new StreamReader(args.GetDataStream().AsStreamForRead());
        string ms = await reader.ReadLineAsync();
        Debug.Log(ms);
    }
#endif
}