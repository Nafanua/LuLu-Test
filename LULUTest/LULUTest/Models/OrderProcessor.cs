using LULUTest.Models.Abstract;
using LULUTest.Util.Abstract;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LULUTest.Models
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IEmailService _emailService;
        private readonly IBookReposytory _repo;

        public OrderProcessor(IEmailService emailService, IBookReposytory repo)
        {
            _emailService = emailService;
            _repo = repo;
        }

        public async Task<bool> ProcessOrder(Cart cart, OrderViewModel order)
        {
            StringBuilder body = new StringBuilder()
            .AppendLine("Новый заказ обработан")
            .AppendLine()
            .AppendLine("Товары:");

            foreach (var line in cart.Lines)
            {
                var subTotal = line.Book.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (итого: {2})", line.Quantity, line.Book.Name, subTotal);
            }
            body.AppendLine()
                .AppendFormat("Общаяя стоимость: {0}", cart.ComputeTotalValue())
                .AppendLine()
                .AppendLine("Доставка:")
                .AppendLine("Имя:" + order.Name)
                .AppendLine("Адрес: " + order.Adress)
                .AppendLine("Город: " + order.City)
                .AppendLine("Индекс: " + order.PostalCode);

            var subject = "Order N:";

            try
            {
                foreach (var line in cart.Lines)
                {
                    var book = await _repo.GetBookByIdAsync(line.Book.Id);

                    if (book != null)
                    {
                        if (book.Quantity > line.Quantity)
                        {
                            book.Quantity -= line.Quantity;

                            await _repo.EditBookAsync(book);
                        }
                        else if (book.Quantity < line.Quantity)
                        {
                            throw new Exception("Количество книг в заказе превышает количество книг на складе");
                        }
                        else
                        {
                            await _repo.DeleteBookAsync(book.Id);
                        }
                    }
                }

                await _emailService.SendEmailAsync(order.Name, order.Email, subject, body.ToString(), false);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
    }
}