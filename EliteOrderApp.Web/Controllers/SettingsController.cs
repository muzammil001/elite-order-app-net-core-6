using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Configs;
using Microsoft.AspNetCore.Identity;

namespace EliteOrderApp.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly BackupService _backupService;
        public SettingsController(BackupService backupService)
        {
            _backupService = backupService;
        }

        public IActionResult Index()
        {
            return View("AllSettings");
        }

        [HttpPost]
        public IActionResult BackUpData()
        {
            _backupService.BackupDatabase(AppConfig.DatabaseName);
            return Json("Backed Up");
        }

        [HttpPost]
        public IActionResult RestoreData()
        {
            _backupService.RestoreDatabase(AppConfig.DatabaseName);
            return Json("Restored Data");
        }
    }
   
}
