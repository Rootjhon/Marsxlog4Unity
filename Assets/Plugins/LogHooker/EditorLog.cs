/**
* @author    Junqiang Zhu
* @copyright 2017-2019 onemt
* @created   2019年6月17日 17:39:54
* @edit      
*
* @brief     
*/
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
using System;
using UnityEngine;

public partial class LogHook : ILogHandler
{
    #region [Fields]
    private ILogHandler _defaultLogHandler = Debug.unityLogger.logHandler;
    #endregion

    #region [ILogHandler]
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        _defaultLogHandler.LogException(exception, context);
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        var logTime = DateTime.Now;
        var tempTimeSpaneStr = string.Format("{0:00}-{1:00} {2:00}:{3:00}:{4:00}.{5:000} | ",
            logTime.Month, logTime.Day, logTime.Hour, logTime.Minute, logTime.Second, logTime.Millisecond);
        _defaultLogHandler.LogFormat(logType, context, tempTimeSpaneStr + format, args);
    }
    #endregion
}
#endif