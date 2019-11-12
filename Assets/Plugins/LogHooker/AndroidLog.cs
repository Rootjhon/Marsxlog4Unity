/**
* @author    Junqiang Zhu
* @copyright 2017-2019 onemt
* @created   2019年6月17日 17:39:54
* @edit      
*
* @brief     
*/
#if UNITY_ANDROID && !UNITY_EDITOR
using System;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

public partial class LogHook : ILogHandler
{
    #region [Fields]
    private readonly AndroidJavaObject _javaObj = new AndroidJavaObject("com.tencent.mars.xlog.Log");
    private readonly string[] _androidLog = new string[] { "e", "f", "w", "i", "e" };
    #endregion

    #region [ILogHandler]
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        this.LogFormat(LogType.Exception, context, "{0}\n{1}", exception.Message, exception.StackTrace);
    }

    [Il2CppSetOption(Option.ArrayBoundsChecks | Option.NullChecks, false)]
    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        var IsMain = IsMainThread();
        if (!IsMain)
        {
            AndroidJNI.AttachCurrentThread();
        }
        _javaObj.CallStatic(_androidLog[(int)logType], _productName, string.Format(format, args));
        if (!IsMain)
        {
            AndroidJNI.DetachCurrentThread();
        }
    }
    #endregion
}
#endif