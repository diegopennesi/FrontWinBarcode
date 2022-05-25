using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barcode_reader.Engine
{
    public class Dao
    {
        Engine engine = Engine.Instance;

        public String getFirstDocumentFrom(String DatabaseName, String collecionName)
        {
            var dat = engine.GetClient().GetDatabase(DatabaseName);
            var collection = dat.GetCollection<BsonDocument>(collecionName);
            var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
            return Utility.toJson(firstDocument);
        }
    }
}
