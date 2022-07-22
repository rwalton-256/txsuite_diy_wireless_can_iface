using System.IO;
using RGiesecke.DllExport;

namespace mct_can
{
    public class DIY_can_iface
    {
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
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_Open\n  bitrate: " + bitrate + "\n");
            return true;
        }
        [DllExport("MctAdapter_IsOpen")]
        public static bool MctAdapter_IsOpen()
        {
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_IsOpen\n");
            return true;
        }
        [DllExport("MctAdapter_SendMessage")]
        public static bool MctAdapter_SendMessage(uint id, byte length, ulong data)
        {
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_SendMessage\n  id:" + id.ToString("X") + "\n  length:" + length + "\n  data:" + data.ToString("X") + "\n" );
            return true;
        }
        [DllExport("MctAdapter_ReceiveMessage")]
        public static bool MctAdapter_ReceiveMessage(out uint id, out byte length, out ulong data)
        {
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_ReceiveMessage\n");
            id = 0;
            length = 0;
            data = 0;
            return false;
        }
        [DllExport("MctAdapter_Close")]
        public static bool MctAdapter_Close()
        {
            File.AppendAllText("C:\\Users\\waltor\\Desktop\\foo.txt", "MctAdapter_Close\n");
            return true;
        }
    }
}
