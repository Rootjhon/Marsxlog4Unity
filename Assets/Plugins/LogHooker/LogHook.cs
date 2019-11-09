/**
* @file      LogHook.cs
* @author    Junqiang Zhu
* @copyright 2017-2019 onemt
* @created   2019年6月17日 17:39:54
* @edit      
*
* @brief     
*/

using System;
using UnityEngine;
using System.Threading;

using UDebug = UnityEngine.Debug;

#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
#endif
public partial class LogHook
{
    #region [Fields]
    private string _productName = Application.productName;
    private static int _mainThreadId = Thread.CurrentThread.ManagedThreadId;
    #endregion

    #region [Hooker]
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void HookLogHandler()
    {
        UDebug.unityLogger.logHandler = new LogHook();
    }
    #endregion

    #region [Tools]
    private bool IsMainThread()
    {
        return Thread.CurrentThread.ManagedThreadId == _mainThreadId;
    }
    #endregion
}