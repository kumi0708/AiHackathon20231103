using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.IO.Ports;
using UnityEngine;
using UniRx;

public class getLocation : MonoBehaviour
{
    [SerializeField]
    private Text debugtex;

    [SerializeField]
    private Location location;

    [SerializeField]
    private Serial2 sri;

    public bool buttonDown = false;
    public bool buttonDownBefore = false;

    private string clientID = "";
    private string url  = "https://map.yahooapis.jp/placeinfo/V1/get?appid=" +clientID + "&output=json";
    private string url2 = "https://map.yahooapis.jp/search/local/V1/localSearch?appid=" + clientID + "&detail=full&image=true&&output=json";
    private string url3 = "https://www.bellesalle.co.jp/assets_c/2018/12/1fa16abee67bd5889bb3ebb1d3c0420922c8f8d2-thumb-autox460-3487.jpg";

    public string TwitterSS="";

    private string place = "�x���T�[�� �H�t��";

    private string twitterTxte = "�ō��̔ޏ��ƃx���T�[���H�t���ŉ߂����K���Ȏ��ԁB#�x���T�[���H�t�� #�ޏ��Ƃ̃f�[�g #�������v���o";
    [SerializeField]
    private Test2ImageTest img2image;

    // Start is called before the first frame update
    void Start()
    {
        buttonDown = false;

        OpneSerialPort();
    }


    void OpneSerialPort()
    {

        this.serial = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);

        try
        {
            this.serial.Open();
            //�ʃX���b�h�Ŏ��s  
            Scheduler.ThreadPool.Schedule(() => ReadData()).AddTo(this);
        }
        catch (Exception e)
        {
            Debug.Log("�|�[�g���J���܂���ł����B�ݒ肵�Ă���l���Ԉ���Ă���ꍇ������܂�");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            getLoc();
        }
        if (buttonDown != buttonDownBefore )
        {
            debugtex.text = location.error;

            debugtex.text = "Update";
            buttonDown = false;
            buttonDownBefore = buttonDown;
            //if (location.getFlag)
            {
                string ss = url + $"&lat={location.latitude}" + $"&lon={location.longitude}";
                //url += $"&lat={location.latitude}" + $"&lon={location.longitude}";
                debugtex.text = ss;
                StartCoroutine(Method(ss));
            }
        }
    }
    public void getLoc()
    {
        buttonDown = true;
        //StartCoroutine(location.StartLocationService());

        location.latitude = 35.69999335336424f;
        location.longitude = 139.77086886230634f;
        debugtex.text = "getLoc";
    }

    private IEnumerator Method(string url)
    {
        //1.UnityWebRequest�𐶐�
        UnityWebRequest request = UnityWebRequest.Get(url);

        //2.SendWebRequest�����s���A����M�J�n
        yield return request.SendWebRequest();

        //3.isNetworkError��isHttpError�ŃG���[����
        if (request.isHttpError || request.isNetworkError)
        {
            //4.�G���[�m�F
            Debug.Log(request.error);
        }
        else
        {
            //4.���ʊm�F
            Debug.Log(request.downloadHandler.text);

            yahoo.Application app =  yahoo.parameters.Deserialize(request.downloadHandler.text);

            string ss = url2 + $"&lat={location.latitude}" + $"&lon={location.longitude}" + $"&query =" + app.ResultSet.Result[0].Name;

            StartCoroutine(Method2(ss));

        }
    }
    private IEnumerator Method2(string url)
    {

        //1.UnityWebRequest�𐶐�
        UnityWebRequest request = UnityWebRequest.Get(url);

        //2.SendWebRequest�����s���A����M�J�n
        yield return request.SendWebRequest();

        //3.isNetworkError��isHttpError�ŃG���[����
        if (request.isHttpError || request.isNetworkError)
        {
            //4.�G���[�m�F
            Debug.Log(request.error);
        }
        else
        {
            //4.���ʊm�F
            Debug.Log(request.downloadHandler.text);

            yahoo2.Application app = yahoo2.parameters.Deserialize(request.downloadHandler.text);
            app.Feature[0].Property.Detail.Image1 = url3;
            debugtex.text = app.Feature[0].Property.Detail.Image1 = url3;
            //place = 
            StartCoroutine(img2image.ToBase64String(url3));
            
            yield return new WaitForSeconds(1);

            img2image.SendPrompt(twitterTxte);

            //StartCoroutine(ChatCompletionRequest(place));

            TwitterSS = twitterTxte;


        }
    }

    IEnumerator ChatCompletionRequest( string ss )
    {
        var chatCompletionAPI = new OpenAIChatCompletionAPI("");

        List<OpenAIChatCompletionAPI.Message> messages = new List<OpenAIChatCompletionAPI.Message>()
        {
            new OpenAIChatCompletionAPI.Message(){role = "system", content = "���Ȃ��͗D�G��AI�A�V�X�^���g�ł��B"},
            new OpenAIChatCompletionAPI.Message(){role = "user", content = $"Twitter�ɔޏ���{place}�ŎB�e�����ʐ^�𓊍e����ۂ̕��ʂ��n�b�V���^�O�t���ōl���Ă�������"},
        };

        var request = chatCompletionAPI.CreateCompletionRequest(
            new OpenAIChatCompletionAPI.RequestData() { messages = messages }
        );

        yield return request.Send();

        var message = request.Response.choices[0].message;

        Debug.Log(message.content); // ���_���ꂽ����
        TwitterSS = message.content;
    }


    public string portName = "COM3";
    public int baudRate = 115200;

    private SerialPort serial=null;
    private bool isLoop = true;

    //�f�[�^��M���ɌĂ΂��
    public void ReadData()
    {
        Debug.Log($"{twitterTxte}");
        while (this.isLoop)
        {
            //ReadLine�œǂݍ���
            string message = this.serial.ReadLine();
            Debug.Log(message);
            getLoc();
        }

    }

    void OnDestroy()
    {
        this.isLoop = false;
        this.serial.Close();
    }

}
