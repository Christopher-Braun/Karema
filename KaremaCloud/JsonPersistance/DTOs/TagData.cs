using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KaReMa.Interfaces
{
    [DebuggerDisplay("{Caption}")]
    public class TagData 
    {
        public TagData()
        {

        }

        public TagData(Guid id, String caption)
        {
            this.Id = id;
            this.Caption = caption;
        }

        public Guid Id
        {
            get;
            set;
        }

        public String Caption
        {
            get;
            set;
        }
    }
}
