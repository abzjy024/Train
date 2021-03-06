﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train.Utilities;

namespace Train.Packets
{
    /// <summary>
    /// 车到地——车载设备电话号码
    /// </summary>
    public class Packet003Train : AbstractPacket
    {
        int NID_PACKET;         //8bit
        int L_PACKET;           //13bit
        int N_ITER;             //5bit
        ulong[] NID_RADIO;      //64bit

        public override BitArray Resolve()
        {
            BitArray bitArray = new BitArray(200);
            int[] intArray = new int[] { 8, 13 };
            int[] DataArray = new int[] { NID_PACKET, L_PACKET };
            int pos = 0;
            for (int i = 0; i < intArray.Length; i++)
            {
                Bits.ConvergeBitArray(bitArray, DataArray[i], ref pos, intArray[i]);
            }
            Bits.ConvergeBitArray(bitArray, N_ITER, ref pos, 5);
            for (int i = 0; i < N_ITER; i++)
            {
                Bits.ConvergeBitArray(bitArray, NID_RADIO[i], ref pos, 64);
            }
            return bitArray;
        }
    }
}
