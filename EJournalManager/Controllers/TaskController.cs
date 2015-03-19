using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EJournalManager.Data;
using EJournalManager.Entity;
using iTextSharp.text;

namespace EJournalManager.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/
        [HttpGet]
        public ActionResult NewTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewTask(/*HttpPostedFileWrapper source, HttpPostedFileWrapper destination,*/ TaskModel taskModel)
        {

            var objDbTasks = new DbTasks();
            bool status = Convert.ToBoolean(objDbTasks.InsertTask(taskModel));
            /*string sourceFolder = "F:/" + source.FileName;
            string destinationFolder ="F:/" + destination.FileName; */
            /*string FsourceFolder = Path.GetFullPath(source.FileName);
            string FdestinationFolder = Path.GetFullPath(destination.FileName);*/

            /* string sourceDir = sourceFolder.Substring(0, sourceFolder.LastIndexOf("/", StringComparison.Ordinal));
             var targetDir = destinationFolder.Substring(0, destinationFolder.LastIndexOf("/", StringComparison.Ordinal));
             var logic = new SyncSharpLogic();
             String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
             root = root.Substring(0, 1);
             SyncTask syncTask = new SyncTask(taskModel.TaskName, sourceDir, targetDir, true,sourceDir.StartsWith(root), targetDir.StartsWith(root),
                                             new TaskSettings(), new Filters());

             var machineId= logic.GetMachineID();
             var profile = new SyncProfile(machineId);
             profile.AddTask(syncTask);*/
            //logic.ImportProfile(taskName);
            //logic.Profile.AddTask(syncTask);
            /* logic.LoadProfile();
             //if (!logic.SaveProfile()) return View();
             logic.UpdateRemovableRoot();
             bool runAutoForm = false;
             if (logic.Profile.TaskCollection != null)
             {
                 foreach (SyncTask task in logic.Profile.TaskCollection)
                 {
                     runAutoForm = task.Settings.PlugSync;
                     if (runAutoForm) break;
                 }
             }
             logic.RemoveAllUIs();
             _logicController = logic;
             logic.Profile.AddTask(syncTask);
             SyncTask selTask = _logicController.Profile.GetTask(taskModel.TaskName);*/

            /*  if (Directory.Exists(sourceFolder.Substring(0, sourceFolder.LastIndexOf("/", StringComparison.Ordinal))) &&
                   Directory.Exists(destinationFolder.Substring(0,destinationFolder.LastIndexOf("/", StringComparison.Ordinal))))
              {
                  _syncCaller.BeginInvoke(selTask, false, SyncCompleted, _syncCaller);
              }*/
            if (status)
                return RedirectToAction("GetAllTasks");
            return View();
        }

        public ActionResult GetAllTasks()
        {
            DbTasks objDbTasks = new DbTasks();
            List<TaskModel> taskList = new List<TaskModel>();
            taskList = objDbTasks.GetAllTasks();
            /*string sourceFolder = "F:/" + source.FileName;
            string destinationFolder ="F:/" + destination.FileName; */
            /*string FsourceFolder = Path.GetFullPath(source.FileName);
            string FdestinationFolder = Path.GetFullPath(destination.FileName);*/

            /* string sourceDir = sourceFolder.Substring(0, sourceFolder.LastIndexOf("/", StringComparison.Ordinal));
             var targetDir = destinationFolder.Substring(0, destinationFolder.LastIndexOf("/", StringComparison.Ordinal));
             var logic = new SyncSharpLogic();
             String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
             root = root.Substring(0, 1);
             SyncTask syncTask = new SyncTask(taskModel.TaskName, sourceDir, targetDir, true,sourceDir.StartsWith(root), targetDir.StartsWith(root),
                                             new TaskSettings(), new Filters());

             var machineId= logic.GetMachineID();
             var profile = new SyncProfile(machineId);
             profile.AddTask(syncTask);*/
            //logic.ImportProfile(taskName);
            //logic.Profile.AddTask(syncTask);
            /* logic.LoadProfile();
             //if (!logic.SaveProfile()) return View();
             logic.UpdateRemovableRoot();
             bool runAutoForm = false;
             if (logic.Profile.TaskCollection != null)
             {
                 foreach (SyncTask task in logic.Profile.TaskCollection)
                 {
                     runAutoForm = task.Settings.PlugSync;
                     if (runAutoForm) break;
                 }
             }
             logic.RemoveAllUIs();
             _logicController = logic;
             logic.Profile.AddTask(syncTask);
             SyncTask selTask = _logicController.Profile.GetTask(taskModel.TaskName);*/

            /*  if (Directory.Exists(sourceFolder.Substring(0, sourceFolder.LastIndexOf("/", StringComparison.Ordinal))) &&
                   Directory.Exists(destinationFolder.Substring(0,destinationFolder.LastIndexOf("/", StringComparison.Ordinal))))
              {
                  _syncCaller.BeginInvoke(selTask, false, SyncCompleted, _syncCaller);
              }*/
            if (taskList.Any())
                return View(taskList);
            return null;
        }
	}
}