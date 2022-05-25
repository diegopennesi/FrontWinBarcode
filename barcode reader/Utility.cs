using MongoDB.Bson;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace barcode_reader
{
    public static class Utility
    {


        public static void initUtility()
        {
            if (!Directory.Exists(pathusers))
            {
                Directory.CreateDirectory(pathusers);
            }

        }
        public static void initUtility(string fileName)
        {
            if (!Directory.Exists(pathusers))
            {
                Directory.CreateDirectory(pathusers);
            }
            if (!File.Exists(pathusers+fileName))
            {
                File.Create(pathusers+fileName);
            }
        }

        private readonly static string pathusers = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))+"\\DLTESTAPP\\DATABSEMOCK";

        public static string ToString(Object obj)
        {
           return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        }
        public static string toJson(BsonDocument document)
        {   // from Bson to Json
            return document.ToJson(new JsonWriterSettings { Indent = true });
        }
        public static long getUnixTime()
        { // get unixTime NOW
           return getUnixTime(DateTime.Now); 
        }

        public static long getUnixTime(DateTime date)
        { // get unixTime from a Date
         return ((DateTimeOffset)date).ToUnixTimeMilliseconds();
        }


        //MOCK
        public static UserPojo getUserLogin(UserPojo userPojo)
        {
            // TBD la variabile dilogin attempt deve essere aggiornata ad ogni select nel DB reale!
            // per ora è FAKE
            UserPojo request=new UserPojo();
            List<UserPojo> userList = new List<UserPojo>();
            userList = JsonSerializer.Deserialize<List<UserPojo>>(System.IO.File.ReadAllText(pathusers+ "\\users.txt"));
            foreach(UserPojo x in userList)
            {
                if(x.username.Equals(userPojo.username) && x.Password.Equals(userPojo.Password)){
                  return userPojo = x; // CONVERTERPOJOFACTORY PER IL DB REALE! POJO!=MODEL
                }
            }
            return new UserPojo();
        }

        public static Boolean register(UserPojo obj)
        {
            List<UserPojo> userList = new List<UserPojo>();
            initUtility(@"\\users.txt");
            try
            {
                userList = JsonSerializer.Deserialize<List<UserPojo>>(System.IO.File.ReadAllText(pathusers + "\\users.txt"));
                foreach (UserPojo x in userList)
                {
                    if (x.username.Equals(obj.username))
                    {
                        return false;
                    }
                }
            } catch (Exception e)
            {

            }
            userList.Add(obj);
            System.IO.File.WriteAllText(pathusers+@"\users.txt", JsonSerializer.Serialize(userList));
            return true;
        }
        public static Boolean registerItem(PojoOggetto oggetto)
        {
            List < PojoOggetto > pojoOggettoList = new List<PojoOggetto>();
            try
            {
                pojoOggettoList = JsonSerializer.Deserialize<List<PojoOggetto>>(System.IO.File.ReadAllText(pathusers+"\\items.txt"));
            }
            catch (Exception e)
            {

            }
            pojoOggettoList.Add(oggetto);
            System.IO.File.WriteAllText(pathusers+@"\items.txt", JsonSerializer.Serialize(pojoOggettoList));
            return true;
        }
        public static List<PojoOggetto> getAllItemfromUser(UserPojo pojo)
        {
            List<PojoOggetto> response = new List<PojoOggetto>();
            initUtility(@"\\items.txt");
            try
            {
                response = JsonSerializer.Deserialize<List<PojoOggetto>>(System.IO.File.ReadAllText(pathusers+"\\items.txt"));
            foreach(PojoOggetto x in response)
            {
                if (!pojo.username.Equals(x.iseritoDa))
                {
                    response.Remove(x);
                }
            }
            }
            catch (Exception e)
            {

            }
            return response; 
        }
    }
}
