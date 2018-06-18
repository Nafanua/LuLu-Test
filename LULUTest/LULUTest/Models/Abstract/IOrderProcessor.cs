using System.Threading.Tasks;

namespace LULUTest.Models.Abstract
{
    public interface IOrderProcessor
    {
        Task<bool> ProcessOrder(Cart cart, OrderViewModel order);
    }
}