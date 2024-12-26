namespace Eshop.Models
{
    public class Cart
    {
       
            public class CartLine

            {

                public int CartLineID { get; set; }

                public Product Product { get; set; } = new();

                public int Quantity { get; set; }

            }
            public List<CartLine> Lines { get; set; } = new List<CartLine>();

            public void AddItem(Product product, int quantity)
            {

                CartLine? line = Lines

                .Where(p => p.Product.ProductId == product.ProductId)

    .FirstOrDefault();

                if (line == null)
                {

                    Lines.Add(new CartLine
                    {

                        Product = product,

                        Quantity = quantity

                    });

                }
                else
                {

                    line.Quantity += quantity;

                }

            }

            public void RemoveLine(Product product) =>   Lines.RemoveAll(l => l.Product.ProductId == product.ProductId);

        public decimal ComputeTotalValues() => (decimal)Lines.Sum(e => e.Product.ProductPrice * e.Quantity);
        public void Clear()=>Lines.Clear();
    }


    }

