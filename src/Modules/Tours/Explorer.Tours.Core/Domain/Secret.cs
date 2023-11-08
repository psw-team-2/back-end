using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Secret
    {
        public int CheckPointId {  get; set; }
        public string Text { get; set; }

        public Secret(int checkPointId, string text) 
        {
            CheckPointId = checkPointId;
            Text = text;
        }
    }
}
