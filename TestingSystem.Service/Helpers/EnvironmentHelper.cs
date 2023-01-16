using System.IO;

namespace TestingSystem.Service.Helpers
{
    public class EnvironmentHelper
    {
        public static string WebRootPath { get; set; }
        public static string AttachmentPath => Path.Combine(WebRootPath, "images");
        public static string ExcelRootPath => Path.Combine(WebRootPath, "excels");
        public static string FilePath => "images";
        public static string ExcelPath => "excels";
    }
}
