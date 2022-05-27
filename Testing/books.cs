using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class books
    {
        dbconnect db = new dbconnect();
        public int book_id { get; set; }
        public string book_title { get; set; }
        public string book_author { get; set; }
        public string barcode_id { get; set; }
        public int available { get; set; }
        public int total { get; set; }
        public string issue { get; set; }
        public string due { get; set; }

    }
}
