using System.Net.Http.Headers;
using System.Text.Json;
using VShopping.Web.Models;
using VShopping.Web.Services.Interfaces;

namespace VShopping.Web.Services
{
    public class CategoryService : ICategoryService 
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/categories/";
        private readonly JsonSerializerOptions _options;
        public CategoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategory(string token)
        {
            var client = _clientFactory.CreateClient("ProductAPI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            IEnumerable<CategoryViewModel> categories;
       
            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categories = await JsonSerializer.DeserializeAsync<ICollection<CategoryViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categories;
        }

    }
}
