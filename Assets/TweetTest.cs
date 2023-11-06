using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetTest : MonoBehaviour
{

    private const string FILENAME = "screenShot.png";

	public void Start()
	{
        TweetMake();

    }
	public void TweetMake()
    {

        TwitterManager.DelFuncs delfunc = new TwitterManager.DelFuncs(CallbackTweetSuccsess, CallbackTweetFailed);
        //string filename = Application.persistentDataPath + "/" + FILENAME;

        //TwitterManager.TweetSet(delfunc, "�e�X�g�c�C�[�g" + System.DateTime.Now + "\n#�e�X�g���e", filename);
        TwitterManager.TweetSet(delfunc, "�e�X�g�c�C�[�g" + System.DateTime.Now + "\n#�e�X�g���e");

    }

    //�R�[���o�b�N�p�̊֐�
    private void CallbackTweetSuccsess()
    {
        Debug.Log("�c�C�[�g�����I");

    }

    private void CallbackTweetFailed(string errMsg)
    {
        Debug.Log("�c�C�[�g���s�c :" + errMsg);

    }

}