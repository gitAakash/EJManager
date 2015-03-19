using System;
using System.IO;
using System.Web.Mvc;
using EJournalManager.Entity;

namespace EJournalManager.Controllers
{
    public class SynchronizerController : Controller
    {
        //
        // GET: /Synchronizer/
        public ActionResult NewTask()
        {
            return View();
        }

        public bool SyncTask(TaskModel taskModel, bool recCall = false)
        {
            try
            {
                if (!recCall)
                {
                    taskModel.Source = @"\\PBT-R3P11\AakashFiles";
                    taskModel.Destination = @"\\10.0.30.108\NitinFileB";
                }

                if (taskModel.Destination[taskModel.Destination.Length - 1] != Path.DirectorySeparatorChar)
                    taskModel.Destination += Path.DirectorySeparatorChar;
                if (!Directory.Exists(taskModel.Destination)) Directory.CreateDirectory(taskModel.Destination);
                string[] files = Directory.GetFileSystemEntries(taskModel.Source);
                foreach (string element in files)
                {
                    // Sub directories
                    if (Directory.Exists(element))
                    {
                        SyncTask(new TaskModel
                        {
                            Source = element,
                            Destination = taskModel.Destination + Path.GetFileName(element)
                        }, true);
                    }
                    else
                    {
                        // Files in directory
                        System.IO.File.Copy(element, taskModel.Destination + Path.GetFileName(element), true);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }
    }
}