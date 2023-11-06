using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTest2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var chatGPTConnection = new AAA.OpenAI.ChatGPTConnection("");
        chatGPTConnection.RequestAsync("Twitterに彼女とベルサール 秋葉原で撮影した写真を投稿する際の文面をハッシュタグ付きで考えてください");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
