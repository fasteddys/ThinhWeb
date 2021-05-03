using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
    public class BaseController : Controller
    {
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<string> UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            //your custom code logic here

            //1)check if the file is image

            //2)check if the file is too large

            //etc

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            var directory = "wwwroot/CKEditorImages";
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            //save file under wwwroot/CKEditorImages folder

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), directory,
                fileName);



            using (var stream = System.IO.File.Create(filePath))
            {
                await upload.CopyToAsync(stream);
            }

            var url = $"{"/CKEditorImages/"}{fileName}";

            var successMessage = "image is uploaded";

            dynamic success = Newtonsoft.Json.JsonConvert.DeserializeObject("{ 'uploaded': 1,'fileName': \"" + fileName + "\",'url': \"" + url + "\", 'error': { 'message': \"" + successMessage + "\"}}");
            var json = JsonConvert.SerializeObject(success);
            return json;
        }
    }
}
