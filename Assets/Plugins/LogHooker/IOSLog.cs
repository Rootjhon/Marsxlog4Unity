/**
* @author    Junqiang Zhu
* @copyright 2017-2019 onemt
* @created   2019年6月17日 17:39:54
* @edit      
*
* @brief     
*/

#if UNITY_IOS && !UNITY_EDITOR
using System;
using UnityEngine;
using System.Runtime.InteropServices;

public partial class LogHook : ILogHandler
{
    #region [Native]
    [DllImport("__Internal", EntryPoint = "LOGERROR")]
    private extern static void LOGERROR(string varLog);

    [DllImport("__Internal", EntryPoint = "LOGASSERT")]
    private extern static void LOGASSERT(string varLog);

    [DllImport("__Internal", EntryPoint = "LOGWARNING")]
    private extern static void LOGWARNING(string varLog);

    [DllImport("__Internal", EntryPoint = "LOGINFO")]
    private extern static void LOGINFO(string varLog);

    [DllImport("__Internal", EntryPoint = "LOGEXCEPTION")]
    private extern static void LOGEXCEPTION(string varLog);
    #endregion

    #region [ILogHandler]
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        LOGEXCEPTION(string.Format("[{0}]\n{1}", exception.Message, exception.StackTrace));
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        switch (logType)
        {
            case LogType.Error:
                LOGERROR(string.Format(format, args));
                break;
            case LogType.Assert:
                LOGASSERT(string.Format(format, args));
                break;
            case LogType.Warning:
                LOGWARNING(string.Format(format, args));
                break;
            case LogType.Log:
                LOGINFO(string.Format(format, args));
                break;
            case LogType.Exception:
                LOGEXCEPTION(string.Format(format, args));
                break;
            default:
                break;
        }
    }
    #endregion
}
#endif