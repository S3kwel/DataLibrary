using DATA.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spike.Models;

namespace Spike.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUnitOfWork<SpikeContext> _unitOfWork;
        public IndexModel(ILogger<IndexModel> logger, IUnitOfWork<SpikeContext> unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            //TODO:  Option / Flag to just export results without all the metadata?  
            //Global success indicator is not working correctly.  
            var test = _unitOfWork.Repository<AuthorV1>().GetAll();

            Console.Write("aaaa"); 
        }
    }
}