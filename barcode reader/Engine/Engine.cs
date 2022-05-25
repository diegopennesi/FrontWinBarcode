using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barcode_reader.Engine
{
    public sealed class Engine
    {
        private MongoClient Client  = new MongoClient("mongodb+srv://admin:admin@cluster0.j5w1g.mongodb.net/?retryWrites=true&w=majority");
        private string database { get; set; }
        private string collection { get; set; }

       private static readonly Engine instance = new Engine();
        static Engine() { }
        private Engine() { }
        public static Engine Instance    
    {    
        get    
        {    
            return instance;    
        }    
    }    

        public MongoClient GetClient()
        {
            return Client;
        }
    }
}
