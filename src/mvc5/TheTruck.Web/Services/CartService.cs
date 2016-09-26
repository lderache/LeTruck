using System.Collections.Generic;
using System.Web.SessionState;

namespace TheTruck.Web.Services
{
    public class CartService
    {
        private readonly string _sessionKey = "products";
        private HttpSessionState _session;

        public CartService(HttpSessionState session)
        {
            this._session = session;
        }

        public List<int> GetProducts()
        {
            var products = _session[_sessionKey] as List<int>;
            if (products == null)
                return new List<int>();
            else
                return products;
        }

        public void AddProduct(int product)
        {
            var products = _session[_sessionKey] as List<int>;
            if (products == null)
            {
                products = new List<int>();
            }

            products.Add(product);
            _session[_sessionKey] = products;
        }

        public void RemoveProduct(int product)
        {
            var products = _session[_sessionKey] as List<int>;
            if (products != null)
            {
                products.Remove(product);
                _session[_sessionKey] = products;
            }
        }
    }
}