using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SHSHQ_CONTROL_PROPERTIES
{
    class ControlProperties
    {
        private delegate void SetControlText(Control requestingControl, string property, string text);
        private delegate void SetControlProperty(Control requestingControl, string property, object value);
        public void SetControlPoperties(Control requestingControl, string property, object value)
        {

            if (requestingControl.GetType().GetProperty(property) != null)
            {
                if (requestingControl.InvokeRequired)
                {
                    SetControlProperty currentControl = new SetControlProperty(SetControlPoperties);
                    requestingControl.BeginInvoke(currentControl, requestingControl, property, value);
                }
                else
                    requestingControl.GetType().GetProperty(property).SetValue(requestingControl, value, null);
            }
        }
        public void SetControlTextProperty(Control requestingControl, string property, string text)
        {

            if (requestingControl.GetType().GetProperty(property) != null)
            {
                if (requestingControl.InvokeRequired)
                {
                    SetControlText currentControl = new SetControlText(SetControlTextProperty);
                    requestingControl.BeginInvoke(currentControl, requestingControl, property, text);
                }
                else
                    requestingControl.GetType().GetProperty(property).SetValue(requestingControl, text, null);
            }
        }
    }
}
