﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Unity日志助手类
/// </summary>
public class LogHelper : MonoBehaviour
{
    /// <summary>
    /// 日志文件的输出文件夹
    /// </summary>
    public static string outputPath;

    /// <summary>
    /// 日志文件的完整保存路径(包括文件名+后缀)
    /// </summary>
    public static string filePath;

    /// <summary>
    /// 日志文件的名称
    /// </summary>
    public static string fileName = "gamelog.txt";

    private void Awake()
    {
        outputPath = Path.Combine(Application.persistentDataPath, "logs");
        filePath = Path.Combine(outputPath, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        using (File.Create(filePath))
        {
        }

        LogSysInfo();
    }

    /// <summary>
    /// LogCallback，Unity的Log回调
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    public void LogCallback(string condition, string stackTrace, LogType type)
    {
        //todo:这里可以加一些过滤条件
        LogFileReport(condition,stackTrace,type);
    }

    /// <summary>
    /// 向日志文件中写入一条消息
    /// </summary>
    /// <param name="message"></param>
    private static void WriteLog(string message)
    {
        using (StreamWriter sw = File.AppendText(filePath))
        {
            string logContent = string.Format("{0:G}: {1}", System.DateTime.Now, message);
            sw.Write(logContent);
        }
    }

    /// <summary>
    /// LogFileReport的内部实现
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    private void LogFileReport(string condition, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Log:
                WriteLog(string.Format("[log] {0}", condition));
                break;
            case LogType.Warning:
                WriteLog(string.Format("[warning] {0}", condition));
                break;
            case LogType.Exception:
                WriteLog(string.Format("[exception] {0}:\n{1}", condition, stackTrace));
                break;
            case LogType.Error:
                WriteLog(string.Format("[error] {0}:\n{1}", condition, stackTrace));
                break;
            default:
                WriteLog(string.Format("[unknow] {0}", condition));
                break;
        }
    }

    /// <summary>
    /// 向日志中记录当前设备系统信息
    /// </summary>
    private void LogSysInfo()
    {
        WriteLog(string.Format(string.Format("[{0}] {1}", "sysinfo", CommonHelper.GetDeviceInfo())));
    }
}
