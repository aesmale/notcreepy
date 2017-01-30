using notcreepy.Models;
using System.Collections.Generic;
namespace notcreepy.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}