using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using JarvisEdge.ServiceInterfaces;
using JarvisEdge.DataTransferModels.Photo;

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class PhotoController : Controller
    {
        private IFileService fileService;

        public PhotoController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto([FromForm]PhotoPostModel model, int propertyId)
        {
            var result = await fileService.PostImagesForProperty(propertyId, model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        public IActionResult DeletePhoto(int id )
        {
            var result = fileService.DeletePhoto(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        public IActionResult ModifyPhoto(int id, int orderNumber)
        {
            var result = fileService.ModifyPhoto(id, orderNumber);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

    }
}