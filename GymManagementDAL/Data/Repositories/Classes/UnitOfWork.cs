using GymManagementDAL.Data.Context;
using GymManagementDAL.Data.Repositories.Interfaces;
using GymManagementDAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly Dictionary<Type , object> _repositries= new ();
        private readonly GymDbContext _dbContext;

        public UnitOfWork(GymDbContext dbContext , ISessionRepository sessionRepository)
        {
            _dbContext = dbContext;
            this.sessionRepository = sessionRepository;
        }

        public ISessionRepository sessionRepository { get; }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var TypeEntity = typeof(TEntity);
            if (_repositries.TryGetValue(TypeEntity, out var Repo))
                return (IGenericRepository<TEntity>)Repo;

            var newRepo = new GenericRepository<TEntity>(_dbContext);
            _repositries.Add(TypeEntity, newRepo);
            return newRepo;

        }

        public int SaveChanges()
        {
           return _dbContext.SaveChanges();
        }
    }
}
