﻿using System;
using System.IO;
using Microsoft.Extensions.Logging.Abstractions;

using SFTPService;

namespace sftp
{
    class Program
    {
        private static void Main()
        {
            var config = new SftpConfig
            {
                Host = "locslhost",
                Port = 22,
                UserName = "dev",
               Password = "homolog"

            };
            var sftpService = new SftpService(new NullLogger<SftpService>(), config);

            // list files
            var files = sftpService.ListAllFiles("/homologacao");
            foreach (var file in files)
            {
                if (file.IsDirectory)
                {
                    Console.WriteLine($"Directory: [{file.FullName}]");

                }
                else if (file.IsRegularFile)
                {
                    Console.WriteLine($"File: [{file.FullName}]");
                }
            }

            var testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "teste1.txt");
            //sftpService.UploadFile(testFile, "/.");
            sftpService.UploadFile(testFile, "/homologacao/teste/teste1.txt" );

        }
    }
}
