using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    public class LVData
    {
        public string Name { get; set; }
        public string Pic { get; set; }
    }
    public class IconSelect : EventArgs
    {
        private LVData m_newtitle;
        public LVData newtitle
        {
            get
            {
                return m_newtitle;
            }
        }
        public IconSelect(LVData mm)
        {
            m_newtitle = mm;
        }

    }
}
