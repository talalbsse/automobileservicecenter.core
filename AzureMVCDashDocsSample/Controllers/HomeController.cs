using AzureMVCDashDocsSample.Models;
using AzureMVCDashDocsSample.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AzureMVCDashDocsSample.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            Guid customerId = Guid.Parse("48CC71AE-22E9-4CA3-96AD-5D53D38215AE");
            var dbContext = new DashDocsContext();
            var documents = from document in dbContext.Documents
                            join user in dbContext.Users on document.OwnerId
                            equals user.Id
                            where user.CustomerId == customerId
                            select document;
            return View(documents.Include(d => d.Owner).ToList());
        }

        public async Task<ActionResult> Upload(HttpPostedFileBase document)
        {
            // Ids used in the seed method
            Guid customerId = Guid.Parse("48CC71AE-22E9-4CA3-96AD-5D53D38215AE");
            Guid userId = Guid.Parse("AD3C9066-A800-41A8-9C8D-A06ABB72AE2B");
            var blobStorageService = new BlobStorageService();
            var documentId = Guid.NewGuid();
            var path = await blobStorageService.
            UploadDocumentAsync(document, customerId, documentId);
            var dbContext = new DashDocsContext();
            dbContext.Documents.Add(new Document
            {
                Id = documentId,
                DocumentName = Path.GetFileName(document.FileName).
            ToLower(),
                OwnerId = userId,
                CreatedOn = DateTime.UtcNow,
                BlobPath = path
            });
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<FileResult> Download(Guid documentId)
        {
            var dbContext = new DashDocsContext();

            var document = await dbContext.Documents.SingleAsync(d => d.Id == documentId);
            var blobStorageService = new BlobStorageService();
            var content = await blobStorageService.DownloadDocumentAsync(documentId, Guid.Parse("48CC71AE-22E9-4CA3-96AD-5D53D38215AE"));
            content.Value.Position = 0;
            return File(content.Value, System.Net.Mime.MediaTypeNames.
            Application.Octet, content.Key);
        }
    }
}
