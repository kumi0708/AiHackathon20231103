using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTest2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var chatGPTConnection = new AAA.OpenAI.ChatGPTConnection("");
        chatGPTConnection.RequestAsync("Twitter�ɔޏ��ƃx���T�[�� �H�t���ŎB�e�����ʐ^�𓊍e����ۂ̕��ʂ��n�b�V���^�O�t���ōl���Ă�������");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
