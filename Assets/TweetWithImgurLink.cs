using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 
/// Unity 2018.2.11f1
/// 
/// ClientIDは https://api.imgur.com/oauth2/addclient で取得
/// 
/// ClientIDを忘れた場合は https://imgur.com/account/settings/apps で確認
/// 
/// </summary>

public class TweetWithImgurLink : MonoBehaviour
{
    // Plugins
    [DllImport("__Internal")] private static extern void OpenNewWindow(string URL);

    // Inspector
    [SerializeField] private string imgurClientID;


    // ツイートボタンを押したときの処理
    public void ClickTweetButton( string tw, string file, string tag)
    {
        // 画像リンク付きツイート
        StartCoroutine(Tweet(tw, file, tag));
    }


    // 画像リンク付きツイート
    private IEnumerator Tweet(string comment,string filePath, string tag)
    {
        // 1. スクリーンショットを撮る
        //string fileName = string.Format("{0:yyyyMMddHmmss}", DateTime.Now);
        //string filePath = Application.persistentDataPath + "/" + fileName + ".png";
        //ScreenCapture.CaptureScreenshot(filePath);
        /*
        float startTime = Time.time;
        while (File.Exists(filePath) == false)
        {
            if (Time.time - startTime > 6.0f)
            {
                yield break;
            }
            else
            {
                yield return null;
            }
        }
        */
        byte[] imageData = File.ReadAllBytes(filePath);
        //File.Delete(filePath);  

        // 2. imgurへアップロード
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("image", Convert.ToBase64String(imageData));
        wwwForm.AddField("type", "base64");

        UnityWebRequest www;
        www = UnityWebRequest.Post("https://api.imgur.com/3/image.xml", wwwForm);
        www.SetRequestHeader("AUTHORIZATION", "Client-ID " + imgurClientID);
        yield return www.SendWebRequest();

        string uploadedURL = "";

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            XDocument xDoc = XDocument.Parse(www.downloadHandler.text);
            uploadedURL = xDoc.Element("data").Element("link").Value;
        }


        // 3. 画像リンク付きツイート
        string tweetURL = "http://twitter.com/intent/tweet?text=" + comment + "&url=" + uploadedURL + "&hashtags=" + tag;
        //string tweetURL = "http://twitter.com/intent/tweet?text=" + comment + "&url=" + uploadedURL;

        Debug.Log(tweetURL);
#if UNITY_EDITOR
        Application.OpenURL(tweetURL);
#elif UNITY_WEBGL
          OpenNewWindow(tweetURL);
#else
          Application.OpenURL(tweetURL);
#endif
    }
}