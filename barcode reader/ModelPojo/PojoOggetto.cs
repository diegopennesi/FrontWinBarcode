using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barcode_reader
{
    public class PojoOggetto
    {
        public String isbn { get; set; }
        public String definizione { get; set; }
        public String[] categoria { get; set; }   
        public String descrizione { get; set; }
        public DateTime data { get; set; }
        public int quantità { get; set; }
        public String iseritoDa { get; set; }

        private String prettyArray (String[] array)
        {
            string response = "";
            foreach(String x in array)
            {
                response += "\n \t" + x;
            }

            return response;
        }

        public override string ToString()
        {
            return Utility.ToString(this);
        }
    }
   
}
