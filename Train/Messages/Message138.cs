﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train.Packets;
using Train.Utilities;
using Train.Data;

namespace Train.Messages
{
    public class Message138 : AbstractSendMessage
    {
        /// <summary>
        /// 车到地——拒绝缩短MA的请求
        /// </summary>
        const int MESSAGEID = 138;
        int ID01;

        AbstractPacket ap01;        //可选择的信息包0/1

        const int BitArrayLEN = 80;
        const int byteLEN = BitArrayLEN / 8;

        public override byte[] Resolve()
        {
            BitArray bitArray = new BitArray(BitArrayLEN);
            int[] intArray = new int[] { 8, 10, 32, 24, 32 };
            int[] DataArray = new int[] { NID_MESSAGE, L_MESSAGE, 0, NID_ENGINE, 0 };
            int pos = 0;
            for (int i = 0; i < intArray.Length; i++)
            {
                if (i == 2)
                {
                    Bits.ConvergeBitArray(bitArray, T_TRAIN, ref pos, intArray[i]);
                }
                else if (i == 4)
                {
                    Bits.ConvergeBitArray(bitArray, T_TRAIN2, ref pos, intArray[i]);
                }
                else
                {
                    Bits.ConvergeBitArray(bitArray, DataArray[i], ref pos, intArray[i]);
                }
            }
            ap01 = AbstractPacket.GetPacket(ID01);
            BitArray bit = ap01.Resolve();
            for (int i = 0; i < bit.Length; i++)
            {
                bitArray[pos] = bit[i];
                pos++;
            }

            byte[] sendData = new byte[byteLEN];
            Bits.ToByte(sendData, bitArray);

            return sendData;
        }
        public override int GetMessageID()
        {
            return MESSAGEID;
        }
    }
}
