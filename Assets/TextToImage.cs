using System;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TextToImage
{

    /// <summary>
    /// Text to Image��API���N�G�X�g
    /// </summary>
    /// <param name="json"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async UniTask<Texture2D> PostT2I(string json, string url, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        var request = new UnityWebRequest(url + "/sdapi/v1/img2img", "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postData);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");


        // API���N�G�X�g�𑗐M
        Debug.Log("Send Prompt");   
        await request.SendWebRequest().WithCancellation(cancellationToken);

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Request Success");

            // ���X�|���X��Json���擾
            var response = request.downloadHandler.text;

            // �u"�v�ň͂܂ꂽ������𒊏o
            var matches = new Regex("\"(.+?)\"").Matches(response);

            // �摜�f�[�^���擾
            var imageData = matches[1].ToString().Trim('"');

            // Base64��byte�^�z��ɕϊ�
            byte[] data = Convert.FromBase64String(imageData);

            // byte�^�z����e�N�X�`���ɕϊ�
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(data);

            return texture;
        }
        else
        {
            Debug.Log("Error:" + request.result);

            Texture2D texture = new Texture2D(1, 1);
            return texture;
        }
    }
}