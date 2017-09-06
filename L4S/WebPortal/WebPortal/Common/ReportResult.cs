using System;

using System.Web;
using System.Web.Mvc;
using DoddleReport;
using DoddleReport.Configuration;


namespace WebPortal.Common
{
    //change default routing from Doodle to 
    //http://localhost/Area/Controller/Action?extension={extension}
    
    public class ReportResult : DoddleReport.Web.ReportResult
    {
        private readonly Report _report;

        public ReportResult(Report report)
            : base(report)
        {
            _report = report;
        }

        protected override string GetDownloadFileExtension(HttpRequestBase request, string defaultExtension)
        {
            var extension = request.Params["extension"];

            if (string.IsNullOrEmpty(extension))
                return defaultExtension;

            return "." + extension;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            string defaultExtension = Config.Report.Writers.GetWriterConfigurationByFormat(Config.Report.DefaultWriter).FileExtension;

            var response = context.HttpContext.Response;

            var writerConfig = GetWriterFromExtension(context, defaultExtension);
            response.ContentType = writerConfig.ContentType;
            var writer = writerConfig.LoadWriter();

            if (!string.IsNullOrEmpty(FileName))
            {
                var extension = GetDownloadFileExtension(context.HttpContext.Request, defaultExtension);
                context.HttpContext.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}{1}", FileName, extension));
            }

            writer.WriteReport(_report, response.OutputStream);
        }

        private WriterElement GetWriterFromExtension(ControllerContext context, string defaultExtension)
        {
            string extension = GetDownloadFileExtension(context.RequestContext.HttpContext.Request, defaultExtension);

            var writerConfig = Config.Report.Writers.GetWriterConfigurationForFileExtension(extension);
            if (writerConfig == null)
                throw new InvalidOperationException(
                    string.Format(
                        "Unable to locate a report writer for the extension '{0}'. Did you add this fileExtension to the web.config for DoddleReport?",
                        extension));

            return writerConfig;
        }
    }
}