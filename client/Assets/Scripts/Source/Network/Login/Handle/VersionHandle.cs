﻿
using System.Collections.Generic;
using Game.Base;
using Game.Resource;
using UnityEngine;
using Game.Network;

//  VersionHandle.cs
//  Author: Lu zexi
//  2013-12-11


/// <summary>
/// 版本句柄
/// </summary>
public class VersionHandle
{
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        VersionAckPkt pkt = (VersionAckPkt)packet;

        GUI_FUNCTION.LOADING_HIDEN();

        if (pkt.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, pkt.header.desc);
            return;
        }

        //记录资源地址
        Debug.Log(pkt.m_strResTxtPath + " path");
        GAME_DEFINE.RES_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_RES_PATH;
        GAME_DEFINE.RESOURCE_MODEL_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_MODEL_PATH;
        GAME_DEFINE.RESOURCE_TEX_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_TEX_PATH;
        GAME_DEFINE.RESOURCE_EFFECT_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_EFFECT_PATH;
        GAME_DEFINE.RESOURCE_AVATAR_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_AVATAR_PATH;
        GAME_DEFINE.RESOURCE_ITEM_PATH = pkt.m_strResTxtPath + GAME_DEFINE.RESOURCE_ITEM_PATH;
        GAME_DEFINE.RES_VERSION = pkt.m_iResVersion;

        if (pkt.m_iVersion != GAME_SETTING.VERSION)
        {
            PlatformManager.GetInstance().UpdateVersion(pkt.m_strVersionPath);
            return;
        }

        //GAME_DEFINE.RESOURCE_GUI_PATH = "gui";
        //GAME_DEFINE.RESOURCE_EFFECT_PATH = "effect";
        //GAME_DEFINE.RESOURCE_TEX_PATH = "tex";
        //GAME_DEFINE.RESOURCE_MODEL_PATH = "model";
        //GAME_DEFINE.RESOURCE_TABLE_PATH = "table";

        //加载资源文件
        ResourceMgr.ClearProgress();
		
		CScene.Switch<LoadingScene>();
		Debug.Log (GAME_DEFINE.RES_PATH);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RES_PATH);

        return;
    }


    /// <summary>
    /// 资源下载完成
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="asset"></param>
    private static void DownLoadCallBack2(string resName, object asset, object[] arg)
    {
        Debug.Log("ok 2" + resName);
    }

    /// <summary>
    /// 资源下载完成
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="asset"></param>
    private static void DownLoadCallBack3(string resName, object asset, object[] arg)
    {
        Debug.Log("ok 3" + resName);

        int version = (int)arg[0];
        string md5 = (string)arg[1];

        Debug.Log(version + " version");
        Debug.Log(md5 + " md5");

        PlayerPrefs.SetString(resName, md5);
        PlayerPrefs.SetInt(resName + "V", version);
        PlayerPrefs.Save();
    }

	//
}


/// <summary>
/// Send agent.
/// </summary>
public partial class SendAgent
{
	/// <summary>
	/// 发送版本数据请求
	/// </summary>
	public static void SendVersionReq()
	{
		VersionReqPkt packet = new VersionReqPkt();
		SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, packet , VersionHandle.Excute );
	}
}
