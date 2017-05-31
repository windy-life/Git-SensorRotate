using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor_Rotate
{
    class CommandsToSend
    {
        public byte[] startDebug = new byte[] 
        {//启动配置
            0xEF,0x11,0x11,0x11,0x11,0xFF,0x0D,0x0A
        };
        public byte[] endDebug = new byte[]
        {//停止配置
            0xEF,0x00,0x00,0x00,0x00,0xFF,0x0D,0x0A
        };
        public byte[] readSysMode = new byte[]
        {//读取系统工作模式
            0xED,0xE0,0xFF,0x0D,0x0A
        };
        public byte[] readCAN1 = new byte[]
        {//读取CAN口配置1
            0xED,0xE1,0xFF,0x0D,0x0A
        };
        public byte[] readCAN2 = new byte[]
        {//读取CAN口配置2
            0xED,0xE2,0xFF,0x0D,0x0A
        };
        public byte[] readCommA = new byte[]
        {//读取串口A
            0xED,0xE3,0xFF,0x0D,0x0A
        };
        public byte[] readCommB = new byte[]
        {//读取串口B
            0xED,0xE4,0xFF,0x0D,0x0A
        };
        public byte[] TZeroC = new byte[]
        {//温度零位标定
            0xEC,0x40,0x00,0xFF,0x0D,0x0A
        };
        public byte[] TGainC = new byte[]
        {//温度零位标定EC 42 03 FF 0D 0A
            0xEC,0x42,0x03,0xFF,0x0D,0x0A
        };
        public byte[] XNegativeZeroC = new byte[]
        {//电压零位（负零位）标定EC 10 02 FF 0D 0A
            0xEC,0x10,0x02,0xFF,0x0D,0x0A
        };
        public byte[] XPositiveZeroC = new byte[]
        {//电压零位（正零位）标定EC 10 01 FF 0D 0A
            0xEC,0x10,0x01,0xFF,0x0D,0x0A
        };
        public byte[] XZeroC = new byte[]
        {//X轴倾角角度零位标定EC 10 00 FF 0D 0A
            0xEC,0x10,0x00,0xFF,0x0D,0x0A
        };
        public byte[] YNegativeZeroC = new byte[]
        {//电压零位（负零位）标定EC 20 02 FF 0D 0A
            0xEC,0x20,0x02,0xFF,0x0D,0x0A
        };
        public byte[] YPositiveZeroC = new byte[]
        {//电压零位（正零位）标定EC 20 01 FF 0D 0A
            0xEC,0x20,0x01,0xFF,0x0D,0x0A
        };
        public byte[] YZeroC = new byte[]
        {//X轴倾角角度零位标定EC 20 00 FF 0D 0A
            0xEC,0x20,0x00,0xFF,0x0D,0x0A
        };
        public byte[] Initiate = new byte[]
        {//配置初始化指令EE 00 00 00 00 00 FF 0D 0A
            0xEE,0x00,0x00,0x00,0x00,0x00,0xFF,0x0D,0x0A
        };
    }
}
