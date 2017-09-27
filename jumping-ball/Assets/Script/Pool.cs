using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    //创建一个字典dic 键为子弹或敌人种类 值为数组 存放多个同样的子弹或敌人
    public Dictionary<string, ArrayList> dic = new Dictionary<string, ArrayList>();

    //方法（函数）：调用后会返回一个属于需要的种类（dic的key值（实际为key + "(Clone)"））的 敌人或子弹（即一个gameobject）。
    //key是子弹或敌人预设体的名字，预设体放在Resources中
    //方法名get（可以自定义） 返回值为 参数为（obj名（子弹 敌人种类 dic的key），激活位置，激活角度）
    public GameObject Get(string key, Vector3 position, Quaternion rotation)
    {
        //Debug.Log(key);
        GameObject go;
        //拼接制作dic的key名，因为instantiate出的gameobject都会自动命名为gameobject(Clone)
        //这里是为了通下面return方法里给key的命名匹配
        string GameObjectName = key + "(Clone)";
        //如果字典里有gameobjectname这个key 并且key对应的数组不为空
        //（有该种类子弹，且该种类子弹中有《已经创建过的》（未激活）的子弹gameobject）
        if (dic.ContainsKey(GameObjectName) && dic[GameObjectName].Count > 0)
        {
            ArrayList list = dic[GameObjectName];//从gameobjectname这个key位置取出数组
            go = (GameObject)list[0];//取出一号位的子弹
            list.RemoveAt(0);  //从列表中去除这个子弹（拿出来用）
            go.SetActive(true);//将子弹gameobject使用时激活
            go.transform.position = position; //参数赋值于子弹                
            go.transform.rotation = rotation; //参数赋值于子弹
        }
        //如果没有
        else
        {
            //
            //Debug.Log("chi" + key);
            //在给定位置创建一个resources中名为给定key的预设体的gameobject
            go = Instantiate(Resources.Load(key), position, rotation) as GameObject;
        }
        //返回创建的东西
        return go;

    }


    //函数（方法） 名return（可自定义） 参数是需要取消激活的对象g
    //将需要取消激活的对象取消激活
    public GameObject Return(GameObject g)
    {
        //获取gameobject的名字，会是一个在上面get方法里创建的（预设体的）gameobject，名字会是gameobject(Clone)；
        string key = g.name;
        //如果字典里有这个key
        if (dic.ContainsKey(key))
        {   //就在这个key所对应的数组中加入这个g  （这个g就是已经用完的子弹，放到这个数组里的gameobjet都是不销毁只是取消激活等待再次利用的gameobject）
            dic[key].Add(g);
        }
        //如果没有这个key
        else
        {
            //建立一个这个key的arraylist 并把g加进去
            dic[key] = new ArrayList() { g };
        }
        //不销毁而是取消激活
        g.SetActive(false);
        //返回g
        return g;
    }

}
