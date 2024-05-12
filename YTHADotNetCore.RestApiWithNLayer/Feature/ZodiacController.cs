using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace YTHADotNetCore.RestApiWithNLayer.Feature
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZodiacController : ControllerBase
    {
        private async Task<Zodiac> GetDataAsync()
        {
            string json = await System.IO.File.ReadAllTextAsync("Zodiac.json");
            var model = JsonConvert.DeserializeObject<Zodiac>(json)!;
            return model;
        }

        [HttpGet("ZodiacSigns")]
        public async Task<IActionResult>GetZodiacSidnsAsync()
        {
           var model = await GetDataAsync();
            return Ok(model.ZodiacSignsDetail);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetZodiacSidnAnswerAsync(int id)
        {
            var model = await GetDataAsync();
            var result = model.ZodiacSignsDetail.FirstOrDefault(x => x.Id == id);
            return Ok(result);
        }

        [HttpGet("SearchByBirthDate/{birthday}")]
        public async Task<IActionResult> SearchByBirthDate(string birthday)
        {
            var model = await GetDataAsync();
            string[] birthDate = birthday.Split('-');
            string month = birthDate[1].ToString();
            int day = Convert.ToInt32(birthDate[0]);
            var zodiacId = GetZodiacSignId(month,day);
            var result = model.ZodiacSignsDetail.FirstOrDefault(x => x.Id == zodiacId);
            if(result is null)
            {
                return NotFound("Result is not found.");
            }
            return Ok(result);

        }

        [NonAction]
        private int GetZodiacSignId(string month, int day)
        {
            if(((month == "March") && (day >= 21 && day <=31)) || ((month == "April") &&  (day >= 01 && day <= 19)))
            {
                return 1;
            }
            else if (((month == "April") && (day >= 20 && day <= 30)) || ((month == "May") && (day >= 01 && day <= 20)))
            {
                return 2;
            }
            else if (((month == "May") && (day >= 21 && day <= 31)) || ((month == "June") && (day >= 01 && day <= 20)))
            {
                return 3;
            }
            else if (((month == "June") && (day >= 21 && day <= 30)) || ((month == "July") && (day >= 01 && day <= 22)))
            {
                return 4;
            }
            else if (((month == "July") && (day >= 23 && day <= 31)) || ((month == "August") && (day >= 01 && day <= 22)))
            {
                return 5;
            }
            else if (((month == "August") && (day >= 23 && day <= 31)) || ((month == "September") && (day >= 01 && day <= 22)))
            {
                return 6;
            }
            else if (((month == "September") && (day >= 23 && day <= 30)) || ((month == "October") && (day >= 01 && day <= 22)))
            {
                return 7;
            }
            else if (((month == "October") && (day >= 23 && day <= 31)) || ((month == "November") && (day >= 01 && day <= 21)))
            {
                return 8;
            }
            else if (((month == "November") && (day >= 22 && day <= 30)) || ((month == "December") && (day >= 01 && day <= 21)))
            {
                return 9;
            }
            else if (((month == "December") && (day >= 22 && day <= 31)) || ((month == "January") && (day >= 01 && day <= 19)))
            {
                return 10;
            }
            else if (((month == "January") && (day >= 20 && day <= 31)) || ((month == "February") && (day >= 01 && day <= 18)))
            {
                return 11;
            }
            else
            {
                return 12;
            }
            return 0;
        }
    }


    public class Zodiac
    {
        public Zodiacsignsdetail[] ZodiacSignsDetail { get; set; }
    }

    public class Zodiacsignsdetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MyanmarMonth { get; set; }
        public string ZodiacSignImageUrl { get; set; }
        public string ZodiacSign2ImageUrl { get; set; }
        public string Dates { get; set; }
        public string Element { get; set; }
        public string ElementImageUrl { get; set; }
        public string LifePurpose { get; set; }
        public string Loyal { get; set; }
        public string RepresentativeFlower { get; set; }
        public string Angry { get; set; }
        public string Character { get; set; }
        public string PrettyFeatures { get; set; }
        public Trait[] Traits { get; set; }
    }

    public class Trait
    {
        public string name { get; set; }
        public int percentage { get; set; }
    }

}
