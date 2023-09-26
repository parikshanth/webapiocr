using Microsoft.AspNetCore.Mvc;
using Tesseract;

namespace dotnetocr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OcrController : Controller
    {
        private TesseractEngine tesseractEngine;
        private ILogger logger;
        public OcrController(TesseractEngine tesseractEngine, ILogger logger)
        {
            this.tesseractEngine = tesseractEngine;
            this.logger = logger;
        }


        [HttpPost(Name = "GetTextFromImage")]
        public string Get()
        {
            try
            {
                var file = Request.Form.Files[0];
                byte[] data;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    data = ms.ToArray();

                }

                using var pix = Pix.LoadFromMemory(data);
                using var page = tesseractEngine.Process(pix);
                return page.GetText();

            }
            catch (Exception ex)
            {
                logger.LogError("error in  GetTextFromImage ", ex);
                return "Error processing";
            }


        }
    }
}
