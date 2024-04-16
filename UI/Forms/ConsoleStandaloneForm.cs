using G_MBIVautoTester._Globalz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G_MBIVautoTester.UI.Forms
{
    public partial class ConsoleStandaloneForm : Form
    {
        StringBuilder _sb;
        public ConsoleStandaloneForm()
        {
            InitializeComponent();
            if (_sb == null)
                _sb = new StringBuilder();
            EventsManagerLib.OnLogConsoleEvent += AddTextToConsole_withDirectAction;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            EventsManagerLib.OnLogConsoleEvent -= AddTextToConsole_withDirectAction;
        }

        private void ClearConsole()
        {
            _sb.Clear();
            textBox_Display.Text = _sb.ToString();
        }
        int cnt = 0;

        void AddTextToConsole_withExplicitDelegate(string argText)
        {
            cnt++;
            if(cnt >48)
            {
                return;
            }
            if (textBox_Display.InvokeRequired)
            {
                textBox_Display.Invoke(
                        new MethodInvoker(
                                    delegate    {
                                                _sb.AppendLine(argText);
                                                textBox_Display.Text = _sb.ToString();
                                                }
                                            )
                                     );
            }
            else
            {
                _sb.AppendLine(argText);
                textBox_Display.Text = _sb.ToString();
            }
        }
        /// <summary>
        /// If AddTextToConsole is called from a non-UI thread, it uses Invoke to call itself (AddTextToConsole) on the UI thread. 
        /// This keeps the method thread-safe and avoids cross-thread operations on the textBox_Display.
        /// </summary>
        /// <param name="text"></param>
        private void AddTextToConsole_withDirectAction(string text)
        {
            // Check if an invoke is required (if the call comes from a different thread)
            if (textBox_Display.InvokeRequired)
            {
                // Invoke AddTextToConsole itself to handle cross-thread operation.
                textBox_Display.Invoke(new Action<string>(AddTextToConsole_withDirectAction), text);
            }
            else
            {
                _sb.AppendLine(text);
                textBox_Display.Text = _sb.ToString();
            }
        }
    }
}
