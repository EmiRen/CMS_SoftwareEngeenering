using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CMS4.Models;

namespace CMSWebsite.Controllers
{
    [Authorize]
    public class FileUploadController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home
        [HttpGet]
        public ActionResult UploadedImage()
        {
            return View();
        }

        // GET: Home
        [HttpGet]
        public ActionResult DirectoryList()
        {

            ArrayList fileList = new ArrayList();
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/"));
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension != ".mdb")
                {
                    fileList.Add(file.Name);
                }
            }
            ViewBag.fileList = fileList;
            return View();
        }

        // GET: Home
        [HttpGet]
        public ActionResult CreateFile()
        {


            string filePath = Server.MapPath("~/" + "/newFile.txt");
            string ErrorMessage;
            try
            {
                StreamWriter file = System.IO.File.CreateText(filePath);

                for (int i = 1; i <= 4; i++)
                {
                    file.WriteLine("This is text line " + i);
                }
                file.WriteLine("The Date is " + DateTime.Now);
                file.Close();
                ErrorMessage = "File Written";
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            ViewBag.Message = ErrorMessage;
            return View();
        }


        // GET: Home
        [HttpGet]
        public ActionResult CopyFile()
        {


            string fromFilePath = Server.MapPath("~/" + "/newFile.txt");
            string toFilePath = Server.MapPath("~/" + "/newnewFile.txt");

            string ErrorMessage;

            try
            {
                System.IO.File.Copy(fromFilePath, toFilePath);
                ErrorMessage = "File Copied";
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            ViewBag.Message = ErrorMessage;
            return View();
        }

        // GET: Home
        [HttpGet]
        public ActionResult DeleteFile()
        {
            string filePath = Server.MapPath("~/" + "/newnewFile.txt");

            string ErrorMessage;

            try
            {
                System.IO.File.Delete(filePath);
                ErrorMessage = "File Deleted";
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            ViewBag.Message = ErrorMessage;
            return View();
        }

        // GET: Home
        [HttpGet]
        public ActionResult DirectoryListLink()
        {

            ArrayList fileList = new ArrayList();
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Uploads/"));
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension != ".mdb")
                {
                    fileList.Add(file.Name);
                }
            }
            ViewBag.fileList = fileList;
            return View();
        }

        // GET: Home
        [HttpGet]
        public ActionResult ListFile(string FileName)
        {
            string filePath = Server.MapPath("~/Uploads/" + FileName);

            FileInfo file = new FileInfo(filePath);

            string code;
            if (file.Extension == ".pdf")
            {
                code = ReadFile(filePath);
            }
            else
            {
                code = "Sorry you can't read a file with an extension of " + file.Extension;
            }
            ViewBag.Message = code;
            return View();
        }

        private string ReadFile(string filepath)
        {
            string fileOutput = "";
            try
            {
                StreamReader FileReader = new StreamReader(filepath);
                //The returned value is -1 if no more characters are 
                //currently available. 
                while (FileReader.Peek() > -1)
                {
                    //ReadLine() Reads a line of characters from the 
                    //current stream and returns the data as a string. 
                    fileOutput += FileReader.ReadLine().Replace("<", "&lt;").
                    Replace(" ", "&nbsp;&nbsp;&nbsp;&nbsp;")
                                         + "<br />";
                }
                FileReader.Close();
            }
            catch (FileNotFoundException e)
            {
                fileOutput = e.Message;
            }
            return fileOutput;
        }

        // GET: Home
        [HttpGet]
        public ActionResult FileDirectoryInfo()
        {

            FileInfo file = new FileInfo(Server.MapPath("~/Uploads/Test.txt"));
            string fileProp;
            fileProp = "<b>File Information</b><br />";
            fileProp += "<b>Name:</b> " + file.Name + "<br />";
            fileProp += "<b>Path:</b> "
              + file.DirectoryName + "<br />";
            fileProp += "<b>Is Read Only:</b> "
              + file.IsReadOnly + "<br />";
            fileProp += "<b>Last Access:</b> "
              + file.LastAccessTime + "<br />";
            fileProp += "<b>Last Write:</b> "
              + file.LastWriteTime + "<br />";
            fileProp += "<b>Length:</b> "
              + file.Length / 1024;

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("."));
            string dirProp;
            dirProp = "<b>Directory Information</b><br />";
            dirProp += "<b>Name:</b> " + dir.Name + "<br />";
            dirProp += "<b>Parent:</b> " + dir.Parent + "<br />";
            dirProp += "<b>Full Name:</b> "
             + dir.FullName + "<br />";
            dirProp += "<b>Attributes:</b> "
             + dir.Attributes + "<br />";
            dirProp += "<b>Creation Time:</b> "
             + dir.CreationTime;

            ViewBag.Message = fileProp;
            ViewBag.Message2 = dirProp;

            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.Message = "File uploaded successfully.";
            }

            return View();
        }
    }
}