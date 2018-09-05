using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Modbus.Device;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

namespace ModbusTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            //myModbus.Writestringtosingleregister(1,03,"ddd",);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //myModbus.ReadData();
            ModbusSerialRtuMasterReadRegisters();

        }
        /// <summary>
        /// 读机械爪状态数据
        /// </summary>
        public void ModbusSerialRtuMasterReadRegisters()
        {
            using (SerialPort port = new SerialPort("COM3"))
            {
                // configure serial port
                port.BaudRate = 115200;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                // create modbus master
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                byte slaveId = 9;
                ushort startAddress = 2000;
                ushort numRegisters = 3;

                // read five registers		
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
                //Task<ushort[]> registers1 = master.ReadHoldingRegistersAsync(slaveId, startAddress, numRegisters);
                // registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
                // registers1 = master.ReadHoldingRegistersAsync(slaveId, startAddress, numRegisters);
                //分割状态位
                ushort GRIPPER_STATUS = Convert.ToUInt16(registers[0] / 256);//取高两位,夹爪状态
                ushort FAULT_STATUS = Convert.ToUInt16(registers[1] / 256);//取高两位
                ushort POS_REQUEST = Convert.ToUInt16(registers[1] % 256);//取低两位
                ushort POSITION = Convert.ToUInt16(registers[2] / 256);//取高两位
                ushort CURRENT = Convert.ToUInt16(registers[2] % 256);//取低两位
                //for (int i = 0; i < numRegisters; i++)
                //    Console.WriteLine("Register {0}={1}", startAddress + i, registers[i]);
                Dictionary<string, int> gripperStatusDic = GripperStatusMes(GRIPPER_STATUS);
                Dictionary<string, int> faultStatusDic = FaultStatusMes(FAULT_STATUS);
                Dictionary<string, int> posRequestDic = PosRequestMes(POS_REQUEST);
                Dictionary<string, int> positionDic = PositionMes(POSITION);
                Dictionary<string, int> currentDic = CurrentMes(CURRENT);
                StatusTB.Text = null;
                #region gripperStatus状态解析
                switch (gripperStatusDic["gOBJ"])
                {
                    case 0:
                        StatusTB.Text += "gOBJ=0x00\r\n";
                        break;
                    case 1:
                        StatusTB.Text += "gOBJ=0x01\r\n";
                        break;
                    case 2:
                        StatusTB.Text += "gOBJ=0x02\r\n";
                        break;
                    case 3:
                        StatusTB.Text += "gOBJ=0x03\r\n";
                        break;
                    default:
                        break;
                }
                switch (gripperStatusDic["gSTA"])
                {
                    case 0:
                        StatusTB.Text += "gSTA=0x00\r\n";
                        break;
                    case 1:
                        StatusTB.Text += "gSTA=0x01\r\n";
                        break;
                    case 2:
                        StatusTB.Text += "gSTA=0x02\r\n";
                        break;
                    case 3:
                        StatusTB.Text += "gSTA=0x03\r\n";
                        break;
                    default:
                        break;
                }

                switch (gripperStatusDic["gGTO"])
                {
                    case 0:
                        StatusTB.Text += "gGTO=0x00\r\n";
                        break;
                    case 1:
                        StatusTB.Text += "gGTO=0x01\r\n";
                        break;
                    default:
                        break;
                }
                switch (gripperStatusDic["gACT"])
                {
                    case 0:
                        StatusTB.Text += "gACT=0x00\r\n";
                        break;
                    case 1:
                        StatusTB.Text += "gACT=0x01\r\n";
                        break;
                    default:
                        break;
                }
                #endregion

                #region faultStatus状态解析
                switch (faultStatusDic["gFLT"])
                {
                    case 0:
                        StatusTB.Text += "gFLT=0\r\n";
                        break;
                    case 5:
                        StatusTB.Text += "gFLT=5\r\n";
                        break;
                    case 7:
                        StatusTB.Text += "gFLT=7\r\n";
                        break;
                    case 8:
                        StatusTB.Text += "gFLT=8\r\n";
                        break;
                    case 10:
                        StatusTB.Text += "gFLT=A\r\n";
                        break;
                    case 11:
                        StatusTB.Text += "gFLT=B\r\n";
                        break;
                    case 12:
                        StatusTB.Text += "gFLT=C\r\n";
                        break;
                    case 13:
                        StatusTB.Text += "gFLT=D\r\n";
                        break;
                    case 14:
                        StatusTB.Text += "gFLT=E\r\n";
                        break;
                    case 15:
                        StatusTB.Text += "gFLT=F\r\n";
                        break;
                    default:
                        break;
                }
                #endregion

                #region posRequest状态解析
                StatusTB.Text += "gPR=" + posRequestDic["gPR"].ToString() + "\r\n";
                #endregion

                #region position状态解析
                StatusTB.Text += "gPO=" + positionDic["gPO"].ToString() + "\r\n";
                #endregion

                #region current状态解析
                StatusTB.Text += "gCU=" + currentDic["gCU"].ToString() + "\r\n";
                #endregion
            }
        }
        /// <summary>
        /// 获取指定位数值
        /// </summary>
        /// <param name="intOrig"></param>待取数
        /// <param name="bitNumber"></param>取第几位
        public static int GetIntBit(ushort intOrig, int bitNumber)
        {
            return intOrig >> bitNumber & 1;
        }
        /// <summary>
        /// 获取gripperStatus信息
        /// </summary>
        /// <param name="gripperStatus"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GripperStatusMes(ushort gripperStatus)
        {

            int gOBJ = GetIntBit(gripperStatus, 7) * 2 + GetIntBit(gripperStatus, 6);
            int gSTA = GetIntBit(gripperStatus, 5) * 2 + GetIntBit(gripperStatus, 4);
            int gGTO = GetIntBit(gripperStatus, 3);
            int gACT = GetIntBit(gripperStatus, 0);

            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("gOBJ", gOBJ);
            dic.Add("gSTA", gSTA);
            dic.Add("gGTO", gGTO);
            dic.Add("gACT", gACT);
            return dic;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="faultStatus"></param>
        /// <returns></returns>
        public Dictionary<string, int> FaultStatusMes(ushort faultStatus)
        {

            int kFLT = faultStatus >> 4;
            int gFLT = faultStatus % 16;

            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("kFLT", kFLT);
            dic.Add("gFLT", gFLT);
            return dic;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="posRequest"></param>
        /// <returns></returns>
        public Dictionary<string, int> PosRequestMes(ushort posRequest)
        {

            int gPR = posRequest;

            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("gPR", gPR);
            return dic;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Dictionary<string, int> PositionMes(ushort position)
        {

            int gPO = position;

            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("gPO", gPO);
            return dic;
        }
        public Dictionary<string, int> CurrentMes(ushort current)
        {

            int gCU = current;

            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("gCU", gCU);
            return dic;
        }
        /// <summary>
        /// 写机械爪数据
        /// </summary>
        public static void ModbusSerialRtuMasterWriteRegisters()
        {
            using (SerialPort port = new SerialPort("COM3"))
            {
                // configure serial port
                port.BaudRate = 115200;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                // create modbus master
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                byte slaveId = 9;
                ushort startAddress = 1000;
                ushort[] registers = new ushort[] { 2304, 125, 65535 };

                // write three registers
                master.WriteMultipleRegisters(slaveId, startAddress, registers);
            }
        }
        /// <summary>
        /// 控制手爪
        /// </summary>
        /// <param name="positionReq"></param>位置
        /// <param name="speedReq"></param>速度
        /// <param name="forceReq"></param>力
        public static void ModbusSerialRtuMasterWriteRegisters(int positionReq, int speedReq, int forceReq)
        {
            if (positionReq > 100 || positionReq < 0 || speedReq > 100 || speedReq < 0 || forceReq > 100 || forceReq < 0)
            {
                MessageBox.Show("输入参数有误，请重新输入！");
            }
            else
            {
                using (SerialPort port = new SerialPort("COM3"))
                {
                    // configure serial port
                    port.BaudRate = 115200;
                    port.DataBits = 8;
                    port.Parity = Parity.None;
                    port.StopBits = StopBits.One;
                    port.Open();

                    // create modbus master
                    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                    byte slaveId = 9;
                    ushort startAddress = 1000;
                    int realSpeedReq = Convert.ToUInt16(speedReq * 255 / 100);
                    int realForceReq = Convert.ToUInt16(forceReq * 255 / 100);
                    ushort realSpeedForceReq = Convert.ToUInt16(realSpeedReq * 256 + realForceReq);
                    ushort realPositionReq = Convert.ToUInt16(positionReq * 255 / 100);

                    ushort[] registers = new ushort[] { 2304, realPositionReq, realSpeedForceReq };

                    // write three registers
                    master.WriteMultipleRegisters(slaveId, startAddress, registers);
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int posReq=0, speReq=0, forReq=0;
            try
            {
                if ((PosReqTB.Text!=null) && (SpeReqTB.Text != null)&& (ForReqTB.Text != null))
                {
                    posReq = Convert.ToInt32(PosReqTB.Text);
                    speReq = Convert.ToInt32(SpeReqTB.Text);
                    forReq = Convert.ToInt32(ForReqTB.Text);
                    ModbusSerialRtuMasterWriteRegisters(posReq, speReq, forReq);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
