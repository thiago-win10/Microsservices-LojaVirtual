using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VShopping.Web.Models;
using VShopping.Web.Services.Interfaces;

namespace VShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/products/";
        private readonly JsonSerializerOptions _options;
        private ProductViewModel productView;
        private IEnumerable<ProductViewModel> productViews;
        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            
                     
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string token)
        {
            var client = _clientFactory.CreateClient("ProductAPI");
            PutTokenAuthorizarion(token, client);

            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productViews = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return productViews;
        }

        public async Task<ProductViewModel> GetProductById(int id, string token)
        {

            var client = _clientFactory.CreateClient("ProductAPI");
            PutTokenAuthorizarion(token, client);

            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productView = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return productView;
        }

        public async Task<ProductViewModel> CreateProduct(ProductViewModel productView, string token)
        {

            var client = _clientFactory.CreateClient("ProductAPI");
            PutTokenAuthorizarion(token, client);

            StringContent content = new StringContent(JsonSerializer.Serialize(productView),
                                        Encoding.UTF8, "application/json");

          
            using (var response = await client.PostAsJsonAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productView = await JsonSerializer
                        .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
                
            }
            return productView;


        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productView, string token)
        {
            var client = _clientFactory.CreateClient("ProductAPI");
            PutTokenAuthorizarion(token, client);

            ProductViewModel productUpdate = new ProductViewModel();
            using (var response = await client.PutAsJsonAsync(apiEndpoint, productView))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productUpdate = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return productUpdate;

        }
        public async Task<bool> DeleteProductById(int id, string token)
        {
            var client = _clientFactory.CreateClient("ProductAPI");
            PutTokenAuthorizarion(token, client);

            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

            }
            return false;

        }
        private static void PutTokenAuthorizarion(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
