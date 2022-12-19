using Flowchart_Framework.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren_Py_Designer.Views
{
    public class ReturnableOutPort : OutPort
    {

        public delegate void ValueEventHandler(object sender, ValueChangedEventArgs e);

        public event ValueEventHandler ValueChanged;

        //public event EventHandler LinkedChanged;

        public void LinkedValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }

        public override List<InPort> Linked
        {

            get { return _linked; }
            set
            {
                if (value.Count <= 1)
                {
                    if (Linked.Count > 0)
                    {
                        Linked[0].Parent.ValueChanged -= LinkedValueChanged;
                    }
                    if (value.Count > 0)
                    {
                        value[0].Parent.ValueChanged += LinkedValueChanged;
                    }
                    ValueChanged?.Invoke(null, new ValueChangedEventArgs(value[0].Parent.Value));
                    _linked = value;
                }
                else
                {
                    throw new InvalidOperationException("SinglePort takes to linked ports");
                }
            }
        }

        //public
    }
}
