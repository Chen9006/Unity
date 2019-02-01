using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController _instance;

    public Text oneShootCostText;


    public Text goldText;
    public Text lvText;
    public Text lvNameText;
    public Text rewardCountDownText;
    public Text timerCountDownText;


    public Button bigCountDownButton;
    public Button backButton;
    public Button settingButton;

    public Slider EXPSlider;


    public Transform bulletHolder;

    public GameObject[] GunGameObjects;
    public GameObject[] bullet1Gos;
    public GameObject[] bullet2Gos;
    public GameObject[] bullet3Gos;
    public GameObject[] bullet4Gos;
    public GameObject[] bullet5Gos;

    public Color goldColor;

    public GameObject fireEffect;
    public GameObject changeGunEffect;
    public GameObject lvUpTips;
    public GameObject lvUpEffect;
    public GameObject bonusEffect;

    public Sprite[] bgSprites;
    public Image bgImage;
    public int bgIndex = 0;
    public GameObject seaWaveEffect;

    private int costIndex = 0;//使用的是第几档的炮弹
    private int[] oneShootCost =
        {
          5,10,20, 30,
          40,50,60,70,
          80,90, 100,200,
          300,400,500,600,
          700,800,900,1000
         }; //一发子弹的金币量等同于伤害量

    private string[] LVName =
        {
        "黑铁","青铜","白银","黄金","铂金",
        "钻石","大师","宗师","王者"
        };

    public int LV = 0;
    public int EXP = 0;
    public int gold = 500;
    public const int timerCountDown = 240; //240s一次大奖金
    public const int rewardCountDown = 60; //60s一次小奖金
    public float timer = timerCountDown;
    public float rewardTimer = rewardCountDown;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        gold = PlayerPrefs.GetInt("gold", gold);
        LV = PlayerPrefs.GetInt("lv", LV);
        timer = PlayerPrefs.GetFloat("bonusCountDown", timer);
        rewardTimer = PlayerPrefs.GetFloat("rewardCountDown", rewardTimer);
        EXP = PlayerPrefs.GetInt("exp", EXP);
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
        ChangeBulletCost();
        Fire();
        changeBackGround();
    }

    void changeBackGround()
    {
        if (bgIndex != LV / 20)
        {
            bgIndex = LV / 20;

            AudioManager._instance.playEffectAudio(AudioManager._instance.seaWaveClip);

            Instantiate(seaWaveEffect);
            if (bgIndex >= 3)
            {
                bgImage.sprite = bgSprites[3];
            }
            else
            {
                bgImage.sprite = bgSprites[bgIndex];
            }
        }
    }

    void UpdateUI()
    {
        timer -= Time.deltaTime;
        rewardTimer -= Time.deltaTime;

        if (rewardTimer <= 0)
        {
            rewardTimer = rewardCountDown;
            gold += 50;
        }

        if (timer <= 0 && bigCountDownButton.gameObject.activeSelf == false)
        {
            timerCountDownText.gameObject.SetActive(false);
            bigCountDownButton.gameObject.SetActive(true);
        }

        //经验等级换算：升级所需经验=1000+200*当前等级
        while (EXP >= 1000+200*LV)
        {
            EXP = EXP - (1000+200*LV);
            LV++;

            lvUpTips.SetActive(true);
            lvUpTips.transform.Find("Text").GetComponent<Text>().text = LV.ToString();
            Instantiate(lvUpEffect);
            AudioManager._instance.playEffectAudio(AudioManager._instance.lvUpClip);
            StartCoroutine(lvUpTips.GetComponent<HideSelf>().hideSelf(0.6f));

        }

        lvText.text = LV.ToString();
        goldText.text = "$" + gold;

        if ((LV / 10) <= 8)
        {
            lvNameText.text = LVName[LV / 10];
        }
        else
        {
            lvNameText.text = LVName[8];
        }

        rewardCountDownText.text = "  " + (int)rewardTimer / 10 + "  " + (int)rewardTimer % 10;
        timerCountDownText.text = (int)timer + "s";

        EXPSlider.value = (float)EXP / (1000 + LV * 200);
    }


    void ChangeBulletCost()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            OnButtonDecreaseDown();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonAddDown();
        }
    }

    public void OnButtonAddDown()
    {
        GunGameObjects[costIndex / 4].SetActive(false);
        costIndex++;

        AudioManager._instance.playEffectAudio(AudioManager._instance.changeGunClip);
        Instantiate(changeGunEffect);

        costIndex = (costIndex > oneShootCost.Length - 1) ? 0 : costIndex;
        GunGameObjects[costIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCost[costIndex];
    }

    public void OnButtonDecreaseDown()
    {
        GunGameObjects[costIndex / 4].SetActive(false);
        costIndex--;

        AudioManager._instance.playEffectAudio(AudioManager._instance.changeGunClip);
        Instantiate(changeGunEffect);

        costIndex = (costIndex < 0) ? oneShootCost.Length - 1 : costIndex;
        GunGameObjects[costIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCost[costIndex];
    }

    void Fire()
    {
        GameObject[] useBullets = bullet5Gos;
        int bulletIndex;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (gold - oneShootCost[costIndex] >= 0)
            {
                switch (costIndex / 4)
                {
                    case 0: useBullets = bullet1Gos; break;
                    case 1: useBullets = bullet2Gos; break;
                    case 2: useBullets = bullet3Gos; break;
                    case 3: useBullets = bullet4Gos; break;
                    case 4: useBullets = bullet5Gos; break;
                }

                bulletIndex = (LV / 10 >= 9) ? 9 : LV / 10;

                gold -= oneShootCost[costIndex];
                AudioManager._instance.playEffectAudio(AudioManager._instance.FireClip);

                Instantiate(fireEffect);

                GameObject bullet = Instantiate(useBullets[bulletIndex]);
                bullet.transform.SetParent(bulletHolder, false);
                bullet.transform.position = GunGameObjects[costIndex / 4].transform.Find("FirePos").position;
                bullet.transform.rotation = GunGameObjects[costIndex / 4].transform.Find("FirePos").rotation;
                bullet.GetComponent<BulletAttr>().damage = oneShootCost[costIndex];
                bullet.AddComponent<Ef_AutoMove>().dir = Vector3.up;
                bullet.GetComponent<Ef_AutoMove>().speed = bullet.GetComponent<BulletAttr>().speed;

            }
            else // 金币不足,无法发炮,提示金币不足
            {
                StartCoroutine("shortageOfGold");
            }

        }



    }

    public void OnBonusCountDownButtonDown()
    {
        gold += 500;
        AudioManager._instance.playEffectAudio(AudioManager._instance.rewardClip);
        Instantiate(bonusEffect);
        bigCountDownButton.gameObject.SetActive(false);
        timerCountDownText.gameObject.SetActive(true);
        timer = timerCountDown;
    }

    IEnumerator shortageOfGold()
    {
        goldText.color = goldColor;
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        goldText.color = goldColor;
    }

}
