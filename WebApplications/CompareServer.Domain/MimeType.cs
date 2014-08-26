using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareServer.Domain
{
    public static class MimeType
    {
        private static Dictionary<string, string> mimeTypes;

        static MimeType()
        {
            mimeTypes = new Dictionary<string, string>();
            /*-- Images --*/
            mimeTypes.Add(".bmp", "image/bmp"); 
            mimeTypes.Add(".gif", "image/gif");
            mimeTypes.Add(".jpeg", "image/jpeg"); 
            mimeTypes.Add(".jpg", "image/jpeg");
            mimeTypes.Add(".png", "image/png");
            mimeTypes.Add(".tif", "image/tiff"); 
            mimeTypes.Add(".tiff", "image/tiff"); 
            /*-- Documents --*/ 
            mimeTypes.Add(".doc", "application/msword"); 
            mimeTypes.Add(".docx", "application/vnmimeTypes.openxmlformats-officedocument.wordprocessingml.document");
            mimeTypes.Add(".pdf", "application/pdf");
            mimeTypes.Add(".rtf", "application/rtf");
            mimeTypes.Add(".wdf", "application/wdf");
            /*-- Slideshows --*/ 
            mimeTypes.Add(".ppt", "application/vnd.ms-powerpoint");
            mimeTypes.Add(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation");
            /*-- Data --*/ 
            mimeTypes.Add(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            mimeTypes.Add(".xls", "application/vnd.ms-excel");
            mimeTypes.Add(".csv", "text/csv");
            mimeTypes.Add(".xml", "text/xml");
            mimeTypes.Add(".txt", "text/plain");
            mimeTypes.Add(".cmd", "text/cmd");
            mimeTypes.Add(".css", "text/css");
            mimeTypes.Add(".cvs", "text/cvs");
            mimeTypes.Add(".html", "text/html");
            mimeTypes.Add(".htm", "text/html");
            mimeTypes.Add(".js", "text/javascript");
            /*-- Compressed Folders --*/
            mimeTypes.Add(".zip", "application/zip");
            /*-- Audio --*/
            mimeTypes.Add(".ogg", "application/ogg");
            mimeTypes.Add(".mp3", "audio/mpeg");
            mimeTypes.Add(".wma", "audio/x-ms-wma");
            mimeTypes.Add(".wav", "audio/x-wav");
            /*-- Video --*/ 
            mimeTypes.Add(".wmv", "audio/x-ms-wmv");
            mimeTypes.Add(".swf", "application/x-shockwave-flash");
            mimeTypes.Add(".avi", "video/avi");
            mimeTypes.Add(".mp4", "video/mp4");
            mimeTypes.Add(".mpeg", "video/mpeg"); 
            mimeTypes.Add(".mpg", "video/mpeg");
            mimeTypes.Add(".qt", "video/quicktime");
        }

        public static string getContentType(string extension){
            try
            {
                return mimeTypes[extension];
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Unknown extension {0}", extension));
            }
        }
    }
}
