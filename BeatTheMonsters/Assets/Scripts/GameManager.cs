using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;
using System.Xml;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public bool isPause = true;
    public GameObject menuGameObject;

    public GameObject[] targetGameObjects;



    private void Awake()
    {
        _instance = this;
        Pause();
    }

    private void Update()
    {
        /*按下ESC键，暂停游戏*/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        GameOver();
    }


    /*暂停状态*/
    private void Pause()
    {
        isPause = true;
        menuGameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    /*非暂停状态*/
    private void UnPause()
    {
        isPause = false;
        menuGameObject.SetActive(false);
        Time.timeScale = 1;
 //       Cursor.visible = false;
    }


    private void GameOver()
    {
        if (UIManager._instance.restTime <= 0)
        {
            foreach (GameObject target in targetGameObjects)
            {
                target.GetComponent<TargetManager>().GameOver();
            }

            UIManager._instance.showMessage("时间到");


            //记录得分
            PlayerPrefs.SetInt("lastScore", UIManager._instance.score);

            if (PlayerPrefs.GetInt("TheBestScore", 0) < UIManager._instance.score)
            {
                PlayerPrefs.SetInt("TheBestScore", UIManager._instance.score);
            }

            //   Time.timeScale = 1;

            StartCoroutine(wait(1.0f));
        }
    }

    IEnumerator wait(float t) //协程
    {
        yield return new WaitForSeconds(t);

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    /*创建Save对象并存储当前游戏信息*/
    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        /*遍历所有target
           如果其中有处于激活状态的怪物，就把target的位置信息和激活状态的怪物类型加入List中
         */
        foreach (GameObject targetGO in targetGameObjects)
        {
            TargetManager targetManager = targetGO.GetComponent<TargetManager>();
            if (targetManager.activeMonster != null)
            {
                save.livingTargetPosition.Add(targetManager.targetPosition);
                int type = targetManager.activeMonster.GetComponent<MonsterManager>().monsterType;
                save.livingMonsterTypes.Add(type);
            }
        }

        save.shootNum = UIManager._instance.shootNum;
        save.score = UIManager._instance.score;
        save.restTime = UIManager._instance.restTime;

        return save; //返回Save对象
    }

    /*继续游戏*/
    public void ContinueGame()
    {
        UnPause();
        UIManager._instance.showMessage("");
    }

    /*新游戏*/
    public void NewGame()
    {
        foreach (GameObject target in targetGameObjects)
        {
            target.GetComponent<TargetManager>().UpdateMonster();
        }

        UIManager._instance.score = 0;
        UIManager._instance.shootNum = 0;
        UIManager._instance.restTime = 30;

        UIManager._instance.showMessage("");

        UnPause();
    }

    /*退出游戏*/
    public void ExitGame()
    {
        Application.Quit();
    }

    /*保存游戏*/
    public void SaveGame()
    {
        // SaveByBin();

        SaveByJSON();

        // SaveByXML();

        ////文件存在则显示保存成功
        //if (File.Exists(Application.dataPath + "/StreamingFile" + "/byBin.txt"))
        //{
        //    UIManager._instance.showMessage("保存成功");
        //}

    }

    /*加载游戏*/
    public void LoadGame()
    {
        //LoadByBin();

        LoadByJSON();

        // LoadByXML();

    }

    /*通过读档信息，恢复保存时的游戏状态*/
    private void setGame(Save save)
    {
        //先将所有target里的怪物清空，并重置所有计时
        foreach (GameObject target in targetGameObjects)
        {
            target.GetComponent<TargetManager>().UpdateMonster();
        }

        //通过反序列化得到的Save对象存储的信息，激活怪物
        for (int i = 0; i < save.livingTargetPosition.Count; i++)
        {
            int position = save.livingTargetPosition[i];
            int type = save.livingMonsterTypes[i];

            targetGameObjects[position].GetComponent<TargetManager>().ActivateMonsterByType(type);

        }

        //更新UI显示
        UIManager._instance.shootNum = save.shootNum;
        UIManager._instance.score = save.score;
        UIManager._instance.restTime = save.restTime;

        //调整为未暂停状态
        UnPause();
    }

    /*二进制方法 存档读档*/
    private void SaveByBin()
    {
        //序列化过程(将Save对象转换为字节流)
        Save save = CreateSaveGameObject();

        //创建一个二进制格式化程序
        BinaryFormatter bf = new BinaryFormatter();

        //创建一个文件流
        FileStream fs = File.Create(Application.dataPath + "/StreamingAssets" + "/byBin.txt");

        //用二进制格式化程序的序列化方法来序列化Save对象,参数：创建的文件流和需要序列化的对象
        bf.Serialize(fs, save);
        fs.Close();

    }

    private void LoadByBin()
    {
        //反序列化过程

        if (File.Exists(Application.dataPath + "/StreamingAssets" + "/byBin.txt"))
        {
            //创建一个二进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();

            //打开一个文件流
            FileStream fs = File.Open(Application.dataPath + "/StreamingAssets" + "/byBin.txt", FileMode.Open);

            //调用格式化程序的反序列化方法，将一个文件流转换成一个Save对象
            Save save = (Save)bf.Deserialize(fs);

            fs.Close();

            setGame(save);

            UIManager._instance.showMessage("");
        }

        else
        {
            UIManager._instance.showMessage("存档文件不存在");
        }

    }

    /*XML 存档读档*/
    private void SaveByXML()
    {
        Save save = CreateSaveGameObject();

        //创建XML文件存储路径
        string filePath = Application.dataPath + "/StreamingAssets" + "/byXML.txt";

        //创建XML文档
        XmlDocument xmlDoc = new XmlDocument();
        //创建根节点，即最上层节点
        XmlElement root = xmlDoc.CreateElement("save");
        //设置根节点的值
        root.SetAttribute("name", "saveFile1");

        //创建XmlElement
        XmlElement target;
        XmlElement targetPosition;
        XmlElement monsterType;

        //遍历save中存储的数据，将数据转换成XML格式
        for (int i = 0; i < save.livingTargetPosition.Count; i++)
        {
            target = xmlDoc.CreateElement("target");

            targetPosition = xmlDoc.CreateElement("targetPosition");
            targetPosition.InnerText = save.livingTargetPosition[i].ToString();

            monsterType = xmlDoc.CreateElement("monsterType");
            monsterType.InnerText = save.livingMonsterTypes[i].ToString();

            //设置节点间的关系 root -- target -- (targetPosition,monsterType)
            target.AppendChild(targetPosition);
            target.AppendChild(monsterType);
            root.AppendChild(target);

        }

        //设置分数射击数节点，并设置层级关系 xmDoc -- root -- (target , shootNum ,score )
        XmlElement shootNum = xmlDoc.CreateElement("shootNum");
        shootNum.InnerText = save.shootNum.ToString();
        root.AppendChild(shootNum);

        XmlElement score = xmlDoc.CreateElement("score");
        score.InnerText = save.score.ToString();
        root.AppendChild(score);

        xmlDoc.AppendChild(root);

        xmlDoc.Save(filePath);

        if (File.Exists(Application.dataPath + "/StreamingAssets" + "/byXML.txt"))
        {
            UIManager._instance.showMessage("保存成功");
        }

    }

    private void LoadByXML()
    {
        string filePath = Application.dataPath + "/StreamingAssets" + "/byXML.txt";

        if (File.Exists(filePath))
        {
            Save save = new Save();

            //加载XML文档
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            //通过节点名称获得元素，结果为xmlNodeList类型
            XmlNodeList targets = xmlDoc.GetElementsByTagName("target");

            //遍历所有target节点，获取子节点的InnerText
            if (targets.Count != 0)
            {
                foreach (XmlNode target in targets)
                {
                    XmlNode targetPosition = target.ChildNodes[0];
                    int targetPositionIndex = int.Parse(targetPosition.InnerText);

                    //把得到的值存储到save中
                    save.livingTargetPosition.Add(targetPositionIndex);

                    XmlNode monsterType = target.ChildNodes[1];
                    int monsterTypeIndex = int.Parse(monsterType.InnerText);
                    save.livingMonsterTypes.Add(monsterTypeIndex);

                }
            }

            //得到存档时的射击数
            XmlNodeList shootNum = xmlDoc.GetElementsByTagName("shootNum");
            save.shootNum = int.Parse(shootNum[0].InnerText);

            //得到存档时的分数 
            XmlNodeList score = xmlDoc.GetElementsByTagName("score");
            save.score = int.Parse(score[0].InnerText);

            setGame(save);

            UIManager._instance.showMessage("");
        }

        else
        {
            UIManager._instance.showMessage("存档文件不存在");
        }

    }

    /*JSON 存档读档*/
    private void SaveByJSON()
    {
        Save save = CreateSaveGameObject();
        string filePath = Application.dataPath + "/StreamingAssets" + "/byJson.json";

        //利用JsonMapper将Save对象转换成Json格式字符串
        string saveJsonStr = JsonMapper.ToJson(save);

        //将这个字符串写入文件中
        //创建一个StreamWriter,将字符串写入文件中
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);

        sw.Close();

        UIManager._instance.showMessage("保存成功");
    }

    private void LoadByJSON()
    {
        string filePath = Application.dataPath + "/StreamingAssets" + "/byJson.json";

        if (File.Exists(filePath))
        {
            //创建一个StreamReader，用来读取流
            StreamReader sr = new StreamReader(filePath);

            //将读取的流赋给jsonStr
            string jsonStr = sr.ReadToEnd();

            sr.Close();

            //jsonStr转换为Save对象
            Save save = JsonMapper.ToObject<Save>(jsonStr);

            setGame(save);

            UIManager._instance.showMessage("");
        }

        else
        {
            UIManager._instance.showMessage("存档文件不存在");
        }

    }
}
