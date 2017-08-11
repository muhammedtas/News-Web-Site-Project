using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetim.DAL;

namespace Yonetim.BLL.Repository
{
    public abstract class RepositoryBase<T, ID> where T : class // RepositoryBase yi eski uygulamalardan aldık. Nede olsa evladiyelik bir kardeş.  T bir class olsun. 

        // buraya yazılacak T tipinin DBSet dipinde olmalı. 


        // public abstract yaptık ve jenerik yaptık. Neden public abstract yaptık? Base class olduğu için. Tablo tipi ve id kolonları için burada bir jenerik de oluşturduk. Bir şart ta koyduk t için
    {
        protected internal static MyContext dbContext; // ramda de her zaman hazır olsun.Protected internal public bir nesne, UI tarafında da kalıtım alarak kullanabiliriz. AMa kalıtım alırsak context de gelir. Protected internal yaparsak bll içinde kalıtım alınan nesnelerin içinde gelebilir. Öyle her yerden kalıtım alındığında context gelmesin deyü. 

        // şimdi standart 5 tane işlemimiz var. Bunları yapalım.  

        public virtual List<T> GetAll()// yani eğer ürünler tablosunda çalıştıysak ürünler.getall deyince alayı gelecek. 
        {
            dbContext = new MyContext();
            /* return dbContext.Urunler.ToList();*/ // şimdi bunun da jenerik olması lazım. Yukarıya T dediysek bu da T olmalı. Set metodu içine vermiş olduğunuz tipin tablosunu döndürür. 

            return dbContext.Set<T>().ToList(); // id verirsen de ürünün kendisi gelir. Onu da yazalım.

        }
        public virtual T GetByID(ID id) // ne tipinde bir id dönecek. Evladiyelik olsun. // yukarıdaki En başta class isminde tanımladığımız ID tipinde olsun.
        {

            dbContext = new MyContext();
            return dbContext.Set<T>().Find(id);

        }

        public virtual void Insert(T entity)
        {
            // dışarıdan bir insert var. try cartch yapalım

            try
            {

                dbContext = dbContext ?? new MyContext(); // dbcontext null değilse dbcontext i kullan, değilse new le instance al.
                dbContext.Set<T>().Add(entity); // şimdi dbcontext imiz ne tipindeyse o şekilde çağırıp kullanacak. Tutupta yerine kategori ya da ürün diye ayrı ayrı yazmıyoruz. 
                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public virtual void Delete(T entity)
        {
            // dışarıdan bir insert var. try cartch yapalım

            try
            {

                dbContext = dbContext ?? new MyContext(); // dbcontext null değilse dbcontext i kullan, değilse new le instance al.
                dbContext.Set<T>().Remove(entity); // şimdi dbcontext imiz ne tipindeyse o şekilde çağırıp kullanacak. Tutupta yerine kategori ya da ürün diye ayrı ayrı yazmıyoruz. 
                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public virtual void Update()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
