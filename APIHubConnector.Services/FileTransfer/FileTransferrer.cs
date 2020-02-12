using APIHUbConnector.Services.FileRead;
using APIHUbConnector.Services.FileRead.DTOs;
using APIHUbConnector.Services.FileTransfer.DTOs;
using APIHUbConnector.Services.FileTransfer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APIHUbConnector.Services.FileTransfer
{
    public class FileTransferrer : IFileTransferrer<FileTransfererResult>
    {
        private readonly IFileReader<FileReaderResult> fileReader;
        private readonly IList<string> imageExtensions;

        public FileTransferrer(
            IFileReader<FileReaderResult> fileReader,
            IList<string> imageExtensions)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            this.imageExtensions = imageExtensions ?? throw new ArgumentNullException(nameof(imageExtensions));
        }

        public async Task<FileTransfererResult> FilesToListAsync(string sourceDirName, bool copySubDirs = true)
        {
            var filePaths = new List<string>();
            var fileContents = new List<string>();

            try
            {
                await this.DirectoryCoppy(sourceDirName, filePaths, fileContents);

                var result = new FileTransfererResult(

                    true,
                    "Transfering complete",
                    new List<ConvertedFileElement>(filePaths.Zip(fileContents, (fp, fc) => new ConvertedFileElement()
                    {
                        FilePath = fp,
                        FileContent = fc
                    }))
                );

                return result;
            }
            catch (Exception ex)
            {
                return new FileTransfererResult(
                    false, $"{nameof(FileTransferrer)} : {nameof(FilesToListAsync)} : Can't read file : {ex.Message} : " +
                    $"{ (ex.InnerException.Message != null ? ex.InnerException.Message : "no inner exceptions")}");
            }

        }

        /// <summary>
        /// Recursive method for iterrating over directory, iterrates all files and
        /// all sub directorys ( if copy sub dirs is on ). All results are passed
        /// to filePaths and fileContent by refference
        /// </summary>
        /// <param name="sourceDirName">Source of files</param>
        /// <param name="filePaths">Generated file paths list</param>
        /// <param name="fileContents">Generated Content of each file</param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        /// <returns>filePaths and fileContent reference results</returns>
        private async Task DirectoryCoppy(string sourceDirName, List<string> filePaths, List<string> fileContents, string destDirName = "", bool copySubDirs = true)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (destDirName == "")
                {
                    string temppath = destDirName + file.Name;

                    filePaths.Add(temppath);

                    var fileContent = await this.fileReader.ReadFileAsync(file.FullName);

                    if (fileContent.Success)
                    {
                        if (this.imageExtensions.Any(e => temppath.Contains(e)))
                        {
                            fileContents.Add(file.FullName.GetBase64String());
                        }
                        else
                        {
                            fileContents.Add(fileContent.Result);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException(fileContent.Result);
                    }

                }
                else
                {
                    string temppath = destDirName + "/" + file.Name;

                    filePaths.Add(temppath);

                    var fileContent = await this.fileReader.ReadFileAsync(file.FullName);

                    if (fileContent.Success)
                    {
                        if (this.imageExtensions.Any(e => temppath.Contains(e)))
                        {
                            fileContents.Add(file.FullName.GetBase64String());
                        }
                        else
                        {
                            fileContents.Add(fileContent.Result);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException(fileContent.Result);
                    }
                }
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    if (destDirName == "")
                    {
                        string temppath = destDirName + subdir.Name;

                        await DirectoryCoppy(subdir.FullName, filePaths, fileContents, temppath, copySubDirs);
                    }
                    else
                    {
                        string temppath = destDirName + "/" + subdir.Name;

                        await DirectoryCoppy(subdir.FullName, filePaths, fileContents, temppath, copySubDirs);
                    }
                }
            }
        }
    }
}
