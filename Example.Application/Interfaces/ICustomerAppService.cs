using System;
using System.Collections.Generic;
using Example.Application.ViewModels;

namespace Example.Application.Interfaces
{
    /// <summary>
    /// 定义 ICustomerAppService 服务接口
    /// 并继承IDisposable，显式释放资源
    /// 注意这里我们使用的对象，是视图对象模型
    /// </summary>
    public interface ICustomerAppService : IDisposable
    {
        Task Register(CustomerViewModel customerViewModel);
        Task<IEnumerable<CustomerViewModel>> GetAll();
        Task<CustomerViewModel> GetById(Guid id);
        Task Update(CustomerViewModel customerViewModel);
        Task Remove(Guid id);
    }
}
