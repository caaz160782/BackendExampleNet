namespace Backend.Repository
{
    public interface IRepository<IEntity>
    {
        Task<IEnumerable<IEntity>> Get();
        Task<IEntity> GetbyId(int id);
        Task Add(IEntity entity);
        void Update(IEntity entity);
        void Delete(IEntity entity);
        Task Save();
        IEnumerable<IEntity> Search(Func<IEntity, bool> filter);
    }
}
