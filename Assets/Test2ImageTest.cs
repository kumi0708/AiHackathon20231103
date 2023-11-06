using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Test2ImageTest : MonoBehaviour
{
    [SerializeField] private string url = "http://127.0.0.1:7860";

    [SerializeField] private Image image;
    [SerializeField] private InputField inputField;
    [SerializeField] private Button sendButton;


    [SerializeField]
    private TexturePngConverter save;

    private TextToImage _t2I = new TextToImage();

    private string encodedText;

    [SerializeField]
    private TweetWithImgurLink Tw;


    [System.Serializable]
    public class RequestData
    {
        public string prompt = "girl";
        public Alwayson_Scripts alwayson_scripts;
    }


    public class RequestData2
    {
        public List<string> init_images = new List<string>();
        public string prompt = "girl";
        public Alwayson_Scripts alwayson_scripts;
        public int steps = 16;

    }

    [System.Serializable]
    public class Alwayson_Scripts
    {
        public Args controlnet;
    }

    [System.Serializable]
    public class Args
    {
        public List<Controlnet> args = new List<Controlnet>() ;
    }

    [System.Serializable]
    public class Controlnet
    {
        public string input_image;
        public string module = "reference_only";
        public string model = "none";
    }

    void Start()
    {

        //StartCoroutine(Test());
        if (sendButton != null)
        {
            sendButton.onClick.AddListener(() =>
            {
                SendPrompt(inputField.text);
            });
        }
    }

    public async void SendPrompt(string twitterTxte, string prompt="")
    {
        // Jsonに変換
        RequestData2 requestData = new RequestData2();
        requestData.prompt = prompt;
        if (requestData.prompt =="")
        {
            requestData.prompt = "girl";
        }
        requestData.init_images.Add( encodedText);
        //requestData.input_image.Add("a");
        requestData.alwayson_scripts = new Alwayson_Scripts();

        Controlnet _args = new Controlnet();

        _args.input_image = encodedText;
        //_args.input_image = "b";
        requestData.alwayson_scripts.controlnet = new Args();

        requestData.alwayson_scripts.controlnet.args.Add(_args);

        var json = JsonUtility.ToJson(requestData);

        // リクエスト
        var result = await _t2I.PostT2I(json,url);

        // Texture2DからSpriteに変換
        image.sprite = Sprite.Create(result, new Rect(0, 0, result.width, result.height), Vector2.zero);


        var filePath =  save.Save();

        await Task.Delay(100);

        var tag = "";

        string[] dest = twitterTxte.Split('#');

        for (var i = 1; i < dest.Length; i++)
        {
            //tag += $"#{dest[i]} ";
            tag += $"{dest[i]},";
        }
        twitterTxte = dest[0];
       



        Tw.ClickTweetButton(twitterTxte, filePath,tag);


    }

    //private const string URI = "https://rimage.gnst.jp/rest/img/dvwj2p0m0000/s_00cb.jpg";

    [SerializeField] private RawImage _image;

    public IEnumerator ToBase64String(string url )
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        //画像を取得できるまで待つ
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //取得した画像のテクスチャをRawImageのテクスチャに張り付ける
            var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            //Texture->byte変換
            byte[] byte_Before = texture.EncodeToJPG();

            //BASE64への変換
            encodedText = System.Convert.ToBase64String(byte_Before);
            Debug.Log(encodedText);
        }
    }
}

