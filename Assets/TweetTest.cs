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

        //TwitterManager.TweetSet(delfunc, "テストツイート" + System.DateTime.Now + "\n#テスト投稿", filename);
        TwitterManager.TweetSet(delfunc, "テストツイート" + System.DateTime.Now + "\n#テスト投稿");

    }

    //コールバック用の関数
    private void CallbackTweetSuccsess()
    {
        Debug.Log("ツイート成功！");

    }

    private void CallbackTweetFailed(string errMsg)
    {
        Debug.Log("ツイート失敗… :" + errMsg);

    }

}