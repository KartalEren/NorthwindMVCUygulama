using NorthwindMVCUygulama.NorthwindDB;

namespace NorthwindMVCUygulama.SingletonDb
{
    public class SingletonContext
    {
        private static NorthwindContext _db;
       

        public SingletonContext()
        {
          
        }

        public static NorthwindContext Db               
        {
            get
            {
                if (_db == null)
                {
                    _db = new NorthwindContext();
                }
                return _db;
            }
        }


    }
}
