using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using System;
using System.Text;
using System.IO;
using System.Threading;

public sealed class NewBehaviourScript
{
    #region aaa
    //Animator controller;

    // Start is called before the first frame update
    //void Start()
    //{
    //controller.SetTrigger("Trigger");
    // 以下是底层的实现机制，若制作一个类似行为树系统就不太一样？
    //AudioClip.Create("sds",2,2,2,1, new AudioClip.PCMReaderCallback(adsd),)
    //}
    //public void adsd(float[] a) {
    //}
    // Update is called once per frame
    //void Update()
    //{
    //    //控制主摄像机x、y轴方向上的移动
    //    Camera.main.transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 
    //        Input.GetAxis("Vertical"), 0) * Time.deltaTime);
    //}

    #endregion

    #region int
    private static int _intLength = 4;
    public static int INT_LENGTH { get { return _intLength; } }

    /// <summary>
    /// 以字节数组的形式返回指定的32位有符号整数值
    /// </summary>
    /// <param name="value">要转换的数字</param>
    /// <returns>长度为4的字节数组；</returns>
    /// 
    public static byte[] GetBytes(int value)
    {
        return BitConverter.GetBytes(value);
    }

    ///<summary>
    ///返回由字节数组中前四个字节转换来的32位有符号整数
    /// </summary>
    /// <param name="value">字节数组</param>
    /// <returns>由四个字节构成的32位有符号整数</returns>
    /// 
    public static int GetInt32(byte[] value)
    {
        return BitConverter.ToInt32(value, 0);
    }

    ///<summary>
    ///返回有字节数组中指定位置的四个字节转换来的32位有符号整数
    /// </summary>
    /// <param name="value">字节数组</param>
    /// <param name="startIndex">value内的起始位置</param>
    /// <returns>由四个字节构成的32位有符号整数</returns>
    /// 
    public static int GetInt32(byte[] value, int startIndex)
    {
        return BitConverter.ToInt32(value, startIndex);
    }

    #endregion

    #region string
    ///<summary>
    ///以UTF-8字节数组的形式返回指定的字符串；
    /// </summary>
    /// <param name="value">要转换的字符串</param>
    /// <returns> UTF-8字节数组</returns>
    /// 
    public static byte[] GetBytes(string value)
    {
        return Encoding.UTF8.GetBytes(value);
    }

    ///<summary>
    ///返回由UTF-8字节数组转换来的字符串
    /// </summary>
    /// <param name="value">UTF-8字节数组</param>
    /// <returns>字符串</returns>
    /// 
    public static string GetString(byte[] value)
    {
        if (value == null)
        {
            throw new Exception("value is invalid.");

        }
        return Encoding.UTF8.GetString(value, 0, value.Length);
    }
    #endregion

    //protected static void ReadString(ref byte[] byteArr, ref int byteOffset, out string str)
    //{
    //    int len;
    //    BinaryReader
    //}

    #region Txt
    /// <summary>
    /// 保存txt
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="contents">内容</param>
    //public static void Write2Txt(string path, )
    #endregion

    #region 申请引用对象
    //public int spawn(Type type)
    //{
    //    return 1;
    //}



    //public T spawn<T>() where T : class, new()
    //{
    //    Type type = typeof(T);

    //    return spawn(type) as T;
    //}
    #endregion

    StackTrace ST = new StackTrace();

}