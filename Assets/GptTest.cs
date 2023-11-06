using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GptTest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ChatCompletionRequest());
    }

    IEnumerator ChatCompletionRequest()
    {
        var chatCompletionAPI = new OpenAIChatCompletionAPI("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");

        List<OpenAIChatCompletionAPI.Message> messages = new List<OpenAIChatCompletionAPI.Message>()
        {
            new OpenAIChatCompletionAPI.Message(){role = "system", content = "���Ȃ��͗D�G��AI�A�V�X�^���g�ł��B"},
            new OpenAIChatCompletionAPI.Message(){role = "user", content = "Twitter�ɔޏ��ƃx���T�[�� �H�t���ŎB�e�����ʐ^�𓊍e����ۂ̕��ʂ��n�b�V���^�O�t���ōl���Ă�������"},
        };

        var request = chatCompletionAPI.CreateCompletionRequest(
            new OpenAIChatCompletionAPI.RequestData() { messages = messages }
        );

        yield return request.Send();

        var message = request.Response.choices[0].message;

        Debug.Log(message.content); // ���_���ꂽ����
    }

}
