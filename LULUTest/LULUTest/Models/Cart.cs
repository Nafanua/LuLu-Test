using System.Collections.Generic;
using System.Linq;

namespace LULUTest.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddLine(BookModel book, int quantity)
        {
            CartLine line = lineCollection.Where(b => b.Book.Id == book.Id).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Book = book, Quantity = quantity });
            }
            else if (book.Quantity == line.Quantity)
            {
                return;
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveBook(BookModel book)
        {
            var line = lineCollection.Find(i => i.Book.Id == book.Id);

            if (line.Quantity > 1)
            {
                line.Quantity--;
            }
            else
            {
                lineCollection.Remove(line);
            }
        }

        public void RemoveLine(BookModel book)
        {
            lineCollection.RemoveAll(i => i.Book.Id == book.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(b => b.Book.Price * b.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public BookModel Book { get; set; }

        public int Quantity { get; set; }
    }
}