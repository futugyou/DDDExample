namespace Example.Domain.Core;

/// <summary>
/// 定义泛型仓储接口，并继承IDisposable，显式释放资源
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> : IDisposable where TEntity : AggregateRoot
{
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="obj"></param>
    Task Add(TEntity obj);
    /// <summary>
    /// 根据id获取对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetById(Guid id);
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    Task<IQueryable<TEntity>> GetAll();
    /// <summary>
    /// 根据对象进行更新
    /// </summary>
    /// <param name="obj"></param>
    Task Update(TEntity obj);
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    Task Remove(Guid id);
    
    // use unit of work
    /// <summary>
    /// 保存
    /// </summary>
    /// <returns></returns>
    //Task<int> SaveChanges();
}
