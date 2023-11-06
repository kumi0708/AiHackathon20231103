using System.IO;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �e�N�X�`���[ �� Png�摜 �̕ϊ��ƕۑ��Ɠǂݍ���
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


        //�ǂݍ���
        byte[] bytes = File.ReadAllBytes(path);
        //�摜���e�N�X�`���ɕϊ�
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


        //�Z�[�u�{�^��
        //_saveButton.OnPointerClickAsObservable().Subscribe(_ => ConvertToPngAndSave(GetSavePath(IMAGE_SAVE_FOLDER))).AddTo(this);
        //���[�h�{�^��
        //_loadButton.OnPointerClickAsObservable().Subscribe(_ => ConvertToTextureAndLoad(GetSavePath(IMAGE_SAVE_FOLDER))).AddTo(this);
        //���Z�b�g�{�^��
        //_resetButton.OnPointerClickAsObservable().Subscribe(_ => _painter.ResetTexture());
    }

    ///  /// <summary>
    /// �ۑ���̃p�X�擾
    /// </summary>
    /// <param name="folderName">��؂�̃t�H���_��</param>
    /// <returns>�ۑ���̃p�X</returns>
    private string GetSavePath(string folderName)
    {
        string directoryPath = Application.persistentDataPath + "/" + folderName + "/";

        var year = System.DateTime.Now.Year;
        //��
        var month = System.DateTime.Now.Month;
        //��
        var day = System.DateTime.Now.Day;
        //��
        var hour = System.DateTime.Now.Hour;
        //��
        var minute = System.DateTime.Now.Minute;
        //�b
        var second = System.DateTime.Now.Second;
        //�~���b
        var millisecond = System.DateTime.Now.Millisecond;

        var time = System.DateTime.Now.ToString();
        time = $"{year}-{month}-{day}-{hour}-{second}-{millisecond}"; 
        if (!Directory.Exists(directoryPath))
        {
            //�܂����݂��ĂȂ�������쐬
            Directory.CreateDirectory(directoryPath);
            return directoryPath + $"paint{time}.png";
        }

        return directoryPath + $"paint{time}.png";
    }

    /// <summary>
    /// �摜�ɕϊ����ۑ�
    /// </summary>
    private void ConvertToPngAndSave(string path)
    {
        //Png�ɕϊ�
        byte[] bytes = _paintImage.sprite.texture.EncodeToPNG();
        //�ۑ�
        File.WriteAllBytes(path, bytes);
    }

    /// <summary>
    /// �e�N�X�`���ɕϊ����ǂݍ���
    /// </summary>
    private void ConvertToTextureAndLoad(string path)
    {
        //�ǂݍ���
        byte[] bytes = File.ReadAllBytes(path);
        //�摜���e�N�X�`���ɕϊ�
        Texture2D loadTexture = new Texture2D(2, 2);
        loadTexture.LoadImage(bytes);
        //�e�N�X�`�����X�v���C�g�ɕϊ�
        _paintImage.sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), Vector2.zero);
    }
}
