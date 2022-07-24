using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using RGiesecke.DllExport;

namespace mct_can
{
    public class DIY_can_iface
    {
        private static Socket _mSocket;
        [DllExport("MctAdapter_Create")]
        public static void MctAdapter_Create()
        {
            File.WriteAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_Create\n");
        }
        [DllExport("MctAdapter_Release")]
        public static void MctAdapter_Release()
        {
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_Release\n");
        }
        [DllExport("MctAdapter_Open")]
        public static bool MctAdapter_Open(string bitrate)
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                IPAddress[] ipAddress = Dns.GetHostAddresses("192.168.100.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress[0], 502);

                // Create a TCP/IP  socket
                _mSocket = new Socket(ipAddress[0].AddressFamily, SocketType.Stream, ProtocolType.Tcp );

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    _mSocket.Connect(remoteEP);

                    File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", _mSocket.RemoteEndPoint.ToString() + "\n" );
                }
                catch (SocketException se)
                {
                    File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", se.ToString() + "\n");
                    return false;
                }
                catch (Exception e)
                {
                    File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", e.ToString() + "\n");
                    return false;
                }

            }
            catch (Exception e)
            {
                File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", e.ToString() + "\n");
                return false;
            }
            return true;
        }
        [DllExport("MctAdapter_IsOpen")]
        public static bool MctAdapter_IsOpen()
        {
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_IsOpen\n");
            return _mSocket.Connected;
        }
        [DllExport("MctAdapter_SendMessage")]
        public static bool MctAdapter_SendMessage(uint id, byte length, ulong data)
        {
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_SendMessage\n  id:" + id.ToString("X") + "\n  length:" + length + "\n  data:" + data.ToString("X") + "\n" )            try
            {
                File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_SendMessage:...\n");
                byte[] buf = new byte[16];
                Buffer.BlockCopy(BitConverter.GetBytes(id), 0, buf, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(length), 0, buf, 4, 1);
                Buffer.BlockCopy(BitConverter.GetBytes(data), 0, buf, 8, 8);
                _mSocket.Send(buf);
                File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_SendMessage:Done\n");
            }
            catch (SocketException se)
            {
                File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", se.ToString() + "\n");
                return false;
            }
            catch (Exception e)
            {
                File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", e.ToString() + "\n");
                return false;
            }
            return true;
        }
        [DllExport("MctAdapter_ReceiveMessage")]
        public static bool MctAdapter_ReceiveMessage(out uint id, out byte length, out ulong data)
        {
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_ReceiveMessage\n");
            byte[] buffer = new byte[16];
            _mSocket.Receive(buffer);
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", BitConverter.ToString(buffer) + "\n");
            id = BitConverter.ToUInt32(buffer,0);
            length = buffer[4];
            data = BitConverter.ToUInt64(buffer,8);
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_ReceiveMessage:Done\n");
            return true;
        }
        [DllExport("MctAdapter_Close")]
        public static bool MctAdapter_Close()
        {
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_Close\n");
            // Release the socket.  
            _mSocket.Shutdown(SocketShutdown.Both);
            _mSocket.Close();
            return true;
        }
    }
}
