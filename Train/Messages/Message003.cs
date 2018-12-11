﻿using System;
using System.Collections;
using Train.Packets;
using Train.Utilities;

namespace Train.Messages
{
    public class Message003:AbstractRecvMessage
    {
        /// <summary>
        /// 地到车——行车许可
        /// </summary>
        const int MESSAGEID = 3;
        int ID;

        Packet015 p15 = new Packet015();
        AbstractPacket ap;          //可选择的信息包

        public override void Resolve(byte[] recvData)
        {
            BitArray bitArray = new BitArray(recvData);
            Bits.ToByte(recvData, bitArray);
            bitArray = new BitArray(recvData);

            int[] intArray = new int[] { 8, 10, 32, 1, 24 };
            int Len = intArray.Length;
            int[] resultArray = new int[Len];
            int i = 0, pos = 0;
            for (i = 0; i < Len; i++)
            {
                resultArray[i] = Bits.ToInt(bitArray, ref pos, intArray[i]);
            }

            NID_MESSAGE = resultArray[0];
            L_MESSAGE = resultArray[1];
            T_TRAIN = Convert.ToUInt32(resultArray[2]);
            if (resultArray[3] == 1)
            {
                M_ACK = true;
            }
            else
            {
                M_ACK = false;
            }
            NID_LRBG = resultArray[4];
            bitArray = Bits.SubBitArray(bitArray, pos, bitArray.Length - pos);
            p15.Resolve(bitArray);
            pos = p15.GetPacketLength();
            bitArray = Bits.SubBitArray(bitArray, pos, bitArray.Length - pos);
            int start = 0;
            ID = Bits.ToInt(bitArray, ref start, 8);
            ap = AbstractPacket.GetPacket(ID);
            ap.Resolve(bitArray);
        }
        public override int GetMessageID()
        {
            return MESSAGEID;
        }
    }
}