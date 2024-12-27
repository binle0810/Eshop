namespace Eshop.Models.ViewModel
{
    public class ProductListViewmodel
    {
        public IEnumerable<Product> Products { get; set; }=Enumerable.Empty<Product>();
        public PagingInfo PagingInfo { get; set; }=new PagingInfo();
        public string keywork;
    }
}
