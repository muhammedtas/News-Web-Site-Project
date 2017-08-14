using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetim.DAL;
using Yonetim.Model.Entities;
using Yonetim.Model.ViewModels;

namespace Yonetim.BLL.Repository
{
  public  class Repository
    {
        public class HaberRepo : RepositoryBase<Haber, int> {
            public void Insert(HaberViewModel model)
            {
                MyContext db = new MyContext();
                using (db.Database.BeginTransaction())

                    //şimdiburaya bir transaction yapacağız. Neden, eğer bir hata olursa, eklenmiş haberi de geri alsın diye.
                {
                    try
                    {
                        var kategoriler = db.Kategoriler.Where(x => model.Kategoriler.Contains(x.Id)).ToList();
                        var yeniHaber = new Haber()
                        {
                            Baslik = model.Baslik,
                            Icerik = model.Icerik,
                            Keywords = model.Keywords

                        };
                        db.Haberler.Add(yeniHaber);
                        db.SaveChanges();
                        yeniHaber.Kategoriler.AddRange(kategoriler);
                        db.SaveChanges();
                        db.Database.CurrentTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        db.Database.CurrentTransaction.Rollback();
                        
                    }
                }
            }

        }
        public class KategoriRepo : RepositoryBase<Kategori, int>
        {
         
        }
    }
}
