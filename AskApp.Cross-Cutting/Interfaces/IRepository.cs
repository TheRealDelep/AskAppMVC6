using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Cross_Cutting
{
    public interface IRepository<TEntity>
    {
        List<TEntity> GetAll();

        TEntity GetById(int Id);

        TEntity Insert(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);
    }
}