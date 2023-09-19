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
            //var test = _unitOfWork.HistoricRepository<AuthorHistoricV1>().HistoricAllTime();   
        }
    }
}