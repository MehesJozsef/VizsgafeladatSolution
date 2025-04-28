using CommonLibrary.MODEL;

namespace Vizsgafeladat.VIEWMODEL
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int? Quantity { get; set; }

        /// <summary>
        /// A kosárban rögzített ár (Ft).
        /// Fontos: ez nem változik, ha a termék ára időközben változik!
        /// </summary>
        public int? Price { get; set; }
    }
}
