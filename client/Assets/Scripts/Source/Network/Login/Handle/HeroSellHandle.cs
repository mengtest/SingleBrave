﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;

//  HeroSellHandle.cs
//  Author: sanvey
//  2013-12-17

//英雄出售请求应答句柄
public class HeroSellHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.HERO_SELL_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        HeroSellPktAck ack = (HeroSellPktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }

        //更新出售回来的金钱，服务器返回全量
        int getMoney = ack.m_iGetGold;
        Role.role.GetBaseProperty().m_iGold = ack.m_iGold;
        GUIBackFrameTop guitop = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        guitop.UpdateGold();

        foreach (int item in ack.m_lstDeleteIDs)
        {
            Role.role.GetHeroProperty().DelHero(item);
        }

        GUI_FUNCTION.MESSAGEM(MessageOk, "出售获得 " + getMoney + " 金币");

        
    }

    /// <summary>
    /// 出售确定
    /// </summary>
    public static void MessageOk()
    {
        GUIHeroSell herosell = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HEROSELL) as GUIHeroSell;

        herosell.Show();
    }
}