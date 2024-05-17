using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace YTHADotNetCore.RestApiWithNLayer.Feature
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarProverbsController : ControllerBase
    {
        private async Task<Mmproverbs> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("MyanmarProverbs.json");
            var model = JsonConvert.DeserializeObject<Mmproverbs>(jsonStr);
            return model;
        }

        [HttpGet]
        public async Task<IActionResult>GetMyanmarProverbs()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MMProverbsTitle);
        }

        [HttpGet("{titleName}")]
        public async Task<IActionResult>GetMyanmarProverbsTitle(string titleName)
        {
            var model = await GetDataAsync();
            var item = model.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == titleName);
            if (item is null) return NotFound();
            var titleId = item.TitleId;
            var result = model.Tbl_MMProverbs.Where(x => x.TitleId == titleId);

            List<Tbl_MmproverbsHead> lst = result.Select(x => new Tbl_MmproverbsHead
            {
                ProverbId = x.ProverbId,
                TitleId = x.TitleId,
                ProverbName = x.ProverbName,
            }).ToList();
            
            return Ok(lst);
        }

        [HttpGet("{titleId}/{proverbId}")]
        public async Task<IActionResult> GetMyanmarProverbsDetail(int titleId, int proverbId)
        {
            var model = await GetDataAsync();
            var item = model.Tbl_MMProverbs.FirstOrDefault(x => x.ProverbId == proverbId && x.TitleId == titleId);
            return Ok(item);
        }
    }


    public class Mmproverbs
    {
        public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
        public Tbl_MmproverbsDetail[] Tbl_MMProverbs { get; set; }
    }

    public class Tbl_Mmproverbstitle
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class Tbl_MmproverbsHead
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
    }

    public class Tbl_MmproverbsDetail
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
        public string ProverbDesp { get; set; }
    }

}
