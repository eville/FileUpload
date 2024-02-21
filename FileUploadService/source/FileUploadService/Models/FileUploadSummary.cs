namespace FileUploadService.Models
{
    public class FileUploadSummary
    {
        public bool FileValid { get; set; }
       
        public List<string> InvalidLines { get; set; }
    }
}
