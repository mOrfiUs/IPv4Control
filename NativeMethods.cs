using System;
using System.Runtime.InteropServices;

namespace nsNativeMethods
{
    /// <summary>
    /// P/Invoke methods and values.
    /// </summary>
	internal sealed class NativeMethods
	{
		/// <summary>Prevent external instantiation.</summary>
		private NativeMethods(){}

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb775514(v=vs.85).aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NMHDR
        {
            internal IntPtr hwndFrom;
            internal IntPtr idFrom;
            internal int code;
        }
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761375(v=vs.85).aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NMIPADDRESS
        {
            internal NMHDR hdr;
            internal int iField;
            internal int iValue;
        }
        [Flags]
        public enum RedrawWindowFlags : int
        {
            //The following flags are used to invalidate the window.
            /// <summary>
            /// Causes any part of the nonclient area of the window that intersects the update region to receive a WM_NCPAINT message. The RDW_INVALIDATE flag must also be specified; otherwise, RDW_FRAME has no effect. The WM_NCPAINT message is typically not sent during the execution of RedrawWindow unless either RDW_UPDATENOW or RDW_ERASENOW is specified.
            /// </summary>
            RDW_FRAME = 0x0400,
            /// <summary>
            /// Invalidates lprcUpdate or hrgnUpdate (only one may be non-NULL). If both are NULL, the entire window is invalidated.
            /// </summary>
            RDW_INVALIDATE = 0x0001,
            /// <summary>
            /// Causes a WM_PAINT message to be posted to the window regardless of whether any portion of the window is invalid.
            /// </summary>
            RDW_INTERNALPAINT = 0x0002,
            /// <summary>
            /// Causes the window to receive a WM_ERASEBKGND message when the window is repainted. The RDW_INVALIDATE flag must also be specified; otherwise, RDW_ERASE has no effect.
            /// </summary>
            RDW_ERASE = 0x0004,
            //The following flags are used to validate the window.
            /// <summary>
            /// Suppresses any pending WM_ERASEBKGND messages.
            /// </summary>
            RDW_NOERASE = 0x0020,
            /// <summary>
            /// Suppresses any pending WM_NCPAINT messages. This flag must be used with RDW_VALIDATE and is typically used with RDW_NOCHILDREN. RDW_NOFRAME should be used with care, as it could cause parts of a window to be painted improperly.
            /// </summary>
            RDW_NOFRAME = 0x0800,
            /// <summary>
            /// Suppresses any pending internal WM_PAINT messages. This flag does not affect WM_PAINT messages resulting from a non-NULL update area.
            /// </summary>
            RDW_NOINTERNALPAINT = 0x0010,
            /// <summary>
            /// Validates lprcUpdate or hrgnUpdate (only one may be non-NULL). If both are NULL, the entire window is validated. This flag does not affect internal WM_PAINT messages.
            /// </summary>
            RDW_VALIDATE = 0x0008,
            //The following flags control when repainting occurs. RedrawWindow will not repaint unless one of these flags is specified.            
            /// <summary>
            /// Causes the affected windows (as specified by the RDW_ALLCHILDREN and RDW_NOCHILDREN flags) to receive WM_NCPAINT and WM_ERASEBKGND messages, if necessary, before the function returns. WM_PAINT messages are received at the ordinary time.
            /// </summary>
            RDW_ERASENOW = 0x0200,
            /// <summary>
            /// Causes the affected windows (as specified by the RDW_ALLCHILDREN and RDW_NOCHILDREN flags) to receive WM_NCPAINT, WM_ERASEBKGND, and WM_PAINT messages, if necessary, before the function returns.
            /// </summary>
            RDW_UPDATENOW = 0x0100,
            //By default, the windows affected by RedrawWindow depend on whether the specified window has the WS_CLIPCHILDREN style. Child windows that are not the WS_CLIPCHILDREN style are unaffected; non-WS_CLIPCHILDREN windows are recursively validated or invalidated until a WS_CLIPCHILDREN window is encountered. The following flags control which windows are affected by the RedrawWindow function.
            /// <summary>
            /// Excludes child windows, if any, from the repainting operation.
            /// </summary>
            RDW_NOCHILDREN = 0x0040,
            /// <summary>
            /// Includes child windows, if any, in the repainting operation.
            /// </summary>
            RDW_ALLCHILDREN = 0x0080,
        }
        /// <summary>
        /// Window Style Flags
        /// </summary>
        [Flags]
        internal enum WindowStyleFlags : uint
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,
            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,
        }
        /// <summary>
        /// Class Style Flags
        /// </summary>
        [Flags]
        internal enum ClassStyleFlags : uint
        {
            CS_VREDRAW = 0x0001,
            CS_HREDRAW = 0x0002,
            CS_DBLCLKS = 0x0008,
            CS_GLOBALCLASS = 0x4000,
        }
        /// <summary>
        /// Extended Windows Style flags
        /// </summary>
        [Flags]
        internal enum ExtendedWindowStyleFlags : int
        {
            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,
            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,
            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,
            WS_EX_LAYERED = 0x00080000,
            /// <summary>
            /// Disable inheritence of mirroring by children
            /// </summary>
            WS_EX_NOINHERITLAYOUT = 0x00100000,
            /// <summary>
            /// Right to left mirroring
            /// </summary>
            WS_EX_LAYOUTRTL = 0x00400000,
            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_NOACTIVATE = 0x08000000
        }
        /// <summary>
        /// WM_ACTIVATE wParam values.
        /// </summary>
        [Flags]
        internal enum WMActivate : int
        {
            /// <summary>
            /// Deactivated.
            /// </summary>
            WA_INACTIVE = 0x0000,
            /// <summary>
            /// Activated by some method other than a mouse click (for example, by a call to the SetActiveWindow function or by use of the keyboard interface to select the window).
            /// </summary>
            WA_ACTIVE = 0x0001,
            /// <summary>
            /// Activated by a mouse click.
            /// </summary>
            WA_CLICKACTIVE = 0x0002,
        }
        /// <summary>
        /// WM_MOUSEACTIVATE return values
        /// </summary>
        [Flags]
        internal enum MouseActivate : int
        {
            /// <summary>
            /// Activa la ventana y no se descarta el mensaje de ratón.
            /// </summary>
            MA_ACTIVATE = 0x00000001,
            /// <summary>
            /// Activa la ventana y descarta el mensaje de ratón.
            /// </summary>
            MA_ACTIVATEANDEAT = 0x00000002,
            /// <summary>
            /// No activa la ventana, y no descarta el mensaje de ratón.
            /// </summary>
            MA_NOACTIVATE = 0x00000003,
            /// <summary>
            /// No activa la ventana, pero descarta el mensaje de ratón.
            /// </summary>
            MA_NOACTIVATEANDEAT = 0x00000004
        }
        /// <summary>
        /// Return code for WM_GETDLGCODE
        /// </summary>
        [Flags]
        internal enum DLGC : int
        {
            /// <summary>
            /// Button.
            /// </summary>
            DLGC_BUTTON = 0x2000,
            /// <summary>
            /// Default push button.
            /// </summary>
            DLGC_DEFPUSHBUTTON = 0x0010,
            /// <summary>
            /// EM_SETSEL messages.
            /// </summary>
            DLGC_HASSETSEL = 0x0008,
            /// <summary>
            /// Radio button.
            /// </summary>
            DLGC_RADIOBUTTON = 0x0040,
            /// <summary>
            /// Static control.
            /// </summary>
            DLGC_STATIC = 0x0100,
            /// <summary>
            /// Non-default push button.
            /// </summary>
            DLGC_UNDEFPUSHBUTTON = 0x0020,
            /// <summary>
            /// All keyboard input.
            /// </summary>
            DLGC_WANTALLKEYS = 0x0004,
            /// <summary>
            /// Direction keys.
            /// </summary>
            DLGC_WANTARROWS = 0x0001,
            /// <summary>
            /// WM_CHAR messages.
            /// </summary>
            DLGC_WANTCHARS = 0x0080,
            /// <summary>
            /// All keyboard input (the application passes this message in the MSG structure to the control).
            /// </summary>
            DLGC_WANTMESSAGE = 0x0004,
            /// <summary>
            /// TAB key.
            /// </summary>
            DLGC_WANTTAB = 0x0002,
            /// <summary>
            /// User message EnumChildProc
            /// </summary>
        }
        /// <summary>
        /// WM_
        /// </summary>
        [Flags]
        internal enum WM : int
        {
            /// <summary>
            /// Sent to a window after it has gained the keyboard focus.
            /// wParam A handle to the window that has lost the keyboard focus. This parameter can be NULL.
            /// lParam This parameter is not used.
            /// An application should return zero if it processes this message.
            /// </summary>
            WM_SETFOCUS = 0x0007,
            /// <summary>
            /// Sent to a window immediately before it loses the keyboard focus.
            /// wParam A handle to the window that receives the keyboard focus. This parameter can be NULL.
            /// lParam This parameter is not used.
            /// An application should return zero if it processes this message.
            /// </summary>
            WM_KILLFOCUS = 0x0008,
            /// <summary>
            /// Sets the text of a window.
            /// wParam This parameter is not used.
            /// lParam A pointer to a null-terminated string that is the window text.
            /// The return value is TRUE if the text is set.
            /// It is FALSE (for an edit control), LB_ERRSPACE (for a list box), or CB_ERRSPACE (for a combo box) if insufficient space is available to set the text in the edit control. It is CB_ERR if this message is sent to a combo box without an edit control.
            /// The DefWindowProc function sets and displays the window text. For an edit control, the text is the contents of the edit control. For a combo box, the text is the contents of the edit-control portion of the combo box. For a button, the text is the button name. For other windows, the text is the window title.
            /// </summary>
            WM_SETTEXT = 0x000C,
            /// <summary>
            /// An application sends a WM_GETTEXT message to copy the text that corresponds to a window into a buffer provided by the caller.
            /// </summary>
            WM_GETTEXT = 0x000D,
            /// <summary>
            /// An application sends a WM_GETTEXTLENGTH message to determine the length, in characters, of the text associated with a window.
            /// </summary>
            WM_GETTEXTLENGTH = 0x000E,
            WM_NOTIFY = 0x004E,
            /// <summary>
            /// Window received a command notification
            /// siempre devuelve el mismo valor porque no es un díalogo, sino un form
            /// en el caso de un diálogo devuleve el ID del control dentro del diálogo, NO EL hWnd
            /// sin embargo lParam contiene el hWnd de cada edit 
            /// </summary>
            WM_COMMAND = 0x0111,
            WM_COMMAND_REFLECT = WM_COMMAND + WM_REFLECT,
            /// <summary>
            /// Sent to the window procedure associated with a control. By default, the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior, the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.
            /// wParam The virtual key, pressed by the user, that prompted Windows to issue this notification. The handler must selectively handle these keys. For instance, the handler might accept and process VK_RETURN but delegate VK_TAB to the owner window. For a list of values, see Virtual-Key Codes.
            /// lParam A pointer to an MSG structure (or NULL if the system is performing a query).
            /// The return value is one or more of the following values, indicating which type of input the application processes.
            /// Remarks Although the DefWindowProc function always returns zero in response to the WM_GETDLGCODE message, the window procedure for the predefined control classes return a code appropriate for each class.
            /// The WM_GETDLGCODE message and the returned values are useful only with user-defined dialog box controls or standard controls modified by subclassing.
            /// See DLGC for Return code/value
            /// </summary>
            WM_GETDLGCODE = 0x0087,
            WM_GETDLGCODE_REFLECT = WM_GETDLGCODE + WM_REFLECT,
            WM_USER = 0x0400,
            /// <summary>
            /// User message for enumerate child window process
            /// </summary>
            WM_ENUMCHILPROC = WM_USER + 1,
            /// <summary>
            /// occurs when the application becomes the active application or becomes inactive.
            /// </summary>
            WM_ACTIVATEAPP = 0x001C,
            /// <summary>
            /// Sent to both the window being activated and the window being deactivated.
            /// If the windows use the same input queue, the message is sent synchronously, first to the window procedure of the top-level window being deactivated, then to the window procedure of the top-level window being activated.
            /// If the windows use different input queues, the message is sent asynchronously, so the window is activated immediately.
            /// 
            /// wParam The low-order word specifies whether the window is being activated or deactivated.
            /// This parameter can be one of the following values. The high-order word specifies the minimized state of the window being activated or deactivated.
            /// A nonzero value indicates the window is minimized.
            /// WA_ACTIVE 1
            /// Activated by some method other than a mouse click (for example, by a call to the SetActiveWindow function or by use of the keyboard interface to select the window).
            /// WA_CLICKACTIVE 2
            /// Activated by a mouse click.
            /// WA_INACTIVE 0
            /// Deactivated.
            /// lParam A handle to the window being activated or deactivated, depending on the value of the wParam parameter.
            /// If the low-order word of wParam is WA_INACTIVE, lParam is the handle to the window being activated.
            /// If the low-order word of wParam is WA_ACTIVE or WA_CLICKACTIVE, lParam is the handle to the window being deactivated. This handle can be NULL.
            /// If an application processes this message, it should return zero.
            /// Remarks If the window is being activated and is not minimized, the DefWindowProc function sets the keyboard focus to the window.
            /// If the window is activated by a mouse click, it also receives a WM_MOUSEACTIVATE message.
            /// </summary>
            WM_ACTIVATE = 0x0006,
            WM_ACTIVATED_REFLECT = WM_ACTIVATE + WM_REFLECT,
            /// <summary>
            /// TN062: Message Reflection for Windows Controls
            /// http://msdn.microsoft.com/en-us/library/eeah46xd.aspx
            /// </summary>
            WM_REFLECT = WM_USER + 0x1C00,
            WM_NOTIFY_REFLECT = WM_NOTIFY + WM_REFLECT,
            /// <summary>
            /// hdcEdit = (HDC) wParam
            /// Identifies the device context for the edit control window.
            /// hwndEdit = (HWND) lParam
            /// Identifies the edit control.
            /// If an application processes this message, it must return the handle of a brush.
            /// Windows uses the brush to paint the background of the edit control.
            /// </summary>
            WM_CTLCOLOREDIT = 0x0133,
            WM_PAINT = 0x000F,
            WM_MOUSEACTIVATE = 0x0021,
            /// <summary>
            /// An application sends the WM_SETREDRAW message to a window to allow changes in that window to be redrawn or to prevent changes in that window from being redrawn.
            /// wParam The redraw state. If this parameter is TRUE, the content can be redrawn after a change. If this parameter is FALSE, the content cannot be redrawn after a change.
            /// lParam This parameter is not used.
            /// An application returns zero if it processes this message.
            /// </summary>
            WM_SETREDRAW = 0xB,
            /// <summary>
            /// Posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.#define
            /// wParam The virtual-key code of the nonsystem key. See Virtual-Key Codes.
            /// lParam The repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown following.
            /// Bits	Meaning
            /// 0-15	The repeat count for the current message. The value is the number of times the keystroke is autorepeated as a result of the user holding down the key. If the keystroke is held long enough, multiple messages are sent. However, the repeat count is not cumulative.
            /// 16-23	The scan code. The value depends on the OEM.
            /// 24	Indicates whether the key is an extended key, such as the right-hand ALT and CTRL keys that appear on an enhanced 101- or 102-key keyboard. The value is 1 if it is an extended key; otherwise, it is 0.
            /// 25-28	Reserved; do not use.
            /// 29	The context code. The value is always 0 for a WM_KEYDOWN message.
            /// 30	The previous key state. The value is 1 if the key is down before the message is sent, or it is zero if the key is up.
            /// 31	The transition state. The value is always 0 for a WM_KEYDOWN message.
            /// An application should return zero if it processes this message.
            /// VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
            /// Used only as parameters to GetAsyncKeyState() and GetKeyState().
            /// No other API or message will distinguish left and right keys in this way.
            /// </summary>
            WM_KEYDOWN = 0x0100,
            /// <summary>
            /// Posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.
            /// wParam The virtual-key code of the nonsystem key. See Virtual-Key Codes.
            /// lParam The repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown in the following table.
            /// Bits	Meaning
            /// 0-15	The repeat count for the current message. The value is the number of times the keystroke is autorepeated as a result of the user holding down the key. The repeat count is always 1 for a WM_KEYUP message.
            /// 16-23	The scan code. The value depends on the OEM.
            /// 24	Indicates whether the key is an extended key, such as the right-hand ALT and CTRL keys that appear on an enhanced 101- or 102-key keyboard. The value is 1 if it is an extended key; otherwise, it is 0.
            /// 25-28	Reserved; do not use.
            /// 29	The context code. The value is always 0 for a WM_KEYUP message.
            /// 30	The previous key state. The value is always 1 for a WM_KEYUP message.
            /// 31	The transition state. The value is always 1 for a WM_KEYUP message.
            /// An application should return zero if it processes this message.
            /// </summary>
            WM_KEYUP = 0x0101,
            VK_LSHIFT = 0xA0,
            VK_RSHIFT = 0xA1,

            /// <summary>
            /// Replaces a handle to the background brush associated with the class.
            /// </summary>
            GCLP_HBRBACKGROUND = -10,
            /// <summary>
            /// retrieve the identifier (zero based) of Edit Control
            /// </summary>
            GWL_USERDATA = -21,
        }
        /// <summary>
        /// IP Address Messages
        /// </summary>
        [Flags]
        internal enum IPAMess : int
        {
            /// <summary>
            /// Sent when an edit control receives the keyboard focus. The parent window of the edit control receives this notification code through a WM_COMMAND message.
            /// wParam The LOWORD contains the identifier of the edit control.
            /// The HIWORD specifies the notification code.
            /// lParam Handle to the edit control.
            /// The parent window always receives a WM_COMMAND message for this event, it does not require a notification mask sent with EM_SETEVENTMASK.
            /// </summary>
            EN_SETFOCUS = 0x0100,
            /// <summary>
            /// Sent when an edit control loses the keyboard focus. The parent window of the edit control receives this notification code through a WM_COMMAND message.
            /// wParam The LOWORD contains the identifier of the edit control.
            /// The HIWORD specifies the notification code.
            /// lParam Handle to the edit control.
            /// The parent window always receives a WM_COMMAND message for this event, it does not require a notification mask sent with EM_SETEVENTMASK.
            /// </summary>
            EN_KILLFOCUS = 0x0200,
            /// <summary>
            /// Sent when the user has taken an action that may have altered text in an edit control.
            /// Unlike the EN_UPDATE notification code, this notification code is sent after the system updates the screen.
            /// The parent window of the edit control receives this notification code through a WM_COMMAND message.
            /// wParam The LOWORD contains the identifier of the edit control. The HIWORD specifies the notification code.
            /// lParam A handle to the edit control.
            /// </summary>
            EN_CHANGE = 0x0300,
            /// <summary>
            /// Sent when an edit control is about to redraw itself. This notification code is sent after the control has formatted the text, but before it displays the text. This makes it possible to resize the edit control window, if necessary. The parent window of the edit control receives this notification code through a WM_COMMAND message.
            /// wParam The LOWORD contains the identifier of the edit control. The HIWORD specifies the notification code.
            /// lParam A handle to the edit control.
            /// </summary>
            EN_UPDATE = 0x0400,
            EN_ERRSPACE = 0x0500,
            /// <summary>
            /// Sent when the current text insertion has exceeded the specified number of characters for the edit control. The text insertion has been truncated.
            /// This notification code is also sent when an edit control does not have the ES_AUTOHSCROLL style and the number of characters to be inserted would exceed the width of the edit control.
            /// This notification code is also sent when an edit control does not have the ES_AUTOVSCROLL style and the total number of lines resulting from a text insertion would exceed the height of the edit control.
            /// wParam The LOWORD contains the identifier of the edit control. The HIWORD specifies the notification code.
            /// lParam A handle to the edit control.
            /// </summary>
            EN_MAXTEXT = 0x0501,
            EN_HSCROLL = 0x0601,
            EN_VSCROLL = 0x0602,
            /// <summary>
            /// Sent when the user changes a field in the control or moves from one field to another.
            /// This notification message is sent in the form of a WM_NOTIFY message.
            /// The return value is ignored.
            /// lParam lpnmipa LPNMIPADDRESS
            /// Address of an NMIPADDRESS structure that contains information about the changed address.
            /// The iValue member of this structure will contain the entered value, even if it is out of the range of the field.
            /// You can modify this member to any value that is within the range for the field in response to this notification.
            /// This notification is not sent in response to a IPM_SETADDRESS message.
            /// The return value is ignored
            /// </summary>
            IPN_FIELDCHANGED = -860,
            /// <summary>
            /// Sets the valid range for the specified field in the IP address control.
            /// wParam A zero-based field index to which the range will be applied.
            /// lParam A WORD value that contains the lower limit of the range in the low-order byte and the upper limit in the high-order byte. 
            /// Both of these values are inclusive. The MAKEIPRANGE macro can also be used to create the range.
            /// Returns nonzero if successful, or zero otherwise.
            /// </summary>
            IPM_SETRANGE = (WM.WM_USER + 103),
            /// <summary>
            /// lParam = LPDWORD for TCP/IP address
            /// Returns the number of nonblank fields.
            /// </summary>
            IPM_GETADDRESS = (WM.WM_USER + 102),
            /// <summary>
            /// lparam = TCP/IPaddress
            /// </summary>
            IPM_SETADDRESS = (WM.WM_USER + 101),
            /// <summary>
            /// wParam = 0; lParam = 0;
            /// Clears the contents of the IP address control.
            /// The return value is not used.
            /// </summary>
            IPM_CLEARADDRESS = (WM.WM_USER + 100),
            /// <summary>
            /// wParam = 0; lParam = 0;
            /// Determines if all fields in the IP address control are blank.
            /// Returns nonzero if all fields are blank, or zero otherwise.
            /// </summary>
            IPM_ISBLANK = (WM.WM_USER + 105),
            /// <summary>
            /// Sets the keyboard focus to the specified field in the IP address control. All of the text in that field will be selected.
            /// The return value is not used.
            /// wParam = nField
            /// Zero-based field index to which the focus should be set.
            /// If this value is greater than the number of fields, focus is set to the first blank field.
            /// If all fields are nonblank, focus is set to the first field.
            /// lParam Not used; must be zero.
            /// </summary>
            IPM_SETFOCUS = (WM.WM_USER + 104),
            ICC_INTERNET_CLASSES = 0x00000800,
            /// <summary>
            /// Prevents a single-line edit control from receiving keyboard focus.
            /// wParam Not used; must be zero. lParam Not used; must be zero.
            /// The return value is not used.
            /// </summary>
            EM_NOSETFOCUS = (0x1500 + 7),
            /// <summary>
            /// Forces a single-line edit control to receive keyboard focus.
            /// wParam Not used; must be zero. lParam Not used; must be zero.
            /// The return value is not used.
            /// </summary>
            EM_TAKEFOCUS = (0x1500 + 8),
        }
        internal delegate int EnumWindowsProc(IntPtr hwnd, int lParam);
        //You could use "SetClassLong(Ptr)" with "GCL(P)_HBRBACKGROUND"... but that would change all your buttons.
        internal static IntPtr SetClassLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size > 4)
                return SetClassLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetClassLongPtr32(hWnd, nIndex, unchecked((uint)dwNewLong.ToInt32())));
        }
        /// <summary>
        /// Enables or disables mouse and keyboard input to the specified window or control.
        /// When input is disabled, the window does not receive input such as mouse clicks and key presses.
        /// When input is enabled, the window receives all input.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be enabled or disabled.</param>
        /// <param name="bEnable">Indicates whether to enable or disable the window.
        /// If this parameter is TRUE, the window is enabled. If the parameter is FALSE, the window is disabled.</param>
        /// <returns>If the window was previously disabled, the return value is nonzero.
        /// If the window was not previously disabled, the return value is zero.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(System.Drawing.Point pt) : this(pt.X, pt.Y) { }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public UInt32 message;
            public IntPtr wParam;
            public IntPtr lParam;
            public UInt32 time;
            public POINT pt;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            internal int Left, Top, Right, Bottom;

            internal RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
            internal RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool RedrawWindow(HandleRef hWnd, [In] ref RECT lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool RedrawWindow(HandleRef hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags); 

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetWindowLongPtr(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetClassLong", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern uint SetClassLongPtr32(HandleRef hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetClassLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpRect">A pointer to a RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern uint SetDCBrushColor(IntPtr hdc, int crColor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern uint SetTextColor(IntPtr hdc, int crColor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern uint SetBkMode(IntPtr hdc, int crColor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetStockObject(StockObjects fnObject);
        internal enum StockObjects : int
        {
            WHITE_BRUSH = 0,
            LTGRAY_BRUSH = 1,
            GRAY_BRUSH = 2,
            DKGRAY_BRUSH = 3,
            BLACK_BRUSH = 4,
            NULL_BRUSH = 5,
            HOLLOW_BRUSH = NULL_BRUSH,
            WHITE_PEN = 6,
            BLACK_PEN = 7,
            NULL_PEN = 8,
            OEM_FIXED_FONT = 10,
            ANSI_FIXED_FONT = 11,
            ANSI_VAR_FONT = 12,
            SYSTEM_FONT = 13,
            DEVICE_DEFAULT_FONT = 14,
            DEFAULT_PALETTE = 15,
            SYSTEM_FIXED_FONT = 16,
            DEFAULT_GUI_FONT = 17,
            DC_BRUSH = 18,
            DC_PEN = 19,
        }
        /// <summary>
        /// - IntPtr WindowFromPoint(int x, int y) works in 32-bit but fails in 64-bit process.
        /// </summary>
        /// <param name="xPoint"></param>
        /// <param name="yPoint"></param>
        /// <returns>Window Handle</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int SetFocus(IntPtr hWndParent);

        /// <summary>
		/// The GetSystemMetrics function retrieves various system metrics (widths and 
		/// heights of display elements) and system configuration settings. All dimensions 
		/// retrieved by GetSystemMetrics are in pixels.
		/// </summary>
		/// <param name="nIndex">System metric or configuration setting to retrieve.</param>
		/// <returns>
		/// If the function succeeds, the return value is the requested system metric or configuration setting.
		/// If the function fails, the return value is zero.
		/// </returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal static extern int GetSystemMetrics(int nIndex);

		/// <summary>
		/// Determines whether pop-up menus are left-aligned or right-aligned, relative to the corresponding 
		/// menu-bar item. The pvParam parameter must point to a BOOL variable that receives TRUE if left-aligned, 
		/// or FALSE otherwise.
		/// </summary>
		internal const uint SPI_GETMENUDROPALIGNMENT = 27;

		/// <summary>Index used with GetSystemMetrics to determine whether the current machine is a Tablet PC.</summary>
		internal const int SM_TABLETPC = 86;
		
		/// <summary>The GetCurrentProcessId function retrieves the process identifier of the calling process.</summary>
		/// <returns>The return value is the process identifier of the calling process.</returns>
		[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern uint GetCurrentProcessId();

		/// <summary>
		/// The GetWindowThreadProcessId function retrieves the identifier of the thread that created the specified window 
		/// and, optionally, the identifier of the process that created the window. 
		/// </summary>
		/// <param name="handle">Handle to the window.</param>
		/// <param name="processId">Pointer to a variable that receives the process identifier.</param>
		/// <returns>The return value is the identifier of the thread that created the window.</returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern int GetWindowThreadProcessId(HandleRef handle, out uint processId);

		/// <summary>The GetWindow function retrieves a handle to a window that has the specified 
		/// relationship (Z-Order or owner) to the specified window.</summary>
		/// <param name="hWnd">Handle to a window.</param>
		/// <param name="uCmd">Specifies the relationship between the specified window and the window whose handle is to be retrieved.</param>
		/// <returns>
		/// If the function succeeds, the return value is a window handle. 
		/// If no window exists with the specified relationship to the specified window, the return value is NULL.
		/// </returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		internal static extern IntPtr GetWindow(HandleRef hWnd, int uCmd);

		/// <summary>The IsWindowVisible function retrieves the visibility state of the specified window.</summary>
		/// <param name="hWnd">Handle to the window to test.</param>
		/// <returns>
		/// If the specified window, its parent window, its parent's parent window, and so forth, have the WS_VISIBLE style, 
		/// the return value is nonzero. Otherwise, the return value is zero. 
		/// </returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsWindowVisible(HandleRef hWnd);

		/// <summary>
		/// The EnumWindows function enumerates all top-level windows on the screen by passing the handle to each window, in turn, 
		/// to an application-defined callback function. EnumWindows continues until the last top-level window is 
		/// enumerated or the callback function returns FALSE.
		/// </summary>
		/// <param name="callback">Pointer to an application-defined callback function.</param>
		/// <param name="extraData">Specifies an application-defined value to be passed to the callback function.</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EnumWindows(NativeMethods.EnumThreadWindowsCallback callback, IntPtr extraData);

		/// <summary>Delegate used for callbacks from EnumWindows.</summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		internal delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

		/// <summary>The ShowWindowAsync function sets the show state of a window created by a different thread.</summary>
		/// <param name="hWnd">Handle to the window.</param>
		/// <param name="cmdShow">Specifies how the window is to be shown.</param>
		/// <returns>
		/// If the window was previously visible, the return value is nonzero. If the window was previously hidden, the return value is zero.
		/// </returns>
		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

		/// <summary>
		/// The SetForegroundWindow function puts the thread that created the specified window into the foreground and activates the window. 
		/// </summary>
		/// <param name="hWnd">Handle to the window that should be activated and brought to the foreground.</param>
		/// <returns>
		/// If the window was brought to the foreground, the return value is nonzero.
		/// If the window was not brought to the foreground, the return value is zero.
		/// </returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetForegroundWindow(HandleRef hWnd);

		/// <summary>
		/// The CreateFileMapping function creates or opens a named or unnamed file mapping object for the specified file.
		/// </summary>
		/// <param name="hFile">Handle to the file from which to create a mapping object.</param>
		/// <param name="lpAttributes">
		/// Pointer to a SECURITY_ATTRIBUTES structure that determines whether the returned handle can be inherited by child processes.
		/// </param>
		/// <param name="flProtect">Protection desired for the file view, when the file is mapped.</param>
		/// <param name="dwMaxSizeHi">High-order DWORD of the maximum size of the file mapping object.</param>
		/// <param name="dwMaxSizeLow">Low-order DWORD of the maximum size of the file mapping object.</param>
		/// <param name="lpName">Pointer to a null-terminated string specifying the name of the mapping object.</param>
		/// <returns>
		/// If the function succeeds, the return value is a handle to the file mapping object. If the object existed before the function call, the function 
		/// returns a handle to the existing object (with its current size, not the specified size) and GetLastError returns ERROR_ALREADY_EXISTS. 
		/// If the function fails, the return value is NULL.
		/// </returns>
		[DllImport("Kernel32", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern IntPtr CreateFileMapping(IntPtr hFile, 
			IntPtr lpAttributes, int flProtect, int dwMaxSizeHi, int dwMaxSizeLow, string lpName);

		/// <summary>The OpenFileMapping function opens a named file mapping object.</summary>
		/// <param name="dwDesiredAccess">Access to the file mapping object.</param>
		/// <param name="bInheritHandle">If this parameter is TRUE, the new process inherits the handle. Otherwise, it does not.</param>
		/// <param name="lpName">
		/// Pointer to a string that names the file mapping object to be opened. If there is an open handle to a file mapping 
		/// object by this name and the security descriptor on the mapping object does not conflict with the dwDesiredAccess 
		/// parameter, the open operation succeeds.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is an open handle to the specified file mapping object.
		/// If the function fails, the return value is NULL.
		/// </returns>
		[DllImport("Kernel32", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern IntPtr OpenFileMapping(int dwDesiredAccess, 
			[MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

		/// <summary>
		/// The MapViewOfFile function maps a view of a file into the address space of the calling process.
		/// </summary>
		/// <param name="hFileMapping">Handle to an open handle of a file mapping object.</param>
		/// <param name="dwDesiredAccess">Type of access to the file view and, therefore, the protection of the pages mapped by the file.</param>
		/// <param name="dwFileOffsetHigh">High-order DWORD of the file offset where mapping is to begin.</param>
		/// <param name="dwFileOffsetLow">Low-order DWORD of the file offset where mapping is to begin.</param>
		/// <param name="dwNumberOfBytesToMap">Number of bytes of the file to map. If this parameter is zero, the entire file is mapped.</param>
		/// <returns>
		/// If the function succeeds, the return value is the starting address of the mapped view.
		/// If the function fails, the return value is NULL.
		/// </returns>
		[DllImport("Kernel32", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern IntPtr MapViewOfFile(IntPtr hFileMapping, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, int dwNumberOfBytesToMap);

		/// <summary>
		/// The UnmapViewOfFile function unmaps a mapped view of a file from the calling process's address space.
		/// </summary>
		/// <param name="pvBaseAddress">Pointer to the base address of the mapped view of a file that is to be unmapped.</param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero, and all dirty pages within the specified range are written "lazily" to disk.
		/// If the function fails, the return value is zero.
		/// </returns>
		[DllImport("Kernel32", CharSet=CharSet.Auto, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

		/// <summary>The CloseHandle function closes an open object handle.</summary>
		/// <param name="handle">The CloseHandle function closes an open object handle.</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
		[DllImport("kernel32.dll", SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// Copies the text of the specified window's title bar (if it has one) into a buffer.
        /// If the specified window is a control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another application.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control containing the text.</param>
        /// <param name="lpString">[out] The buffer that will receive the text.
        /// If the string is as long or longer than the buffer, the string is truncated and terminated with a null character.</param>
        /// <param name="nMaxCount">nMaxCount [in] The maximum number of characters to copy to the buffer, including the null character.
        /// If the text exceeds this limit, it is truncated.</param>
        /// <returns>If the function succeeds, the return value is the length, in characters, of the copied string, not including the terminating null character.
        /// If the window has no title bar or text, if the title bar is empty, or if the window or control handle is invalid, the return value is zero.
        /// This function cannot retrieve the text of an edit control in another application.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(HandleRef hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        internal static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SendMessage(HandleRef hWnd, WM msg, IntPtr wParam, System.Text.StringBuilder lParam);

        /// <summary>
        /// The SendMessage function sends the specified message to a window or windows. 
        /// It calls the window procedure for the specified window and does not return until the window procedure has processed the message. 
        /// </summary>
        /// <param name="hWnd">Handle to the window whose window procedure will receive the message.</param>
        /// <param name="msg">Specifies the message to be sent.</param>
        /// <param name="wParam">Specifies additional message-specific information.</param>
        /// <param name="lParam">Specifies additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SendMessage(int hWnd, int msg, int wParam, int lparam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, out IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SendMessage(HandleRef hWnd, WM msg, IntPtr wParam, out IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SendMessage(HandleRef hWnd, WM msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr PostMessage(HandleRef hWnd, WM msg, IntPtr wParam, IntPtr lParam);

		/// <summary>Registers specific common control classes from the common control dynamic-link library (DLL).</summary>
		/// <param name="icc">Pointer to an INITCOMMONCONTROLSEX structure that contains information specifying which control classes will be registered.</param>
		/// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
		[DllImport("comctl32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);
 
		/// <summary>Carries information used to load common control classes from the dynamic-link library (DLL).</summary>
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal class INITCOMMONCONTROLSEX
		{
			/// <summary>Size of the structure, in bytes.</summary>
			public int dwSize;
			/// <summary>Set of bit flags that indicate which common control classes will be loaded from the DLL.</summary>
			public int dwICC;
		}

		/// <summary>The GetWindowPlacement function retrieves the show state and the restored, minimized, and maximized positions of the specified window.</summary>
		/// <param name="hWnd">Handle to the window.</param>
		/// <param name="placement">Pointer to the WINDOWPLACEMENT structure that receives the show state and position information.</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetWindowPlacement(HandleRef hWnd, ref WINDOWPLACEMENT placement);

		/// <summary>The IsIconic function determines whether the specified window is minimized (iconic).</summary>
		/// <param name="hWnd">Handle to the window to test.</param>
		/// <returns>If the window is iconic, the return value is nonzero. If the window is not iconic, the return value is zero.</returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsIconic(HandleRef hWnd);

		/// <summary>The WINDOWPLACEMENT structure contains information about the placement of a window on the screen.</summary>
		[StructLayout(LayoutKind.Sequential)]
			internal struct WINDOWPLACEMENT
		{
			/// <summary>Specifies the length, in bytes, of the structure.</summary>
			public int length;
			/// <summary>Specifies flags that control the position of the minimized window and the method by which the window is restored.</summary>
			public int flags;
			/// <summary>Specifies the current show state of the window.</summary>
			public int showCmd;
			/// <summary>Specifies the X coordinate of the window's upper-left corner when the window is minimized.</summary>
			public int ptMinPosition_x;
			/// <summary>Specifies the Y coordinate of the window's upper-left corner when the window is minimized.</summary>
			public int ptMinPosition_y;
			/// <summary>Specifies the X coordinate of the window's upper-left corner when the window is maximized.</summary>
			public int ptMaxPosition_x;
			/// <summary>Specifies the Y coordinate of the window's upper-left corner when the window is maximized.</summary>
			public int ptMaxPosition_y;
			/// <summary>Specifies the window's left coordinate when the window is in the restored position.</summary>
			public int rcNormalPosition_left;
			/// <summary>Specifies the window's top coordinate when the window is in the restored position.</summary>
			public int rcNormalPosition_top;
			/// <summary>Specifies the window's right coordinate when the window is in the restored position.</summary>
			public int rcNormalPosition_right;
			/// <summary>Specifies the window's bottom coordinate when the window is in the restored position.</summary>
			public int rcNormalPosition_bottom;
		}

		/// <summary>The GetCurrentProcess function retrieves a pseudo handle for the current process.</summary>
		/// <returns>The return value is a pseudo handle to the current process.</returns>
		[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern IntPtr GetCurrentProcess();

		/// <summary>
		/// The SystemParametersInfo function retrieves or sets the value of 
		/// one of the system-wide parameters. This function can also update the user profile while setting a parameter.
		/// </summary>
		/// <param name="uiAction">System-wide parameter to be retrieved or set.</param>
		/// <param name="uiParam">Depends on the system parameter being queried or set.</param>
		/// <param name="pvParam">Depends on the system parameter being queried or set.</param>
		/// <param name="fWinIni">If a system parameter is being set, specifies whether the user profile is to be updated.</param>
		/// <returns>
		/// If the function succeeds, the return value is a nonzero value.
		/// If the function fails, the return value is zero.
		/// </returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SystemParametersInfo(uint uiAction, uint uiParam, 
			[MarshalAs(UnmanagedType.Bool)] ref bool pvParam, uint fWinIni);

		/// <summary>The SetProcessWorkingSetSize function sets the minimum and maximum working set sizes for the specified process.</summary>
		/// <param name="hProcess">Handle to the process whose working set sizes is to be set.</param>
		/// <param name="dwMinimumWorkingSetSize">Minimum working set size for the process, in bytes.</param>
		/// <param name="dwMaximumWorkingSetSize">Maximum working set size for the process, in bytes.</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
		[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetProcessWorkingSetSize(IntPtr hProcess, IntPtr dwMinimumWorkingSetSize, IntPtr dwMaximumWorkingSetSize);
	}
}