using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity,new()
    {
        private readonly BlogContext _db;

        public BaseService(BlogContext db)
        {
            _db = db;
        }


        public async Task CreateAsync(T model, bool saved = true)
        {
            _db.Set<T>().Add(model);
            if (saved) await _db.SaveChangesAsync();
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            _db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
           await  RemoveAsync(model.Id, saved);
            
        }

        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T() { Id=id};
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsRemove = true;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task Save()
        {
             await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }
        public async Task<T> GetOneByIdAsync(Guid id)
        {
           return await GetAll().FirstAsync(predicate:m=>m.Id==id);
        }


        /// <summary>
        /// 返回所有未被删除的数据（没有真的执行）
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().Where(m => !m.IsRemove).AsNoTracking();
        }

        public IQueryable<T> GetAllByOrder(bool asc = true)
        {
            var datas = GetAll();
            return datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
        }

        public IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0)
        {
           return GetAll().Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return GetAllByOrder(asc).Skip(pageSize * pageIndex).Take(pageSize);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
