using FileUploadService.DataValidation;
using FileUploadService.Exceptions;
using FileUploadService.Helpers;
using FileUploadService.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using System.Text;

namespace FileUploadService.FileService
{
    public class FileService : IFileService
    {
        private readonly IEnumerable<string> allowedExtensions = new List<string> { ".txt", ".csv" };

        private readonly IDataValidator dataValidator;
        private readonly IValidationMessageHelper validationMessageHelper;
        private readonly ILogger<IFileService> logger;

        public FileService(IDataValidator dataValidator, IValidationMessageHelper validationMessageHelper, ILogger<IFileService> logger)
        {
            this.dataValidator = dataValidator ?? throw new ArgumentNullException(nameof(dataValidator));
            this.validationMessageHelper = validationMessageHelper ?? throw new ArgumentNullException(nameof (validationMessageHelper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        public async Task<FileUploadSummary> ProcessFileAsync(Stream fileStream, string contentType)
        {
            var boundary = GetBoundary(MediaTypeHeaderValue.Parse(contentType));
            var multipartReader = new MultipartReader(boundary, fileStream);

            MultipartSection multipartSection;
            var fileSectionsÇount = 0;

            while ((multipartSection = await multipartReader.ReadNextSectionAsync()) != null)
            {
                FileMultipartSection fileSection = multipartSection.AsFileSection();

                fileSectionsÇount++;

                if (fileSection != null)
                {
                    var extension = Path.GetExtension(fileSection.FileName);

                    // todo: make it config value and need to use signature to make sure it is needed file type
                    if (!allowedExtensions.Contains(extension))
                    {
                        throw new BadRequestException("File extension is not supported.");
                    }

                    if (fileSectionsÇount > 1)
                    {
                        throw new BadRequestException("Multiple files could not be uploaded.");
                    }

                    return await ProcessLinesAsync(fileSection);

                }
            }

            return null;
        }

        private async Task<FileUploadSummary> ProcessLinesAsync(FileMultipartSection fileSection)
        {
            var invalidLines = new List<string>();

            const int bufferSize = 1024;
            using (var fileStream = fileSection.FileStream)
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
            {
                string line;
                int lineNumber = 0;

                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    lineNumber++;

                    var data = line.Split();

                    var allDetailsStart = Stopwatch.GetTimestamp();

                    var accountNameValidationStartTime = Stopwatch.GetTimestamp();

                    var isAccountNameValid = false;
                    if(data.Length >= 1)
                    {
                        var accountName = data.ElementAtOrDefault(0);
                        isAccountNameValid = this.dataValidator.Validate("name", accountName);
                    }
                    var accountNameValidationElapsedTime = Stopwatch.GetElapsedTime(accountNameValidationStartTime);
                    logger.LogInformation("Validation for account name took {accountNameValidationElapsedTime} ms in {lineNumber}.", accountNameValidationElapsedTime, lineNumber);

                    var accountNumberValidationStartTime = Stopwatch.GetTimestamp();
                    var isAccountNumberValid = false;
                    if (data.Length >= 1)
                    {
                        var accountNumber = data.ElementAtOrDefault(1);
                        isAccountNumberValid = this.dataValidator.Validate("number", accountNumber);
                    }
                    var accountNumberValidationElapsedTime = Stopwatch.GetElapsedTime(accountNumberValidationStartTime);
                    logger.LogInformation("Validation for account number took {accountNumberValidationElapsedTime} ms in {lineNumber}.", accountNameValidationElapsedTime, lineNumber);

                    var allDetailsElapsedTime = Stopwatch.GetElapsedTime(allDetailsStart);

                    logger.LogInformation("Validation for account details took {elapsedTime} ms in {lineNumber}.", allDetailsElapsedTime, lineNumber);

                    var messsage = this.validationMessageHelper.ProccessMesage(isAccountNameValid, isAccountNumberValid, lineNumber, line);
                    
                    if (!string.IsNullOrEmpty(messsage))
                    {
                        invalidLines.Add(messsage);
                    }
                }
            }

            var invalidLinesCount = invalidLines.Count;
            return new FileUploadSummary
            {
                FileValid = (invalidLinesCount == 0),
                InvalidLines = (invalidLinesCount == 0) ? null : invalidLines
            };
        }

        private static string GetBoundary(MediaTypeHeaderValue contentType)
        {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;

            if (string.IsNullOrWhiteSpace(boundary))
            {
                throw new InvalidDataException("Missing content-type boundary.");
            }

            return boundary;
        }
    }
}
