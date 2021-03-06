﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;


//  BattleReliveHandle.cs
//  Author: Lu Zexi
//  2014-03-04





/// <summary>
/// 战斗复活句柄
/// </summary>
public class BattleReliveHandle
{

    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.BATTLE_RELIVE_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        BattleRelivePktAck ack = packet as BattleRelivePktAck;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL_(null, ack.header.desc);
            
        }

        if (!ack.m_bRelive)
        {
            
        }

        Role.role.GetBaseProperty().m_iDiamond = ack.m_iDiamond;

        GUIBattleLose gui = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BATTLE_LOSE) as GUIBattleLose;
        gui.Relive();
        gui.Hiden();

        
    }

}
