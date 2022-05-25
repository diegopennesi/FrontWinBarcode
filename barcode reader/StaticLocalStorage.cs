using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barcode_reader
{
    public static class StaticLocalStorage
    {
        public static UserPojo userpojo { get; set; }   
        public static List<PojoOggetto> pojoOggettoList { get; set; }

        public static Boolean isUserLoggedIn { get; set; } = false;
        

    }
}
