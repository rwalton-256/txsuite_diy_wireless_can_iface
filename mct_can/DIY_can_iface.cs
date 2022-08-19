using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using RGiesecke.DllExport;

namespace mct_can
{
    public class DIY_can_iface
    {
        private static TcpClient _mClient;
        private static NetworkStream _mStream;
        [DllExport("MctAdapter_Create")]
        public static void MctAdapter_Create()
        {
        }
        [DllExport("MctAdapter_Release")]
        public static void MctAdapter_Release()
        {
        }
        [DllExport("MctAdapter_Open")]
        public static bool MctAdapter_Open(string bitrate)
        {
            // Connect to a remote device.  
            try
            {
                _mClient = new TcpClient("192.168.100.1", 5555);
                _mClient.NoDelay = true;
                _mStream = _mClient.GetStream();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        [DllExport("MctAdapter_IsOpen")]
        public static bool MctAdapter_IsOpen()
        {
            return _mClient.Connected;
        }
        [DllExport("MctAdapter_SendMessage")]
        public static bool MctAdapter_SendMessage(uint id, byte length, ulong data)
        {
            try
            {
                byte[] buf = new byte[16];
                Buffer.BlockCopy(BitConverter.GetBytes(id), 0, buf, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(length), 0, buf, 4, 1);
                Buffer.BlockCopy(BitConverter.GetBytes(data), 0, buf, 8, 8);
                _mStream.Write(buf, 0, 16);
            }
            catch (SocketException se)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        [DllExport("MctAdapter_ReceiveMessage")]
        public static bool MctAdapter_ReceiveMessage(out uint id, out byte length, out ulong data)
        {
            byte[] buffer = new byte[16];
            _mStream.Read(buffer, 0, 16);
            id = BitConverter.ToUInt32(buffer,0);
            length = buffer[4];
            data = BitConverter.ToUInt64(buffer,8);
            return true;
        }
        [DllExport("MctAdapter_Close")]
        public static bool MctAdapter_Close()
        {
            // Release the socket.  
            _mClient = new TcpClient();
            _mStream = _mClient.GetStream();
            return true;
        }
    }
}
