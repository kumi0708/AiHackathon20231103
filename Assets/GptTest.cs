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
            new OpenAIChatCompletionAPI.Message(){role = "system", content = "あなたは優秀なAIアシスタントです。"},
            new OpenAIChatCompletionAPI.Message(){role = "user", content = "Twitterに彼女とベルサール 秋葉原で撮影した写真を投稿する際の文面をハッシュタグ付きで考えてください"},
        };

        var request = chatCompletionAPI.CreateCompletionRequest(
            new OpenAIChatCompletionAPI.RequestData() { messages = messages }
        );

        yield return request.Send();

        var message = request.Response.choices[0].message;

        Debug.Log(message.content); // 推論された応答
    }

}
