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
/// ClientID�� https://api.imgur.com/oauth2/addclient �Ŏ擾
/// 
/// ClientID��Y�ꂽ�ꍇ�� https://imgur.com/account/settings/apps �Ŋm�F
/// 
/// </summary>

public class TweetWithImgurLink : MonoBehaviour
{
    // Plugins
    [DllImport("__Internal")] private static extern void OpenNewWindow(string URL);

    // Inspector
    [SerializeField] private string imgurClientID;


    // �c�C�[�g�{�^�����������Ƃ��̏���
    public void ClickTweetButton( string tw, string file, string tag)
    {
        // �摜�����N�t���c�C�[�g
        StartCoroutine(Tweet(tw, file, tag));
    }


    // �摜�����N�t���c�C�[�g
    private IEnumerator Tweet(string comment,string filePath, string tag)
    {
        // 1. �X�N���[���V���b�g���B��
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

        // 2. imgur�փA�b�v���[�h
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


        // 3. �摜�����N�t���c�C�[�g
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