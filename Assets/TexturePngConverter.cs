using System.IO;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// テクスチャー ⇔ Png画像 の変換と保存と読み込み
/// </summary>
public class TexturePngConverter : MonoBehaviour
{
    //[SerializeField] private Button _saveButton;
    //[SerializeField] private Button _loadButton;
    //[SerializeField] private Button _resetButton;
    [SerializeField] private Image _paintImage;
    //[SerializeField] private Painter _painter;

    private const string IMAGE_SAVE_FOLDER = "Image";
    [SerializeField] private Texture2D tex2d;

    public string Save()
    {
        var path = GetSavePath(IMAGE_SAVE_FOLDER);
        ConvertToPngAndSave(path);

        tex2d = Resources.Load("heart_frame_01_square_a_pink") as Texture2D;


        //読み込み
        byte[] bytes = File.ReadAllBytes(path);
        //画像をテクスチャに変換
        Texture2D texture2 = new Texture2D(2, 2);
        texture2.LoadImage(bytes);

        Texture2D combinedTexture = new Texture2D(texture2.width, texture2.height);

        for (int x = 0; x < tex2d.width; x++)
        {
            for (int y = 0; y < tex2d.height; y++)
            {
                Color color1 = tex2d.GetPixel(x, y);
                Color color2 = texture2.GetPixel(x, y);
                Color newColor = color1 + color2;
                combinedTexture.SetPixel(x, y, newColor);
            }
        }
       
        byte[] bytes2 = combinedTexture.EncodeToPNG();
        string filePath = path;
        File.WriteAllBytes(filePath, bytes2);

        return filePath;
    }

    private void Start()
    {

        tex2d = Resources.Load("heart_frame_01_square_a_pink") as Texture2D;


        //セーブボタン
        //_saveButton.OnPointerClickAsObservable().Subscribe(_ => ConvertToPngAndSave(GetSavePath(IMAGE_SAVE_FOLDER))).AddTo(this);
        //ロードボタン
        //_loadButton.OnPointerClickAsObservable().Subscribe(_ => ConvertToTextureAndLoad(GetSavePath(IMAGE_SAVE_FOLDER))).AddTo(this);
        //リセットボタン
        //_resetButton.OnPointerClickAsObservable().Subscribe(_ => _painter.ResetTexture());
    }

    ///  /// <summary>
    /// 保存先のパス取得
    /// </summary>
    /// <param name="folderName">区切りのフォルダ名</param>
    /// <returns>保存先のパス</returns>
    private string GetSavePath(string folderName)
    {
        string directoryPath = Application.persistentDataPath + "/" + folderName + "/";

        var year = System.DateTime.Now.Year;
        //月
        var month = System.DateTime.Now.Month;
        //日
        var day = System.DateTime.Now.Day;
        //時
        var hour = System.DateTime.Now.Hour;
        //分
        var minute = System.DateTime.Now.Minute;
        //秒
        var second = System.DateTime.Now.Second;
        //ミリ秒
        var millisecond = System.DateTime.Now.Millisecond;

        var time = System.DateTime.Now.ToString();
        time = $"{year}-{month}-{day}-{hour}-{second}-{millisecond}"; 
        if (!Directory.Exists(directoryPath))
        {
            //まだ存在してなかったら作成
            Directory.CreateDirectory(directoryPath);
            return directoryPath + $"paint{time}.png";
        }

        return directoryPath + $"paint{time}.png";
    }

    /// <summary>
    /// 画像に変換＆保存
    /// </summary>
    private void ConvertToPngAndSave(string path)
    {
        //Pngに変換
        byte[] bytes = _paintImage.sprite.texture.EncodeToPNG();
        //保存
        File.WriteAllBytes(path, bytes);
    }

    /// <summary>
    /// テクスチャに変換＆読み込み
    /// </summary>
    private void ConvertToTextureAndLoad(string path)
    {
        //読み込み
        byte[] bytes = File.ReadAllBytes(path);
        //画像をテクスチャに変換
        Texture2D loadTexture = new Texture2D(2, 2);
        loadTexture.LoadImage(bytes);
        //テクスチャをスプライトに変換
        _paintImage.sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), Vector2.zero);
    }
}
