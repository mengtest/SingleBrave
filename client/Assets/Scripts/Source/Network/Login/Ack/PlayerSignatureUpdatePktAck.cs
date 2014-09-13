﻿using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;

//  SignatureUpdatePktAck.cs
//  Author: sanvey
//  2013-12-23


/// <summary>
/// 更新签名应答数据包
/// </summary>
public class PlayerSignatureUpdatePktAck : HTTPPacketBase
{
    public string m_strSign;

    public PlayerSignatureUpdatePktAck()
    {
        this.m_strAction = PACKET_DEFINE.SIGN_UPDATE_REQ;
    }
}


/// <summary>
/// 更新签名应答数据包工厂类
/// </summary>
public class SignatureUpdatePktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取数据包Action
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.SIGN_UPDATE_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        PlayerSignatureUpdatePktAck pkt = PACKET_HEAD.PACKET_ACK_HEAD<PlayerSignatureUpdatePktAck>(json);

        if (pkt.m_iErrorCode != 0)
        {
            return pkt;
        }


        pkt.m_strSign = json["data"]["new_signature"].StringValue;

        return pkt;
    }
}