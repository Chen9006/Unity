using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{

    private static MainUIController _instance;
    public static MainUIController Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public int score = 0;
    public int length = 0;
    public Text msgText;
    public Text scoreText;
    public Text lengthText;
    public Image bgImage;
    private Color tempColor;

    public bool isPause = false;
    public Button pauseButton;
    public Sprite[] pauseSprites;

    public bool hasBorder = true; 

    void Start()
    {
        if(PlayerPrefs.GetInt("Border", 1) == 0)
        {
            hasBorder = false;

            foreach(Transform t in bgImage.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }

    void Update()
    {
        switch (score / 100)
        {

            case 0:
            case 1:
            case 2:
                break;


            case 3:
            case 4:
                ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 2;
                break;


            case 5:
            case 6:
                ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 3;
                break;


            case 7:
            case 8:
                ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 4;
                break;

            case 9:
            case 10:
                ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 5;
                break;

            default:
                ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "无尽阶段";
                break;

        }
    }

    public void updateUI(int s = 5, int l = 1)
    {
        score = score + s;
        length = length + l;
        scoreText.text = "得分:\n" + score;
        lengthText.text = "长度:\n" + length;
    }

    public void Pause()
    {
        isPause = !isPause;

        if(isPause)
        {
            Time.timeScale = 0;
            pauseButton.GetComponent<Image>().sprite = pauseSprites[1];
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.GetComponent<Image>().sprite = pauseSprites[0];
        }

    } 

    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
