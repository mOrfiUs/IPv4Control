namespace nsIPv4Control

{
    using nsNativeMethods;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using System.Diagnostics;

    #region help and links

    //Enabling Visual Styles
    //http://msdn.microsoft.com/en-us/library/windows/desktop/bb773175(v=vs.85).aspx

    //The history of the Windows XP common controls
    //http://blogs.msdn.com/b/oldnewthing/archive/2008/01/29/7294949.aspx

    //Clases estáticas y sus miembros (Guía de programación de C#)
    //http://msdn.microsoft.com/es-es/library/79b3xss3(v=vs.80).aspx

    //Modificadores de acceso (Guía de programación de C#)
    //http://msdn.microsoft.com/es-es/library/ms173121(v=VS.90).aspx

    //Niveles de accesibilidad (Referencia de C#)
    //http://msdn.microsoft.com/es-es/library/ba0a1yw2(v=vs.90).aspx

    //Provocar un evento
    //http://msdn.microsoft.com/es-es/library/vstudio/wkzf914z(v=vs.100).aspx

    //[EDIT] PtrToStructure bug in xp x64
    //http://social.msdn.microsoft.com/Forums/windows/en-US/a1d2a94d-251f-4515-8fd9-51ecb967b193/edit-ptrtostructure-bug-in-xp-x64

    //C# Event Implementation Fundamentals, Best Practices and Conventions
    //http://www.codeproject.com/Articles/20550/C-Event-Implementation-Fundamentals-Best-Practices

    //Custom Generic EventArgs
    //http://www.c-sharpcorner.com/UploadFile/rmcochran/customgenericeventargs05132007100456AM/customgenericeventargs.aspx
    
    //sequence Control.Leave
    //http://msdn.microsoft.com/es-es/library/system.windows.forms.control.leave.aspx

    #endregion help and links

    /// <summary>
    /// Class derivated from TextBox
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public class IPv4Control : TextBox
    {

        #region constructor
        private static IntPtr _Handle = IntPtr.Zero;
        private bool IsFirstTime = true;
        /// <summary>
        /// Raises the HandleCreated event, show the ip value in the control, and enumerate all edit controls.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            NativeMethods.EnumChildWindows(Handle, new NativeMethods.EnumWindowsProc(EnumChildProc), (int)Handle);
            _Handle = Handle;
        }
        private static int ChildCount = 0;
        private int EnumChildProc(IntPtr lhWnd, int lParam)
        {
            if (ChildCount == 0) ChildCount = 4;
            Message m = Message.Create((IntPtr)lParam, ((int)NativeMethods.WM.WM_ENUMCHILPROC), (IntPtr)ChildCount--, lhWnd);
            WndProc(ref m);
            if (ChildCount > 0) return 1;
            return 0;
        }
        private static bool InitCC = false;
        /// <summary>
        /// Creates a handle for the control.
        /// Init CommonControls
        /// </summary>
        protected override void CreateHandle()
        {
            if (!this.RecreatingHandle && (!InitCC))
            {
                NativeMethods.INITCOMMONCONTROLSEX ic = new NativeMethods.INITCOMMONCONTROLSEX();
                ic.dwSize = Marshal.SizeOf(typeof(NativeMethods.INITCOMMONCONTROLSEX));
                ic.dwICC = (int)NativeMethods.IPAMess.ICC_INTERNET_CLASSES;
                InitCC = NativeMethods.InitCommonControlsEx(ic);
            }
            base.CreateHandle();// -> invoke CreateParams
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase CreateParams.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
                CreateParams cp = base.CreateParams;
                if (InitCC)
                {
                    const string WC_IPADDRESS = "SysIPAddress32";
                    cp.ClassName = WC_IPADDRESS;
                    cp.Style = (int)(NativeMethods.WindowStyleFlags.WS_CHILD | NativeMethods.WindowStyleFlags.WS_VISIBLE);//(NativeMethods.WindowStyleFlags.WS_TABSTOP)                    
                    cp.ExStyle = (int)(NativeMethods.ExtendedWindowStyleFlags.WS_EX_NOPARENTNOTIFY | NativeMethods.ExtendedWindowStyleFlags.WS_EX_CLIENTEDGE);
                    cp.ClassStyle = (int)(NativeMethods.ClassStyleFlags.CS_VREDRAW | NativeMethods.ClassStyleFlags.CS_HREDRAW | NativeMethods.ClassStyleFlags.CS_DBLCLKS | NativeMethods.ClassStyleFlags.CS_GLOBALCLASS);
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        cp.ExStyle |= (int)NativeMethods.ExtendedWindowStyleFlags.WS_EX_LAYOUTRTL;
                        cp.ExStyle &= ~(int)(NativeMethods.ExtendedWindowStyleFlags.WS_EX_RIGHT | NativeMethods.ExtendedWindowStyleFlags.WS_EX_RTLREADING | NativeMethods.ExtendedWindowStyleFlags.WS_EX_LEFTSCROLLBAR);
                    }
                }
                return cp;
            }
        }
        #endregion constructor

        #region Object field
        public class IPv4Field
        {
            private static IntPtr _HandleParent;
            private static int nField;
            private static HFV _hfv;
            #region constructor
            internal IPv4Field(int Field, IntPtr HandleParent, HFV hfv)
            {
                nField = Field;
                _HandleParent = HandleParent;
                _hfv = hfv;
            }
            #endregion constructor
            /// <summary>
            /// Get or sets the value in the specified field in the IP Address Control.
            /// </summary>
            /// <returns>Value for the specified field if success, -1 otherwise</returns>
            public int Value
            {
                get
                {
                    return (_hfv.iValue[nField]);
                }
                set
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(value.ToString());
                    if (NativeMethods.SendMessage(new HandleRef(Control.FromHandle(_HandleParent), _hfv.hWnd[nField]), NativeMethods.WM.WM_SETTEXT, new IntPtr(0), sb))
                    {
                        (_hfv.iValue[nField]) = value;
                    }
                }
            }
            /// <summary>
            /// Sets the range in the specified field in the IP Address Control.
            /// </summary>
            /// <param name="nLower">A reference to an short receiving the lower limit of the specified field in this IP Address Control.</param>
            /// <param name="nUpper">A reference to an short receiving the upper limit of the specified field in this IP Address Control.</param>
            public void GetRange(ref byte nLower, ref byte nUpper)
            {
                nLower = _hfv.nLower[nField];
                nUpper = _hfv.nUpper[nField];
            }
            /// <summary>
            /// Sets the valid range for the specified field in the IP address control.
            /// <param name="nLower">the lower limit of the range</param>
            /// <param name="nUpper">the upper limit of the range</param>
            /// <returns>Returns nonzero if successful, or zero otherwise.</returns>
            public bool SetRange(byte nLower, byte nUpper)
            {
                IntPtr ipRange = MakeIPRange(nLower, nUpper);
                _hfv.nLower[nField] = nLower;
                _hfv.nUpper[nField] = nUpper;
                return NativeMethods.SendMessage(new HandleRef(Control.FromHandle(_HandleParent), _HandleParent), (int)NativeMethods.IPAMess.IPM_SETRANGE, (IntPtr)nField, (IntPtr)ipRange).ToInt32() > 0;
            }
            /// <summary>
            /// Enables or disables mouse and keyboard input to the specified Edit Control.
            /// When input is disabled, the window does not receive input such as mouse clicks and key presses.
            /// When input is enabled, the window receives all input.
            /// </summary>
            /// <param name="bEnable">Indicates whether to enable or disable the window.
            /// If this parameter is TRUE, the window is enabled. If the parameter is FALSE, the window is disabled.</param>
            /// <returns>If the window was previously disabled, the return value is nonzero.
            /// If the window was not previously disabled, the return value is zero.
            /// </returns>
            public bool Enable(bool bEnable)
            {
                return NativeMethods.EnableWindow(_hfv.hWnd[nField], bEnable);
            }
            /// <summary>
            /// The Window handle of this window.
            /// </summary>
            public IntPtr Handle
            {
                get
                {
                    return (_hfv.hWnd[nField]);
                }
            }
            /// <summary>
            /// Check if nField is enable.
            /// </summary>
            /// <returns>True if enabled, false otherwise.</returns>
            public bool IsEnabled
            {
                get
                {
                    return NativeMethods.IsWindowEnabled(_hfv.hWnd[nField]);
                }
            }
       }
        public IPv4Field Field(int nField)
        {
            if (_Handle == IntPtr.Zero)
            {
                return null;
            }
            else
            {
                return new IPv4Field(nField, _Handle, _hfv);
            }
        }
        #endregion Object field

        #region class and struc
        /// <summary>
        /// class to send information about each field
        /// </summary>
        public class IPv4EventArgs : EventArgs
        {
            public int nField { get; set; }
            public string Value { get; set; }
            public string OldValue { get; set; }
            public IPv4EventArgs(int nField, string Value = null, string OldValue = null)
            {
                this.Value = Value;
                this.OldValue = OldValue;
                this.nField = nField;
            }
        }
        /// <summary>
        /// variable to store information about each field
        /// </summary>
        private HFV _hfv = new HFV();
        /// <summary>
        /// class to store information about each field
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class HFV
        {
            /// <summary>
            /// Handle of edit control
            /// </summary>
            internal IntPtr[] hWnd = new IntPtr[4];
            /// <summary>
            /// Field value.
            /// </summary>
            internal int[] iValue = new int[4] { 0, 0, 0, 0 };
            /// <summary>
            /// true if has changed.
            /// </summary>
            internal bool[] HasChanged = new bool[4];
            /// <summary>
            /// the lower limit of the range
            /// </summary>
            internal byte[] nLower = new byte[4] { 0, 0, 0, 0 };
            /// <summary>
            /// the upper limit of the range
            /// </summary>
            internal byte[] nUpper = new byte[4] { 255, 255, 255, 255 };
        }
        #endregion class and struc

        #region override functions
        new public bool Focus()
        {
            return Focus(0);
        }
        /// <summary>
        /// http://social.msdn.microsoft.com/Forums/silverlight/en-US/0ae40192-e866-480a-9f90-2a8d2a75b045/getting-proplem-with-focus-of-customcontrol
        /// </summary>
        /// <returns></returns>
        public bool Focus(int nField = 0)
        {
            Message m;
            IPv4Field f = this.Field(nField);
            m = Message.Create(Handle, (int)NativeMethods.WM.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            WndProc(ref m);
            if ((nField == 0) || (f.IsEnabled == false))
            {
                NativeMethods.SetFocus(Handle);
            }
            else
            {
                iWM_MOUSEACTIVATE = 2;

                NativeMethods.SetFocus(Handle);

                iWM_MOUSEACTIVATE = 1;

                m = Message.Create(Handle, (int)NativeMethods.IPAMess.IPM_SETFOCUS, (IntPtr)nField, IntPtr.Zero);
                WndProc(ref m);

                iWM_MOUSEACTIVATE = 0;
            }
            m = Message.Create(Handle, (int)NativeMethods.WM.WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            WndProc(ref m);
            return NativeMethods.RedrawWindow(new HandleRef(this, Handle), IntPtr.Zero, IntPtr.Zero, NativeMethods.RedrawWindowFlags.RDW_ALLCHILDREN | NativeMethods.RedrawWindowFlags.RDW_INVALIDATE | NativeMethods.RedrawWindowFlags.RDW_ERASENOW);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            if (e != EventArgs.Empty)
            {
                base.OnLostFocus(e);
            }
        }
        protected override void OnGotFocus(EventArgs e)
        {
            if (e != EventArgs.Empty)
            {
                base.OnGotFocus(e);
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            if (e != EventArgs.Empty)
            {
                base.OnTextChanged(e);
            }
        }
        private static bool bOnLeave = false;
        protected override void OnLeave(EventArgs e)
        {
            if (e != EventArgs.Empty)
            {
                base.OnLeave(e);
                if (iWM_MOUSEACTIVATE == 2)
                {
                    base.OnLeave(new IPv4EventArgs(-1, base.Text));
                }
                if (bOnLeave)
                {
                    bOnLeave = false;
                    base.OnLeave(new IPv4EventArgs(-1, base.Text));
                }
            }
            else
            {
                if (iWM_MOUSEACTIVATE != 2)
                {
                    bOnLeave = true;
                }
            }
        }
        /// <summary>
        /// int to prevent stupid flash when the control take the focus
        /// posibles values 0, 1, 2 (sequence order)
        /// </summary>
        private static int iWM_MOUSEACTIVATE = 0;
        /// <summary>
        /// prevent stupid duplicate WM_NOTIFY_REFLECT when override ProcessDialogKey
        /// </summary>
        private static int lastMessage = 0;
        /// <summary>
        /// Process windows messages
        /// </summary>
        /// <param name="m">Object message</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            //prevent stupid duplicate WM_NOTIFY_REFLECT
            if (lastMessage == (int)(NativeMethods.WM.WM_NOTIFY_REFLECT))
            {
                Debug.WriteLine("lastMessageWM_NOTIFY_REFLECT");
                lastMessage = m.Msg;
                base.WndProc(ref m);
                return;
            }
            lastMessage = m.Msg;
            switch (m.Msg)
            {
                case (int)NativeMethods.WM.WM_GETDLGCODE:
                    // The .NET forms aren't dialogs, so the operating system dont manage navigation keys correctly
                    m.Result = new IntPtr(((int)NativeMethods.DLGC.DLGC_WANTCHARS | (int)NativeMethods.DLGC.DLGC_WANTARROWS | m.Result.ToInt32()));
                    Debug.WriteLine("WM_GETDLGCODE");
                    return;
                case (int)(NativeMethods.WM.WM_COMMAND):
                    Debug.WriteLine("WM_COMMAND");
                    if (ProcessControlCommand(ref m))
                    {
                        return;
                    }
                    break;
                case (int)(NativeMethods.WM.WM_COMMAND_REFLECT):
                    Debug.WriteLine("WM_COMMAND_REFLECT");
                    if (ProcessControlCommandReflect(ref m))
                    {
                        return;
                    }
                    break;
                case (int)(NativeMethods.WM.WM_NOTIFY_REFLECT):
                    Debug.WriteLine("WM_NOTIFY_REFLECT");
                    Debug.WriteLine("iWM_MOUSEACTIVATE = " + iWM_MOUSEACTIVATE);
                    if (iWM_MOUSEACTIVATE != 1)
                    {
                        Debug.WriteLine("WM_NOTIFY_REFLECT");
                        ProcessControlNotification(ref m);
                        if ((int)m.Result > 0)
                        {
                            return;
                        }
                    }
                    break;
                case (int)NativeMethods.WM.WM_CTLCOLOREDIT:
                    if (iWM_MOUSEACTIVATE != 0) break;
                    //Debug.WriteLine("WM_CTLCOLOREDIT");
                    if ((ProcessControlCtlColorEdit(ref m)))
                    {
                        return;
                    }
                    break;
                case (int)NativeMethods.WM.WM_MOUSEACTIVATE:
                    //protected override void OnMouseClick, OnMouseDown dont work                   
                    if (ProcessControlMouseActivate(ref m))
                    {
                        return;
                    }
                    break;
                case (int)NativeMethods.WM.WM_ENUMCHILPROC:
                    Debug.WriteLine("WM_ENUMCHILPROC");
                    if ((int)_hfv.hWnd[0] == 0)
                    {
                        _hfv.hWnd[(int)m.WParam - 1] = m.LParam;
                    }
                    break;
                //default:
                //    Debug.WriteLine("WM_DEFAULT");
                //    break;
            }
            base.WndProc(ref m);
        }
        #endregion override functions
  
        #region properties control
        /// <summary>Color for modified values (text color).</summary>
        public Color FieldChangedColor { get; set; }
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                IPAddress ip;
                if (!IPAddress.TryParse(value, out ip))
                {
                    return;
                }
                byte[] b = new byte[4];
                b = (byte[])ip.GetAddressBytes();
                for (int i = 0; i < 4; i++)
                {
                    _hfv.iValue[i] = b[i];
                }
                int iIP = IPAddress.HostToNetworkOrder((int)ip.Address);
                Message m = Message.Create(Handle, (int)NativeMethods.IPAMess.IPM_SETADDRESS, IntPtr.Zero, (IntPtr)iIP);
                WndProc(ref m);
                IsFirstTime = false;
            }
        }
        /// <summary>
        /// Enables or disables mouse and keyboard input to the specified field.
        /// </summary>
        /// <param name="nField">(Zero based value) field to to be enabled or disabled.</param>
        /// <param name="bEnable">Indicates whether to enable or disable the window.
        /// If this parameter is TRUE, the window is enabled. If the parameter is FALSE, the window is disabled.</param>
        /// <returns>If the window was previously disabled, the return value is nonzero.
        /// If the window was not previously disabled, the return value is zero.
        /// </returns>
        /// <summary>
        /// Get or set ipAddress shown in the control
        /// </summary>
        public IPAddress ipAddress
        {
            get
            {
                IPAddress ipText;
                if (IPAddress.TryParse(base.Text, out ipText))
                {
                    return ipText;
                }
                return null;
            }
        }
        #endregion properties control

        #region internal and private functions
        /// <summary>
        /// usefull macro
        /// </summary>
        private static IntPtr MakeIPRange(byte low, byte high)
        {
            return (IntPtr)((high << 8) + low);
        }
        /// <summary>
        /// usefull macro
        /// </summary>
        private void HiLoWord(IntPtr WParam, ref int LOWORD, ref int HIWORD)
        {
            LOWORD = unchecked((short)(long)WParam);
            HIWORD = unchecked((short)((long)WParam >> 16));
        }
        /// <summary>
        /// Gets the text of the specified control.
        /// </summary>
        /// <param name="hWnd">A handle to the control containing the text.</param>
        /// <returns>the text of the control, or null</returns>
        private string GWTGetControlText(IntPtr hWnd)
        {
            // set the size of the string required to hold the window text. 
            int sbLength = 256;

            // If the return is 0, there is no title. 
            System.Text.StringBuilder sb = new System.Text.StringBuilder(sbLength);
            sbLength = NativeMethods.GetWindowText(new HandleRef(this, hWnd), sb, sb.Capacity);
            if (sbLength < 0) return null;
            return sb.ToString().Substring(0, sbLength);
        }
        /// <summary>
        /// Convert string into a pseudo nullable uint
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>converted uinteger or -1 if string is null or negative</returns>
        private int ToPseudoNullableInt32(string s)
        {
            uint i;
            if (UInt32.TryParse(s, out i)) return (int)i;
            return -1;
        }
        /// <summary>
        /// Handles all notification messages from the contol, callbacks handled here
        /// </summary>
        /// <param name="m">Message</param>
        private void ProcessControlNotification(ref Message m)
        {
            NativeMethods.NMIPADDRESS nmIP = (NativeMethods.NMIPADDRESS)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.NMIPADDRESS));
            if (nmIP.hdr.code == (int)NativeMethods.IPAMess.IPN_FIELDCHANGED)
            {
                Debug.WriteLine("IPN_FIELDCHANGED");
                Debug.WriteLine("nmIP.iField = " + nmIP.iField.ToString() + "\tnmIP.iValue = " + nmIP.iValue.ToString());
                OnLeave(new IPv4EventArgs(nmIP.iField, nmIP.iValue.ToString(), _hfv.iValue[nmIP.iField].ToString()));
                if (nmIP.iValue == -1)
                {
                    nmIP.iValue = 0;
                }
                Marshal.StructureToPtr(nmIP, m.LParam, false);
                _hfv.iValue[nmIP.iField] = nmIP.iValue;
                m.Result = (IntPtr)1;
            }
        }
        /// <summary>
        /// Handles MouseActivate message from the contol
        /// </summary>
        /// <param name="m">message to process</param>
        /// <returns>true if process message, false otherwise</returns>
        private bool ProcessControlMouseActivate(ref Message m) 
        {
            Debug.WriteLine("WM_MOUSEACTIVATE");
            int currentFocus = NativeMethods.GetFocus();
            for (int i = 0; i < 4; i++)
            {
                if (currentFocus == (int)_hfv.hWnd[i])
                {
                    m.Result = (IntPtr)NativeMethods.MouseActivate.MA_NOACTIVATE;
                    return true;
                }
            }

            NativeMethods.RECT lpRect = new NativeMethods.RECT();
            NativeMethods.GetWindowRect(new HandleRef(this, Handle), out lpRect);
            Size sz = new Size((lpRect.Right - lpRect.Left) / 4, lpRect.Bottom - lpRect.Top);
            Rectangle[] SplitRects = new Rectangle[4];
            Point p = Cursor.Position;
            int nFieldIPM_SETFOCUS = 0;

            for (int i = 0; i < 4; i++)
            {
                SplitRects[i] = new Rectangle(new Point(lpRect.Left + (sz.Width * i), lpRect.Top), sz);
                if (SplitRects[i].Contains(p))
                {
                    IPv4Field f = this.Field(i);
                    if (!f.IsEnabled)
                    {
                        m.Result = (IntPtr)NativeMethods.MouseActivate.MA_NOACTIVATEANDEAT;
                        return true;
                    }
                    nFieldIPM_SETFOCUS = i;
                }
            }
            Debug.WriteLine("BeforeIPM_SETFOCUS");
            Focus(nFieldIPM_SETFOCUS);
            Debug.WriteLine("AfterIPM_SETFOCUS");
            return false;
        }
        /// <summary>
        /// Handles CtlColorEdit message from the contol
        /// </summary>
        /// <param name="m">message to process</param>
        /// <returns>true if process message, false otherwise</returns>
        private bool ProcessControlCtlColorEdit(ref Message m)
        {
            for (int i = 0; i < 4; i++)
            {
                if ((_hfv.hWnd[i] == m.LParam))
                {
                    if ((_hfv.HasChanged[i]) && (!FieldChangedColor.IsEmpty))
                    {
                        //if FieldChangedColor and HasChanged
                        NativeMethods.SetTextColor(m.WParam, ColorTranslator.ToWin32(FieldChangedColor));// Text color
                        m.Result = (IntPtr)NativeMethods.GetStockObject(NativeMethods.StockObjects.DC_BRUSH);
                        return true;
                    }
                    if (!ForeColor.IsEmpty)
                    {
                        //if ForeColor
                        NativeMethods.SetTextColor(m.WParam, ColorTranslator.ToWin32(ForeColor));// Text color
                        m.Result = (IntPtr)NativeMethods.GetStockObject(NativeMethods.StockObjects.DC_BRUSH);
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Handles all command messages from the Parent to IP control (Reflect messages), callbacks handled here
        /// </summary>
        /// <param name="m">message to process</param>
        /// <returns>true if process message, false otherwise, but alwways return 0</returns>
        private bool ProcessControlCommandReflect(ref Message m)
        {
            int nFieldLoWord = 0;
            int uEN = 0;
            HiLoWord(m.WParam, ref nFieldLoWord, ref uEN);
            int nField = -1;
            IPAddress ip;
            if (!IPAddress.TryParse(GWTGetControlText(m.LParam), out ip))
            {
                return false;
            }
            switch (uEN)
            {
                case (int)NativeMethods.IPAMess.EN_CHANGE:
                    Debug.WriteLine("EN_CHANGE");
                    Debug.WriteLine("nField = " + nField.ToString() + "\tFieldValue = " + ip.ToString());
                    //if you need more control on change, uncomment line above
                    //OnTextChanged(new IPv4EventArgs(nField, ip.ToString(), ipAddress.ToString()));
                    break;
                case (int)NativeMethods.IPAMess.EN_KILLFOCUS:
                    Debug.WriteLine("EN_KILLFOCUS");
                    Debug.WriteLine("nField = " + nField.ToString() + "\tFieldValue = " + ip.ToString());
                    OnLostFocus(new IPv4EventArgs(nField));
                    break;
            }
            return false;
        }
        private float fFontSize = 0;
        /// <summary>
        /// Handles all command messages from the IP control to the Edit Controls, callbacks handled here
        /// </summary>
        /// <param name="m">message to process</param>
        /// <returns>true if process message, false otherwise, but alwways return 0</returns>
        private bool ProcessControlCommand(ref Message m)
        {
            int nFieldLoWord = 0;
            int uEN = 0;
            HiLoWord(m.WParam, ref nFieldLoWord, ref uEN);
            //get the identifier (zero-based) stored in GWL_USERDATA
            int nField = (int)NativeMethods.GetWindowLongPtr(new HandleRef(this, m.LParam), (int)NativeMethods.WM.GWL_USERDATA);
            int FieldValue = ToPseudoNullableInt32(GWTGetControlText(m.LParam));
            switch (uEN)
            {
                case (int)NativeMethods.IPAMess.EN_MAXTEXT:
                    Debug.WriteLine("EN_MAXTEXT");
                    Debug.WriteLine("nField = " + nField.ToString() + "\tFieldValue = " + FieldValue.ToString());
                    if (fFontSize == 0)
                    {
                        fFontSize = base.Font.Size;
                    }
                    if (base.Font.Size > fFontSize - 2)
                    {
                        //base.OnFontChanged;
                        base.Font = new Font(base.Font.FontFamily, base.Font.Size - 1);
                    }
                    break;
                case (int)NativeMethods.IPAMess.EN_UPDATE:
                    Debug.WriteLine("EN_UPDATE");
                    Debug.WriteLine("nField = " + nField.ToString() + "\tFieldValue = " + FieldValue.ToString());
                    break;
                case (int)NativeMethods.IPAMess.EN_CHANGE:
                    Debug.WriteLine("EN_CHANGE");
                    Debug.WriteLine("nField = " + nField.ToString() + "\tFieldValue = " + FieldValue.ToString());
                    if (!IsFirstTime)
                    {
                        _hfv.HasChanged[nField] = true;
                    }
                    OnTextChanged(new IPv4EventArgs(nField, FieldValue.ToString(), _hfv.iValue[nField].ToString()));
                    break;
                case (int)NativeMethods.IPAMess.EN_SETFOCUS:
                    if (iWM_MOUSEACTIVATE > 1)
                    {
                        Debug.WriteLine("EN_SETFOCUS 0");
                        break;
                    }
                    Debug.WriteLine("EN_SETFOCUS 1");
                    Debug.WriteLine("nField = " + nField.ToString() + "\tFieldValue = " + FieldValue.ToString());
                    OnGotFocus(new IPv4EventArgs(nField, FieldValue.ToString()));
                    break;
            }
            return false;
        }
        #endregion internal and private functions
    }
}