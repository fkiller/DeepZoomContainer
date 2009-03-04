/*----------------------------------------------------------------------
 * ---------------------------------------------------------------------
 *                  Wheel Event Handler for Silverlight
 *                  
 * from
 * http://silverlight.net/blogs/msnow/archive/2008/10/21/silverlight-tip-of-the-day-65-adding-a-mouse-wheel-event-listener-to-your-elements.aspx
 * 
 * ---------------------------------------------------------------------
 * ---------------------------------------------------------------------*/

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Browser;
using System.Diagnostics;

namespace ROH.Web.Client.Silverlight
{
    /// <summary>
    /// Wheel Event Observer Interface for Silverlight
    /// from
    /// http://silverlight.net/blogs/msnow/archive/2008/10/21/silverlight-tip-of-the-day-65-adding-a-mouse-wheel-event-listener-to-your-elements.aspx
    /// </summary>
    public interface IMouseWheelObserver
    {
        void OnMouseWheel(MouseWheelArgs args);
        event MouseEventHandler MouseEnter;
        event MouseEventHandler MouseLeave;
    }

    /// <summary>
    /// Wheel Event Argument for Silverlight
    /// from
    /// http://silverlight.net/blogs/msnow/archive/2008/10/21/silverlight-tip-of-the-day-65-adding-a-mouse-wheel-event-listener-to-your-elements.aspx
    /// </summary>
    public class MouseWheelArgs : EventArgs
    {
        private readonly double
            _Delta;

        private readonly bool
            _ShiftKey,
            _CtrlKey,
            _AltKey;

        public double Delta
        {
            get { return this._Delta; }
        }

        public bool ShiftKey
        {
            get { return this._ShiftKey; }
        }

        public bool CtrlKey
        {
            get { return this._CtrlKey; }
        }

        public bool AltKey
        {
            get { return this._AltKey; }
        }

        public MouseWheelArgs(double delta, bool shiftKey, bool ctrlKey, bool altKey)
        {
            this._Delta = delta;
            this._ShiftKey = shiftKey;
            this._CtrlKey = ctrlKey;
            this._AltKey = altKey;
        }
    }

    /// <summary>
    /// Wheel Event Handler for Silverlight
    /// from
    /// http://silverlight.net/blogs/msnow/archive/2008/10/21/silverlight-tip-of-the-day-65-adding-a-mouse-wheel-event-listener-to-your-elements.aspx
    /// </summary>
    public class WheelMouseListener
    {
        private Stack<IMouseWheelObserver> _ElementStack;

        private WheelMouseListener()
        {
            // If this class is called by non-browser(e.g. Expression Blend),
            // error occurs without exception handling.
            try
            {
                this._ElementStack = new Stack<IMouseWheelObserver>();
                HtmlPage.Window.AttachEvent("DOMMouseScroll", OnMouseWheel);
                HtmlPage.Window.AttachEvent("onmousewheel", OnMouseWheel);
                HtmlPage.Document.AttachEvent("onmousewheel", OnMouseWheel);

                Application.Current.Exit += new EventHandler(OnApplicationExit);
            }
            catch
            {
                Debug.WriteLine("Not browser or not supported brower");
            }
        }

        /// <summary>
        /// Detaches from the browser-generated scroll events.
        /// </summary>

        private void Dispose()
        {
            // If this class is called by non-browser(e.g. Expression Blend),
            // error occurs without exception handling.
            try
            {
                HtmlPage.Window.DetachEvent("DOMMouseScroll", OnMouseWheel);
                HtmlPage.Window.DetachEvent("onmousewheel", OnMouseWheel);
                HtmlPage.Document.DetachEvent("onmousewheel", OnMouseWheel);
            }
            catch
            {
                Debug.WriteLine("Not browser or not supported brower");
            }
        }

        public void AddObserver(IMouseWheelObserver element)
        {
            element.MouseEnter += new MouseEventHandler(OnElementMouseEnter);
            element.MouseLeave += new MouseEventHandler(OnElementMouseLeave);
        }



        private void OnMouseWheel(object sender, HtmlEventArgs args)
        {
            double delta = 0;
            ScriptObject e = args.EventObject;

            if (e.GetProperty("detail") != null)
            {
                // Mozilla and Safari
                delta = ((double)e.GetProperty("detail"));
            }
            else if (e.GetProperty("wheelDelta") != null)
            {
                // IE and Opera
                delta = ((double)e.GetProperty("wheelDelta"));
            }

            delta = Math.Sign(delta);
            if (this._ElementStack.Count > 0)
                this._ElementStack.Peek().OnMouseWheel(new MouseWheelArgs(delta, args.ShiftKey, args.CtrlKey, args.AltKey));
        }

        private void OnElementMouseLeave(object sender, MouseEventArgs e)
        {
            this._ElementStack.Pop();
        }

        private void OnElementMouseEnter(object sender, MouseEventArgs e)
        {
            this._ElementStack.Push((IMouseWheelObserver)sender);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private static WheelMouseListener _Instance = null;
        public static WheelMouseListener Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new WheelMouseListener();
                }
                return _Instance;
            }
        }
    }



}
