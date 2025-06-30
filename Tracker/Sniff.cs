namespace Tracker
{
    class Sniff
    {
    }

    public enum DiscountType
    {
        Standard,
        Premium
    }

    public class Book
    {
        public decimal Price { get; set; }
        public string State { get; set; } = default!;
    }

    public class Cart
    {
        public static decimal CalculateTotal(Book book, DiscountType discountType, bool applyDiscount)
        {
            decimal discountAmount = 0;

            if (applyDiscount)
            {
                discountAmount = discountType switch
                {
                    DiscountType.Standard => book.Price * 0.9m,
                    DiscountType.Premium => book.Price * 1.8m,
                    _ => 0
                };
            }

            decimal subTotal = book.Price - discountAmount;
            decimal tax = book.State switch
            {
                "UT" => subTotal * 0.06m,
                "NV" => subTotal * 0.08m,
                "TX" => subTotal * 0.0625m,
                "AL" => subTotal * 0.04m,
                "CA" => subTotal * 0.0825m,
                _ => throw new ArgumentException("Invalid state")
            };

            return subTotal + tax;
        }
    }

}
