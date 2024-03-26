using ElasticSearch.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly ECommerceRepository _repository;

        public ECommerceController(ECommerceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> TermQuery(string customerFirstName)
        {
            return Ok(await _repository.TermQuery(customerFirstName));
        }

        [HttpPost]
        public async Task<IActionResult> TermsQuery(List<string> customerFirstNameList)
        {
            return Ok(await _repository.TermsQuery(customerFirstNameList));
        }

        [HttpGet]
        public async Task<IActionResult> PrefixQuery(string customerFullName = "Abi")
        {
            return Ok(await _repository.PrefixQuery(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> RangeQuery(double fromPrice, double toPrice)
        {
            return Ok(await _repository.RangeQuery(fromPrice, toPrice));
        }

        [HttpGet]
        public async Task<IActionResult> MatchAll()
        {
            return Ok(await _repository.MatchAll());
        }

        [HttpGet]
        public async Task<IActionResult> PaginationQuery(int page = 1, int pageSize = 3)
        {
            return Ok(await _repository.PaginationQuery(page, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> WildcardQuery(string customerFullName = "Eddi*")
        {
            return Ok(await _repository.WildcardQuery(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> FuzzyQuery(string customerName = "Edie") //yazım yanlışlarını tolere eder
        {
            return Ok(await _repository.FuzzyQuery(customerName));
        }


        [HttpGet]
        public async Task<IActionResult> MatchQueryFullText(string categoryName)
        {
            return Ok(await _repository.MatchQueryFullText(categoryName));
        }

        [HttpGet]
        public async Task<IActionResult> MatchBoolPrefixFullText(string customerFullName)
        {
            return Ok(await _repository.MatchBoolPrefixFullText(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> MatchPhraseFullText(string customerFullName)
        {
            return Ok(await _repository.MatchPhraseFullText(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> CompoundQueryExampleOne(string cityName = "New York", double taxFulTotalPrice = 100, string categoryName = "Women's Clothing", string manufactorer = "Tigress Enterprises")
        {
            return Ok(await _repository.CompoundQueryExampleOne(cityName, taxFulTotalPrice, categoryName, manufactorer));
        }

        [HttpGet]
        public async Task<IActionResult> CompoundQueryExampleTwo(string customerFullName)
        {
            return Ok(await _repository.CompoundQueryExampleTwo(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> MultiMatchQueryFullText(string name)
        {
            return Ok(await _repository.MultiMatchQueryFullText(name));
        }
    }
}
