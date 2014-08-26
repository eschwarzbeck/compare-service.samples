using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CompareServer.Domain;
using CompareServer.Domain.ComparerProxy;


namespace CompareServer.WebClient.Models
{
    public class ComparerModel
    {
        [Required(ErrorMessage="Please Select original Document")]
        public string OriginalDoc { get; set; }
        [HiddenInput(DisplayValue=false)]
        [Required(ErrorMessage="Original Document is not uploaded")]
        public string OriginalDocFile { get; set; }

        [Required(ErrorMessage="Please Add modified Document")]
        public string ModifiedDoc1 { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Modified Document is not uploaded")]
        public string ModifiedDocFile1 { get; set; }

        public string ModifiedDoc2 { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ModifiedDocFile2 { get; set; }

        public string ModifiedDoc3 { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ModifiedDocFile3 { get; set; }

        public string ModifiedDoc4 { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ModifiedDocFile4 { get; set; }

        public string ModifiedDoc5 { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ModifiedDocFile5 { get; set; }


        [Required(ErrorMessage="Rendering Set is not set")]
        public string RenderingSet { get; set; }
        [Required(ErrorMessage="Output format is not set")]
        public string OutputFormat { get; set; }

        public IEnumerable<SelectListItem> GetRenderingSets(HttpRequest request)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            try
            {
               
                foreach (string file in RenderingSets.GetList())
                {
                    list.Add(new SelectListItem() { Text = Path.GetFileNameWithoutExtension(file), Value = file });
                }
            }
            catch (Exception)
            {
                list.Add(new SelectListItem() { Text = "No rendering Set", Value = "" });
            }
            return list;
        }

        public IEnumerable<SelectListItem> GetResponseOptions()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (ResponseOptions item in Enum.GetValues(typeof(ResponseOptions)))
            {
                list.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
            }
            return list;
        }

        private void FillDocuments(ref ComparerArguments arguments, string uploadPath)
        {
            if (!string.IsNullOrEmpty(this.OriginalDocFile))
            {
                arguments.OriginalDoc = new ServerFile(this.OriginalDoc, this.OriginalDocFile);
            }
            arguments.ModifiedDoc = new List<ServerFile>();
            if (!string.IsNullOrEmpty(this.ModifiedDocFile1)) 
            {
                arguments.ModifiedDoc.Add(new ServerFile(ModifiedDoc1, ModifiedDocFile1));
            }
            if (!string.IsNullOrEmpty(this.ModifiedDocFile2))
            {
                arguments.ModifiedDoc.Add(new ServerFile(ModifiedDoc2, ModifiedDocFile2));
            }
            if (!string.IsNullOrEmpty(this.ModifiedDocFile3))
            {
                arguments.ModifiedDoc.Add(new ServerFile(ModifiedDoc3, ModifiedDocFile3));
            }
            if (!string.IsNullOrEmpty(this.ModifiedDocFile4))
            {
                arguments.ModifiedDoc.Add(new ServerFile(ModifiedDoc4, ModifiedDocFile4));
            }
            if (!string.IsNullOrEmpty(this.ModifiedDocFile5))
            {
                arguments.ModifiedDoc.Add(new ServerFile(ModifiedDoc5, ModifiedDocFile5));
            }

        }

        public ComparerArguments GetComparerArguments(string uploadPath)
        {
            ComparerArguments arguments = new ComparerArguments();
            FillDocuments(ref arguments, uploadPath);
            arguments.OutputFormat = (ResponseOptions)Convert.ToInt32(OutputFormat);
            arguments.RenderingSet = RenderingSet;
            string path = ConfigurationManager.AppSettings.Get("renderset.path");
            if (!string.IsNullOrEmpty(path))
            {
                arguments.RenderingSet = Path.Combine(path, arguments.RenderingSet);
            }
            return arguments;
        }

    }
}