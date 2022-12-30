using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer.Native.Services;
using Microsoft.AspNetCore.Mvc;

namespace EliteOrderApp.Web.Controllers
{
    public class CustomWebDocumentController : WebDocumentViewerController
    {
        public
            CustomWebDocumentController(IWebDocumentViewerMvcControllerService controllerService) : base(controllerService)
        {

        }
    }
}
