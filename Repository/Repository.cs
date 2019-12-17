using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Repository.Contex;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        public void Dispose()
        {
            ctx.Dispose();
        }

        BaseContext ctx = new BaseContext();

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return ctx.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar, erro = " + ex.InnerException.Message);
            }
        }

        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            try
            {
                return GetAll().Where(predicate).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar, erro = " + ex.InnerException.Message);
            }
        }

        public int GetNumberPages(int pageSize)
        {
            try
            {
                var count = ctx.Set<TEntity>().Count();

                var numberPages = count / pageSize;

                if(count % pageSize != 0)
                {
                    numberPages += 1;
                }

                return numberPages;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar, erro = " + ex.InnerException.Message);
            }
        }

        public TEntity Find(params object[] key)
        {
            try
            {
                var find = ctx.Set<TEntity>().Find(key);
                Detached(find);
                return find;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar, erro = " + ex.InnerException.Message);
            }
        }

        public void Detached(TEntity obj)
        {
            ctx.Entry(obj).State = EntityState.Detached;
        }

        public void Update(TEntity obj)
        {
            try
            {
                ctx.Entry(obj).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro a atualizar, erro = " + ex.InnerException.Message);
            }
        }

        public virtual void Save(TEntity obj)
        {
            try
            {
                ctx.Set<TEntity>().Add(obj);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar, erro = " + ex.InnerException.Message);
            }
        }

        public async Task SaveAsync(TEntity obj)
        {
            try
            {
                ctx.Set<TEntity>().Add(obj);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar, erro = " + ex.InnerException.Message);
            }
        }

        public void SaveAll()
        {            
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar, erro = " + ex.InnerException.Message);
            }
        }

        public void Add(TEntity obj)
        {
            try
            {
                ctx.Set<TEntity>().Add(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar, erro = " + ex.InnerException.Message);
            }
        }

        public void Delete(Func<TEntity, bool> predicate)
        {
            try
            {
                ctx.Set<TEntity>()
                .Where(predicate).ToList()
                .ForEach(del => ctx.Set<TEntity>().Remove(del));

                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar, erro = " + ex.InnerException.Message);
            }
        }

        //Não valida DateTime
        public void ValidateObject(TEntity obj)
        {
            var resultadoValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, contexto, resultadoValidacao, true);

            if (resultadoValidacao.Count != 0)
            {
                var erros = "";
                foreach (var erro in resultadoValidacao)
                {
                    erros += erro.ErrorMessage;

                    if(resultadoValidacao.Count > 1)
                    {
                        erros += " / ";
                    }
                }

                throw new Exception(erros);
            }
        }
    }
}