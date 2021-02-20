using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDP : MonoBehaviour
{
    static UdpClient udp;
    public string ref_text;
    IPEndPoint remoteEP = null;
    int i = 0;
    //Use this for initialization
    void Start () {
        int LOCA_LPORT = 50007;
        udp = new UdpClient(LOCA_LPORT);
        udp.Client.ReceiveTimeout = 2000;
    }

    // Update is called once per frame
    void Update ()
    {
        IPEndPoint remoteEP = null;
        byte[] data = udp.Receive(ref remoteEP);
        string text = Encoding.UTF8.GetString(data);
        ref_text = text;
    }

    public string check()
    {
        return ref_text;
    }
}